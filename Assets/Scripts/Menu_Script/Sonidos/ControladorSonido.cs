using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//*************************************************
//**************************TODO*******************
//*************************************************
public abstract class ControladorSonido : MonoBehaviour
{

    protected AudioSource source;

    //Método para crear un AduioSource
    protected AudioSource CrearAudioSource(AudioClip clipAudio)
    {
        //Creo un audio source configuro el play on Awake
        source = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        source.playOnAwake = false;
        source.volume = 0.0f;
        source.clip = clipAudio;

        return source;
    }

    //Método para la configuración inicial del audio source.
    protected void ConfigInitialAudioSource(bool activeMusic, float volumen)
    {
        //Establezco configuración inicial según esté en el XML guardado
        if (activeMusic)
        {
            source.mute = true;
        }
        else
        {
            source.mute = false;
        }
        //establezco el volumen inicial
        source.volume = volumen;

    }


    //Método para que se reproduzca la música
    public void PlayMusic(AudioClip clip)
    {
        source.clip = clip;
        source.Play();
    }

    //Método para que se mutee la 
    public void StopMusic()
    {
        source.Stop();
    }

    //Método para mutear
    protected void MuteMusic()
    {
        source.mute = true;
    }

    //Método para que se mutee la 
    protected void UnmutedMusic()
    {
        source.mute = false;
    }

    //Método para cambiar el volumen
    protected void changeVolumen(float volumen)
    {
        source.volume = volumen;
    }

}

