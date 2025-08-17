namespace Yogurt.Arena;

public static class BoundsEx
{
    public static Vector3 GetRandomPoint(this Bounds bounds)
    {
        float f = 100;
        float x = Random.Range((int) (bounds.min.x * f), (int) (bounds.max.x * f)) / f;
        float y = Random.Range((int) (bounds.min.y * f), (int) (bounds.max.y * f)) / f;
        float z = Random.Range((int) (bounds.min.z * f), (int) (bounds.max.z * f)) / f;
        return new Vector3(x, y, z);
    }
}