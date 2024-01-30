using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Tutorial : GameplayManager
{

    // For restating the game
    Restart restart = new Restart();


    // Start is called before the first frame update
    void Start()
    {       
        NextLevel();
        curve = 7;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case States.Idle:
                if (started)
                {
                    soundTimer -= Time.deltaTime;

                    if (soundTimer < 0)
                    {


                        audioSource.PlayOneShot(scream);
                        swordMovementScript.CanMove = true;
                        audioSource.clip = null;

                      // if (soundCounter < gameObjectList.Count)
                      // {
                      //     Debug.Log("I was called!");
                      //     Instantiate(gameObjectList[soundCounter], new Vector3(-30, 0, 0), Quaternion.identity);
                      // }
                      // if (soundCounter == gameplaySounds.Count - 1)
                      // {
                      // }
                      // else
                      // {
                      //     soundCounter++;
                      //     //audioSource.clip = gameplaySounds[soundCounter];
                      //     SetTimer();
                      // }
                    }

                }
                idleTimer = 1.4f;
                break;


            case States.SwordClapped:
                idleTimer -= Time.deltaTime;

                if (idleTimer < 0)
                {
                    swordCaught.Invoke();
                    SceneManager.LoadScene("GameScene");

                    //NextLevel();
                }
                break;


            case States.Dead:
                restart.RestartGame();
                break;

        }

    }

    public void SetTimer()
    {
        soundTimer = timerScale;
        soundTimer += Random.Range(0.5f, 0.8f);
    }

    public void NextLevel()
    {

        //MakeSounds();
        SetTimer();
        started = true;

        curve++;

        if (curve % swordMovementScript.TranslationSpeed == 0)
        {
            swordMovementScript.TranslationSpeed++;
            curve = 0;
        }

        
        currentState = States.Idle;
    }

    public void MakeSounds()
    {
        soundCounter = 0;
        gameplaySounds.Clear();
        gameObjectList.Clear();
        int randVal = Random.Range(0, listSounds.Count);

        for (int i = 0; i < Random.Range(0, 3); i++)
        {
            gameplaySounds.Add(listSounds[randVal]);
            gameObjectList.Add(ListObjects[randVal]);
        }

        gameplaySounds.Add(scream);

        audioSource.clip = gameplaySounds[soundCounter];


    }
 
}
