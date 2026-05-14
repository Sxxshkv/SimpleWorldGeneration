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

        public int chunksSqrWorldSize;
        public int chunkSize;
        public ChunkContext chunkContext;
    }
}
