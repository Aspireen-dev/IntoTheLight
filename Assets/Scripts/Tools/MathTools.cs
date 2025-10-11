using UnityEngine;

public static class MathTools
{
    public static Vector3 GetDirection(Vector3 from, Vector3 to)
    {
        return to - from;
    }

    public static Vector3 GetDirectionNormalized(Vector3 from, Vector3 to)
    {
        return GetDirection(from, to).normalized;
    }

    public static float GetRotationAngle2D(Vector2 from, Vector2 to)
    {
        Vector2 direction = to - from;
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    }
}
