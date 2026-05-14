using System;
using Configs;
using SimpleWorldGeneration.NoiseGenerator;
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
            BaseNoiseGenerator baseNoiseGenerator = _worldGenerationConfig.noiseType switch
            {
                WorldGenerationConfig.NoiseType.Perlin => new PerlinNoiseGenerator(
                    _worldGenerationConfig.noiseSeed,
                    _worldGenerationConfig.perlinNoiseConfig.freq,
                    _worldGenerationConfig.perlinNoiseConfig.amplitude
                ),
                WorldGenerationConfig.NoiseType.White => new WhiteNoiseGenerator(
                    _worldGenerationConfig.noiseSeed,
                    _worldGenerationConfig.whiteNoiseConfig.amplitude
                ),
                WorldGenerationConfig.NoiseType.Simplex => new SimplexNoiseGenerator(
                    _worldGenerationConfig.noiseSeed,
                    _worldGenerationConfig.simplexNoiseConfig.freq,
                    _worldGenerationConfig.simplexNoiseConfig.amplitude
                ),
                WorldGenerationConfig.NoiseType.FractalPerlin => new FractalPerlinNoiseGenerator(
                    new PerlinNoiseGenerator(
                        _worldGenerationConfig.noiseSeed,
                        _worldGenerationConfig.perlinNoiseConfig.freq,
                        _worldGenerationConfig.perlinNoiseConfig.amplitude
                    ),
                    _worldGenerationConfig.fractalPerlinNoiseConfig.octavesCount,
                    _worldGenerationConfig.fractalPerlinNoiseConfig.persistence,
                    _worldGenerationConfig.fractalPerlinNoiseConfig.lacunarity
                ),
                _ => throw new ArgumentOutOfRangeException()
            };

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
                }
            };

            worldGenerator.controller.Generate(worldGenerationSettings, baseNoiseGenerator);

            _worldGenerator = worldGenerator;
        }
    }
}
