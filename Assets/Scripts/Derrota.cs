using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Derrota : MonoBehaviour
{

    public GameObject canvas;
    public GameObject jugador;

    void Awake()
    {
        canvas.SetActive(false);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Temporizador.pararTemporizador();

            jugador.SetActive(false);
            canvas.SetActive(true);
        }

    }
}
