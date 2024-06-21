using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Enemigo : MonoBehaviour
{
    //public TextMeshProUGUI gano;
    //public Image GameOverCanvas;
    //public Button IniciarNuevamente;
    public Animator animator; 
    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Impacto");
            GetComponent<Collider>().enabled = false;
            //GameOverCanvas.gameObject.SetActive(true);
            //IniciarNuevamente.gameObject.SetActive(true);
            //gano.gameObject.SetActive(true);
        }
    }

   
}
