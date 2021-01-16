using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{

    GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("CanvasGG");
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<AudioSource>().Play();
            Temporizador.pararTemporizador();

            //this.gameObject.SetActive(false);
            transform.position = new Vector3(100f, transform.position.y, transform.position.z); 

            canvas.SetActive(true);

        }
    }
}
