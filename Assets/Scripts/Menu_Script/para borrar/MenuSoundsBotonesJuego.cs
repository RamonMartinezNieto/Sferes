using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundsBotonesJuego : MenuSounds
{

     
    override public void activarDesactivarSonido()
    {
        Object[] listado = Resources.FindObjectsOfTypeAll<AudioSource>();

        foreach (AudioSource go in listado)
        {
            if (SaveConfigurationXML.getMusicGame().Equals("off"))
            {
                go.GetComponent<AudioSource>().mute = true;
            }
            else
            {
                go.GetComponent<AudioSource>().mute = false;
            }
        }
    }
    
}
