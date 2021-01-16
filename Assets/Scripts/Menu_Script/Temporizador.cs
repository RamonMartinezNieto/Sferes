using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temporizador : MonoBehaviour
{

    //Todo crear interfaz para recuperar el tiempo

    Text texto;
    public static float timer; 
    private static bool tempRun = true; 
    private static string minutos, segundos;

    GameObject canvasTemp; 

    void Awake()
    {
        canvasTemp = GameObject.Find("CanvasContador");
        texto = this.GetComponent<Text>();
        iniciarTemporizador();
    }

    void Start(){
    }

    void Update()
    {
        if(tempRun){
            canvasTemp.SetActive(true);
            timer += Time.deltaTime;

            minutos = Mathf.Floor(timer/60).ToString("00");
            segundos = Mathf.Floor(timer%60).ToString("00");
            
            texto.text = string.Format("{0}:{1}",minutos,segundos);
        } else {
            canvasTemp.SetActive(false);
        }
    }


    public static void pararTemporizador(){
        tempRun = false; 
    }

    public void iniciarTemporizador(){
        tempRun = true; 
    }

    public static void resetTemporizador(){
        timer = Time.deltaTime; 
        minutos = "00";
        segundos = "00";
    }
    public static string getTiempo(){
        return string.Format("{0}:{1}",minutos,segundos);
    }


}