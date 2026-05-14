using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/WorldGeneration", fileName = "WorldGeneration")]
    public class WorldGenerationConfig : ScriptableObject
    {
        public enum NoiseType
        {
            Perlin,
            White,
            Simplex,
            FractalPerlin
        }

        [Serializable]
        public class PerlinNoiseConfig
        {
            public float freq;
            public float amplitude;
        }

        [Serializable]
        public class WhiteNoiseConfig
        {
            public float amplitude;
        }

        [Serializable]
        public class SimplexNoiseConfig
        {
            public float freq;
            public float amplitude;
        }

        [Serializable]
        public class FractalPerlinNoiseConfig
        {
            public int octavesCount;
            public float persistence;
            public float lacunarity;
        }

        [Serializable]
        public class ChunkConfig
        {
            public Material surfaceMeshMaterial;
            public int chunkGoLayer;
        }

        public NoiseType noiseType;
        public int noiseSeed;
        public PerlinNoiseConfig perlinNoiseConfig;
        public WhiteNoiseConfig whiteNoiseConfig;
        public SimplexNoiseConfig simplexNoiseConfig;
        public FractalPerlinNoiseConfig fractalPerlinNoiseConfig;
        public int chunksSqrWorldSize;
        public int chunkSize;
        public ChunkConfig chunkConfig;
    }
}
