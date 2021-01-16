using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Obstaculos : MonoBehaviour
{

    private GameObject player;

    //variables para comprobar el tipo de collider
    private bool boxCollider2D;
    private bool poligonCollider2d;
    private bool tileMapCollider2d; 

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");

        //Compruebo los collider
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>() != null;
        poligonCollider2d = gameObject.GetComponent<PolygonCollider2D>() != null;
        tileMapCollider2d = gameObject.GetComponent<Collider2D>() != null; 

    }

    void Update()
    {
        //Comparo si el material del jugador coincide con el del obstaculo
        if (!EstadosJugador._blanco)
        {
            //Identifico el tipo de plataforma
            if (this.tag == "ObstaculoNegro" || this.gameObject.tag == "ObstaculoNegroPared")
            {
                //Llamo al método que cambiará el trigger
                activarDesactivarTrigger(true);
                cambiarLayerMask(false);
            }
            else if (this.tag == "ObstaculoBlanco" || this.gameObject.tag == "ObstaculoBlancoPared")
            {
                activarDesactivarTrigger(false);
                cambiarLayerMask(true);
            }

        }
        else if (EstadosJugador._blanco)
        {
            if (this.tag == "ObstaculoBlanco" || this.gameObject.tag == "ObstaculoBlancoPared")
            {
                activarDesactivarTrigger(true);
                cambiarLayerMask(false);
            }
            else if (this.tag == "ObstaculoNegro" || this.gameObject.tag == "ObstaculoNegroPared")
            {
                activarDesactivarTrigger(false);
                cambiarLayerMask(true);
            }
        }
    }


    //Método para activar odesactivar el trigger depende del collider que tenga
    private void activarDesactivarTrigger(bool triggerBool)
    {
        if (boxCollider2D)
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = triggerBool;
        }
        else if (poligonCollider2d)
        {
            this.gameObject.GetComponent<PolygonCollider2D>().isTrigger = triggerBool;
        } else if (tileMapCollider2d)
        {
            this.gameObject.GetComponent<Collider2D>().isTrigger = triggerBool;
        }
    }

    //true activo ground, false desactivo ground
    private void cambiarLayerMask(bool activarGround){
        int defaultMask = 0;
        int groundMask = 8;

        if(activarGround){
           this.gameObject.layer = groundMask;
        } else {
            this.gameObject.layer = defaultMask;
        }
    }
}
