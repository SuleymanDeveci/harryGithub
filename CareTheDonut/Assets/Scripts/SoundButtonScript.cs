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
    private bool sliderFirstValueChanged;  //_theme music slideri nin On value changde sesi açmasý oyun her baþladýðýnda sesin açýlmasýna neden oluyordu bunu engellemek için ilk value change de sesi açma sadece 
                                           // ses seviyesini ayarla demek için bu deðiþkeni kullanacaðým
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
        sliderFirstValueChanged = true; // oyun ilk çalýþtýðýnda playerpref içerisinden sessin açýk olup olmadýðý bilgisi alýnýp uygulanýyor. uygulama esnasýnda slider OnValueChanged kodu tetikleniyor
                                        // o ilk uygulama esnasýnda bu kod çalýþmasýn diye bu deðiþken konuldu
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
