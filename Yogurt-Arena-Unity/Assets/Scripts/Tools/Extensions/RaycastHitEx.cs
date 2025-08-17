namespace Yogurt.Arena
{
    public static class RaycastHitEx
    {
        public static Entity GetEntity(this RaycastHit hit)
        {
            EntityLink entityLink = hit.transform.GetComponentInParent<EntityLink>();
            return entityLink != null ? entityLink.Entity : default;
        }
    }
}