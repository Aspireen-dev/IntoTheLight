using UnityEngine;

public class ScreenBorderManager : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _topScreenBorder;
    [SerializeField] private BoxCollider2D _bottomScreenBorder;
    [SerializeField] private BoxCollider2D _leftScreenBorder;
    [SerializeField] private BoxCollider2D _rightScreenBorder;

    private float _borderThickness = 0.1f;

    void Start()
    {
        Camera mainCamera = Camera.main;

        // Calculate screen dimensions in world units
        float screenHeight = 2f * mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;

        // Fit borders to screen edges
        FitBorder(_topScreenBorder, screenWidth, _borderThickness, new Vector2(0f, screenHeight / 2f));
        FitBorder(_bottomScreenBorder, screenWidth, _borderThickness, new Vector2(0f, -screenHeight / 2f));
        FitBorder(_leftScreenBorder, _borderThickness, screenHeight, new Vector2(-screenWidth / 2f, 0f));
        FitBorder(_rightScreenBorder, _borderThickness, screenHeight, new Vector2(screenWidth / 2f, 0f));
    }

    /// <summary>
    /// Adjusts the size and position of the specified 2D box collider.
    /// </summary>
    /// <param name="collider">The <see cref="BoxCollider2D"/> to be resized and repositioned.</param>
    /// <param name="width">The new width of the collider.</param>
    /// <param name="height">The new height of the collider.</param>
    /// <param name="localPosition">The new local position of the collider.</param>
    private void FitBorder(BoxCollider2D collider, float width, float height, Vector2 localPosition)
    {
        collider.size = new Vector2(width, height);
        collider.transform.localPosition = localPosition;
    }
}
