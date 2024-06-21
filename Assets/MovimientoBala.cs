using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class MovimientoBala : MonoBehaviour
{
    private float moveSpeed = 20f; // Velocidad a la que la bala se mueve hacia adelante
    private float horizontalRotationSpeed = 100f; // Velocidad para girar de izquierda a derecha
    private float verticalRotationSpeed = 50f; // Velocidad para girar hacia arriba y abajo
    public float maxVerticalAngle = 45f; // Ángulo máximo para limitar la rotación vertical
    private Vector3 originalPosition;
    public float lifetime = 5f;
    private bool isMoving = false;
    private Rigidbody rb;
    private int cantidadbalas = 5; // Contador de vueltas completadas
    public Text cantbalastext; // Referencia al objeto de texto en el canvas
    public TextMeshProUGUI perdio;
    public Image GameOverCanvas;
    public Button IniciarNuevamente;
    public AudioClip sonidoDisparo;
    public ParticleSystem sistemaParticulas;
    public AudioSource audioSource;
    public Image MiraCanvas;
    public TextMeshProUGUI gano;
    public CinemachineVirtualCamera camaraPrincipal; // Referencia a la cámara virtual principal de Cinemachine
    public Camera camaraSecundaria;
    public Camera camaraSecundariaFinal;
    void Start()
    {
        UpdateBalasText();
        camaraPrincipal.gameObject.SetActive(true);
        camaraSecundaria.gameObject.SetActive(false);
        camaraSecundariaFinal.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
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
            sistemaParticulas.Play();
            audioSource.PlayOneShot(sonidoDisparo);
            lifetime = 10f;
            MiraCanvas.gameObject.SetActive(false);
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
        if (cantidadbalas == 0)
        {
            MostrarSinBalasCanvas();
        }
    }

    
    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        isMoving = false;
        if(collision.gameObject.CompareTag("Untagged")){
            transform.position = originalPosition;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            cantidadbalas--; 
            UpdateBalasText();
        }
        if(collision.gameObject.CompareTag("Enemigo")){
            CambiarACamaraSecundaria();
            Invoke("CambiarACamaraPrincipal", 4f);
            transform.position = new Vector3(100f, 5.32f, 20f);
            originalPosition = transform.position;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isMoving = false;
            cantidadbalas=6;
        }
        if(collision.gameObject.CompareTag("EnemigoFinal")){
            CambiarACamaraSecundariaFinal();
            Invoke("CambiarACanvas", 4f);
            transform.position = new Vector3(24.4f, 5.37f, 11f);
            originalPosition = transform.position;
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            isMoving = false;
            
            cantidadbalas=6;
        
        }
    }
    void UpdateBalasText()
    {
        // Actualiza el texto en el objeto de texto del canvas
        if (cantbalastext != null)
        {
            cantbalastext.text = "Balas: " + cantidadbalas;
        }
    }
    void CambiarACanvas(){
        GameOverCanvas.gameObject.SetActive(true);
        IniciarNuevamente.gameObject.SetActive(true);
        gano.gameObject.SetActive(true);
    }
    void MostrarSinBalasCanvas()
    {
        GameOverCanvas.gameObject.SetActive(true);
        IniciarNuevamente.gameObject.SetActive(true);
        perdio.gameObject.SetActive(true);
        cantidadbalas=6;
        transform.position = new Vector3(24.4f, 5.37f, 11f);
        originalPosition = transform.position;
    }
    void CambiarACamaraSecundaria()
    {
        camaraPrincipal.gameObject.SetActive(false);
        camaraSecundaria.gameObject.SetActive(true);
        camaraSecundariaFinal.gameObject.SetActive(false);
    }

    void CambiarACamaraPrincipal()
    {
        camaraSecundaria.gameObject.SetActive(false);
        camaraSecundariaFinal.gameObject.SetActive(false);
        camaraPrincipal.gameObject.SetActive(true);
    }
    void CambiarACamaraSecundariaFinal()
    {
        camaraPrincipal.gameObject.SetActive(false);
        camaraSecundaria.gameObject.SetActive(false);
        camaraSecundariaFinal.gameObject.SetActive(true);
    }
}
