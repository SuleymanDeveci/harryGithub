using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Fish fish;
    public ObstacleSpawner obstacleSpawner;
    public static GameManager Instance { get; private set; }

    
    public Animator animator;
    public static bool gameOver;
    public static bool gameStarted;
    public static bool isFirstStart;
    public GameObject gameOverPanel;
    //public float sayacTime;
    //public int fps;
    private void Awake()
    {
        SingletonThisGameObject();
        
            

    }
    void Start()
    {

        //fps = 0;
        //sayacTime = 0;
        gameOver = false;
        gameStarted = false;
        isFirstStart = true;
        fish.OnFirstStart();
        animator.SetBool("IsGameStarted", false);
    }
    private void Update()
    {
        //ShowFps();
    }

    /*
    private void ShowFps()
    {
        if (sayacTime >= 1)
        {
            Debug.Log(fps);
            sayacTime = 0;
            fps = 0;
        }
        else
        {
            sayacTime += Time.deltaTime;
            fps++;
        }
    }
    */




    public void GameOver()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);

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
