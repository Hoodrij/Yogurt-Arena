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

            EntityBlueprint blueprint = new EntityBlueprint
            {
                Components = components
            };

            Entity.Create()
                .Add(blueprint)
                .Add(blueprint.Components)
                .SetParent(game);
        }
    }
}
