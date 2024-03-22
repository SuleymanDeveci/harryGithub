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

            //string name = PlayGamesPlatform.Instance.GetUserDisplayName();
        }
        else
        {
            //Disable your integration with Play Games Services or show a login button
            // to ask user to sign-in. clicking it should call
            //PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication).
        }
    }

    public void AddToLeaderboard(int score)
    {

        // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
        Social.ReportScore(score, "CgkI2qaGpcwREAIQAQ", (bool success) => {
            // handle success or failure
            if (success)
            {

            }
            else
            {

            }

        });
    }

    public void ShowLeaderBoardBtn()
    {
        Social.ShowLeaderboardUI();
    }

    public void ShowAchievementsUI()
    {
        Social.ShowAchievementsUI();
    }

    public void DoGrantAchivement(string _achievement)  // Tek seferde a��lan ba�ar�mlar i�in kullan�l�r. gizli ba�ar�m i�in kullan�l�rsa hem a���a ��kar�r hemde tamamlar
    {
        Social.ReportProgress(_achievement, 100.00f, (bool success) =>
        {
            if (success)
            {

            }
            else
            {

            }
        });
    }

    public void DoIncrementalAchievement(string _achievement)
    {
        PlayGamesPlatform platform = (PlayGamesPlatform)Social.Active;

        platform.IncrementAchievement(_achievement, 1, (bool success) =>
        {
            if (success)
            {

            }
            else
            {

            }


        });
    }

    



}
