using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControlSonidosBotones : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    private AudioClip clipDown;
    private AudioClip clipEnter;

    void Start()
    {
        //Busco los clips en los recursos
        clipDown = Resources.Load<AudioClip>("Sounds/cursorDown");
        clipEnter = Resources.Load<AudioClip>("Sounds/mouseMove");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SonidosBotones.play(clipEnter);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        SonidosBotones.play(clipDown);
    }

    
}
