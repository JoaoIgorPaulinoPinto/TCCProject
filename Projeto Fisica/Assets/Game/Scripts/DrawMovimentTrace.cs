using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMovimentTrace : MonoBehaviour
{
    public Transform objectToFollow; // Objeto que ser� seguido
    public int maxPositions = 100; // N�mero m�ximo de pontos na linha
    public float lineWidth = 0.2f; // Largura da linha
    public Material lineMaterial; // Material da linha

    private LineRenderer lineRenderer;
    private Vector3[] positions;

    void Start()
    {
        // Inicializa o LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // Inicializa o array de posi��es
        positions = new Vector3[maxPositions];

        objectToFollow = gameObject.transform;
    }

    void Update()
    {
        // Atualiza as posi��es do array
        UpdatePositions();

        // Define as posi��es no LineRenderer
        lineRenderer.positionCount = maxPositions;
        lineRenderer.SetPositions(positions);
    }

    void UpdatePositions()
    {
        // Move as posi��es uma posi��o para frente
        for (int i = maxPositions - 1; i > 0; i--)
        {
            positions[i] = positions[i - 1];
        }

        // Adiciona a nova posi��o
        positions[0] = objectToFollow.position;
    }
}