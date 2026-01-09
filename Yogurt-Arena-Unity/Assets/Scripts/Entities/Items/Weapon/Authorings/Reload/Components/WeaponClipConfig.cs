namespace Yogurt.Arena;

[Serializable]
public record struct WeaponClipConfig : IComponent
{
    public int BulletsInClip;
    public float ClipCooldown;
}