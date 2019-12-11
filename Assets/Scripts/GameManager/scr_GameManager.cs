using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scr_GameManager : MonoBehaviour
{

    public scr_DisplayDocument document;
    public scr_GeneradorDeDocumento documentGen;

    public int playerSelection;

    public Text playerPointsText;
    public GameObject playerPointsObj;
    public bool isScaleGoingBack = false;
    int playerPoints = 0;
    public Text timeTest;

    public GameObject wrongTextObj;
    public GameObject correctTextObj;
    public GameObject returnButtonGameObj;
    public GameObject retryButtonGameObj;

    [Space(20)]
    public float gameTime;
    float cGameTime;
    bool isPlaying = true;

    bool canEnterDenyOnce = true;
    bool canEnterAcceptOnce = true;
    bool canEnterIdleOnce = true;

    private void Start()
    {
        playerPointsText.text = playerPoints.ToString();
        documentGen.GenNewDocument();
        cGameTime = gameTime;
        returnButtonGameObj.SetActive(false);
        retryButtonGameObj.SetActive(false);
        this.correctTextObj.SetActive(false);
        this.wrongTextObj.SetActive(false);
    }

    void Update()
    {
        timeTest.text = cGameTime.ToString("n1");

        if (isPlaying && cGameTime > 0)
        {
            cGameTime -= Time.deltaTime;
            if (document.isValid && (Input.GetKeyDown(KeyCode.Y) || this.playerSelection == 1) && this.canEnterAcceptOnce == true)
            {
                // valido
                this.playerPoints++;
                CanvasUpdate();

                this.correctTextObj.SetActive(true);
                this.wrongTextObj.SetActive(false);

                this.canEnterDenyOnce = true;
                this.canEnterAcceptOnce = false;
                this.canEnterIdleOnce = true;
            }
            else if (!document.isValid && (Input.GetKeyDown(KeyCode.Y) || this.playerSelection == 1) && this.canEnterAcceptOnce == true)
            {
                // invalido
                this.playerPoints--;
                if (this.playerPoints < 0)
                {
                    this.playerPoints = 0;
                }
                CanvasUpdate();

                this.correctTextObj.SetActive(false);
                this.wrongTextObj.SetActive(true);

                this.canEnterDenyOnce = true;
                this.canEnterAcceptOnce = false;
                this.canEnterIdleOnce = true;
            }
            else if (document.isValid && (Input.GetKeyDown(KeyCode.N) || this.playerSelection == 2) && this.canEnterDenyOnce == true)
            {
                // invalido
                this.playerPoints--;
                if(this.playerPoints < 0)
                {
                    this.playerPoints = 0;
                }
                CanvasUpdate();

                this.correctTextObj.SetActive(false);
                this.wrongTextObj.SetActive(true);

                this.canEnterDenyOnce = false;
                this.canEnterAcceptOnce = true;
                this.canEnterIdleOnce = true;
            }
            else if (!document.isValid && (Input.GetKeyDown(KeyCode.N) || this.playerSelection == 2) && this.canEnterDenyOnce == true)
            {
                // valido
                this.playerPoints++;
                CanvasUpdate();

                this.correctTextObj.SetActive(true);
                this.wrongTextObj.SetActive(false);

                this.canEnterDenyOnce = false;
                this.canEnterAcceptOnce = true;
                this.canEnterIdleOnce = true;
            }
            else if (this.playerSelection == 0 && this.canEnterIdleOnce == true)
            {
                //CanvasUpdate();

                this.canEnterDenyOnce = true;
                this.canEnterAcceptOnce = true;
                this.canEnterIdleOnce = false;
            }

        }
        else if (isPlaying)
        {
            EndGame();
            isPlaying = false;
        }
        else if (isPlaying == false)
        {
            Debug.Log("Game Over");
            if(this.isScaleGoingBack == false)
            {
                this.playerPointsObj.transform.localScale = new Vector3(this.playerPointsObj.transform.localScale.x + 0.01f, this.playerPointsObj.transform.localScale.y + 0.01f, this.playerPointsObj.transform.localScale.z + 0.01f);
                if (this.playerPointsObj.transform.localScale.x >= 1.5 && this.playerPointsObj.transform.localScale.y >= 1.5 && this.playerPointsObj.transform.localScale.z >= 1.5)
                {
                    this.isScaleGoingBack = true;
                }
            }
            if(this.isScaleGoingBack == true)
            {
                this.playerPointsObj.transform.localScale = new Vector3(this.playerPointsObj.transform.localScale.x - 0.01f, this.playerPointsObj.transform.localScale.y - 0.01f, this.playerPointsObj.transform.localScale.z - 0.01f);
                if (this.playerPointsObj.transform.localScale.x <= 1.0 && this.playerPointsObj.transform.localScale.y <= 1.0 && this.playerPointsObj.transform.localScale.z <= 1.0)
                {
                    isScaleGoingBack = false;
                }
            }
        }
    }

    void EndGame()
    {
        returnButtonGameObj.SetActive(true);
        retryButtonGameObj.SetActive(true);
    }

    void CanvasUpdate()
    {
        documentGen.GenNewDocument();
        playerPointsText.text = playerPoints.ToString();
    }

}
