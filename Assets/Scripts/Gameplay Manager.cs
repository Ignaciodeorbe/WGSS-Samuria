using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public delegate void OnGameOver();

public class GameplayManager : MonoBehaviour
{

    public UnityEvent swordCaught;

    [SerializeField]
    SwordMovement swordMovementScript;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    List<AudioClip> listSounds = new List<AudioClip>();

    List<AudioClip> gameplaySounds = new List<AudioClip>();

    [SerializeField]
    AudioClip scream;

    [SerializeField]
    GameObject resetButton;

    [SerializeField]
    GameObject bloodSplatter; //Owen Addition

    float timerScale = 2.3f;

    float soundTimer = 1;
    int soundCounter = 0;

    float idleTimer = 1.4f;

    int level;

    bool started = false;

    int curve;

    public int Curve { get { return curve; } }

    States currentState;

    public States CurrentState { set { currentState = value; } get { return currentState; } }

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
                bloodSplatter.SetActive(false);
                if (started)
                {
                    soundTimer -= Time.deltaTime;

                    if (soundTimer < 0)
                    {

                        audioSource.PlayOneShot(audioSource.clip);
                        if (soundCounter == gameplaySounds.Count - 1)
                        {
                            swordMovementScript.CanMove = true;
                            audioSource.clip = null;
                        }
                        else
                        {
                            soundCounter++;
                            audioSource.clip = gameplaySounds[soundCounter];
                            SetTimer();
                        }
                    }

                }
                idleTimer = 1.4f;
                break;


            case States.SwordClapped:
                bloodSplatter.SetActive(false);
                idleTimer -= Time.deltaTime;

                if (idleTimer < 0)
                {
                    swordCaught.Invoke();
                    NextLevel();
                }
                break;


            case States.Dead:
                resetButton.SetActive(true);
                bloodSplatter.SetActive(true);  //Show blood
                break;

        }
        
    }

    public void SetTimer()
    {
        soundTimer = timerScale;
        soundTimer += Random.Range(0.5f,0.8f); 
    }

    public void NextLevel()
    {
        level++;
        
        MakeSounds();
        SetTimer();
        started= true;

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

        for(int i = 0; i < Random.Range(0,3); i++)
        {
            gameplaySounds.Add(listSounds[Random.Range(0, listSounds.Count)]);
        }

        gameplaySounds.Add(scream);

        audioSource.clip = gameplaySounds[soundCounter];
    }
}
