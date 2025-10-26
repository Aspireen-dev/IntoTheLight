using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    #region UnityEvents

    public UnityEvent OnLevelPassedEvent;

    #endregion UnityEvents

    #region Serialized fields

    [SerializeField] private LineRenderer2D _lineRenderer2D;
    [SerializeField] private GameObject _photonVisual;
    [SerializeField] private GameObject _trailRenderer;
    [SerializeField] private ParticleSystem _explosionParticleSystem;
    [SerializeField] private StartPlacementManager _startPlacementManager;

    #endregion Serialized fields

    #region Private variables

    private Rigidbody2D _rigidbody2D;

    private Vector3 _aimingDirectionNormalized;

    private float _impulseForce = 7f;

    #endregion Private variables

    #region Public properties

    public bool CanAim { get; private set; }

    #endregion Public properties

    void Start()
    {
        _lineRenderer2D.enabled = false;

        _rigidbody2D = GetComponent<Rigidbody2D>();

        ResetPosition();
    }

    public void OnTargetTriggered()
    {
        StopMovement();

        PlayerSettingsManager.UnlockNextLevel();
        OnLevelPassedEvent?.Invoke();
    }

    public void OnScreenBorderTriggered()
    {
        StartCoroutine(Explode());
    }

    public void OnObstacleTriggered()
    {
        StartCoroutine(Explode());
    }

    /// <summary>
    /// Enables the aiming functionality by activating the visual aiming indicator.
    /// </summary>
    /// <remarks>This method disables the ability to aim again until the current aiming process is completed.
    /// The visual indicator for aiming is displayed by enabling the associated 2D line renderer.</remarks>
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
        transform.rotation = Quaternion.Euler(0, 0, 180 + angle);

        _aimingDirectionNormalized = MathTools.GetDirectionNormalized(currentDraggingPosition, startDraggingPosition);
    }

    /// <summary>
    /// Stops the aiming process and initiates the launch action.
    /// </summary>
    /// <remarks>This method disables the visual aiming indicator and triggers the launch operation.  Ensure
    /// that any necessary setup for launching is completed before calling this method.</remarks>
    public void StopAiming()
    {
        _lineRenderer2D.enabled = false;
        CanAim = false;
        Launch();
    }

    /// <summary>
    /// Launches the object by applying an impulse force in the specified aiming direction.
    /// </summary>
    /// <remarks>This method applies a force to the object's Rigidbody2D using the current aiming direction 
    /// and impulse force magnitude. After the force is applied, the aiming direction is reset to zero.</remarks>
    private void Launch()
    {
        _trailRenderer.SetActive(true);
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
        StopMovement();
        HidePhotonVisual();

        // Play explosion particle system
        _explosionParticleSystem.Play();
        yield return new WaitForSeconds(1f);

        ResetPosition();
    }

    /// <summary>
    /// Resets the object's position and state to its initial configuration.
    /// </summary>
    /// <remarks>This method repositions the object to its starting position, reactivates its visual
    /// representation, and ensures it is ready for interaction. It also enables aiming functionality.</remarks>
    private void ResetPosition()
    {
        _startPlacementManager.ResetPlayerPosition();
        _rigidbody2D.WakeUp();
        _photonVisual.SetActive(true);
        CanAim = true;
    }

    /// <summary>
    /// Stops all movement of the object by disabling its trail renderer, setting its linear and angular velocities to
    /// zero, and putting the Rigidbody2D to sleep.
    /// </summary>
    /// <remarks>This method ensures that the object comes to a complete stop and remains stationary until
    /// further action is taken.  It is typically used to halt motion in scenarios where the object should no longer
    /// move or interact with physics.</remarks>
    private void StopMovement()
    {
        _trailRenderer.SetActive(false);

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
