using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

    int score;
    [SerializeField] int highScore;
    TextMeshProUGUI scoreText;
    bool isScored;
    [SerializeField] TextMeshProUGUI panelScore;
    [SerializeField] TextMeshProUGUI panelHighScore;
    [SerializeField] float timer;


    void Start()
    {
        timer = 0;
        score = 0;
        isScored = false;
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = score.ToString();
        highScore = PlayerPrefs.GetInt("highScore");
    }

    public void Scored()
    {
        score++;
        scoreText.text = score.ToString();
        isScored=true;
    }

    public void GameOver()
    {
        panelScore.text = scoreText.text;
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        panelHighScore.text = highScore.ToString();

    }
    void Update()
    {
        
        if (isScored)
        {
            
            if (timer < 1) 
            {
                timer += Time.deltaTime * 10;
                scoreText.outlineWidth = timer;
            }
            else
            {
                isScored = false;
            }
            
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime * 10;
                scoreText.outlineWidth = timer;
            }

        }
        
    }
}
