using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEscOpciones : MonoBehaviour
{


    public Toggle toggleMusicGame;
    public Slider sliderControlMusicGame;

    void Start()
    {
        
        if (toggleMusicGame != null)
        {
            //establezco las propiedades del toggle musica juego
            if (SaveConfigurationXML.getMusicGame().Equals("on"))
            {
                toggleMusicGame.isOn = true;
            }
            else if (SaveConfigurationXML.getMusicGame().Equals("off"))
            {
                toggleMusicGame.isOn = false;
            }
        }

        if (sliderControlMusicGame != null)
        {
            //Establezco la posición del volumen del juego.
            if (SaveConfigurationXML.getVolumenGame() > 0)
            {
                sliderControlMusicGame.SetValueWithoutNotify(SaveConfigurationXML.getVolumenGame());
            }
            else
            {
                sliderControlMusicGame.SetValueWithoutNotify(0.0f);
            }
        } 

    }

    public void ChangeWindDirection(Slider slider)
    {
        if (slider.name == "SliderMusicGame")
        {
            SaveConfigurationXML.setVolumenMusicGame(slider.value);

            MenuSounds.establecerVolumen(slider.value); 
        }

    }

    
    //Activar / desactivar musica del juego
    public void activeDisableMusicGame()
    {
        if (toggleMusicGame.isOn)
        {
            SaveConfigurationXML.setMusicGame("on");
        }
        else
        {
            SaveConfigurationXML.setMusicGame("off");
        }
    }
}
