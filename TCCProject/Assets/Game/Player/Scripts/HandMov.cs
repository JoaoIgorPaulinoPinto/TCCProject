using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class HandMov : MonoBehaviour
{
    public Transform target; // O alvo a ser alcançado
    public float speed = 5f; // Velocidade de movimento
    public int numRays = 8; // Número de raycasts em torno do objeto
    public float avoidanceDistance = 2f; // Distância para detectar e desviar de obstáculos
    public float avoidanceForce = 5f; // Força de desvio dos obstáculos
    public bool debugMode = true; // Booleana para ativar o modo de depuração
    public LayerMask obstacleLayerMask; // Máscara de camada para os obstáculos


    public bool called;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

    }
    private void Update()
    {

        if (called)
        {
            Move();
            GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        else {
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
     
    }

    private Vector2 CalculateAvoidanceDirection()
    {
        //Cria varias divisões num angulo de 360 graus
        Vector2 avoidanceDirection = Vector2.zero;
        float angleIncrement = 360f / numRays;

        //Cria as linhas de captura de informação e devolve:
        //O angulo do ajuste do movimento
        //E os caminhos que ele colidira se seguir
        for (int i = 0; i < numRays; i++)
        {
           //
           //
           //
           //
           //               TALVEZ O "* Vector2.right" esteja dando problema na linha 57
           //
           //
           //
           //

           Vector2 rayDirection = Quaternion.Euler(0, 0, angleIncrement * i) * Vector2.right;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, avoidanceDistance, obstacleLayerMask);
            if (hit.collider != null)
            {
                float avoidanceFactor = Mathf.Clamp01(1f - (hit.distance / avoidanceDistance));
                avoidanceDirection += avoidanceFactor * Vector2.Perpendicular(rayDirection);

                if (debugMode)
                    Debug.DrawRay(transform.position, rayDirection * hit.distance, Color.red);
            }
            else
            {
                if (debugMode)
                    Debug.DrawRay(transform.position, rayDirection * avoidanceDistance, Color.white);
            }
        }
        //retorna o valor ponderado do angulo para evitar a colisão
        return avoidanceDirection.normalized * avoidanceForce;
    }

    void Move()
    {
       


        Vector2 targetDirection = (target.position - transform.position).normalized;

        Vector2 avoidanceDirection = CalculateAvoidanceDirection();
        Vector2 finalDirection = (targetDirection + avoidanceDirection).normalized;
        transform.position += (Vector3)finalDirection * speed * Time.deltaTime;

        // Desenha o raio na direção resultante (alvo + desvio) em verdes
        if (debugMode)
            Debug.DrawRay(transform.position, finalDirection * avoidanceDistance, Color.green);

    }
}

