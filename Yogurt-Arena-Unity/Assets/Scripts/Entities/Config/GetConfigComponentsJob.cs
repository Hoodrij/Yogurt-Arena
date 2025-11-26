using System.Reflection;

namespace Yogurt.Arena
{
    public struct GetConfigComponentsJob
    {
        public IEnumerable<IComponent> Run(IConfigSO configSO)
        {
            Type type = configSO.GetType();

            // Check if the config itself is a component
            if (configSO is IComponent component)
            {
                yield return component;
            }

            // Extract all public fields that implement IComponent
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(configSO);

                if (value is IComponent fieldComponent)
                {
                    yield return fieldComponent;
                }
            }

            // Extract all public properties that implement IComponent
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties)
            {
                if (!property.CanRead)
                    continue;

                object value = property.GetValue(configSO);

                if (value is IComponent propertyComponent)
                {
                    yield return propertyComponent;
                }
            }
        }
    }
}
