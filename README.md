# 🌍 Procedural Terrain Generation Demo

A simple Unity project demonstrating various **noise generation algorithms** for procedural world terrain creation. This example visualizes how different noise functions influence landscape formation, and provides a foundation for building custom terrain systems.

## Supported Noise Algorithms

| Algorithm | Description |
|-----------|-------------|
| **White Noise** | Random, uncorrelated values per sample. Produces grainy, chaotic terrain |
| **Perlin Noise** | Classic gradient-based noise with smooth transitions and continuity |
| **Simplex Noise** | Efficient, higher-dimensional extension of Perlin noise |
| **Perlin-Based Fractal Noise** | Layered (octave) Perlin noise for rich detail: combines low-frequency base terrain with high-frequency surface roughness. |

> *Fractal noise is often the go-to choice for realistic landscapes - mimicking mountains, valleys, and erosion patterns.*

An example of a generated landscape using Perlin-Based Fractal Noise algorithm:

![Terrain Example](https://raw.githubusercontent.com/Sxxshkv/SimpleWorldGeneration/refs/heads/master/PerlineFractalNoiseExample.png)

## How to Use

1. **Open** the project in Unity (2021 LTS or newer recommended);
2. **Configure generation settings** by editing `WorldGenerationConfig`;
3. **Restart the scene** to regenerate terrain with your chosen settings.
