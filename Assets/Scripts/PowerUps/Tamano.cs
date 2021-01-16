using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tamano : MonoBehaviour
{

    private float aumentoX = 0.20f;
    private float aumentoY = 0.20f;
    private float decrementoX = 0.10f;
    private float decrementoY = 0.10f;


    public void OnTriggerEnter2D(Collider2D other)
    {
        //Aumento o disminuyo el tamaño del jugador
        if (other.gameObject.tag == "Player")
        {
            if (gameObject.tag == "NaranjaAumento" && !EstadosJugador.Naranja)
            {
                Vector3 nuevoTamano = new Vector3(aumentoX, aumentoY);

                EstadosJugador.Naranja = true;

                other.gameObject.transform.localScale = nuevoTamano;

                GetComponent<AudioSource>().Play();

                //this.gameObject.SetActive(false);
                transform.position = new Vector2(-1000, -1000);

            }
            else if (gameObject.tag == "NaranjaDecremento" && EstadosJugador.Naranja)
            {
                Vector3 nuevoTamano = new Vector3(decrementoX, decrementoY);

                EstadosJugador.Naranja = false;

                other.gameObject.transform.localScale = nuevoTamano;
                GetComponent<AudioSource>().Play();

                //this.gameObject.SetActive(false);
                transform.position = new Vector2(-1000, -1000);

            }
        }
    }
}
