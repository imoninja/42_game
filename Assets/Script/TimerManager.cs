using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TimerManager : MonoBehaviour
{

    //タイマー用変数
    public float LimitTime = 60.0f;                   //タイマーの設定時間 60秒の初期値を入れてる
    public static bool counting;　　　　　　　　　　//タイマー稼働フラグ

    //プログレスバー用変数
    public Slider slider;                     //Sliderオブジェクト
    private float PaintSpeed = 0.0f;                 //塗りつぶし速度用変数

    [SerializeField]
    private Text _textCountdown;            //カウントダウン用テキスト
    [SerializeField]
    private Image _imageMask;               //カウントダウン時のマスク

    // Start is called before the first frame update
    void Start()
    {
        _textCountdown.text = "";
        StartCoroutine(CountdownCoroutine());       //

        PaintSpeed = LimitTime;         //塗りつぶし速度の設定

    }

    // Update is called once per frame
    void Update()
    {
        if (counting)
        {
            TimerCount();
        }
    }

    //タイマー機能
    void TimerCount()
    {
        //稼働時の経過時間
        LimitTime -= Time.deltaTime;

        ProgressMove();

        //タイマーの停止
        if (LimitTime <= 0)
        {
            counting = false;
            SceneManager.LoadScene("Result");

        }
    }

    //プログレスバー処理
    void ProgressMove()
    {
        //経過時間から移動量の計算
        float amount = Time.deltaTime / PaintSpeed;

        //塗つぶし量を代入する
        slider.value += amount;
    }

    //タイマースタート
    public void PushStart()
    {
        counting = true;
    }

    IEnumerator CountdownCoroutine()
    {
        _imageMask.gameObject.SetActive(true);
        _textCountdown.gameObject.SetActive(true);

        _textCountdown.text = "3";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "2";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "1";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "GO!";
        yield return new WaitForSeconds(1.0f);

        _textCountdown.text = "";
        _textCountdown.gameObject.SetActive(false);
        _imageMask.gameObject.SetActive(false);

        PushStart();        //タイトルでスタートボタン押されてるからね。
    }


}
