using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlFondo : MonoBehaviour
{


    private GameObject jugador;
    private Vector3 posicionFondo;


    public float posicionFondoMas; 

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");

        posicionFondo = jugador.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        posicionFondo = jugador.transform.position;
        posicionFondo.y = jugador.transform.position.y + posicionFondoMas; 
        posicionFondo.z = 350f; 


        transform.position = posicionFondo; 
    }
}
