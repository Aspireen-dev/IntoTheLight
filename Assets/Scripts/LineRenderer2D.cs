using UnityEngine;

public class LineRenderer2D : MonoBehaviour
{
    #region Serialized fields

    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _lineLength = 5f;

    #endregion Serialized fields

    private void OnEnable()
    {
        _lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        _lineRenderer.enabled = false;
    }

    public void DrawLine(Vector2 start, Vector2 directionNormalized)
    {
        Vector2 end = start + (directionNormalized * _lineLength);

        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);
    }
}
