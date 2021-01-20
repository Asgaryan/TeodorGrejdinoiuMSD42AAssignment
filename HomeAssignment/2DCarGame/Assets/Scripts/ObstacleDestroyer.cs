using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D otherObject)
    {
        Destroy(otherObject.gameObject);

        // Adds points only if the object colliding has a ObstaclePathing script
        //to differentiate obstacles from bullets
        // AND if the player is NOT null (if the player object still exists)
        if (otherObject.GetComponent<ObstaclePathing>() != null && FindObjectOfType<Player>() != null)
            FindObjectOfType<GameSession>().AddToPoints();
    }
}