using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoScript : MonoBehaviour
{

    private GameObject jugador;
    public GameObject canvas;

    void Awake()
    {
        jugador = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //Cambio el timeScale para que se pause junto el jugador
        Time.timeScale = MovimientoJugador.getTimeScale();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Temporizador.pararTemporizador();

            canvas.SetActive(true);

            jugador.SetActive(false);
        }
    }
}