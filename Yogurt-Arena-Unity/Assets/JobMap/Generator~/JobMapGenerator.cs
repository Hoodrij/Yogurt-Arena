using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Yogurt.Arena.JobMap.Generator
{
    /// <summary>
    /// Builds a static map of the Arena's job-to-job calls. A job is any project class or struct
    /// whose name ends in Job; an edge exists when its source calls another concrete Job.Run(...).
    /// The map intentionally describes source structure only: it has no runtime dependency.
    /// </summary>
    [Generator(LanguageNames.CSharp)]
    public sealed class JobMapGenerator : IIncrementalGenerator
    {
        private const int VIEWER_SCHEMA_VERSION = 2;

        private static readonly DiagnosticDescriptor GenerationFailed = new(
            "JOBMAP001",
            "Job map generation failed",
            "Job map generation failed: {0}",
            "JobMap",
            DiagnosticSeverity.Warning,
            true);

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValueProvider<ImmutableArray<JobInfo?>> jobs = context.SyntaxProvider
                .CreateSyntaxProvider(
                    static (node, _) => node is TypeDeclarationSyntax declaration
                                        && declaration.Identifier.ValueText.EndsWith("Job", StringComparison.Ordinal),
                    static (source, _) => Extract((TypeDeclarationSyntax)source.Node, source.SemanticModel))
                .Where(static job => job != null)
                .Collect();

            context.RegisterSourceOutput(jobs, static (production, candidates) =>
            {
                List<JobInfo> map = candidates
                    .Where(candidate => candidate != null)
                    .Select(candidate => candidate!)
                    .GroupBy(candidate => candidate.Id, StringComparer.Ordinal)
                    .Select(group => group.First())
                    .OrderBy(candidate => candidate.Id, StringComparer.Ordinal)
                    .ToList();

                if (map.Count == 0)
                {
                    return;
                }

                try
                {
                    WriteViewer(OutputPath(map[0].SourcePath), BuildJson(map));
                }
                catch (Exception exception)
                {
                    production.ReportDiagnostic(Diagnostic.Create(GenerationFailed, Location.None, exception.Message));
                }
            });
        }

        private static JobInfo? Extract(TypeDeclarationSyntax declaration, SemanticModel semanticModel)
        {
            if (semanticModel.GetDeclaredSymbol(declaration) is not INamedTypeSymbol symbol
                || symbol.TypeKind is not (TypeKind.Class or TypeKind.Struct)
                || !IsArenaSource(declaration.SyntaxTree.FilePath))
            {
                return null;
            }

            HashSet<string> calls = new(StringComparer.Ordinal);
            foreach (InvocationExpressionSyntax invocation in declaration.DescendantNodes().OfType<InvocationExpressionSyntax>())
            {
                IMethodSymbol? method = semanticModel.GetSymbolInfo(invocation).Symbol as IMethodSymbol;
                if (method == null
                    || method.Name != "Run"
                    || method.ContainingType is not INamedTypeSymbol target
                    || target.TypeKind is not (TypeKind.Class or TypeKind.Struct)
                    || !target.Name.EndsWith("Job", StringComparison.Ordinal))
                {
                    continue;
                }

                Location? targetLocation = target.Locations.FirstOrDefault(location => location.IsInSource);
                if (!IsArenaSource(targetLocation?.SourceTree?.FilePath))
                {
                    continue;
                }

                calls.Add(IdOf(target));
            }

            FileLinePositionSpan position = declaration.GetLocation().GetLineSpan();
            return new JobInfo(
                IdOf(symbol),
                symbol.Name,
                symbol.ContainingNamespace.IsGlobalNamespace ? "" : symbol.ContainingNamespace.ToDisplayString(),
                declaration.SyntaxTree.FilePath,
                RepoRelative(declaration.SyntaxTree.FilePath),
                position.StartLinePosition.Line + 1,
                calls.OrderBy(id => id, StringComparer.Ordinal).ToList());
        }

        private static bool IsArenaSource(string? path)
        {
            if (path is null || path.Length == 0)
            {
                return false;
            }

            return path.Replace('\\', '/').IndexOf("/Assets/Scripts/", StringComparison.Ordinal) >= 0;
        }

        private static string IdOf(INamedTypeSymbol symbol) => symbol.ToDisplayString(SymbolDisplayFormat.CSharpErrorMessageFormat);

        private static string OutputPath(string anyJobFile)
        {
            string normalized = anyJobFile.Replace('\\', '/');
            int assets = normalized.IndexOf("/Assets/", StringComparison.Ordinal);
            string project = assets < 0 ? Directory.GetCurrentDirectory() : normalized.Substring(0, assets);
            return Path.Combine(project, "Assets", "JobMap", "viewer.html");
        }

        private static string RepoRelative(string path)
        {
            string normalized = path.Replace('\\', '/');
            int assets = normalized.IndexOf("/Assets/", StringComparison.Ordinal);
            return assets < 0 ? normalized : normalized.Substring(assets + 1);
        }

        private static void WriteViewer(string outputPath, string json)
        {
            string directory = Path.GetDirectoryName(outputPath) ?? throw new InvalidOperationException("Viewer output has no directory.");
            string templatePath = Path.Combine(directory, "Generator~", "viewer-template.html");
            if (!File.Exists(templatePath))
            {
                throw new FileNotFoundException("Job map viewer template was not found.", templatePath);
            }

            string safeJson = json.Replace("</", "<\\/");
            string html = File.ReadAllText(templatePath).Replace(
                "<script>/*JOB_MAP_DATA*/</script>",
                "<script>window.JOB_MAP = " + safeJson + ";</script>");

            if (!File.Exists(outputPath) || File.ReadAllText(outputPath) != html)
            {
                File.WriteAllText(outputPath, html);
            }
        }

        private static string BuildJson(List<JobInfo> jobs)
        {
            StringBuilder json = new();
            json.Append("{\n  \"version\": ").Append(VIEWER_SCHEMA_VERSION).Append(",\n  \"jobs\": [");
            for (int index = 0; index < jobs.Count; index++)
            {
                JobInfo job = jobs[index];
                if (index > 0)
                {
                    json.Append(',');
                }

                json.Append("\n    {\"id\": ").Append(Quote(job.Id))
                    .Append(", \"name\": ").Append(Quote(job.Name))
                    .Append(", \"namespace\": ").Append(Quote(job.Namespace))
                    .Append(", \"file\": ").Append(Quote(job.File))
                    .Append(", \"line\": ").Append(job.Line)
                    .Append(", \"calls\": [");
                for (int callIndex = 0; callIndex < job.Calls.Count; callIndex++)
                {
                    if (callIndex > 0)
                    {
                        json.Append(", ");
                    }

                    json.Append(Quote(job.Calls[callIndex]));
                }

                json.Append("]}");
            }

            return json.Append("\n  ]\n}\n").ToString();
        }

        private static string Quote(string value)
        {
            StringBuilder quoted = new("\"");
            foreach (char character in value)
            {
                switch (character)
                {
                    case '\\': quoted.Append("\\\\"); break;
                    case '\"': quoted.Append("\\\""); break;
                    case '\n': quoted.Append("\\n"); break;
                    case '\r': quoted.Append("\\r"); break;
                    case '\t': quoted.Append("\\t"); break;
                    default:
                        if (character < ' ')
                        {
                            quoted.Append("\\u").Append(((int)character).ToString("x4"));
                        }
                        else
                        {
                            quoted.Append(character);
                        }

                        break;
                }
            }

            return quoted.Append('\"').ToString();
        }

        private sealed class JobInfo
        {
            public readonly string Id;
            public readonly string Name;
            public readonly string Namespace;
            public readonly string SourcePath;
            public readonly string File;
            public readonly int Line;
            public readonly List<string> Calls;

            public JobInfo(string id, string name, string @namespace, string sourcePath, string file, int line, List<string> calls)
            {
                Id = id;
                Name = name;
                Namespace = @namespace;
                SourcePath = sourcePath;
                File = file;
                Line = line;
                Calls = calls;
            }
        }
    }
}
