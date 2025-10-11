using UnityEngine;

public class Player : MonoBehaviour
{
    #region Serialized fields

    [SerializeField] private LineRenderer2D _lineRenderer2D;

    #endregion Serialized fields

    #region Private variables

    private Rigidbody2D _rigidbody2D;

    private Vector2 _startPosition = new Vector3(-6, 0);
    private Vector3 _aimingDirectionNormalized;

    private float _impulseForce = 5f;

    #endregion Private variables

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _lineRenderer2D.enabled = false;

        ResetPosition();
    }

    public void StartAiming()
    {
        _lineRenderer2D.enabled = true;
    }

    public void Aim(Vector2 startDraggingPosition, Vector2 currentDraggingPosition)
    {
        float angle = MathTools.GetRotationAngle2D(startDraggingPosition, currentDraggingPosition);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        _aimingDirectionNormalized = MathTools.GetDirectionNormalized(currentDraggingPosition, startDraggingPosition);
        _lineRenderer2D.DrawLine(transform.position, _aimingDirectionNormalized);
    }

    public void StopAiming()
    {
        _lineRenderer2D.enabled = false;
        Launch();
    }

    private void Launch()
    {
        _rigidbody2D.AddForce(_aimingDirectionNormalized * _impulseForce, ForceMode2D.Impulse);
    }

    public void OnTargetTriggered()
    {
        Debug.Log("Player: OnTargetTriggered");
        ResetPosition();
    }

    private void ResetPosition()
    {
        _rigidbody2D.linearVelocity = Vector2.zero;
        _aimingDirectionNormalized = Vector2.zero;
        transform.position = _startPosition;
    }
}
