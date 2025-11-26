namespace Yogurt.Arena;

public struct LoadConfigsJob
{
    public void Run(Entity game)
    {
        ScriptableObject[] allSOs = Resources.LoadAll<ScriptableObject>(string.Empty);
        IEnumerable<IConfigSO> configSOs = allSOs.OfType<IConfigSO>();

        foreach (IConfigSO configSO in configSOs)
        {
            IEnumerable<IComponent> components = new GetConfigComponentsJob().Run(configSO);

            ConfigEntity configEntity = new ConfigEntity
            {
                Components = components
            };

            Entity.Create()
                .Add(configEntity)
                .Add(configEntity.Components)
                .SetParent(game);
        }
    }
}
