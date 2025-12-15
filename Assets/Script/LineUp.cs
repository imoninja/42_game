using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI; // <--忘れがち

public class LineUp : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject TileArea;

    void Start()
    {
        TileArea = GameObject.Find("TileArea");

        for (int TileNum = 0 ; TileNum < 36 ; TileNum++ )
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
    }

    // Update is called once per frame
    void Update()
    {

    }
}