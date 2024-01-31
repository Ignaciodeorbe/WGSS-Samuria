using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;

public delegate void OnGameOver();

// Game states
public enum States
{
    Idle,
    SwordClapped,
    Dead,
    Tutorial,
}

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


    //*0* I didn't think about it til the day we were uploading a queue would have been better than a list (maybe add it 
    //in the fu0ure?) ***

    //List of all moveable objects
    [SerializeField]
    protected List<GameObject> ListObjects = new List<GameObject>();
    //List of moveable objects that get used
    protected List<GameObject> gameObjectList = new List<GameObject>();

    //The sound before the sword comes down
    [SerializeField]
    protected AudioClip scream;

    //Temp Reset button
    [SerializeField]
    protected GameObject resetButton;

    //Temp Main Menu Button
    [SerializeField]
    GameObject mainMenuButton;

    //Countdown Animation
    [SerializeField]
    GameObject countdown;


    //The time in between each sound effect
    protected float timerScale = 1.8f;

    //The time that gets counted down
    protected float soundTimer = 1;
    //To keep track of the sounds that have been played
    protected int soundCounter = 0;

    //FOr the default timer speed
    protected float idleTimer = 1.4f;

    //Level counter
    int level = 0;

    //IS the game started
    protected bool started = false;

    //TO increase game difficulty the curve of game(Referenced in NextLevel() method
    protected int curve;

    //Gets the curve
    public int Curve { get { return curve; } }

    //The current state of the game
    protected States currentState;

    //Gets and sets the current states
    public States CurrentState { set { currentState = value; }}

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownCoroutine());   //Plays Countdown (Ideally should be moved somewhere so it plays before gameplay starts)
        NextLevel(); //Calls next level to set up the game
        curve = 4; //Starts the curve at 4 so first speed increase happens faster
    }

    // Update is called once per frame
    void Update()
    {
        //For the different game states
        switch (currentState)
        {
            //When we are idle(AKA sounds are playing
            case States.Idle:
                //Wait for the game to start this is changed in the Countdown function
                if (started)
                {
                    //Subtracts from the sound timer
                    soundTimer -= Time.deltaTime;

                    //When the timer is up
                    if (soundTimer < 0)
                    {

                        //Plays whatever audio clip is in the cue
                        audioSource.PlayOneShot(audioSource.clip);

                        //==== For Visual effects on screen ====
                        //Checks if the soundcounter doesn't equal the last number this is reserved for the sword. Creates an out
                        //Of bounds error otherwise
                        if (soundCounter < gameObjectList.Count)
                        {
                            //Creates the respective gameobject for the sound effect
                            //It creates it spawns it off screen and at the default rotation(The objects have their own specific
                            //positions in their start functions this just keeps them out of the way)
                            Instantiate(gameObjectList[soundCounter], new Vector3(-30, 0, 0), Quaternion.identity);
                        }
                        //============================================


                        //==== For sound effects ====
                        //Checks if we are on the final index 
                        if (soundCounter == gameplaySounds.Count - 1)
                        {
                            //Makes the sword move
                            swordMovementScript.CanMove = true;
                            //Sets the sound effect to null this stops the clip from playing multiple time
                            audioSource.clip = null;
                        }
                        //For if we are not on the final index
                        else
                        {
                            //Increases the sound counter(Basically sets us to next index)
                            soundCounter++;
                            //Sets the next audio clip in the queue
                            audioSource.clip = gameplaySounds[soundCounter];
                            //Resets the timer
                            SetTimer();
                        }
                    }
                }
                //Sets the idle timer for when state changes to swordclapped
                idleTimer = 1.4f;
                break;

            //FOr when the sword is caught
            case States.SwordClapped:
                //This whole thing is to mainly to give the player feedback that they caught the sword so it waits
                //For a few seconds(idle time variable) then goes into the if statement

                //Subtracts from the time
                idleTimer -= Time.deltaTime;

                if (idleTimer < 0)
                {
                    //Calls the event that the sword has been called for the other scripts
                    //Then proceeds to the next level
                    swordCaught.Invoke();
                    NextLevel();
                }
                break;

            //When the player dies
            case States.Dead:
                //Activates the buttons to either reset or go to the main menu
                resetButton.SetActive(true);
                mainMenuButton.SetActive(true);
                break;

        }

    }

    /// <summary>
    /// Resets the timer
    /// </summary>
    public void SetTimer()
    {
        //Sets the timer equal to a the default +  0.5 to 0.8
        soundTimer = timerScale;
        soundTimer += Random.Range(0.5f, 0.8f);
    }

    /// <summary>
    /// COntinues to the next level
    /// </summary>
    public void NextLevel()
    {
        //Increases level
        level++;

        //Makes new sounds
        //Restes the timer
        MakeSounds();
        SetTimer();

        //Increases the curves
        curve++;

        //If the curve is a factor of the current speed
        if (curve % swordMovementScript.TranslationSpeed == 0)
        {
            //Increases the speed by one
            swordMovementScript.TranslationSpeed++;
            //resets the curve
            curve = 0;
        }

        //Sets the UI text for the level
        levelText.text = "Act: " + level;
        //Sets the current state back to the gameplay loop.
        currentState = States.Idle;
    }

    /// <summary>
    /// Creats the sound list for the current level
    /// </summary>
    public void MakeSounds()
    {
        //Sets the sound counter to 0
        //Clears both the lists
        soundCounter = 0;
        gameplaySounds.Clear();
        gameObjectList.Clear();

        //This is a percentage
        float randPercent = Random.Range(0f, 1.0f);
        //Number of extra sounds
        int extraSoundCount;

        //You are most likely getting 1 to 2 extra sounds
        //Biggest chance of happening at 35%
        if(randPercent > .65)
        {
            extraSoundCount = 1;
        }
        //Second most likely at 30%
        else if(randPercent > .35) 
        {
            extraSoundCount = 2;
        }
        //Third Most likely at 25%
        else if(randPercent > .1)
        {
            extraSoundCount= 0;
        }
        //LOwest chance at 10% 
        else
        {
            extraSoundCount = 3;
        }

        //THis adds the extra sounds to the list
        //For the extra sound counter size loops through
        for (int i = 0; i < extraSoundCount; i++)
        {
            //Selects a random number between 0 and all the sounds in the list
            int randVal = Random.Range(0, listSounds.Count);

            //Adds both the sound and visual from the corresponding list to the game sounds and visuals
            gameplaySounds.Add(listSounds[randVal]);
            gameObjectList.Add(ListObjects[randVal]);
        }

        //Adds the scream as the final thing
        gameplaySounds.Add(scream);

        //Sets the clip to the first sound in the list.
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
        started = true;
        countdown.SetActive(false);
    }
}
