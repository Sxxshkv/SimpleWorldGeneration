using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/WorldGeneration", fileName = "WorldGeneration")]
    public class WorldGenerationConfig : ScriptableObject
    {
        [Serializable]
        public class PerlinNoiseConfig
        {
            public int seed;
            public float noiseFreq;
            public float noiseAmplitude;
        }

        [Serializable]
        public class ChunkConfig
        {
            public Material surfaceMeshMaterial;
            public int chunkGoLayer;
        }

        public PerlinNoiseConfig perlinNoiseConfig;
        public int chunksSqrWorldSize;
        public int chunkSize;
        public ChunkConfig chunkConfig;
    }
}
