using System;

namespace SimpleWorldGeneration.NoiseGenerator.Logic
{
    public class WhiteNoise
    {
        readonly int _seed;

        public WhiteNoise(int seed)
        {
            _seed = seed;
        }

        public float Noise(float x, float y, float amplitude = 1.0f, float frequency = 1.0f)
        {
            double fx = x * frequency;
            double fy = y * frequency;

            long ix = (long)Math.Floor(fx);
            long iy = (long)Math.Floor(fy);

            uint hash = Hash((uint)(ix ^ (_seed & 0xFFFFFFFF)), (uint)(iy ^ ((_seed >> 32) & 0xFFFFFFFF)));
            double noiseValue = (hash & 0x7FFFFFFF) / (double)int.MaxValue;
            double normalized = (noiseValue - 1.0);

            return (float)(normalized * amplitude);
        }

        uint Hash(uint x, uint y)
        {
            uint h = 0u;

            h ^= x;
            h *= 0x85ebca6bU;
            h ^= h >> 16;

            h ^= y;
            h *= 0x85ebca6bU;
            h ^= h >> 16;

            h *= 0x85ebca6bU;
            h ^= h >> 13;

            return h;
        }
    }
}
