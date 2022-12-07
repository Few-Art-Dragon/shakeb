using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SocialPlatforms;

public class PlayServices : MonoBehaviour
{

    public static PlayServices instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        /*
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();

        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.DebugLogEnabled = true;

        PlayGamesPlatform.Activate();
        */
        SingIn();
    }

    private void SingIn()
    {
        Social.localUser.Authenticate(success =>
        {

        });
    }

    public void UnlockAchievement(string id)
    {
        Social.ReportProgress(id, 100, (bool success) =>
        {

        });
    }
    public void IncrementAchievement(string id, int stepToIncrement)
    {
        /*
        PlayGamesPlatform.Instance.IncrementAchievement(id, stepToIncrement, success =>
        {

        });
        */
    }

    public void ShowAchievement()
    {
        Social.ShowAchievementsUI();
    }

    public void AddScoreToLeaderboard(string id, int score)
    {
        Social.ReportScore(score, id, success =>
        {
            
        });
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
