using System;
using Configs;
using SimpleWorldGeneration.WorldGenerator;

namespace SimpleWorldGeneration
{
    public class NoiseTypeConverter
    {
        public GenerationSettings.NoiseContext.NoiseType Convert(WorldGenerationConfig.NoiseType configNoiseType)
        {
            return configNoiseType switch
            {
                WorldGenerationConfig.NoiseType.Perlin => GenerationSettings.NoiseContext.NoiseType.Perlin,
                WorldGenerationConfig.NoiseType.White => GenerationSettings.NoiseContext.NoiseType.White,
                _ => throw new ArgumentException()
            };
        }
    }
}
