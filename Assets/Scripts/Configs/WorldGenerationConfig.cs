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
            White
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
        public class ChunkConfig
        {
            public Material surfaceMeshMaterial;
            public int chunkGoLayer;
        }

        public NoiseType noiseType;
        public int noiseSeed;
        public PerlinNoiseConfig perlinNoiseConfig;
        public WhiteNoiseConfig whiteNoiseConfig;
        public int chunksSqrWorldSize;
        public int chunkSize;
        public ChunkConfig chunkConfig;
    }
}
