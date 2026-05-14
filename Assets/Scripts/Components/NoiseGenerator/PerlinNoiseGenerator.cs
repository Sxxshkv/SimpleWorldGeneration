using System;
using Utilities.Extensions.Tuple;

namespace SimpleWorldGeneration.NoiseGenerator
{
    public class PerlinNoiseGenerator : BaseNoiseGenerator
    {
        public float frequency => _frequency;
        public override float amplitude => _amplitude;

        readonly int[] _perm;
        readonly (float x, float y)[] _gradients;
        readonly float _frequency;
        readonly float _amplitude;

        public PerlinNoiseGenerator(int seed, float frequency, float amplitude)
        {
            _gradients = new (float x, float y)[]
            {
                (1f, 0f),
                (-1f, 0f),
                (0f, 1f),
                (0f, -1f),
                (1f, 1f).Normalize(),
                (-1f, 1f).Normalize(),
                (1f, -1f).Normalize(),
                (-1f, -1f).Normalize()
            };

            _perm = GeneratePermutation(seed);
            _frequency = frequency;
            _amplitude = amplitude;
        }

        public override float Noise(float x, float y)
        {
            x *= _frequency;
            y *= _frequency;

            int X = (int)MathF.Floor(x) & 255;
            int Y = (int)MathF.Floor(y) & 255;

            x -= MathF.Floor(x);
            y -= MathF.Floor(y);

            float u = EaseCurve(x);
            float v = EaseCurve(y);

            int aa = _perm[X + _perm[Y]];
            int ab = _perm[X + _perm[Y + 1]];
            int ba = _perm[X + 1 + _perm[Y]];
            int bb = _perm[X + 1 + _perm[Y + 1]];

            float x0 = x;
            float y0 = y;
            float x1 = x - 1f;
            float y1 = y;
            float x2 = x;
            float y2 = y - 1f;
            float x3 = x - 1f;
            float y3 = y - 1f;

            float dot00 = Dot(_gradients[aa % _gradients.Length], x0, y0);
            float dot10 = Dot(_gradients[ba % _gradients.Length], x1, y1);
            float dot01 = Dot(_gradients[ab % _gradients.Length], x2, y2);
            float dot11 = Dot(_gradients[bb % _gradients.Length], x3, y3);

            float u0 = Lerp(dot00, dot10, u);
            float u1 = Lerp(dot01, dot11, u);

            return Lerp(u0, u1, v) * _amplitude;
        }

        int[] GeneratePermutation(int seed)
        {
            var p = new int[256];
            var rng = new Random(seed);

            for (int i = 0; i < 256; i++)
                p[i] = i;

            for (int i = 255; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (p[i], p[j]) = (p[j], p[i]);
            }

            var perm = new int[512];
            Array.Copy(p, perm, 256);

            Array.Copy(
                p,
                0,
                perm,
                256,
                256
            );

            return perm;
        }

        float EaseCurve(float t) => t * t * t * (t * (t * 6f - 15f) + 10f); // 6t⁵ - 15t⁴ + 10t³
        float Dot((float x, float y) grad, float x, float y) => grad.x * x + grad.y * y;
        float Lerp(float a, float b, float t) => a + t * (b - a);
    }
}
