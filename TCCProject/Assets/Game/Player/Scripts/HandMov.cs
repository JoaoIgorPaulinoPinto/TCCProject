using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class HandMov : MonoBehaviour
{
    public Transform target; // O alvo a ser alcan�ado
    public float speed = 5f; // Velocidade de movimento
    public int numRays = 8; // N�mero de raycasts em torno do objeto
    public float avoidanceDistance = 2f; // Dist�ncia para detectar e desviar de obst�culos
    public float avoidanceForce = 5f; // For�a de desvio dos obst�culos
    public bool debugMode = true; // Booleana para ativar o modo de depura��o
    public LayerMask obstacleLayerMask; // M�scara de camada para os obst�culos


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
        //Cria varias divis�es num angulo de 360 graus
        Vector2 avoidanceDirection = Vector2.zero;
        float angleIncrement = 360f / numRays;

        //Cria as linhas de captura de informa��o e devolve:
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
        //retorna o valor ponderado do angulo para evitar a colis�o
        return avoidanceDirection.normalized * avoidanceForce;
    }

    void Move()
    {
       


        Vector2 targetDirection = (target.position - transform.position).normalized;

        Vector2 avoidanceDirection = CalculateAvoidanceDirection();
        Vector2 finalDirection = (targetDirection + avoidanceDirection).normalized;
        transform.position += (Vector3)finalDirection * speed * Time.deltaTime;

        // Desenha o raio na dire��o resultante (alvo + desvio) em verdes
        if (debugMode)
            Debug.DrawRay(transform.position, finalDirection * avoidanceDistance, Color.green);

    }
}

/*{
    public Transform target; // Alvo para o qual os raios ser�o direcionados
    public int rayCount = 8; // Quantidade de raios
    public float totalAngle = 360f; // �ngulo total em graus
    public float rayLength = 5f; // Comprimento do raio
    public float rayThickness = 0.1f; // Espessura do raio
    public LayerMask obstacleLayer; // Camada para os obst�culos
    public Color hitColor = Color.red; // Cor do raio quando atinge um obst�culo
    public Color missColor = Color.green; // Cor do raio quando n�o atinge um obst�culo
    public Color closestRayColor = Color.white; // Cor do raio mais pr�ximo do alvo

    private Vector2 wayRay; // Dire��o do raio branco
    public bool called = false; // Vari�vel para indicar se o movimento foi chamado
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

        // Encontrar o raio mais pr�ximo do alvo que n�o colide com obst�culos
        float minAngleDifference = float.MaxValue;
        int closestRayIndex = -1;

        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * angleStep;
            Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLength, obstacleLayer);

            Color rayColor = hit ? hitColor : missColor;

            Debug.DrawRay(transform.position, direction * rayLength, rayColor, Time.deltaTime);

            // Calcular a diferen�a de �ngulo entre a dire��o do raio e a dire��o do alvo
            float angleDifference = Vector2.Angle(direction, (target.position - transform.position).normalized);

            // Atualizar o raio mais pr�ximo do alvo que n�o colide com obst�culos
            if (!hit && angleDifference < minAngleDifference)
            {
                minAngleDifference = angleDifference;
                closestRayIndex = i;
                wayRay = direction; // Definir a dire��o do raio branco
            }
        }

        // Pintar o raio mais pr�ximo do alvo de branco
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
public int rayCount = 10; // N�mero de raios
public LayerMask obstacleMask; // M�scara de camada para obst�culos
public Transform target; // Transform do jogador
public float rayLength = 5f; // Comprimento dos raios
public bool Called = false; // Indica se o objeto deve se mover na dire��o do raio azul
RaycastHit2D hit;
void Update()
{
    if (target == null)
        return;

    // Calcula a dire��o do jogador em rela��o ao objeto
    Vector2 directionToTarget = ((Vector2)target.position - (Vector2)transform.position).normalized;

    // Calcula o �ngulo entre os raios e a dire��o do jogador
    float angleStep = 180f / (rayCount - 1);
    float minAngleDifference = Mathf.Infinity;
    int closestRayIndex = 0;

    // Loop para calcular o raio mais pr�ximo da dire��o do jogador
    for (int i = 0; i < rayCount; i++)
    {
        // Calcula a dire��o do raio com base no �ngulo
        float angle = angleStep * i - 45f;
        Vector2 direction = Quaternion.Euler(0, 0, angle) * directionToTarget;

        // Calcula o �ngulo entre o raio e a dire��o do jogador
        float angleDifference = Vector2.Angle(direction, directionToTarget);

        // Atualiza o raio mais pr�ximo, se necess�rio
        if (angleDifference < minAngleDifference)
        {
            // Lan�a o raio para verificar se n�o colide com obst�culos ou colide com o alvo
            hit = Physics2D.Raycast(transform.position, direction, rayLength, obstacleMask);
            if (hit.collider == null || hit.collider.transform == target)
            {
                minAngleDifference = angleDifference;
                closestRayIndex = i;
            }
        }
    }

    // Loop para criar e lan�ar os raios
    for (int i = 0; i < rayCount; i++)
    {
        // Calcula a dire��o do raio com base no �ngulo e na dire��o para o jogador
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

    // Move o objeto na dire��o do raio azul se a vari�vel Called for verdadeira
    if (Called)
    {
        // Calcula a dire��o do raio azul
        float angle = angleStep * closestRayIndex - 45f;
        Vector2 blueRayDirection = Quaternion.Euler(0, 0, angle) * directionToTarget;

        // Move o objeto na dire��o do raio azul
        GetComponent<Rigidbody2D>().velocity = blueRayDirection * 3;
    }
}
}*/