using System;

namespace Utilities.Extensions.Tuple
{
    public static class FloatExtensions
    {
        // Расширение для нормализации кортежа (x, y)
        public static (float x, float y) Normalize(this (float x, float y) v)
        {
            float lenSq = v.x * v.x + v.y * v.y;

            if (lenSq < 1e-12f)
                return (0f, 0f); // избегаем деления на ноль

            float invLen = 1f / MathF.Sqrt(lenSq);
            return (v.x * invLen, v.y * invLen);
        }
    }
}
