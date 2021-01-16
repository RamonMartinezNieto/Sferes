using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SonidosBotones : MonoBehaviour
{

    private static AudioSource sourceButtons;

    void Start()
    {
        //Creo un audio source configuro el play on Awake
        sourceButtons = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        sourceButtons.playOnAwake = false;
        sourceButtons.volume = 0.0f;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            //Establezco configuración inicial según esté en el XML guardado
            if (SaveConfigurationXML.getMusicMenu().Equals("off"))
            {
                sourceButtons.mute = true;
            }
            else
            {
                sourceButtons.mute = false;
            }
            //establezco el volumen inicial
            sourceButtons.volume = SaveConfigurationXML.getVolumenMenu();
        }
        else
        {
            //Establezco configuración inicial de las siguientes escenas (el juego)
            if (SaveConfigurationXML.getMusicGame().Equals("off"))
            {
                sourceButtons.mute = true;
            }
            else
            {
                sourceButtons.mute = false;
            }
            //establezco el volumen inicial (juego)
            sourceButtons.volume = SaveConfigurationXML.getVolumenGame();

        }
    }

    public static void play(AudioClip clip)
    {
        sourceButtons.clip = clip;
        sourceButtons.Play();
    }

    //Busco todos los objetos con AudioSource y los muteo o no según la configuracion
    public void activarDesactivarSonidoBotones()
    {

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (SaveConfigurationXML.getMusicMenu().Equals("off"))
            {
                sourceButtons.GetComponent<AudioSource>().mute = true;
            }
            else
            {
                sourceButtons.GetComponent<AudioSource>().mute = false;
            }
        } else {
               if (SaveConfigurationXML.getMusicGame().Equals("off"))
            {
                sourceButtons.GetComponent<AudioSource>().mute = true;
            }
            else
            {
                sourceButtons.GetComponent<AudioSource>().mute = false;
            }
        }

    }

    // Método para cambiar el volumen en tiempo de ejecución
    public void establecerVolumenBotones(float volumen)
    {
        sourceButtons.GetComponent<AudioSource>().volume = volumen;
    }

}
