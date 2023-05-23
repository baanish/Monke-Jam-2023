using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    [SerializeField]
    public string gameScene = "Game";
    public GameObject instructions;

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
    public void HowToPlay()
    {
        instructions.SetActive(true);
    }
    public void Back()
    {
        instructions.SetActive(false);
    }
}
