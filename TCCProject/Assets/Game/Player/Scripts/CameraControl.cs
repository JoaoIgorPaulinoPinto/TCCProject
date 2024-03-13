using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform target; // O objeto que a c�mera ir� seguir
    public float smoothSpeed = 0.125f; // A velocidade de suaviza��o do movimento da c�mera
    public Vector3 offset; // A dist�ncia entre a c�mera e o objeto

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

        // Se desejar, voc� pode adicionar rota��o suave tamb�m
        // Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        // transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, smoothSpeed);
    }
}
