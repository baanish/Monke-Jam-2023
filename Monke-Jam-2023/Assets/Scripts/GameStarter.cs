using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    public string gameScene = "Game";

    // function to start the game when the button is pressed
    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    // function to quit the game when the button is pressed
    public void QuitGame()
    {
        Application.Quit();
    }
}
