using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public Fish fish;
    public void Pause()
    {
        Time.timeScale = 0f;
        fish.isPaused = true;
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        fish.isPaused = false;
        
    }
}
