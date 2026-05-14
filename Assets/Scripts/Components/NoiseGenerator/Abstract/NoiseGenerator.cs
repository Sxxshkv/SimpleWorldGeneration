namespace SimpleWorldGeneration.NoiseGenerator
{
    public abstract class BaseNoiseGenerator
    {
        public abstract float amplitude { get; }
        public abstract float Noise(float x, float y);
    }
}
