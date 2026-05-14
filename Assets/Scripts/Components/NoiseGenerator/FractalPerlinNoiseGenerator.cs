using System;

namespace SimpleWorldGeneration.NoiseGenerator
{
    public class FractalPerlinNoiseGenerator : BaseNoiseGenerator
    {
        public override float amplitude => _perlinNoiseGenerator.amplitude;

        readonly PerlinNoiseGenerator _perlinNoiseGenerator;
        readonly int _octaves;
        readonly float _persistence;
        readonly float _lacunarity;

        public FractalPerlinNoiseGenerator(PerlinNoiseGenerator perlinNoiseGenerator, int octaves, float persistence,
            float lacunarity)
        {
            _perlinNoiseGenerator = perlinNoiseGenerator;
            _octaves = octaves;
            _persistence = persistence;
            _lacunarity = lacunarity;

            float maxAmplitude = 0f;

            for (int i = 0; i < octaves; i++)
                maxAmplitude += MathF.Pow(persistence, i);

            _normalizationFactor = 1f / maxAmplitude;
        }

        readonly float _normalizationFactor;

        public override float Noise(float x, float y)
        {
            float total = 0.0f;
            float amplitude = _perlinNoiseGenerator.amplitude;
            float frequency = _perlinNoiseGenerator.frequency;

            for (int i = 0; i < _octaves; i++)
            {
                float noiseValue = _perlinNoiseGenerator.CalculateNoise(x, y, frequency, amplitude);
                total += noiseValue;

                frequency *= _lacunarity;
                amplitude *= _persistence;
            }

            return total * _normalizationFactor;
        }
    }
}
