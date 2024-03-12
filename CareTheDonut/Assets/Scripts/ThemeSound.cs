using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSound : MonoBehaviour
{
    // bu kodda oyuncu ses aç ya da kapat tuþuna týkladýðýnda bu veriyi playerPref içerisine kaydediyoruz ve sahne her 
    // yeniden yüklendiðinde sesi çalýp çalmamak için aklýmýzda tutuyoruz
    int soundIsOn;
    [SerializeField] GameObject soundOnObj;
    [SerializeField] GameObject soundOffObj;
    private void Awake()
    {
        soundIsOn = PlayerPrefs.GetInt("SoundIsOn");
        if (soundIsOn == 0)
        {
            soundOffObj.SetActive(true);
            soundOnObj.SetActive(false);
            
        }

    }

    public void SoundOn()
    {
        PlayerPrefs.SetInt("SoundIsOn", 1);
    }

    public void SoundOff()
    {
        PlayerPrefs.SetInt("SoundIsOn", 0);
    }
}
