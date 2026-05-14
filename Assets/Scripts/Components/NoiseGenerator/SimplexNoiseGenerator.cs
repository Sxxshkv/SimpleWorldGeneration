using System;

namespace SimpleWorldGeneration.NoiseGenerator
{
    public class SimplexNoiseGenerator : BaseNoiseGenerator
    {
        public override float amplitude => _amplitude;

        readonly int[] _perm;
        readonly int[] _permMod12;
        readonly float _frequency;
        readonly float _amplitude;
        readonly float _f2;
        readonly float _g2;
        readonly float[] _grad3 = {
            1, 1, 0,
            -1, 1, 0,
            1,-1, 0,
            -1,-1, 0,
            0, 1, 1,
            0,-1, 1,
            0, 1,-1,
            0,-1,-1,
            1, 0, 1,
            -1, 0, 1,
            1, 0,-1,
            -1, 0,-1
        };

        public SimplexNoiseGenerator(int seed, float frequency, float amplitude)
        {
            _perm = GeneratePermutation(seed);
            _permMod12 = new int[512];

            for (int i = 0; i < 512; i++)
            {
                _permMod12[i] = _perm[i] % 12;
            }

            _frequency = frequency;
            _amplitude = amplitude;
            _f2 = 0.5f * (float)(Math.Sqrt(3.0) - 1.0);
            _g2 = (float)(3.0f - Math.Sqrt(3.0)) / 6.0f;
        }

        public override float Noise(float x, float y)
        {
            float xs = x * _frequency;
            float ys = y * _frequency;

            return Noise2D(xs, ys) * _amplitude;
        }

        float Noise2D(float xin, float yin)
        {
            float s = (xin + yin) * _f2;
            int i = FastFloor(xin + s);
            int j = FastFloor(yin + s);

            float t = (i + j) * _g2;
            float X0 = i - t;
            float Y0 = j - t;
            float x0 = xin - X0;
            float y0 = yin - Y0;

            int i1,
                j1;

            if (x0 > y0)
            {
                i1 = 1;
                j1 = 0;
            }
            else
            {
                i1 = 0;
                j1 = 1;
            }

            float x1 = x0 - i1 + _g2;
            float y1 = y0 - j1 + _g2;
            float x2 = x0 - 1.0f + 2.0f * _g2;
            float y2 = y0 - 1.0f + 2.0f * _g2;

            int ii = i & 255;
            int jj = j & 255;

            float n0,
                n1,
                n2;

            float t0 = 0.5f - x0 * x0 - y0 * y0;

            if (t0 < 0)
                n0 = 0.0f;
            else
            {
                t0 *= t0;
                int gi0 = _permMod12[ii + _perm[jj]] * 3;

                float gx0 = _grad3[gi0],
                    gy0 = _grad3[gi0 + 1];

                n0 = t0 * t0 * (gx0 * x0 + gy0 * y0);
            }

            float t1 = 0.5f - x1 * x1 - y1 * y1;

            if (t1 < 0)
                n1 = 0.0f;
            else
            {
                t1 *= t1;
                int gi1 = _permMod12[ii + i1 + _perm[jj + j1]] * 3;

                float gx1 = _grad3[gi1],
                    gy1 = _grad3[gi1 + 1];

                n1 = t1 * t1 * (gx1 * x1 + gy1 * y1);
            }

            float t2 = 0.5f - x2 * x2 - y2 * y2;

            if (t2 < 0)
                n2 = 0.0f;
            else
            {
                t2 *= t2;
                int gi2 = _permMod12[ii + 1 + _perm[jj + 1]] * 3;

                float gx2 = _grad3[gi2],
                    gy2 = _grad3[gi2 + 1];

                n2 = t2 * t2 * (gx2 * x2 + gy2 * y2);
            }

            return
                70.0f
              * (n0 + n1 + n2);
        }

        float Fade(float t) => t * t * t * (t * (t * 6f - 15f) + 10f);

        int FastFloor(float x)
        {
            return x >= 0 ? (int)x : (int)x - 1;
        }

        int[] GeneratePermutation(int seed)
        {
            int[] p = new int[256];
            var rng = new Random(seed);

            for (int i = 0; i < 256; i++)
                p[i] = i;

            for (int i = 255; i > 0; i--)
            {
                int j = rng.Next(i + 1);
                (p[i], p[j]) = (p[j], p[i]);
            }

            int[] perm = new int[512];
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
    }
}
