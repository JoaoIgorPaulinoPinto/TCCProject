
using UnityEngine;

public class Gravidade : MonoBehaviour
{
    public Corpo[] celestialBodies; // Lista de todos os corpos celestes

    private const float G = 6.674f; // Constante gravitacional

    void FixedUpdate()
    {
        // Loop através de todos os corpos celestes para calcular a força gravitacional entre eles
        for (int i = 0; i < celestialBodies.Length; i++)
        {
            for (int j = 0; j < celestialBodies.Length; j++)
            {
                if (i != j)
                {
                    CalculateGravitationalForce(celestialBodies[i], celestialBodies[j]);
                }
            }
        }
    }

    // Método para calcular a força gravitacional entre dois corpos celestes
    private void CalculateGravitationalForce(Corpo body1, Corpo body2)
    {
        Vector2 direction = body2.transform.position - body1.transform.position;
        float distance = direction.magnitude;

        // Evita divisão por zero
        if (distance == 0)
        {
            return;
        }

        float forceMagnitude = (G * body1.mass * body2.mass) / Mathf.Pow(distance, 2);
        Vector2 force = direction.normalized * forceMagnitude;

        // Aplica a força gravitacional aos corpos celestes
        body1.ApplyGravitationalForce(force);
        body2.ApplyGravitationalForce(-force);
    }
}