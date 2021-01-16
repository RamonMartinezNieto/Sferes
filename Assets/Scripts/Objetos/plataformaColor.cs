using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plataformaColor : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Miro si colisionó con le jugador
        if (other.gameObject.tag == "Player")
        {
            //cambio la variable del estado que controla si es blanco o negro
            if (this.gameObject.tag == "PlataformaBlanca")
            {
                EstadosJugador.Blanco = true;
            } else if (this.gameObject.tag == "PlataformaNegra")
            {
                EstadosJugador.Blanco = false;
            }
        }
    }

}

