using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum DireccionH { Izq, Der };

public class plataforma : MonoBehaviour
{

    public float velocidadV = 0.0f; 
    public float velocidadH = 0.0f;
    public DireccionH sentido = DireccionH.Der;
    public float MaxRecorrdio = 5.0f;
    // Variables privadas
    private Transform transformPlataforma;
    private float distanciaRecorridaX = 0.0F;
    private float referenciaPos;
    private Vector3 posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        transformPlataforma = transform;

        posicionInicial = transformPlataforma.position;

        referenciaPos = posicionInicial.x;
    }

    // Update de las plataformas u objetos móviles en horizontal 
    void Update()
    {
        //PAUSE; Cambio el timeScale para que se pause junto el jugador
        Time.timeScale = MovimientoJugador.getTimeScale();

        // Calculo la distancia horizontal recorrida
        distanciaRecorridaX = Mathf.Abs(transformPlataforma.position.x - referenciaPos);

        if (MaxRecorrdio != -1 && distanciaRecorridaX >= MaxRecorrdio)
        {
            if (sentido == DireccionH.Izq)
            {
                sentido = DireccionH.Der;
            }
            else
            {
                sentido = DireccionH.Izq;
            }
            referenciaPos = transform.position.x;
        }

        if (sentido == DireccionH.Der)
        {
            velocidadH = Mathf.Abs(velocidadH);
        }
        else
        {
            velocidadH = -Mathf.Abs(velocidadH);
        }

        transformPlataforma.Translate(new Vector3(velocidadH, velocidadV, 0) * Time.deltaTime);
    }
}
