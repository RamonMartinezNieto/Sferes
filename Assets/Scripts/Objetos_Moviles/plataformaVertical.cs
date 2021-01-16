using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum DireccionV { Up, Down };

public class plataformaVertical : MonoBehaviour
{


    //variables públicas
    public float velocidadV = 0.0f; 
    public DireccionV sentido = DireccionV.Up;
    public float MaxRecorrdio = 0.0f;


    // Variables privadas
    private Transform transformPlataforma;
    private float distanciaRecorridaY = 0.0F;
    private float referenciaPos;
    private Vector3 posicionInicial;

    // Start is called before the first frame update
    void Start()
    {
        transformPlataforma = transform;

        posicionInicial = transformPlataforma.position;

        referenciaPos = posicionInicial.y;
    }

    // Update de las plataformas u objetos móviles en horizontal 
    void Update()
    {
        //PAUSE; Cambio el timeScale para que se pause junto el jugador
        Time.timeScale = MovimientoJugador.getTimeScale();

        // Calculo la distancia horizontal recorrida
        distanciaRecorridaY = Mathf.Abs(transformPlataforma.position.y - referenciaPos);

        if (MaxRecorrdio != -1 && distanciaRecorridaY >= MaxRecorrdio)
        {
            if (sentido == DireccionV.Up)
            {
                sentido = DireccionV.Down;
            }
            else
            {
                sentido = DireccionV.Up;
            }
            referenciaPos = transform.position.y;
        }

        if (sentido == DireccionV.Up)
        {
            velocidadV = Mathf.Abs(velocidadV);
        }
        else
        {
            velocidadV = -Mathf.Abs(velocidadV);
        }

        transformPlataforma.Translate(new Vector3(0, velocidadV, 0) * Time.deltaTime);
    }
}
