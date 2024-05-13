using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara_Bala : MonoBehaviour
{
    public Transform target; // El objeto que la cámara seguirá
    public Vector3 offset = new Vector3(0, 2, -5); // Distancia entre la cámara y el objetivo
    public float smoothSpeed = 0.125f; // Para suavizar el movimiento de la cámara
    public float rotationDamping = 3f; // Suaviza la rotación de la cámara
    private bool isMoving = false;
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = true; // Activar el movimiento
        }

        if (isMoving)
        {
        // Calcular la posición deseada
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Suavizar la transición entre la posición actual y la deseada
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        // Suavizar la rotación para mantener el foco en el jugador
        Quaternion desiredRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationDamping * Time.deltaTime);
    }
    }
}
