using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject ScoreObject = null; //オブジェクトのテキスト取得用 初期値はnull
    Text ScoreText; //キャッシュ用

    // Start is called before the first frame update
    //初期化
    void Start()
    {
        ScoreText = ScoreObject.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //TimerManagerスクリプトの中でカウントしてる制限時間内だったらスコア加算。制限時間外だとスコア加算なし。
        if (TimerManager.counting)
        {
            Score();
        }
    }

    void Score()
    {
        // テキストの表示入換
        //int型をstring型へ
        string ScoreCharacter = PointGetManager.TotalScore.ToString();

        ScoreText.text = ScoreCharacter;

    }
}
