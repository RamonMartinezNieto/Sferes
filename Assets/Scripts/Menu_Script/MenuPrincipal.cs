using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    
    AudioSource musicAmbiental;

    void Start(){
        musicAmbiental = GetComponent<AudioSource>(); 

        if(SaveConfigurationXML.getMusicMenu().Equals("on")){
            musicAmbiental.Play();
        }else {
            musicAmbiental.Stop();
        }

        if(SaveConfigurationXML.getVolumenMenu() > 0){
            musicAmbiental.volume = SaveConfigurationXML.getVolumenMenu();
        } else {
            musicAmbiental.volume = 0;
        }

    }


    //carga escena del juego
    public void startGame(){

        //por nombre
        SceneManager.LoadScene(1);

        //Establezco puntuación y timer a 0 en cada run 
        AmarilloPoints.resetContador();
        Temporizador.resetTemporizador();
        //Por número
        //SceneManager.LoadScene(1);

        //Escena siguiente
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);

    }

    //Salir del juego
    public void exitGame(){
        //Ojo quit no se sale si no se ha ejecutado la aplicación
        Application.Quit();
        
        //TODO quitar
        Debug.Log("Saliendo del juego");
    }



}
