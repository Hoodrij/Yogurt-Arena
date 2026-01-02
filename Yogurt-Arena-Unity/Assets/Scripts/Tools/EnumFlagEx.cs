using System.Runtime.CompilerServices;

namespace Yogurt.Arena;

public static class EnumFlagEx
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasFlagNonAlloc<E>(this E lhs, E rhs) where E : unmanaged, Enum
    {
        switch (Unsafe.SizeOf<E>())
        {
            case 1:
                return (Unsafe.As<E, byte>(ref lhs) & Unsafe.As<E, byte>(ref rhs)) != 0;
            case 2:
                return (Unsafe.As<E, ushort>(ref lhs) & Unsafe.As<E, ushort>(ref rhs)) != 0;
            case 4:
                return (Unsafe.As<E, uint>(ref lhs) & Unsafe.As<E, uint>(ref rhs)) != 0;
            case 8:
                return (Unsafe.As<E, ulong>(ref lhs) & Unsafe.As<E, ulong>(ref rhs)) != 0;
            default:
                throw new Exception("Size does not match a known Enum backing type.");
        }
    }
}