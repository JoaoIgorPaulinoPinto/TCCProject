using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target; // O objeto que a câmera irá seguir
    public float smoothSpeed = 0.125f; // A velocidade de suavização do movimento da câmera
    public Vector3 offset; // A distância entre a câmera e o objeto

    void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Target not assigned to the camera!");
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Se desejar, você pode adicionar rotação suave também
        // Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        // transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed);
    }
}
