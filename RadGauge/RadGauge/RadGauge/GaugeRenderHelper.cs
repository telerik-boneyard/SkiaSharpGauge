namespace RadGauge
{
    internal static class GaugeRenderHelper
    {
        /// <summary>
        /// Gets physical position on a scale.
        /// </summary>
        /// <returns>The position.</returns>
        /// <param name="value">Numeric value.</param>
        /// <param name="min">Numeric minimum.</param>
        /// <param name="max">Numeric maximum.</param>
        /// <param name="start">Scale physical munumum.</param>
        /// <param name="end">Scale physical maximum.</param>
        internal static float GetRelativePosition(double value, double min, double max, float start, float end)
        {
            double relativePosition = (value - min) / (max - min);
            double position = relativePosition * (end - start);
            position = start + position;

            return (float)position;
        }
    }
}
