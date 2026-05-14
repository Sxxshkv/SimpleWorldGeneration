using UnityEngine;

namespace SimpleWorldGeneration.WorldGenerator.Logic
{
    public class ColorCalculator
    {
        public Color GetColorByHeight(float y, float yMin, float yMax)
        {
            if (Mathf.Approximately(yMin, yMax))
                return Color.red;

            float normalizedValue = Mathf.InverseLerp(yMin, yMax, y);
            normalizedValue = Mathf.Clamp01(normalizedValue);

            return new Color(1f - normalizedValue, normalizedValue, 0f);
        }
    }
}
