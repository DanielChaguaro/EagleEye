using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoBala : MonoBehaviour
{
    public float moveSpeed = 10f; // Velocidad a la que la bala se mueve hacia adelante
    public float horizontalRotationSpeed = 100f; // Velocidad para girar de izquierda a derecha
    public float verticalRotationSpeed = 50f; // Velocidad para girar hacia arriba y abajo
    public float maxVerticalAngle = 45f; // Ángulo máximo para limitar la rotación vertical
    private Vector3 originalPosition;
    public float lifetime = 5f;
    private bool isMoving = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Asegúrate de que la bala no use gravedad
        transform.rotation = Quaternion.Euler(0, 0, 0); // Asegúrate de que la bala esté alineada correctamente
        originalPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isMoving = true; // Activar el movimiento
            lifetime = 10f;
        }

        if (isMoving)
        {
            
        // Movimiento automático hacia adelante
        Vector3 forwardMovement = transform.forward * moveSpeed * Time.deltaTime;

        // Rotación izquierda/derecha
        float horizontalInput = Input.GetAxis("Horizontal"); // Flechas izquierda/derecha o A/D
        float horizontalRotationAmount = horizontalInput * horizontalRotationSpeed * Time.deltaTime;

        // Rotación hacia arriba/abajo
        float verticalInput = Input.GetAxis("Vertical"); // Flechas arriba/abajo o W/S
        float verticalRotationAmount = -verticalInput * verticalRotationSpeed * Time.deltaTime;

        Quaternion horizontalRotation = Quaternion.Euler(0, horizontalRotationAmount, 0);

        Quaternion verticalRotation = Quaternion.Euler(verticalRotationAmount, 0, 0);

        Quaternion newRotation = rb.rotation * horizontalRotation * verticalRotation;

        float verticalAngle = Quaternion.Angle(Quaternion.identity, newRotation);
        if (verticalAngle <= maxVerticalAngle)
        {
            rb.MoveRotation(newRotation);
        }

        rb.MovePosition(rb.position + forwardMovement); 
        
        lifetime -= Time.deltaTime;
            if (lifetime <= 0f)
            {
                isMoving = false; // Desactivar el movimiento
                transform.position = originalPosition; // Regresar a la posición original
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }

        }
        
    }

    
    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        isMoving = false;
        transform.position = originalPosition;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
