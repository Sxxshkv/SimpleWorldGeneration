using SimpleWorldGeneration.WorldGenerator.Logic;

namespace SimpleWorldGeneration.WorldGenerator.Interface
{
    class Controller : IController
    {
        readonly ChunksGenerator _chunksGenerator;

        public Controller(ChunksGenerator chunksGenerator)
        {
            _chunksGenerator = chunksGenerator;
        }

        public void Generate(GenerationSettings settings, NoiseGenerator.IController noiseGenerator)
        {
            _chunksGenerator.Generate(settings, noiseGenerator);
        }
    }
}
