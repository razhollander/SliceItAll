public static class MathHandler
{
    public static float ConvertAngleToBeBetween0To360(float angle)
    {
        var between0To360 = angle % 360f;
         
        if (between0To360 < 0)
        {
            between0To360 += 360;
        }

        return between0To360;
    }
}
