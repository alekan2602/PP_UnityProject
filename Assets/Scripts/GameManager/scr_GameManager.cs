using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_GameManager : MonoBehaviour
{

    public scr_DisplayDocument document;
    public scr_GeneradorDeDocumento documentGen;

    public Text puntajeTest;
    int puntaje = 0;
    public Text timeTest;
    public GameObject endGameObj;

    [Space(20)]
    public float gameTime;
    float cGameTime;
    bool isPlaying = true;

    private void Start()
    {
        puntajeTest.text = puntaje.ToString();
        documentGen.GenNewDocument();
        cGameTime = gameTime;
        endGameObj.SetActive(false);
    }

    void Update()
    {
        timeTest.text = cGameTime.ToString("n1");
        
        if (isPlaying && cGameTime > 0)
        {
            cGameTime -= Time.deltaTime;
            if (document.isValid && Input.GetKeyDown(KeyCode.Y))
            {
                // valido
                puntaje++;
                CanvasUpdate();

            }
            else if (!document.isValid && Input.GetKeyDown(KeyCode.Y))
            {
                // invalido
                puntaje--;
                CanvasUpdate();
            }
            else if (document.isValid && Input.GetKeyDown(KeyCode.N))
            {
                // invalido
                puntaje--;
                CanvasUpdate();
            }
            else if (!document.isValid && Input.GetKeyDown(KeyCode.N))
            {
                // valido
                puntaje++;
                CanvasUpdate();
            }
        }
        else if (isPlaying) 
        {
            EndGame();
            isPlaying = false;
        }
        

    }

    void EndGame() 
    {
        endGameObj.SetActive(true);     
    }

    void CanvasUpdate() 
    {
        documentGen.GenNewDocument();
        puntajeTest.text = puntaje.ToString();
    }
}
