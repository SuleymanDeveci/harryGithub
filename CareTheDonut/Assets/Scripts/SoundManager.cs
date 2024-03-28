using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update

    public static SoundManager Instance { get; private set; }

    [SerializeField] AudioSource _themeMusic;
    //public int _soundIsOn;
    [SerializeField] Slider _themeMusicSlider;
    private bool sliderFirstValueChanged; //_theme music slideri nin On value changde sesi açmasý oyun her baþladýðýnda sesin açýlmasýna neden oluyordu bunu engellemek için ilk value change de sesi açma sadece 
                                          // ses seviyesini ayarla demek için bu deðiþkeni kullanacaðým

    public int _soundIsOn { get; set; }
    private void Awake()
    {
        //SingletonThisGameObject();
        _themeMusic = GameObject.FindGameObjectWithTag("ThemeMusic").GetComponent<AudioSource>();
    }
    void Start()
    {
        sliderFirstValueChanged = false;
        _soundIsOn = PlayerPrefs.GetInt("SoundIsOn");
        _themeMusicSlider.value = PlayerPrefs.GetFloat("ThemeMusicValue");

        if( _soundIsOn == 1)
        {
            if(!_themeMusic.isPlaying)
            {
                PlaySound();
            }
            
        }
        else
        {
            PauseSound();
        }
    }

    private void Update()
    {
        if( _soundIsOn == 1)
        {
            if (!_themeMusic.isPlaying)
            {
                _themeMusic.volume = 0;
                _themeMusic.Play();
            }
            
            if(_themeMusic.volume < _themeMusicSlider.value)
            {
                _themeMusic.volume += 0.7f * Time.deltaTime;
            }
        }
        else
        {
            if (_themeMusic.isPlaying)
            {
                if (_themeMusic.volume > 0.01f)
                {
                    _themeMusic.volume -= 0.7f * Time.deltaTime;
                }
                else
                {
                    _themeMusic.Pause();
                }
            }
            
        }
    }

    public void ThemeMusicSlider()
    {
        _themeMusic.volume = _themeMusicSlider.value;
        PlayerPrefs.SetFloat("ThemeMusicValue",_themeMusicSlider.value);
    }

    public void PlaySound()
    {
        _themeMusic.Play();

    }

    public void PauseSound()
    {
        _themeMusic.Pause();

    }

    public void ChangeSoundState()
    {
        if(_soundIsOn == 1)
        {
            _soundIsOn = 0;
            PlayerPrefs.SetInt("SoundIsOn", 0);
        }
        else
        {
            _soundIsOn = 1;
            PlayerPrefs.SetInt("SoundIsOn", 1);
            if (_themeMusicSlider.value == 0f)
            {
                _themeMusicSlider.value = 0.1f;
                PlayerPrefs.SetFloat("ThemeMusicValue", _themeMusicSlider.value);
            }
        }
    }

    public void SoundOnValueChanged()
    {
        
        if (!_themeMusic.isPlaying && sliderFirstValueChanged)
        {
            _soundIsOn = 1;
            PlayerPrefs.SetInt("SoundIsOn", 1);
            _themeMusic.Play();
        }
        sliderFirstValueChanged = true;

    }



    private void SingletonThisGameObject()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
