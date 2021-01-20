using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsDisplay : MonoBehaviour
{

    TextMeshProUGUI pointsText;
    GameSession gameSession;

    // Start is called before the first frame update
    void Start()
    {
        pointsText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();

        // Calls method once to display the correct score at the start of the game
        UpdatePoints(gameSession.GetPoints());
    }

    // UpdateScore is called once when an enemy is destroyed
    public void UpdatePoints(int points)
    {
        pointsText.text = points.ToString();
    }
}
