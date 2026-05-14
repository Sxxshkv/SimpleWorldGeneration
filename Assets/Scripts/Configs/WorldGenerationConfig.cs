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
        public class NoiseConfig
        {
            public int seed;
            public float freq;
            public float amplitude;
        }

        [Serializable]
        public class ChunkConfig
        {
            public Material surfaceMeshMaterial;
            public int chunkGoLayer;
        }

        public NoiseType noiseType;
        public NoiseConfig noiseConfig;
        public int chunksSqrWorldSize;
        public int chunkSize;
        public ChunkConfig chunkConfig;
    }
}
