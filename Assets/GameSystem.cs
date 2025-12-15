using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSystem : MonoBehaviour
{
    // スタートボタン押下時に実行される
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }
}
