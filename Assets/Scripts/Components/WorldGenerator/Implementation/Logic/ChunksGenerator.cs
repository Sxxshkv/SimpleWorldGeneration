using System.Collections.Generic;
using UnityEngine;

namespace SimpleWorldGeneration.WorldGenerator.Logic
{
    public class ChunksGenerator
    {
        class Chunk
        {
            public Vector2Int position;
            public MeshFilter meshFilter;
            public MeshRenderer renderer;
            public GameObject go;
        }

        Dictionary<Vector2Int, Chunk> _activeChunks;
        readonly ColorCalculator _colorCalculator;
        readonly NoiseTypeConverter _noiseTypeConverter;

        public ChunksGenerator(ColorCalculator colorCalculator)
        {
            _colorCalculator = colorCalculator;
            _noiseTypeConverter = new NoiseTypeConverter();
        }

        public void Generate(GenerationSettings settings, NoiseGenerator.IController noiseGenerator)
        {
            _activeChunks = new Dictionary<Vector2Int, Chunk>();

            for (int x = 0; x < settings.chunksSqrWorldSize; x++)
            {
                for (int z = 0; z < settings.chunksSqrWorldSize; z++)
                {
                    var position = new Vector2Int(x, z);

                    Chunk chunk = CreateChunk(position, settings.chunkContext);
                    GenerateChunkMesh(chunk, settings, noiseGenerator);

                    _activeChunks[position] = chunk;
                }
            }
        }

        Chunk CreateChunk(Vector2Int position, GenerationSettings.ChunkContext chunkContext)
        {
            GameObject go = new($"Chunk_{position.x}_{position.y}");
            go.transform.SetParent(chunkContext.root);
            go.layer = chunkContext.layer;

            MeshFilter meshFilter = go.AddComponent<MeshFilter>();
            MeshRenderer meshRenderer = go.AddComponent<MeshRenderer>();

            meshRenderer.material = chunkContext.material;
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

            Chunk chunk = new()
            {
                position = position,
                meshFilter = meshFilter,
                renderer = meshRenderer,
                go = go
            };

            return chunk;
        }

        void GenerateChunkMesh(Chunk chunk, GenerationSettings settings, NoiseGenerator.IController noiseGenerator)
        {
            int chunkSize = settings.chunkSize;
            int chunksSqrWorldSize = settings.chunksSqrWorldSize;

            int w = chunkSize + 1;
            int h = w;

            Vector3[] vertices = new Vector3[w * h];
            Color[] colors = new Color[w * h];
            int[] triangles = new int[(w - 1) * (h - 1) * 6];

            for (int z = 0; z < h; z++)
            {
                for (int x = 0; x < w; x++)
                {
                    int i = z * w + x;
                    float worldX = chunk.position.x * chunkSize + x;
                    float worldY = chunk.position.y * chunkSize + z;

                    float worldNormX = worldX / (chunkSize * chunksSqrWorldSize);
                    float worldNormY = worldY / (chunkSize * chunksSqrWorldSize);

                    float height = noiseGenerator.Noise(
                        worldNormX,
                        worldNormY,
                        settings.noiseContext.freq,
                        settings.noiseContext.amplitude,
                        _noiseTypeConverter.Convert(settings.noiseContext.noiseType)
                    );

                    vertices[i] = new Vector3(worldX, worldY, height);

                    colors[i] = _colorCalculator.GetColorByHeight(
                        height,
                        -settings.noiseContext.amplitude,
                        settings.noiseContext.amplitude
                    );

                    if (x < w - 1
                     && z < h - 1)
                    {
                        int triIdx = (z * (w - 1) + x) * 6;
                        triangles[triIdx] = i;
                        triangles[triIdx + 1] = i + w;
                        triangles[triIdx + 2] = i + w + 1;
                        triangles[triIdx + 3] = i;
                        triangles[triIdx + 4] = i + w + 1;
                        triangles[triIdx + 5] = i + 1;
                    }
                }
            }

            Mesh mesh = new()
            {
                vertices = vertices,
                triangles = triangles,
                colors = colors
            };

            mesh.RecalculateNormals();

            chunk.meshFilter.mesh = mesh;
        }
    }
}
