namespace SimpleWorldGeneration.NoiseGenerator.Logic
{
    public class WhiteNoise
    {
        readonly uint _seedA;
        readonly uint _seedB;

        public WhiteNoise(int seed)
        {
            _seedA = (uint)seed;
            _seedB = (uint)(seed ^ 0x9E3779B9U);
        }

        public float Noise(float x, float y, float amplitude = 1.0f)
        {
            const int resolution = 1_000_000;
            uint ix = (uint)(x * resolution);
            uint iy = (uint)(y * resolution);

            uint hash = Hash(ix, iy);

            float value = ((hash & 0x7FFFFFFF) / (float)int.MaxValue) - 1.0f;

            return value * amplitude;
        }

        uint Hash(uint x, uint y)
        {
            uint h = x ^ y;
            h ^= h >> 16;
            h *= 0x85ebca6bU;
            h ^= h >> 13;
            h *= 0xc2b2ae35U;
            h ^= h >> 16;
            return h;
        }
    }
}
