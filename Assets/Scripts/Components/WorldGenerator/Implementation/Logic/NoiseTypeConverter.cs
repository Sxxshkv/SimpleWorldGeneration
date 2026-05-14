using System;

namespace SimpleWorldGeneration.WorldGenerator.Logic
{
    public class NoiseTypeConverter
    {
        public NoiseGenerator.IController.NoiseType Convert(GenerationSettings.NoiseContext.NoiseType settingsNoiseType)
        {
            return settingsNoiseType switch
            {
                GenerationSettings.NoiseContext.NoiseType.Perlin => NoiseGenerator.IController.NoiseType.Perlin,
                GenerationSettings.NoiseContext.NoiseType.White => NoiseGenerator.IController.NoiseType.White,
                _ => throw new ArgumentException()
            };
        }
    }
}
