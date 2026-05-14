namespace SimpleWorldGeneration.WorldGenerator
{
    public interface IController
    {
        void Generate(GenerationSettings settings, NoiseGenerator.IController noiseGenerator);
    }
}
