using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MenuOpciones : MonoBehaviour
{

    public Toggle toggleMusicaAmbiental;
    public Slider sliderControlMusic;
    public Slider sliderControlMusicGame;
    public Toggle toggleMusicGame;
    public AudioSource musicaMenu;

    void Awake()
    {
        if (toggleMusicaAmbiental != null)
        {
            //establezco las propiedades del toggle musica menu
            if (SaveConfigurationXML.getMusicMenu().Equals("on"))
            {
                toggleMusicaAmbiental.isOn = true;
            }
            else if (SaveConfigurationXML.getMusicMenu().Equals("off"))
            {
                toggleMusicaAmbiental.isOn = false;
            }
            else
            {
                toggleMusicaAmbiental.isOn = true;
            }
        }

        if (sliderControlMusic != null)
        {
            //Establezco la posición del volumen de la musica del menu.
            if (SaveConfigurationXML.getVolumenMenu() > 0)
            {
                sliderControlMusic.SetValueWithoutNotify(SaveConfigurationXML.getVolumenMenu());
            }
            else
            {
                sliderControlMusic.SetValueWithoutNotify(0.0f);
            }
        }


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


    public void activeDisableMusicMenu()
    {
        if (toggleMusicaAmbiental.isOn)
        {
            SaveConfigurationXML.setMusicMenu("on");
        }
        else
        {
            SaveConfigurationXML.setMusicMenu("off");
        }
    }

    public void ChangeWindDirection(Slider slider)
    {
        //PlayerPrefs.SetFloat("volMusicMenu",slider.value);
        if (slider.name == "SliderMusicMenu")
        {
            SaveConfigurationXML.setVolumenMusicMenu(slider.value);

            musicaMenu.volume = slider.value;
            MenuSounds.establecerVolumen(slider.value); 

        }
        else if (slider.name == "SliderMusicGame")
        {
            SaveConfigurationXML.setVolumenMusicGame(slider.value);
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
