using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    // Awake is called right after the script instance is loaded
    void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        // If there is more than one MusicPlayer, destroy the last one created
        if (FindObjectsOfType<MusicPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            // Do not destroy the MusicPlayer when changing scenes
            DontDestroyOnLoad(gameObject);
        }
    }
}
