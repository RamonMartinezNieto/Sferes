using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuFinalPantalla : MonoBehaviour
{
    Text textoPuntos, textoTempFinal;
    private string temporizador, puntos;

    

    //variable de control para el guardado de datos
    private bool control = true;


    void Update()
    {
        gameObject.GetComponent<AudioSource>().volume = SaveConfigurationXML.getVolumenGame();

        textoPuntos = GameObject.Find("PuntosFinal").GetComponent<Text>();
        textoTempFinal = GameObject.Find("TiempoFinal").GetComponent<Text>();

        //Me almaceno los datos
        puntos = AmarilloPoints.getPuntos().ToString();
        temporizador = Temporizador.getTiempo();

        textoPuntos.text = puntos;
        textoTempFinal.text = temporizador;

        //Solo guardo si está activo y uso una variable de control para que no esté guardando todo el rato
        //y solo se guarde una vez, así como pausar el jugador
        if (gameObject.activeSelf)
        {
            while (control)
            {
                MovimientoJugador.Pausado = true; 
                guardarDatosNivel();
                control = false;
            }
        }

    }

    public void menuPrincipal()
    {
        MovimientoJugador.Pausado = false;
        EstadosJugador.resetJugador();
        SceneManager.LoadScene(0);
    }


    //TODO controlar si hay escena siguiente
    public void siguientePantalla()
    {
        //Le quito la pausa al jugador 
        MovimientoJugador.Pausado = false;
        EstadosJugador.resetJugador();
        
        //compruebo cuantas escenas hay
        int escenas = SceneManager.sceneCountInBuildSettings;

        if (escenas > SceneManager.GetActiveScene().buildIndex + 1)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        }
        else
        {
            //*********************** TODO ************************//
            Debug.Log("No hay más escenas");
        }


    }


    //reintentar
    public void reintentar()
    {
        MovimientoJugador.Pausado = false;
        EstadosJugador.resetJugador();
        //Reseteo temporizador y puntuación
        AmarilloPoints.resetContador();
        Temporizador.resetTemporizador();
        puntos = "0";
        temporizador = "00:00";


        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    //Método llamado para almacenar los puntos (desde cualquier parte se puede llamar)
    public void guardarDatosNivel()
    {
        SaveConfigurationXML.setLvlScore(SceneManager.GetActiveScene().buildIndex, puntos);
        SaveConfigurationXML.setTimeScore(SceneManager.GetActiveScene().buildIndex, temporizador);
    }


}
