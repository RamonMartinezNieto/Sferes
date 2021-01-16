using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioCatchObject : MonoBehaviour
{

    private AudioSource source;

    void Start()
    {
        //creo el audioSource
        source = gameObject.AddComponent(typeof(AudioSource)) as AudioSource;
        source.playOnAwake = false;

        source.clip = Resources.Load<AudioClip>("Sounds/catchObject");
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Cambio el sprite del jugador
        if (other.gameObject.tag == "Player")
        {
            source.Play();
        }
    }
}
