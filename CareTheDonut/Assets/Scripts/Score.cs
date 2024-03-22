using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{

    //public int score;
    [SerializeField] int highScore;
    TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreTextPausePanel;
    bool isScored;
    [SerializeField] TextMeshProUGUI panelScore;
    [SerializeField] TextMeshProUGUI panelHighScore;
    [SerializeField] float timer;
    public GPGSManager gpgsManager;

    public int score { get; set; }


    void Start()
    {
        timer = 0;
        score = 0;
        isScored = false;
        scoreText = GetComponent<TextMeshProUGUI>();
        scoreText.text = score.ToString();
        highScore = PlayerPrefs.GetInt("highScore");
        highScoreTextPausePanel.text = highScore.ToString();
    }

    public void Scored()
    {
        score++;
        scoreText.text = score.ToString();
        gpgsManager.DoIncrementalAchievement(GPGSIds.achievement_beginner_wizard);
        gpgsManager.DoIncrementalAchievement(GPGSIds.achievement_expert_wizard);
        gpgsManager.DoIncrementalAchievement(GPGSIds.achievement_master_wizard);
        gpgsManager.DoIncrementalAchievement(GPGSIds.achievement_legend_wizard);

        if (score >= 10)
        {
            gpgsManager.DoGrantAchivement(GPGSIds.achievement_noob);
        }

        if (score >= 25)
        {
            gpgsManager.DoGrantAchivement(GPGSIds.achievement_untrained);
        }

        if (score >= 50)
        {
            gpgsManager.DoGrantAchivement(GPGSIds.achievement_learner);
        }

        if (score >= 100)
        {
            gpgsManager.DoGrantAchivement(GPGSIds.achievement_experienced);
        }

        if (score >= 150)
        {
            gpgsManager.DoGrantAchivement(GPGSIds.achievement_skilled);
        }

        if (score >= 200)
        {
            gpgsManager.DoGrantAchivement(GPGSIds.achievement_professional);
        }

        if (score >= 1000)
        {
            gpgsManager.DoGrantAchivement(GPGSIds.achievement_godlike);
        }
        isScored =true;
    }

    public void GameOver()
    {
        panelScore.text = scoreText.text;
        

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
            gpgsManager.AddToLeaderboard(score);
        }
        panelHighScore.text = highScore.ToString();
        highScoreTextPausePanel.text = highScore.ToString();

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
