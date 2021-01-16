using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayView : MonoBehaviour
{


    public float weaponRange = 1f; 

    public Transform posicionJugador; 

    void Start()
    {
        
    }

    void Update()
    {

        Vector2 origen = new Vector2( posicionJugador.position.x, posicionJugador.position.y - 0.3f); 
        Vector2 direccion =  new Vector2( posicionJugador.position.x, posicionJugador.position.y -0.4f); 

        
        //Rayo Visual de pruebas debug
    }
}
