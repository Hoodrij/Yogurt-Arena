namespace Yogurt.Arena
{
    [Serializable]
    public class CameraConfig : ScriptableObject, IComponent, IConfigSO, IBlueprint
    {
        public Asset<CameraView> Asset;
        public float SmoothValue;
        
        [Range(0f, 1f)]
        [Tooltip("How much mouse position affects camera target (0..1). 0 = off, 1 = full follow")]
        public float MouseInfluence = 0.1f;

        [Min(0f)]
        [Tooltip("Maximum distance (in world units) that mouse can offset the camera target. 0 = unlimited")]
        public float MouseInfluenceMaxDistance = 6f;

        public void Populate(Entity entity)
        {
            entity.Add(this);
        }
    }
}