using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectNum : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickAct()
    {
        if (this.gameObject.CompareTag("Tile")) // Tagと変数が同じだったら
        {
            GetComponent<Renderer>().material.color = Color.red;
            this.tag = "onClickTile";
            //このゲームオブジェクトの子オブジェクトのtextのタグを変更
        } 
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
            this.tag = "Tile";
        }

    }
}