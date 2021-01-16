using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlSonidoObjetos : MonoBehaviour
{
    AudioSource controlAudio;

    void Start()
    {
        //Establezco los sonidos según la configuración
        controlAudio = gameObject.GetComponent<AudioSource>();
        
        if (SaveConfigurationXML.getMusicGame().Equals("on"))
        {
            controlAudio.mute = false;
        }
        else
        {
            controlAudio.mute = true;
        }

        controlAudio.volume = SaveConfigurationXML.getVolumenGame();
           
    }

    public void activarDesactivarSonidoJuego(){
    
        Object [] listado = Resources.FindObjectsOfTypeAll<AudioSource>();

        foreach (AudioSource go in listado)
        {

            if (SaveConfigurationXML.getMusicGame().Equals("off"))
            {
                //Quito el mute cuando el sonido está en OFF
                go.GetComponent<AudioSource>().mute = true;
            }
            else
            {
                go.GetComponent<AudioSource>().mute = false;
            }

        }
    }

}
