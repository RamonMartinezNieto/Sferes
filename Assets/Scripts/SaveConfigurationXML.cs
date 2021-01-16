using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//para trabajawr con xml    
using System.Xml.Linq;
using System.Xml.XPath;
using System.Linq;
using System.IO;
using System.Xml.Serialization;
using System;

public class SaveConfigurationXML : MonoBehaviour
{



    private const string NOMBRE_ARCHIVO = "configuracionUSER.xml";
    private static string PATH_XML_CONF;
    private static string COMPLETE_PATH;
    private static XDocument doc;


    void Awake()
    {
        //PATH_XML_CONF = System.IO.Directory.GetCurrentDirectory() + @"/conf";
        PATH_XML_CONF = Application.persistentDataPath + @"\conf";
        COMPLETE_PATH = Path.Combine(PATH_XML_CONF, NOMBRE_ARCHIVO);

        try
        {
            //creo el archivo y diréctorio si no existen
            crearArchivo(NOMBRE_ARCHIVO);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }

        //cargo el arhivo XML a utilizar en la clase
        doc = XDocument.Load(COMPLETE_PATH);
    }

    private void crearArchivo(string fileName)
    {
        if (!Directory.Exists(PATH_XML_CONF))
        {
            Directory.CreateDirectory(PATH_XML_CONF);

        }

        //Compruebo que el archivo aún no existe
        if (!File.Exists(PATH_XML_CONF + "/" + fileName))
        {
            crearArchivoConArbolInicial();
        }
    }

    //Método para crear un nuevo archivo con la estructura
    private void crearArchivoConArbolInicial()
    {
        //Creo la estructura del documento, Ojo! XDocument incluye  la instrucción de procesamiento
        XDocument doc = new XDocument(
            new XElement("configuration",
                new XElement("player", new XAttribute("nombre", "default"),
                    new XElement("Menu",
                        new XElement("music", "on"),
                        new XElement("volumen", "0,5"),
                        new XElement("musicInGame", "on"),
                        new XElement("volumenInGame", "0,5")),
                    new XElement("Run",
                        new XElement("points", "0"),
                        new XElement("time", "0")),
                    new XElement("MaxScores",
                        new XElement("lvl", new XAttribute("number", "1"),
                            new XElement("time", "00:00"),
                            new XElement("points", "0")
                        )
                    )
                )
            )
        );

        //Creo el documento al guardarlo directamente en LocalLow.
        doc.Save(COMPLETE_PATH, SaveOptions.DisableFormatting);
    }


    public static string getMusicGame()
    {
        return doc.XPathSelectElement("/configuration/player/Menu/musicInGame").Value;
    }

    public static float getVolumenGame()
    {
        return float.Parse(doc.XPathSelectElement("/configuration/player/Menu/volumenInGame").Value);
    }


    public static string getMusicMenu()
    {
        return doc.XPathSelectElement("/configuration/player/Menu/music").Value;
    }

    public static void setMusicMenu(string value)
    {
        doc.XPathSelectElement("/configuration/player/Menu/music").Value = value;

        doc.Save(COMPLETE_PATH);
    }

    public static float getVolumenMenu()
    {
        return float.Parse(doc.XPathSelectElement("/configuration/player/Menu/volumen").Value);
    }


    public static void setMusicGame(string value)
    {
        doc.XPathSelectElement("/configuration/player/Menu/musicInGame").Value = value;

        doc.Save(COMPLETE_PATH);
    }

    public static void setVolumenMusicGame(float value)
    {
        doc.XPathSelectElement("/configuration/player/Menu/volumenInGame").Value = value.ToString();

        doc.Save(COMPLETE_PATH);
    }



    public static void setVolumenMusicMenu(float value)
    {
        doc.XPathSelectElement("/configuration/player/Menu/volumen").Value = value.ToString();

        doc.Save(COMPLETE_PATH);
    }


    //********************  SCORES & TIMES *********************************//
    public static string getLvlScore(int lvlToSave)
    {
        return doc.XPathSelectElement("/configuration/player/MaxScores/lvl[@number=" + lvlToSave + "]/points").Value;
    }

    public static void setLvlScore(int lvlToGetPoints, string score)
    {

        doc.XPathSelectElement("/configuration/player/MaxScores/lvl[@number=" + lvlToGetPoints + "]/points").Value = score;

        doc.Save(COMPLETE_PATH);
    }

    public static string getTimeScore(int lvlToSave)
    {
        return doc.XPathSelectElement("/configuration/player/MaxScores/lvl[@number=" + lvlToSave + "]/time").Value;
    }

    public static void setTimeScore(int lvlToGetPoints, string time)
    {

        doc.XPathSelectElement("/configuration/player/MaxScores/lvl[@number=" + lvlToGetPoints + "]/time").Value = time;

        doc.Save(COMPLETE_PATH);
    }

    //********************  SCORES *********************************//
}