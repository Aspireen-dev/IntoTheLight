using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private variables

    private Rigidbody2D _rigidbody2D;

    private Vector3 _startPosition = new Vector3(-6, 0, 0);

    private float _impulseForce = 7f;

    #endregion Private variables

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        ResetPosition();
    }

    public void Launch()
    {
        _rigidbody2D.AddForce(new Vector2(_impulseForce, 0), ForceMode2D.Impulse);
    }

    public void OnTargetTriggered()
    {
        Debug.Log("Player: OnTargetTriggered");
        ResetPosition();
    }

    private void ResetPosition()
    {
        _rigidbody2D.linearVelocity = Vector2.zero;
        transform.position = _startPosition;
    }
}
