using Configs;
using SimpleWorldGeneration.WorldGenerator;
using UnityEngine;

namespace SimpleWorldGeneration
{
    public class Root : MonoBehaviour
    {
        [SerializeField] WorldGenerationConfig _worldGenerationConfig;
        [SerializeField] Transform _worldRoot;

        WorldGenerator.Component _worldGenerator;

        void Start()
        {
            var noiseGenerator = new NoiseGenerator.Component(_worldGenerationConfig.noiseConfig.seed);

            var worldGenerator = new WorldGenerator.Component();

            var worldGenerationSettings = new GenerationSettings
            {
                chunksSqrWorldSize = _worldGenerationConfig.chunksSqrWorldSize,
                chunkSize = _worldGenerationConfig.chunkSize,
                chunkContext = new GenerationSettings.ChunkContext
                {
                    material = _worldGenerationConfig.chunkConfig.surfaceMeshMaterial,
                    layer = _worldGenerationConfig.chunkConfig.chunkGoLayer,
                    root = _worldRoot
                },
                noiseContext = new GenerationSettings.NoiseContext
                {
                    noiseType = new NoiseTypeConverter().Convert(_worldGenerationConfig.noiseType),
                    freq = _worldGenerationConfig.noiseConfig.freq,
                    amplitude = _worldGenerationConfig.noiseConfig.amplitude
                }
            };

            worldGenerator.controller.Generate(worldGenerationSettings, noiseGenerator.controller);

            _worldGenerator = worldGenerator;
        }
    }
}
