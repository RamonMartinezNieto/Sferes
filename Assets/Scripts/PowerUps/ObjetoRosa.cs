using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoRosa : MonoBehaviour
{
  
    public void OnTriggerEnter2D(Collider2D other)
    {
        //Cambio el sprite del jugador
        if (other.gameObject.tag == "Player" && !EstadosJugador.Rosa)
        {
            //pongo a true la variable rosa
            EstadosJugador.Rosa = true; 
            GetComponent<AudioSource>().Play();

            transform.position = new Vector2(-1000,-1000);

        }
    }
}
