using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リーダーボードをUIに表示するクラス
/// ResultシーンやTitleシーンで使用可能
/// </summary>
public class LeaderboardUI : MonoBehaviour
{
    [Header("UI References")]
    public Transform rankingContainer; // ランキングアイテムを配置する親オブジェクト
    public GameObject rankingItemPrefab; // ランキングアイテムのPrefab
    public Text loadingText; // ローディング表示用
    public Button refreshButton; // 更新ボタン

    [Header("Settings")]
    public int maxEntries = 10; // 表示する最大件数

    void Start()
    {
        if (refreshButton != null)
        {
            refreshButton.onClick.AddListener(() => LoadLeaderboard());
        }

        LoadLeaderboard();
    }

    /// <summary>
    /// リーダーボードを読み込んで表示
    /// </summary>
    public async void LoadLeaderboard()
    {
        if (loadingText != null)
        {
            loadingText.text = "Loading...";
            loadingText.gameObject.SetActive(true);
        }

        if (refreshButton != null)
        {
            refreshButton.interactable = false;
        }

        // 既存のアイテムを削除
        ClearRankingItems();

        try
        {
            // ランキングデータを取得
            var entries = await UGSLeaderboardManager.Instance.GetScores(maxEntries);

            if (entries != null && entries.Length > 0)
            {
                // ランキングアイテムを生成
                foreach (var entry in entries)
                {
                    CreateRankingItem(entry);
                }

                if (loadingText != null)
                {
                    loadingText.gameObject.SetActive(false);
                }
            }
            else
            {
                if (loadingText != null)
                {
                    loadingText.text = "No ranking data available";
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load leaderboard: {e.Message}");
            if (loadingText != null)
            {
                loadingText.text = "Failed to load ranking";
            }
        }

        if (refreshButton != null)
        {
            refreshButton.interactable = true;
        }
    }

    /// <summary>
    /// 既存のランキングアイテムをすべて削除
    /// </summary>
    void ClearRankingItems()
    {
        if (rankingContainer == null) return;

        foreach (Transform child in rankingContainer)
        {
            Destroy(child.gameObject);
        }
    }

    /// <summary>
    /// ランキングアイテムを生成
    /// </summary>
    void CreateRankingItem(LeaderboardEntry entry)
    {
        if (rankingContainer == null || rankingItemPrefab == null) return;

        GameObject item = Instantiate(rankingItemPrefab, rankingContainer);

        // Prefab内のTextコンポーネントを探して設定
        Text[] texts = item.GetComponentsInChildren<Text>();

        // テキストの順番: Rank, Name, Score の想定
        if (texts.Length >= 3)
        {
            texts[0].text = entry.Rank.ToString();
            texts[1].text = entry.PlayerName;
            texts[2].text = entry.Score.ToString();
        }
        else
        {
            // 互換性のため、1つのTextコンポーネントにまとめて表示
            if (texts.Length > 0)
            {
                texts[0].text = $"{entry.Rank}. {entry.PlayerName} - {entry.Score}";
            }
        }
    }
}
