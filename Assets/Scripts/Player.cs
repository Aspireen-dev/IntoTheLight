using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Serialized fields

    [SerializeField] private LineRenderer2D _lineRenderer2D;
    [SerializeField] private GameObject _photonVisual;
    [SerializeField] private ParticleSystem _explosionParticleSystem;

    #endregion Serialized fields

    #region Private variables

    private Rigidbody2D _rigidbody2D;

    private Vector2 _startPosition = new Vector2(-6, 0);
    private Vector3 _aimingDirectionNormalized;

    private float _impulseForce = 7f;

    #endregion Private variables

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _lineRenderer2D.enabled = false;

        ResetPosition();
    }

    public void OnTargetTriggered()
    {
        // TODO: Add score increment logic here
        StopSpeed();
        ResetPosition();
    }

    public void OnScreenBorderTriggered()
    {
        StartCoroutine(Explode());
    }

    /// <summary>
    /// Enables the aiming functionality by activating the visual representation of the aim.
    /// </summary>
    /// <remarks>This method enables the 2D line renderer, which is used to visually indicate the aiming
    /// direction. Ensure that the line renderer is properly configured before calling this method.</remarks>
    public void StartAiming()
    {
        _lineRenderer2D.enabled = true;
    }

    /// <summary>
    /// Adjusts the rotation of the object and renders a line to indicate the aiming direction based on the dragging
    /// positions.
    /// </summary>
    /// <remarks>This method calculates the angle between the start and current dragging positions to rotate
    /// the object accordingly. It also determines the normalized aiming direction and uses a line renderer to visually
    /// represent the aiming direction.</remarks>
    /// <param name="startDraggingPosition">The initial position where the dragging started.</param>
    /// <param name="currentDraggingPosition">The current position of the dragging.</param>
    public void Aim(Vector2 startDraggingPosition, Vector2 currentDraggingPosition)
    {
        float angle = MathTools.GetRotationAngle2D(startDraggingPosition, currentDraggingPosition);
        transform.rotation = Quaternion.Euler(0, 0, angle);

        _aimingDirectionNormalized = MathTools.GetDirectionNormalized(currentDraggingPosition, startDraggingPosition);
        _lineRenderer2D.DrawLine(transform.position, _aimingDirectionNormalized);
    }

    /// <summary>
    /// Stops the aiming process and initiates the launch action.
    /// </summary>
    /// <remarks>This method disables the visual aiming indicator and triggers the launch operation.  Ensure
    /// that any necessary setup for launching is completed before calling this method.</remarks>
    public void StopAiming()
    {
        _lineRenderer2D.enabled = false;
        Launch();
    }

    /// <summary>
    /// Launches the object by applying an impulse force in the specified aiming direction.
    /// </summary>
    /// <remarks>This method applies a force to the object's Rigidbody2D using the current aiming direction 
    /// and impulse force magnitude. After the force is applied, the aiming direction is reset to zero.</remarks>
    private void Launch()
    {
        _rigidbody2D.AddForce(_aimingDirectionNormalized * _impulseForce, ForceMode2D.Impulse);
        _aimingDirectionNormalized = Vector2.zero;
    }

    /// <summary>
    /// Triggers the explosion sequence, including stopping movement, hiding the visual,  playing the explosion effect,
    /// and resetting the position after a delay.
    /// </summary>
    /// <remarks>This method is a coroutine and must be started using <see
    /// cref="UnityEngine.MonoBehaviour.StartCoroutine" />. The explosion effect is displayed for 1 second before the
    /// object resets its position.</remarks>
    /// <returns></returns>
    private IEnumerator Explode()
    {
        // Stop the photon movement and hide its visual
        StopSpeed();
        HidePhotonVisual();

        // Play explosion particle system
        _explosionParticleSystem.Play();
        yield return new WaitForSeconds(1f);

        ResetPosition();
    }

    /// <summary>
    /// Resets the object's position to its initial state and reactivates its associated components.
    /// </summary>
    /// <remarks>This method restores the object's position to its starting point, ensures the Rigidbody2D is
    /// active,  and re-enables the visual representation of the object. It is typically used to reset the object 
    /// during gameplay or after specific events.</remarks>
    private void ResetPosition()
    {
        transform.position = _startPosition;
        _rigidbody2D.WakeUp();
        _photonVisual.SetActive(true);
    }

    /// <summary>
    /// Stops all movement of the object by setting its linear and angular velocity to zero.
    /// </summary>
    /// <remarks>This method halts the object's motion and puts the associated Rigidbody2D into a sleep state.
    /// It is typically used to immediately stop the object in scenarios where all movement must cease.</remarks>
    private void StopSpeed()
    {
        _rigidbody2D.linearVelocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0f;
        _rigidbody2D.Sleep();
    }

    /// <summary>
    /// Disables the visual representation of the photon by deactivating its associated GameObject.
    /// </summary>
    /// <remarks>This method sets the active state of the photon visual GameObject to <see langword="false"/>,
    /// effectively hiding it in the scene. Ensure that the GameObject is not null before calling this method.</remarks>
    private void HidePhotonVisual()
    {
        _photonVisual.SetActive(false);
    }
}
