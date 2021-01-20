using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int points = 0;

    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberOfGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetPoints()
    {
        return points;
    }

    public void AddToPoints()
    {
        // Adds 5 points and updates the display
        points += 5;
        FindObjectOfType<PointsDisplay>().UpdatePoints(points);

        // If the points reach 100 or more load the winner scene
        if (points >= 100)
            FindObjectOfType<Level>().LoadWinner();
    }

    public void ResetGame()
    {
        points = 0;
    }
}
