using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Core;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class GameManager : MonoBehaviour
{
    public Fish fish;
    public Score score;
    public ObstacleSpawner obstacleSpawner;
    public AdManager adManager;
    [SerializeField] TextMeshProUGUI fpsText;
    //public TextMeshProUGUI gameCounterText;
    //public TextMeshProUGUI gameCounterText2;
    

    public static GameManager Instance { get; private set; }


    public Animator animator;
    public static bool gameOver;
    public static bool gameStarted;
    public static bool isFirstStart;
    public GameObject gameOverPanel;
    public GameObject gamePlayPanel;
    static int gameCount; // Oyun a��ld�ktan sonra oynanan oyun say�s� her 5 oyunda bir reklam g�stermek i�in kullanaca��m

    private void Awake()
    {
        SingletonThisGameObject();
    }
    void Start()
    {


        adManager.LoadBannerAd();
        gameCount = 1;
        gameOver = false;
        gameStarted = false;
        isFirstStart = true;
        fish.OnFirstStart();
        animator.SetBool("IsGameStarted", false);
        
    }
    private void Update()
    {

    }


    




    public void GameOver()
    {
        adManager.LoadRewardedAd();  // (bu kodu kald�rd�m ��nk� Ad managerde oyun ba��nda �a��raca��z ��nk� yand��� anda reklam izlemek isteyen biri izleyemiyor) yaz�p bu kodu yorum sat�r�na alm���m ama
        //bana �imdi mant�ks�z geldi ve kodu yorum sat�r�ndan ��kard�m.
        gameOver = true;
        gameOverPanel.SetActive(true);
        gamePlayPanel.SetActive(false);
       
        
        if(score.score >= 5)
        {
            gameCount++;

        }

        if(gameCount % 5 == 4)
        {
            adManager.LoadInterstitialAd();
        }
        else if(gameCount % 5 == 0)
        {
            adManager.ShowInterstitialAd();
            gameCount++;
        }
        

    }

    
    public void Rewarded()
    {
        gameOver = false;
        gameOverPanel.SetActive(false);
        gamePlayPanel.SetActive(true);
        isFirstStart = false;
        gameStarted = true;
    }
    

    public void GameHasStarted()
    {
        gameStarted = true;
        animator.SetBool("IsGameStarted", true);
    }

    public void RestartButton()
    {
        isFirstStart = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameOver = false;
        gameStarted = true;
        

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
