using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; // <--忘れがち
using System.Linq; //Linq使うので



public class PointGetManager : MonoBehaviour
{
    GameObject TileArea;
    GameObject[] tagobjs; //該当タグのオブジェクトを格納する配列

    public int ScoreFormula;
    public static int TotalScore = 0;
    List<int> ScoreFormulaList = new List<int>(); //数字を並べるList型の宣言&初期化
    int next;

    // Start is called before the first frame update
    void Start()
    {
        TileArea = GameObject.Find("TileArea");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickGetPoint()
    {
        if(CalcSumSystem.NumSum == 42)
        {
            //onClickTileタグのオブジェクトを削除
            GameObject[] DelTile = GameObject.FindGameObjectsWithTag("onClickTile");

            //消した数を数えておいて消した分だけ追加するからCreateTile配列にもいれておく。
            int DelTileCount = GameObject.FindGameObjectsWithTag("onClickTile").Length;

            //消失エフェクト開始
//            GameObject DelEffectPrefab = (GameObject)Resources.Load("DelEffect");
//            GameObject Effect = (GameObject)Instantiate(DelEffectPrefab, transform.position, Quaternion.identity);
//            Effect.transform.SetParent(DelTile.transform);
//            Destroy(Effect, 0.5f);
            //消失エフェクト終了

            GetScoreByTag("onClickTile");   //計算式を表示したり正解だったら加点したりする。


            foreach (GameObject Alldestroy in DelTile)
            {

                Destroy(Alldestroy);
            }


            //削除したonClickTileタグのオブジェクトだけ新規でTileタグのオブジェクトを作成
            //CreateTile分だけループ

            for (int CreateTile = DelTileCount; CreateTile >= 1; CreateTile--)
            {
                MakeTile();
            }
        }
    }

    public void GetScoreByTag(string tagname)
    {
        tagobjs = GameObject.FindGameObjectsWithTag(tagname);

        foreach (GameObject AllSelectNum in tagobjs)
        {

            ScoreFormulaList.Add(int.Parse(AllSelectNum.transform.Find("Num_001").GetComponent<Text>().text));
        }

        ScoreFormula = ScoreFormulaList.Aggregate((now, next) => now * next);

        TotalScore += ScoreFormula;

        ScoreFormulaList.Clear();
        ScoreFormula = 0;

    }

    public void MakeTile()
    {
        // ResourcesフォルダにあるTileプレハブをGameObject型で取得
        GameObject prefab = (GameObject)Resources.Load("Tile");
        // Tileプレハブを元に、インスタンスを生成
        GameObject Tile = (GameObject)Instantiate(prefab, transform.position, Quaternion.identity);

        //出現エフェクト開始
        GameObject CreateEffectPrefab = (GameObject)Resources.Load("CreateEffect");
        GameObject Effect = (GameObject)Instantiate(CreateEffectPrefab, transform.position, Quaternion.identity);
        Effect.transform.SetParent(Tile.transform);
        Destroy(Effect, 0.5f);
        //出現エフェクト終了

        // prefabの中にあるNum_001オブジェクト（テキスト）を更新
        Tile.transform.Find("Num_001").GetComponent<Text>().text = Random.Range(1f, 42.0f).ToString("F0");

        //TileAreaオブジェクトに子オブジェクトとしてTileオブジェクトを登録
        Tile.transform.SetParent(TileArea.transform);
    }

    public static int GetScore()
    {
        return TotalScore;
    }
}
