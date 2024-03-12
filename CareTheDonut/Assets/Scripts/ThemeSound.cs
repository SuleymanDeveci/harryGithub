using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThemeSound : MonoBehaviour
{
    // bu kodda oyuncu ses a� ya da kapat tu�una t�klad���nda bu veriyi playerPref i�erisine kaydediyoruz ve sahne her 
    // yeniden y�klendi�inde sesi �al�p �almamak i�in akl�m�zda tutuyoruz
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
