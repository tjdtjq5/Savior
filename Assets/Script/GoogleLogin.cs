using UnityEngine;
// Text UI 사용
// 구글 플레이 연동
using GooglePlayGames;
using GooglePlayGames.BasicApi;
public class GoogleLogin : MonoBehaviour
{
    bool bWait = false;
    
    void Awake()
    {
        PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    void Start()
    {
        OnLogin();
    }


    public void OnLogin()
    {
        if (!Social.localUser.authenticated)
        {
            Social.localUser.Authenticate((bool bSuccess) =>
            {
                if (bSuccess)
                {
                    Debug.Log("Success : " + Social.localUser.userName);
                }
                else
                {
                    Debug.Log("Fall");
                }
            });
        }
    }

    public void OnLogOut()
    {
        ((PlayGamesPlatform)Social.Active).SignOut();
    }

    // 리더보드에 점수등록 후 보기
    public void OnShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }

    public void SetLeaderBoard(int score)
    {
        Social.ReportScore(score, GPGSIds.leaderboard_savior, (bool bSuccess) =>
        {
            if (bSuccess)
            {
                Debug.Log("ReportLeaderBoard Success");
            }
            else
            {
                Debug.Log("ReportLeaderBoard Fall");
            }
        }
      );
    }
    
}
