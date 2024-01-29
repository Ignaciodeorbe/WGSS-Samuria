using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public delegate void OnGameOver();

public class GameplayManager : MonoBehaviour
{
    //Event for when the sword is caught used for changing states
    public UnityEvent swordCaught;

    //Sword movement script
    [SerializeField]
    protected SwordMovement swordMovementScript;

    //The audio source
    [SerializeField]
    protected AudioSource audioSource;

    //Text for the level
    [SerializeField]
    Text levelText;

    //Contains all possible sounds in the game
    [SerializeField]
    protected List<AudioClip> listSounds = new List<AudioClip>();

    //The sounds that will play per level
    protected List<AudioClip> gameplaySounds = new List<AudioClip>();

    //List of all moveable objects
    [SerializeField]
    protected List<GameObject> ListObjects = new List<GameObject>();
    protected List<GameObject> gameObjectList = new List<GameObject>();

    //The sound before the sword comes down
    [SerializeField]
    protected AudioClip scream;

    //Temp Reset button
    [SerializeField]
    GameObject resetButton;

    //Countdown Animation
    [SerializeField]
    GameObject countdown;



    protected float timerScale = 2.3f;

    protected float soundTimer = 1;
    protected int soundCounter = 0;

    protected float idleTimer = 1.4f;

    int level = 0;

    protected bool started = false;

    protected int curve;

    public int Curve { get { return curve; } }

    protected States currentState;

    public States CurrentState { set { currentState = value; } get { return currentState; } }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownCoroutine());   //Plays Countdown (Ideally should be moved somewhere so it plays before gameplay starts)
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

                        audioSource.PlayOneShot(audioSource.clip);
                        if (soundCounter < gameObjectList.Count)
                        {
                            Debug.Log("I was called!");
                            Instantiate(gameObjectList[soundCounter], new Vector3(-30, 0, 0), Quaternion.identity);
                        }
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
                idleTimer -= Time.deltaTime;

                if (idleTimer < 0)
                {
                    swordCaught.Invoke();
                    NextLevel();
                }
                break;


            case States.Dead:
                resetButton.SetActive(true);
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
        level++;

        MakeSounds();
        SetTimer();
        started = true;

        curve++;

        if (curve % swordMovementScript.TranslationSpeed == 0)
        {
            swordMovementScript.TranslationSpeed++;
            curve = 0;
        }

        levelText.text = "Act: " + level;
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

    //Method to play the coutdown animation then turn it off
    IEnumerator CountdownCoroutine()
    {
        // Activate the countdown
        countdown.SetActive(true);

        // Play length of animation
        float countdownDuration = 2.6f;

        // Wait for the specified duration
        yield return new WaitForSeconds(countdownDuration);

        // Deactivate the countdown after the animation duration
        countdown.SetActive(false);
    }
}
