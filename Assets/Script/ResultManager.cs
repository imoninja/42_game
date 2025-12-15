using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // <--忘れがち
using UnityEngine.SceneManagement; //トップに戻るボタンとリトライボタンのシーン遷移に使うよ。

public class ResultManager : MonoBehaviour
{
    public Text ScoreText = null;
    int Score = 0;

    // Start is called before the first frame update
    void Start()
    {
        Score = PointGetManager.GetScore();
        ScoreText.text = Score.ToString();
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(Score);
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
