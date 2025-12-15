using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject ScoreObject = null; //オブジェクトのテキスト取得用 初期値はnull

    // Start is called before the first frame update
    //初期化
    void Start()
    {

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
        //オブジェクトからtxtコンポーネントを取得
        Text ScoreText = ScoreObject.GetComponent<Text>();
        // テキストの表示入換
        //int型をstring型へ
        string ScoreCharacter = PointGetManager.TotalScore.ToString();

        ScoreText.text = ScoreCharacter;

    }
}
