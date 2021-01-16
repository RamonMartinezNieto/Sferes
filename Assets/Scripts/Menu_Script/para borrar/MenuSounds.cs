using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


//Implemento IPointerEnterHandler e IPointerDownHandeler (al pasar el ratón por encima o al pulsar)
public class MenuSounds : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{

    private AudioClip clipDown;
    private AudioClip clipEnter;
    private AudioSource source;

    void Start()
    {
        //Creo un audio source configuro el play on Awake
        source = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        source.playOnAwake = false;
        source.volume = 0.0f; 
        
        //Busco los clips en los recursos
        clipDown = Resources.Load<AudioClip>("Sounds/cursorDown");
        clipEnter = Resources.Load<AudioClip>("Sounds/mouseMove");

        //Establezco configuración inicial según esté en el XML guardado
        if (SaveConfigurationXML.getMusicMenu().Equals("off"))
        {
            source.mute = true;
        }
        else
        {
            source.mute = false;
        }

        //establezco el volumen inicial
        source.volume = SaveConfigurationXML.getVolumenMenu(); 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        source.clip = clipEnter;
        source.Play();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        source.clip = clipDown;
        source.Play();
    }


    //Busco todos los objetos con AudioSource y los muteo o no según la configuracion
    public virtual void activarDesactivarSonido()
    {
        Object[] listado = Resources.FindObjectsOfTypeAll<AudioSource>();

        foreach (AudioSource go in listado)
        {
            if (SaveConfigurationXML.getMusicMenu().Equals("off"))
            {
                go.GetComponent<AudioSource>().mute = true;
            }
            else
            {
                go.GetComponent<AudioSource>().mute = false;
            }
        }
    }

    // Método para cambiar el volumen en tiempo de ejecución
    public static void establecerVolumen(float volumen)
    {
        Object[] listado = Resources.FindObjectsOfTypeAll<AudioSource>();

        foreach (AudioSource go in listado)
        {
            go.GetComponent<AudioSource>().volume = volumen;
        }
    }

}