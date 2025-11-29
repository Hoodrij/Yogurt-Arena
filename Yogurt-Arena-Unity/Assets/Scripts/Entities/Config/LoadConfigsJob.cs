namespace Yogurt.Arena;

public struct LoadConfigsJob
{
    public void Run(Entity game)
    {
        ScriptableObject[] allSOs = Resources.LoadAll<ScriptableObject>(string.Empty);
        IEnumerable<IConfigSO> configSOs = allSOs.OfType<IConfigSO>();

        foreach (IConfigSO configSO in configSOs)
        {
            EntityBlueprint blueprint = new EntityBlueprint
            {
                Blueprint = configSO as IBlueprint
            };

            Entity.Create()
                .Add(blueprint)
                .PopulateFrom(blueprint)
                .SetParent(game);
        }
    }
}
