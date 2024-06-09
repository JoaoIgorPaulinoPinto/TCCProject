
using UnityEngine;

public class Gravidade : MonoBehaviour
{
    public Corpo[] celestialBodies; // Lista de todos os corpos celestes

    private const float G = 6.674f; // Constante gravitacional

    void FixedUpdate()
    {
        // Loop atrav�s de todos os corpos celestes para calcular a for�a gravitacional entre eles
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

    // M�todo para calcular a for�a gravitacional entre dois corpos celestes
    private void CalculateGravitationalForce(Corpo body1, Corpo body2)
    {
        Vector2 direction = body2.transform.position - body1.transform.position;
        float distance = direction.magnitude;

        // Evita divis�o por zero
        if (distance == 0)
        {
            return;
        }

        float forceMagnitude = (G * body1.mass * body2.mass) / Mathf.Pow(distance, 2);
        Vector2 force = direction.normalized * forceMagnitude;

        // Aplica a for�a gravitacional aos corpos celestes
        body1.ApplyGravitationalForce(force);
        body2.ApplyGravitationalForce(-force);
    }
}