using UnityEngine;

public static class ScreenTools
{
    public static float GetScreenWidth(Camera mainCamera)
    {
        float screenHeight = GetScreenHeight(mainCamera);
        return screenHeight * mainCamera.aspect;
    }

    public static float GetScreenHeight(Camera mainCamera)
    {
        return 2f * mainCamera.orthographicSize;
    }
}
