using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmarilloPoints : MonoBehaviour
{

    Text texto;
    private static int contador;


    void Awake()
    {
        texto = GameObject.Find("Puntos").GetComponent<Text>();

    }

    void Start()
    {
        texto.text = contador.ToString();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        //Cambio el sprite del jugador
        if (other.gameObject.tag == "Player")
        {
            contador++;

            GetComponent<AudioSource>().Play();
            
            transform.position = new Vector2(-1000, -1000);
            //this.gameObject.SetActive(false);

            texto.text = contador.ToString();
        }
    }

    public static int getPuntos()
    {
        return contador;
    }

    public static void resetContador(){
        contador = 0; 
    }
}
