using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; // <--忘れがち

public class CalcSumSystem : MonoBehaviour
{
    public GameObject CalcText = null; //オブジェクトのテキスト取得用 初期値はnull
    GameObject[] tagobjs; //該当タグのオブジェクトを格納する配列
    Text CalcNumText; //Tileの中身の数字を表示する用の変数
    public static int NumSum = 0;

    float timer = 0.0f; //該当タグのオブジェクト数を数えるためタイマー。
    float interval = 0.5f; //同上。updateメソッドに入れるから負荷がかかるので回数を減らすためです。

    // Start is called before the first frame update
    void Start()
    {
        CalcNumText = CalcText.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //onClickTileタグに何も入ってなかったらデフォルトの文字が入るようにする。
        timer += Time.deltaTime;
        if (timer > interval)
        {
            Check("onClickTile");
            timer = 0;
        }

        NumSum = 0;

        //onClickTileタグに入ってたらonClickTileタグの中身を見に行く
        GetObjectByTag("onClickTile");

        CalcNumText.text = NumSum.ToString();
    }

    void GetObjectByTag(string tagname)
    {
        tagobjs = GameObject.FindGameObjectsWithTag(tagname);

        foreach (GameObject AllSelectNum in tagobjs)
        {
            NumSum += int.Parse(AllSelectNum.transform.Find("Num_001").GetComponent<Text>().text);
        }
    }

    void Check(string tagname)
    {
        tagobjs = GameObject.FindGameObjectsWithTag(tagname);
        if (tagobjs.Length == 0)
        {
            CalcNumText.text = "0";
        }
    }
}
