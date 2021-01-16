using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    static GameObject gameObjectPersonaje; 
    Rigidbody2D personajeRB;
    public bool tocaSuelo;
    public bool tocaPared;
    Transform posicionPersonaje;
    //Sistema de particulas
    public ParticleSystem particleSystemDust;
    //arrays con diferentes suelos y paredes
    private string[] SUELOS = new string[] { "Ground", "Plataforma", "PlataformaBlanca", "PlataformaNegra", "ObstaculoBlanco", "ObstaculoNegro" };
    private string[] PAREDES = new string[] { "Pared" };
    //Enum con las direcciones "derecha e izquierda" 
    private enum dirPared { derecha, izquierda };
    //Método para evaluar sobre que dirección se está tocando la pared
    private dirPared direccionPared;
    public Vector3 posAnterior = new Vector3(0f, 0f, 0f);
    private const float VELOCIDAD = 25.0f;
    private const float LIMITE_VEL = 7.0f;
    private static bool _pausado = false;

    //Control de pausa
    public static bool Pausado
    {
        get { return _pausado; }
        set
        {
          //  if (_pausado != value)
          //  {
                _pausado = value;
                //ejecuto la llamada a pause que comprueba si ha de pausar el juego
                pauseGame();
         //   }
        }
    }

    //Método para pausar el juego
    private static void pauseGame()
    {
        if (!_pausado)
        {
            if(SaveConfigurationXML.getMusicGame().Equals("off")){
                gameObjectPersonaje.GetComponent<AudioSource>().mute = true;
            } else {
                gameObjectPersonaje.GetComponent<AudioSource>().mute = false;                
            }

            Time.timeScale = 1;
        }
        else
        {
            gameObjectPersonaje.GetComponent<AudioSource>().mute = true;
            Time.timeScale = 0;
        }
    }

    public static float getTimeScale()
    {
        return Time.timeScale;
    }


    void Start()
    {
        //instancio el estado: 
        gameObjectPersonaje = this.gameObject; 
        personajeRB = GetComponent<Rigidbody2D>();
        posicionPersonaje = GetComponent<Transform>();

        MovimientoJugador.Pausado = false; 
        tocaSuelo = false;
        tocaPared = false;

        //incio las particulas        
        particleSystemDust.Play();
    }


    // Update is called once per frame
    void Update()
    {

        //Para que no haya problemas evaluo el deltaTime para que si es cero no siga con el update
        if (Time.deltaTime > 0f)
        {
            //Método que detecta el suelo a través de RayCast
            detectarSuelo();

            //Método que detecta con raycast si se toca la pared
            deteccionPared();

            //actualizo sistema de particulas del personaje
            iniciarParticulas();

            //actualizo transform   
            posicionPersonaje = gameObject.transform;

            //Límito la velocidad
            float limitedSpeedX = Mathf.Clamp(personajeRB.velocity.x, -LIMITE_VEL, LIMITE_VEL);
            float limitedSpeedY = Mathf.Clamp(personajeRB.velocity.y, -LIMITE_VEL, LIMITE_VEL);

            //Si tengo el estado rosa, pulsa la A y toco la pared
            if (Input.GetKey("a") && EstadosJugador._rosa && tocaPared)
            {
                personajeRB.AddForce(Vector2.left * (VELOCIDAD * 2));
                personajeRB.AddForce(Vector2.up * (VELOCIDAD * 2));

                personajeRB.velocity = new Vector2(0f, personajeRB.velocity.y);
                personajeRB.velocity = new Vector2(limitedSpeedY, personajeRB.velocity.x);

                saltar();
            }
            else if (Input.GetKey("d") && EstadosJugador._rosa && tocaPared)
            {
                personajeRB.AddForce(Vector2.right * (VELOCIDAD * 2));
                personajeRB.AddForce(Vector2.up * (VELOCIDAD * 2));

                personajeRB.velocity = new Vector2(0f, personajeRB.velocity.y);
                personajeRB.velocity = new Vector2(limitedSpeedY, personajeRB.velocity.x);

                saltar();
            }
            //estados de movimiento normal
            else if (Input.GetKey("d"))
            {
                personajeRB.AddForce(Vector2.right * VELOCIDAD);
                personajeRB.velocity = new Vector2(limitedSpeedX, personajeRB.velocity.y);

                saltar();
            }
            else if (Input.GetKey("a"))
            {
                personajeRB.AddForce(Vector2.right * -VELOCIDAD);
                personajeRB.velocity = new Vector2(limitedSpeedX, personajeRB.velocity.y);

                saltar();
            }

            saltar();
        }
    }

    private void saltar()
    {
        if (EstadosJugador._rosa && tocaPared && Input.GetKeyDown(KeyCode.Space))
        {
            //TODO modificar para cambiar el salto de la pared
            //Tengo que detectar en que posición está la pared
            if (direccionPared == dirPared.izquierda)
            {
                personajeRB.velocity = new Vector3(personajeRB.velocity.x + 5F, 7f, 2f);
            }
            else if (direccionPared == dirPared.derecha)
            {
                personajeRB.velocity = new Vector3(personajeRB.velocity.x - 5F, 7f, 2f);
            }

        }
        else if (Input.GetKeyDown(KeyCode.Space) && tocaSuelo)
        {
            personajeRB.velocity = new Vector2(personajeRB.velocity.x, 7F);
        }
    }

    /*
    * Método para detectar el suelo con el uso de RayCast
    * 3 rayCast para la detección
    */
    private void detectarSuelo()
    {
        //Compruebo el tamaño del jugador para ajustar las distancias
        float distanceRayOrigin = 0.15f;
        if (EstadosJugador._naranja)
        {
            distanceRayOrigin = 0.3f;
        }

        //Variables para el Ray2D 
        Vector2 origen = new Vector2(posicionPersonaje.position.x, posicionPersonaje.position.y);
        Vector2 origen2 = new Vector2(posicionPersonaje.position.x + distanceRayOrigin, posicionPersonaje.position.y);
        Vector2 origen3 = new Vector2(posicionPersonaje.position.x - distanceRayOrigin, posicionPersonaje.position.y);

        Vector2 direccion = Vector2.down;
        Vector2 direccion2 = new Vector2(distanceRayOrigin, direccion.y);
        Vector2 direccion3 = new Vector2(-distanceRayOrigin, direccion.y);

        RaycastHit2D hitDetectado = rayDetected(new Vector2(posicionPersonaje.position.x, posicionPersonaje.position.y), direccion, "ground");
        RaycastHit2D hitDetectado2 = rayDetected(new Vector2(posicionPersonaje.position.x + distanceRayOrigin, posicionPersonaje.position.y), direccion2, "ground");
        RaycastHit2D hitDetectado3 = rayDetected(new Vector2(posicionPersonaje.position.x - distanceRayOrigin, posicionPersonaje.position.y), direccion3, "ground");

        /* ***********  RAYCASTS TEST *******************/

        Debug.DrawRay(origen, direccion, Color.red);
        Debug.DrawRay(origen2, direccion2, Color.blue);
        Debug.DrawRay(origen3, direccion3, Color.green);

        /**********************************************/

        if (hitDetectado.collider != null || hitDetectado2.collider != null || hitDetectado3.collider != null)
        {
            //Cojo el gameOjbect del collider detectado
            GameObject sueloDetectado = (hitDetectado.collider != null) ? hitDetectado.collider.gameObject : null;
            GameObject sueloDetectado2 = (hitDetectado2.collider != null) ? hitDetectado2.collider.gameObject : null;
            GameObject sueloDetectado3 = (hitDetectado3.collider != null) ? hitDetectado3.collider.gameObject : null;

            //itero sobre los suelos para ver si detecto alguno
            if (sueloDetectado != null) deteccionSuelo(sueloDetectado);
            if (sueloDetectado2 != null) deteccionSuelo(sueloDetectado2);
            if (sueloDetectado3 != null) deteccionSuelo(sueloDetectado3);
        }
        else
        {
            tocaSuelo = false;
        }
    }

    //Método para que un objeto itere sobre los suelos para ver si lo detecta
    private void deteccionSuelo(GameObject objetoDetector)
    {
        foreach (string suelo in SUELOS)
        {
            if (objetoDetector != null)
            {
                if (objetoDetector.tag == suelo)
                {
                    tocaSuelo = true;
                }
            }
        }
    }

    //Método para crear un raycast y devolver el Raycasthit2D
    private RaycastHit2D rayDetected(Vector2 origin, Vector2 destination, string layerMask)
    {
        //Según el tamaño hay una distancia diferente
        float distancia = 0.400f;
        if (EstadosJugador._naranja)
        {
            distancia = 0.60f;
        }
        //Creo Ray2D y RaycastHit2D indicando la mascara que quiero detectar
        Ray2D rayo = new Ray2D(origin, destination);
        //podemos usar diréctamente el número entero de la capa,  int mascara = 8; 
        int mascara = 1 << LayerMask.NameToLayer(layerMask);
        RaycastHit2D hitDetectado = Physics2D.Raycast(rayo.origin, rayo.direction, distancia, mascara);

        return hitDetectado;
    }

    //Método para detectar la dirección en que se está tocando la pared
    private void deteccionPared()
    {
        Vector2 origen = new Vector2(posicionPersonaje.position.x, posicionPersonaje.position.y);

        Vector2 direccion = Vector2.right;
        Vector2 direccion2 = Vector2.left;

        RaycastHit2D hitDetectadoR = rayDetected(new Vector2(posicionPersonaje.position.x, posicionPersonaje.position.y), direccion, "ground");
        RaycastHit2D hitDetectadoL = rayDetected(new Vector2(posicionPersonaje.position.x, posicionPersonaje.position.y), direccion2, "ground");

        Debug.DrawRay(origen, direccion, Color.yellow);
        Debug.DrawRay(origen, direccion2, Color.yellow);

        if (hitDetectadoR.collider != null || hitDetectadoL.collider != null)
        {
            //Cojo el gameOjbect del collider detectado
            GameObject paredDerecha = (hitDetectadoR.collider != null) ? hitDetectadoR.collider.gameObject : null;
            GameObject paredIzquierda = (hitDetectadoL.collider != null) ? hitDetectadoL.collider.gameObject : null;

            if (paredDerecha != null)
            {
                direccionPared = dirPared.derecha;
                deteccionPared(paredDerecha);
            }
            else if (paredIzquierda != null)
            {
                direccionPared = dirPared.izquierda;
                deteccionPared(paredIzquierda);
            }
        }
        else
        {
            tocaPared = false;
        }
    }

    //Método que detecta la pared
    private void deteccionPared(GameObject objetoDetector)
    {
        foreach (string pared in PAREDES)
        {
            if (objetoDetector != null)
            {
                if (objetoDetector.tag == pared)
                {
                    tocaPared = true;
                }
            }
        }
    }




    /***********************************************************
        Inicio de la configuración del sistema de particulas.
    ***********************************************************/
    private void iniciarParticulas()
    {

        UnityEngine.ParticleSystem.ShapeModule editableShape = particleSystemDust.shape;

        //Compruebo si es grande para modificar la posición del sistema de particulas dependiendo si 
        //la bola está con naranja y es grande o pequeña
        if (EstadosJugador._naranja)
        {
            if (tocaPared)
            {
                cambioVector3Particulas(0.35f, -0.56f);
            }
            else if (!tocaSuelo)
            {
                cambioVector3Particulas(0.0f, -0.56f);
            }
            cambioVector3Particulas(-0.35f, -0.56f);
        }
        else if (!EstadosJugador._naranja)
        {
            if (tocaPared)
            {
                cambioVector3Particulas(0.25f, -0.27f);
            }
            else if (!tocaSuelo)
            {
                cambioVector3Particulas(0.0f, -0.27f);
            }
            cambioVector3Particulas(-0.25f, -0.27f);
        }

        //Posición de particulas, depende de si está en una pared o no
        var posicionPersonajeX = posicionPersonaje.rotation.x;
        var posicionPersonajeY = posicionPersonaje.rotation.y;
        if (tocaPared)
        {
            if (Input.GetKey("d"))
            {
                //Le asigno posición Z para que esté siempre debajo
                particleSystemDust.transform.rotation = new Quaternion(posicionPersonajeX, posicionPersonajeY, -1f, -1f);
            }
            else if (Input.GetKey("a"))
            {
                particleSystemDust.transform.rotation = new Quaternion(posicionPersonajeX, posicionPersonajeY, -1f, 1f);
            }
        }
        else
        {
            //Le asigno posición Z para que esté siempre debajo
            particleSystemDust.transform.rotation = new Quaternion(posicionPersonajeX, posicionPersonajeY, 0f, -1);
        }

        //Cambio el color según el power up que tenga del start Color
        var main = particleSystemDust.main;

        if (EstadosJugador._naranja && EstadosJugador._rosa)
        {
            main.startColor = new Color(1.0f, 0.62f, 0.62f, 1.0f);
        }
        else if (EstadosJugador._naranja)
        {
            main.startColor = new Color(1.0f, 0.5f, 0.0f, 1.0f);
        }
        else if (EstadosJugador._rosa)
        {
            main.startColor = new Color(1.0f, 0.0f, 1.0f, 1.0f);
        }
        else if (EstadosJugador._blanco)
        {
            main.startColor = new Color(1f, 1f, 1f, 1f);
        }

        //Detección de los gradientes Blanco y Negro para que las particulas se cambien según la bola
        //variable del fcomponente colroOverLikfeTime
        var col = particleSystemDust.colorOverLifetime;
        Gradient nuevoColor = new Gradient();
        //Cambio de color del color según si está blanco o negro
        if (EstadosJugador._blanco)
        {
            nuevoColor.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.white, 0.0f), new GradientColorKey(Color.white, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });

        }
        else if (!EstadosJugador._blanco)
        {
            nuevoColor.SetKeys(new GradientColorKey[] { new GradientColorKey(Color.grey, 0.0f), new GradientColorKey(Color.grey, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
        }
        col.color = nuevoColor;

    }

    //Método para que las particualas se posición adecuadamente
    private void cambioVector3Particulas(float posX, float posY)
    {
        UnityEngine.ParticleSystem.ShapeModule editableShape = particleSystemDust.shape;

        //Control dirección del sistema de particulas. 
        if (decimal.Round((decimal)posicionPersonaje.position.x, 5) > decimal.Round((decimal)posAnterior.x, 5))
        {
            //modifico el sistema de particulas
            Vector3 nuevaPos = new Vector3(posX, posY, 0f);
            editableShape.position = nuevaPos;

        }
        else if (decimal.Round((decimal)posicionPersonaje.position.x, 5) < decimal.Round((decimal)posAnterior.x, 5))
        {
            //modifico el sistema de particulas
            Vector3 nuevaPos = new Vector3(posX * -1, posY, 0f);
            editableShape.position = nuevaPos;
        }
        //Actualizo la posición X anterior
        posAnterior.x = posicionPersonaje.position.x;
    }
    /***********************************************************
        FIN de la configuración del sistema de particulas.
    ***********************************************************/
}