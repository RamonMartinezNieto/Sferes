using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadosJugador : MonoBehaviour
{

    //Jugador
    private static GameObject player;


    //Variable Naranja, controla el estado del tamaño
    public static bool _naranja = false;
    //Control de cambio de estado naranja
    public static bool Naranja
    {
        get { return _naranja; }
        set
        {
            if (_naranja != value)
            {
                _naranja = value;
                //ejecuto los estados
                indicarEstado();
                cambioDeEstado();
            }
        }
    }
    
    public static bool _rosa = false;
    //Control del cambio del tipo de variable me obliga a  tenerlo todo estático
    public static bool Rosa
    {
        get { return _rosa; }
        set
        {
            if (_rosa != value)
            {
                _rosa = value;
                //ejecuto los estados
                indicarEstado();
                cambioDeEstado();
            }
        }
    }
    //Siempre empieza en blanco true, negro es false 
    public static bool _blanco = true;
    public static bool Blanco
    {
        get { return _blanco; }
        set
        {
            if (_blanco != value)
            {
                _blanco = value;
                //ejecuto los estados
                indicarEstado();
                cambioDeEstado();
            }
        }
    }

    //Estados que puede tener el jugador
    public enum estados
    {
        Blanco = 0,
        Negro = 1,
        NegroRosa = 2,
        BlancoRosa = 3,
        BlancoNaranja = 4,
        NegroNaranja = 5,
        NegroNaranjaRosa = 6,
        BlancoNaranjaRosa = 7
    }

    //El estado inicial siempre es blanco
    public static EstadosJugador.estados estadoActual = EstadosJugador.estados.Blanco;


    //Variables que representan los sprites que puede tener el jugador
    private static Sprite spriteBlanco;
    private static Sprite spriteNegro;
    private static Sprite spriteNegroNaranja;
    private static Sprite spriteBlancoNaranja;
    private static Sprite spriteBlancoRosa;
    private static Sprite spriteNegroRosa;
    private static Sprite spriteBlancoNaranjaRosa;
    private static Sprite spriteNegroNaranjaRosa;

    void Start()
    {
        //Instancio GameObject y Sprite
        player = GameObject.FindGameObjectWithTag("Player");

        //Cargo los diferentes Sprites que puede tener el jugador
        spriteBlanco = Resources.Load<Sprite>("Sprites/blanco");
        spriteNegro = Resources.Load<Sprite>("Sprites/negro");
        spriteNegroNaranja = Resources.Load<Sprite>("Sprites/negroNaranja");
        spriteBlancoNaranja = Resources.Load<Sprite>("Sprites/blancoNaranja");
        spriteBlancoRosa = Resources.Load<Sprite>("Sprites/BlancoRosa");
        spriteNegroRosa = Resources.Load<Sprite>("Sprites/NegroRosa");
        spriteBlancoNaranjaRosa = Resources.Load<Sprite>("Sprites/blancoNaranjaRosa");
        spriteNegroNaranjaRosa = Resources.Load<Sprite>("Sprites/negroNaranjaRosa");

    }

    //método para indicar el estado según las variables
    private static void indicarEstado()
    {
        if (_blanco)
        {
            if (_naranja && _rosa)
            {
                estadoActual = EstadosJugador.estados.BlancoNaranjaRosa;

            }
            else if (_rosa)
            {
                estadoActual = EstadosJugador.estados.BlancoRosa;

            }
            else if (_naranja)
            {
                estadoActual = EstadosJugador.estados.BlancoNaranja;

            }
            else
            {
                estadoActual = EstadosJugador.estados.Blanco;
            }
        }
        else if (!_blanco)
        {
            if (_naranja && _rosa)
            {
                estadoActual = EstadosJugador.estados.NegroNaranjaRosa;

            }
            else if (_rosa)
            {
                estadoActual = EstadosJugador.estados.NegroRosa;

            }
            else if (_naranja)
            {
                estadoActual = EstadosJugador.estados.NegroNaranja;

            }
            else
            {
                estadoActual = EstadosJugador.estados.Negro;
            }
        }
    }


    //Método para cambiar el sprite del jugador depende del estado que tenga
    private static void cambioDeEstado()
    {
        //Compruebo el estadoActual del jugador para asignar uno u otro script
        switch (estadoActual)
        {
            case EstadosJugador.estados.Blanco:
                cambioSprite(spriteBlanco);
                break;

            case EstadosJugador.estados.Negro:
                cambioSprite(spriteNegro);
                break;

            case EstadosJugador.estados.BlancoNaranja:
                cambioSprite(spriteBlancoNaranja);

                break;

            case EstadosJugador.estados.NegroNaranja:
                cambioSprite(spriteNegroNaranja);
                break;

            case EstadosJugador.estados.NegroRosa:
                cambioSprite(spriteNegroRosa);
                break;

            case EstadosJugador.estados.BlancoRosa:
                cambioSprite(spriteBlancoRosa);
                break;

            case EstadosJugador.estados.BlancoNaranjaRosa:
                cambioSprite(spriteBlancoNaranjaRosa);
                break;

            case EstadosJugador.estados.NegroNaranjaRosa:
                cambioSprite(spriteNegroNaranjaRosa);
                break;
        }
    }

    //Método  para realizar el cambio de sprite
    private static void cambioSprite(Sprite spritee)
    {
        player.GetComponent<SpriteRenderer>().sprite = spritee;
    }


    public static void resetJugador(){
        Naranja = false; 
        Blanco = true; 
        Rosa = false; 
    }

}