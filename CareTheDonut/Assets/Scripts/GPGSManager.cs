using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using TMPro;


public class GPGSManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI debugText;

    private void Awake()
    {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    void Start()
    {
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            //Continue with PlayGames Services

            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            debugText.text = "Login Successed " + name;
        }
        else
        {
            debugText.text = "Login Not Successed";
            //Disable your integration with Play Games Services or show a login button
            // to ask user to sign-in. clicking it should call
            //PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

    public void AddToLeaderboard(int score)
    {
        debugText.text = "score basariyla eklenmeye calisir";
        // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
        Social.ReportScore(score, "CgkI2qaGpcwREAIQAQ", (bool success) => {
            // handle success or failure
            if (success)
            {
                debugText.text = "score basariyla eklendi";
            }
            else
            {
                debugText.text = "score eklenemedi";

            }

        });
    }

    public void ShowLeaderBoardBtn()
    {
        debugText.text = "LEADERBORD1";
        Social.ShowLeaderboardUI();
        debugText.text = "LEADERBORD2";
    }

}
