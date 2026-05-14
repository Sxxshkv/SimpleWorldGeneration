using UnityEngine;

namespace SimpleWorldGeneration.WorldGenerator
{
    public class GenerationSettings
    {
        public class ChunkContext
        {
            public Transform root;
            public Material material;
            public int layer;
        }

        public class NoiseContext
        {
            public enum NoiseType
            {
                Perlin,
                White
            }

            public NoiseType noiseType;
            public float freq;
            public float amplitude;
        }

        public int chunksSqrWorldSize;
        public int chunkSize;
        public NoiseContext noiseContext;
        public ChunkContext chunkContext;
    }
}
