using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameplayManager : MonoBehaviour
{
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
    CollisionManager collisionManagerScript;

    float timerScale = 3;

    float soundTimer = 1;
    int soundCounter = 0;

    float idleTimer = 2;

    int level = 0;

    bool started = false;

    States currentState;

    public States CurrentState { set { currentState = value; } get { return currentState; } }

    // Start is called before the first frame update
    void Start()
    {
        NextLevel();
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

                        audioSource.PlayOneShot(audioSource.clip);
                        if (soundCounter == gameplaySounds.Count - 1)
                        {
                            swordMovementScript.CanMove = true;
                        }
                        else
                        {
                            soundCounter++;
                            audioSource.clip = gameplaySounds[soundCounter];
                            SetTimer();
                        }
                    }

                }
                idleTimer = 3;
                break;


            case States.SwordClapped:
                idleTimer -= Time.deltaTime;

                if (idleTimer < 0)
                {
                    NextLevel();
                }
                break;


            case States.Dead:

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
        swordMovementScript.ResetInfo();
        started= true;
        currentState = States.Idle;
        collisionManagerScript.CanCatch = true;
    }

    public void MakeSounds()
    {
        soundCounter = 0;
        gameplaySounds.Clear();

        for(int i = 0; i < level; i++)
        {
            gameplaySounds.Add(listSounds[Random.Range(0, listSounds.Count)]);
        }

        gameplaySounds.Add(scream);

        audioSource.clip = gameplaySounds[soundCounter];
    }
}
