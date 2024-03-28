using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonScript : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] SoundManager _soundManager;
    [SerializeField] Sprite _soundOnSprite;
    [SerializeField] Sprite _soundOffSprite;
    [SerializeField] Slider _slider;  // theme sound slider
    private bool sliderFirstValueChanged;  //_theme music slideri nin On value changde sesi a�mas� oyun her ba�lad���nda sesin a��lmas�na neden oluyordu bunu engellemek i�in ilk value change de sesi a�ma sadece 
                                           // ses seviyesini ayarla demek i�in bu de�i�keni kullanaca��m
    int _soundIsOn;

    void Start()
    {
        sliderFirstValueChanged = false;
        _soundIsOn = PlayerPrefs.GetInt("SoundIsOn");
        if (_soundIsOn == 0)
        {
            _image.sprite = _soundOffSprite;
        }
        else
        {
            _image.sprite = _soundOnSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeImage() // for button
    {
        if(_image.sprite == _soundOnSprite)
        {
            _image.sprite = _soundOffSprite;
        }
        else
        {
            _image.sprite = _soundOnSprite;
        }
        
    }

    public void SoundOnImage() // for slider 
    {
        if (sliderFirstValueChanged == true && _slider.value != 0f && _slider.value != 0.1f)
        {
            _image.sprite = _soundOnSprite;
        }
        sliderFirstValueChanged = true; // oyun ilk �al��t���nda playerpref i�erisinden sessin a��k olup olmad��� bilgisi al�n�p uygulan�yor. uygulama esnas�nda slider OnValueChanged kodu tetikleniyor
                                        // o ilk uygulama esnas�nda bu kod �al��mas�n diye bu de�i�ken konuldu
    }

    public void SoundOffImage() // for slider
    {
        if(_slider.value == 0f)
        {
            _image.sprite = _soundOffSprite;
            _soundManager._soundIsOn = 0;
        }
    }
}
