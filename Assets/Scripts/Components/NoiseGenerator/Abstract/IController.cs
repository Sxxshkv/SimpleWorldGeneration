namespace SimpleWorldGeneration.NoiseGenerator
{
    public interface IController
    {
        float Noise(float x, float y, float frequency, float amplitude);
    }
}
