using SimpleWorldGeneration.NoiseGenerator.Logic;

namespace SimpleWorldGeneration.NoiseGenerator.Interface
{
    public class Controller : IController
    {
        readonly PerlinNoise _perlinNoise;

        public Controller(PerlinNoise perlinNoise)
        {
            _perlinNoise = perlinNoise;
        }

        public float Noise(float x, float y, float frequency, float amplitude)
        {
            return _perlinNoise.Noise(x, y, frequency, amplitude);
        }
    }
}
