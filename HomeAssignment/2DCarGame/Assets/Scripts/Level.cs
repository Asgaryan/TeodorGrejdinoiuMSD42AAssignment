using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delayInSeconds = 2f;
    
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GameScene");

        GameSession gs = FindObjectOfType<GameSession>();

        if (gs != null)
        {
            // Reset the Game from beginning
            gs.ResetGame();
        }
    }

    public void LoadGameOver()
    {
        // Starts coroutine as a loss
        StartCoroutine(WaitAndLoad(false));
    }

    public void LoadWinner()
    {
        // Starts coroutine as a win
        StartCoroutine(WaitAndLoad(true));
    }

    IEnumerator WaitAndLoad(bool win)
    {
        yield return new WaitForSeconds(delayInSeconds);

        // Checks if the game is won to load the correct scene
        if (win)
            SceneManager.LoadScene("WinnerScene");
        else
            SceneManager.LoadScene("GameOverScene");
    }

    public void QuitGame()
    {
        // Works only in .exe programs
        Application.Quit();
        print("Quit");
    }
}
