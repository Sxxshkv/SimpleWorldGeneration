using System;
using SimpleWorldGeneration.NoiseGenerator.Logic;

namespace SimpleWorldGeneration.NoiseGenerator.Interface
{
    public class Controller : IController
    {
        readonly PerlinNoise _perlinNoise;
        readonly WhiteNoise _whiteNoise;

        public Controller(PerlinNoise perlinNoise, WhiteNoise whiteNoise)
        {
            _perlinNoise = perlinNoise;
            _whiteNoise = whiteNoise;
        }

        public float Noise(float x, float y, float frequency, float amplitude, IController.NoiseType noiseType)
        {
            switch (noiseType)
            {
                case IController.NoiseType.White:
                    return _whiteNoise.Noise(x, y, frequency, amplitude);

                case IController.NoiseType.Perlin:
                    return _perlinNoise.Noise(x, y, frequency, amplitude);

                default:
                    throw new ArgumentOutOfRangeException(nameof(noiseType), noiseType, null);
            }
        }
    }
}
