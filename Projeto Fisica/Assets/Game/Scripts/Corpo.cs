
using UnityEngine;

public class Corpo : MonoBehaviour
{
    public float mass = 1f; // Massa do corpo celeste
    public Vector2 initialVelocity = Vector3.zero; // Velocidade inicial do corpo celeste

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = initialVelocity;
    }

    // Método para aplicar força gravitacional entre dois corpos
    public void ApplyGravitationalForce(Vector2 force)
    {
        rb.AddForce(force);
        rb.mass = mass;
    }

}