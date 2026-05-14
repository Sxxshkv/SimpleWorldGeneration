namespace SimpleWorldGeneration.NoiseGenerator
{
    public interface IController
    {
        public enum NoiseType
        {
            White,
            Perlin
        }

        float Noise(float x, float y, float frequency, float amplitude, NoiseType noiseType);
    }
}