/*{
    public Transform target; // Alvo para o qual os raios serão direcionados
    public int rayCount = 8; // Quantidade de raios
    public float totalAngle = 360f; // Ângulo total em graus
    public float rayLength = 5f; // Comprimento do raio
    public float rayThickness = 0.1f; // Espessura do raio
    public LayerMask obstacleLayer; // Camada para os obstáculos
    public Color hitColor = Color.red; // Cor do raio quando atinge um obstáculo
    public Color missColor = Color.green; // Cor do raio quando não atinge um obstáculo
    public Color closestRayColor = Color.white; // Cor do raio mais próximo do alvo

    private Vector2 wayRay; // Direção do raio branco
    public bool called = false; // Variável para indicar se o movimento foi chamado
    public float moveSpeed;

    private void Update()
    {
        CastRays();

        if (called)
        {
            MoveTowardsWhiteRay();
        }
    }

    private void CastRays()
    {
        float angleStep = totalAngle / rayCount;

        // Encontrar o raio mais próximo do alvo que não colide com obstáculos
        float minAngleDifference = float.MaxValue;
        int closestRayIndex = -1;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, obstacleLayer);

            Color rayColor = hit ? hitColor : missColor;

            Debug.DrawRay(transform.position, direction * rayLength, rayColor, Time.deltaTime);

            // Calcular a diferença de ângulo entre a direção do raio e a direção do alvo
            float angleDifference = Vector2.Angle(direction, (target.position - transform.position).normalized);

            // Atualizar o raio mais próximo do alvo que não colide com obstáculos
            if (!hit && angleDifference < minAngleDifference)
            {
                minAngleDifference = angleDifference;
                closestRayIndex = i;
                wayRay = direction; // Definir a direção do raio branco
            }
        }

        // Pintar o raio mais próximo do alvo de branco
        if (closestRayIndex != -1)
        {
            float closestRayAngle = closestRayIndex * angleStep;
            Vector2 closestRayDirection = Quaternion.Euler(0, 0, closestRayAngle) * Vector2.right;
            Debug.DrawRay(transform.position, closestRayDirection * rayLength, closestRayColor, Time.deltaTime);


        }
    }

    private void MoveTowardsWhiteRay()
    {
        GetComponent<Rigidbody2D>().velocity = (Vector2.one * wayRay) * moveSpeed;
    }
}*/
/*
public int rayCount = 10; // Número de raios
public LayerMask obstacleMask; // Máscara de camada para obstáculos
public Transform target; // Transform do jogador
public float rayLength = 5f; // Comprimento dos raios
public bool Called = false; // Indica se o objeto deve se mover na direção do raio azul
RaycastHit2D hit;
void Update()
{
    if (target == null)
        return;

    // Calcula a direção do jogador em relação ao objeto
    Vector2 directionToTarget = ((Vector2)target.position - (Vector2)transform.position).normalized;

    // Calcula o ângulo entre os raios e a direção do jogador
    float angleStep = 180f / (rayCount - 1);
    float minAngleDifference = Mathf.Infinity;
    int closestRayIndex = 0;

    // Loop para calcular o raio mais próximo da direção do jogador
    for (int i = 0; i < rayCount; i++)
    {
        // Calcula a direção do raio com base no ângulo
        float angle = angleStep * i - 45f;
        Vector2 direction = Quaternion.Euler(0, 0, angle) * directionToTarget;

        // Calcula o ângulo entre o raio e a direção do jogador
        float angleDifference = Vector2.Angle(direction, directionToTarget);

        // Atualiza o raio mais próximo, se necessário
        if (angleDifference < minAngleDifference)
        {
            // Lança o raio para verificar se não colide com obstáculos ou colide com o alvo
            hit = Physics2D.Raycast(transform.position, direction, rayLength, obstacleMask);
            if (hit.collider == null || hit.collider.transform == target)
            {
                minAngleDifference = angleDifference;
                closestRayIndex = i;
            }
        }
    }

    // Loop para criar e lançar os raios
    for (int i = 0; i < rayCount; i++)
    {
        // Calcula a direção do raio com base no ângulo e na direção para o jogador
        float angle = angleStep * i - 45f;
        Vector2 direction = Quaternion.Euler(0, 0, angle) * directionToTarget;

        // Define a cor do raio
        Color rayColor = Color.red;
        if (i == closestRayIndex && (hit.collider == null || hit.collider.transform == target))
            rayColor = Color.blue;
        else if (hit.collider == null || hit.collider.transform == target)
            rayColor = Color.green;

        // Desenha o raio
        Debug.DrawRay(transform.position, direction * rayLength, rayColor);
    }

    // Move o objeto na direção do raio azul se a variável Called for verdadeira
    if (Called)
    {
        // Calcula a direção do raio azul
        float angle = angleStep * closestRayIndex - 45f;
        Vector2 blueRayDirection = Quaternion.Euler(0, 0, angle) * directionToTarget;

        // Move o objeto na direção do raio azul
        GetComponent<Rigidbody2D>().velocity = blueRayDirection * 3;
    }
}
}*/