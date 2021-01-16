using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuESC : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject canvas;
    public GameObject panelOpcioensEsc;
    public GameObject canvasMenuFinal, CanvasDerrota;
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Solo se podrá activar si los otros canvas están desactivados, para evitar que se sobreponga uno encima de otro
        if(!canvasMenuFinal.activeInHierarchy && !CanvasDerrota.activeInHierarchy){
            if (Input.GetKey(KeyCode.Escape))
            {
                canvas.SetActive(true);
                pausar();
            }
        }
    }


    private void pausar()
    {
        if (canvas.activeSelf)
        {
            //pauso el jugador
            MovimientoJugador.Pausado = true;
        }
    }

    public void reanudarJuego()
    {
        MovimientoJugador.Pausado = false;
        canvas.SetActive(false);
    }


    public void abrirOpciones(){
        panelOpcioensEsc.SetActive(true);
    }

    public void atrasOpciones(){
        panelOpcioensEsc.SetActive(false);
    }
}