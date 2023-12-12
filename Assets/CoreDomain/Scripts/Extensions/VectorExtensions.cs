using UnityEngine;

namespace CoreDomain.Scripts.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 ToVector2XY(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }

        public static Vector2 ToVector2XZ(this Vector3 vec)
        {
            return new Vector2(vec.x, vec.z);
        }
    }
}