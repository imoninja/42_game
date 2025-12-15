using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    public Text ScoreText = null;
    public Text StatusText = null; // ステータスメッセージ表示用（任意）
    int Score = 0;

    // Start is called before the first frame update
    async void Start()
    {
        Score = PointGetManager.GetScore();
        ScoreText.text = Score.ToString();

        // Unity Gaming Services にスコアを送信
        try
        {
            bool success = await UGSLeaderboardManager.Instance.SubmitScore(Score);

            if (success)
            {
                Debug.Log("スコアをオンラインランキングに送信しました");
                if (StatusText != null)
                {
                    StatusText.text = "Score submitted to online ranking!";
                }
            }
            else
            {
                Debug.LogWarning("スコアの送信に失敗しました");
                if (StatusText != null)
                {
                    StatusText.text = "Failed to submit score";
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"スコア送信エラー: {e.Message}");
            if (StatusText != null)
            {
                StatusText.text = "Score saved locally only";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickTop()
    {
        SceneManager.LoadScene("Title");
        PointGetManager.TotalScore = 0;
    }

    public void onClickRetry()
    {
        SceneManager.LoadScene("Main");
        PointGetManager.TotalScore = 0;
    }
}
