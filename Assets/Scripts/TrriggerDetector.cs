using UnityEngine;
using UnityEngine.Events;

public class TrriggerDetector : MonoBehaviour
{
    // This method is called when another collider enters the trigger collider attached to the object where this script is also attached
    [SerializeField] private UnityEvent OnTriggerEnterEvent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnTriggerEnterEvent?.Invoke();
        }
    }
}
