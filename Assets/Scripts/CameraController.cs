using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private GameObject jugador; 
    private Vector3 posicionCamara;

    public float posicionYMas; 

    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");

        posicionCamara = jugador.transform.position; 
        posicionCamara.z = -10f; 

    }

    // Update is called once per frame
    void Update()
    {   
        posicionCamara = jugador.transform.position;
        posicionCamara.y = jugador.transform.position.y + posicionYMas; 
        posicionCamara.z = -10f; 


        transform.position = posicionCamara; 
    }
}
