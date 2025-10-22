using UnityEngine;

public class GravityZone2D : MonoBehaviour
{
    // Strength of the gravitational pull
    private float _gravityStrength = 15f;
    // Radius of the gravitational pull area
    private float _pullRadius;

    void Start()
    {
        // Initialize the pull radius based on the CircleCollider2D radius
        _pullRadius = GetComponent<CircleCollider2D>().radius;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.attachedRigidbody != null) // Vérifie si l'objet a un Rigidbody2D
        {
            // Calcule la direction vers le centre de la planète
            Vector2 direction = transform.position - other.transform.position;
            float distance = direction.magnitude;

            // Normalise la direction et applique une force inversement proportionnelle à la distance
            if (distance > 0)
            {
                Vector2 gravityForce = direction.normalized * _gravityStrength * other.attachedRigidbody.mass / distance;
                other.attachedRigidbody.AddForce(gravityForce);
            }
        }
    }

    // Optionnel : Visualiser la zone de gravité dans l'éditeur
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, _pullRadius);
    }
}