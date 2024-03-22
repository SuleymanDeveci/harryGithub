using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonScript : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Sprite _soundOnSprite;
    [SerializeField] Sprite _soundOffSprite;
    int _soundIsOn;
    // Start is called before the first frame update
    void Start()
    {
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

    public void ChangeImage()
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
}
