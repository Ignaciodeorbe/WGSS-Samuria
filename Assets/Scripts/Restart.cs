using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    //Method to restart the game (can be used for bugtesting or maybe in final product)
    public void RestartGame()
    {
        //Reload the current scene to reset the game
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
