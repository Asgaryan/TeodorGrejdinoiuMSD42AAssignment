using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    Player player;
    TextMeshProUGUI healthText;
    Slider healthbar;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        healthText = GetComponent<TextMeshProUGUI>();
        healthbar = FindObjectOfType<Slider>();
        healthbar.maxValue = player.GetHealth();

        // Calls method once to display the correct health at the start of the game
        UpdateHealth();
    }

    // UpdateHealth is called once when the player receives damage
    public void UpdateHealth()
    {
        healthText.text = player.GetHealth().ToString();
        healthbar.value = player.GetHealth();
    }
}
