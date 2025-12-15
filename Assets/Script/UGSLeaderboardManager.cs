using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

#if UNITY_SERVICES_ENABLED
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
#endif

/// <summary>
/// Unity Gaming Services のLeaderboard機能を管理するクラス
/// シングルトンパターンで実装
/// </summary>
public class UGSLeaderboardManager : MonoBehaviour
{
    private static UGSLeaderboardManager _instance;
    public static UGSLeaderboardManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("UGSLeaderboardManager");
                _instance = go.AddComponent<UGSLeaderboardManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    // Leaderboard ID (Unity Dashboard で作成したもの)
    private const string LEADERBOARD_ID = "42_game_highscore";

    private bool _isInitialized = false;
    private bool _isInitializing = false;

    public bool IsInitialized => _isInitialized;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Unity Gaming Services を初期化
    /// </summary>
    public async Task<bool> Initialize()
    {
#if UNITY_SERVICES_ENABLED
        if (_isInitialized)
        {
            return true;
        }

        if (_isInitializing)
        {
            // 初期化中の場合は待機
            while (_isInitializing)
            {
                await Task.Delay(100);
            }
            return _isInitialized;
        }

        _isInitializing = true;

        try
        {
            // Unity Services の初期化
            await UnityServices.InitializeAsync();
            Debug.Log("Unity Services initialized successfully");

            // 匿名認証（ユーザー登録不要）
            if (!AuthenticationService.Instance.IsSignedIn)
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
                Debug.Log($"Signed in anonymously. Player ID: {AuthenticationService.Instance.PlayerId}");
            }

            _isInitialized = true;
            _isInitializing = false;
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to initialize Unity Services: {e.Message}");
            _isInitializing = false;
            return false;
        }
#else
        Debug.LogWarning("Unity Gaming Services packages are not installed. Please install Authentication and Leaderboards packages.");
        _isInitializing = false;
        return false;
#endif
    }

    /// <summary>
    /// スコアを送信
    /// </summary>
    public async Task<bool> SubmitScore(int score)
    {
#if UNITY_SERVICES_ENABLED
        if (!await Initialize())
        {
            Debug.LogError("Cannot submit score: UGS not initialized");
            return false;
        }

        try
        {
            await LeaderboardsService.Instance.AddPlayerScoreAsync(LEADERBOARD_ID, score);
            Debug.Log($"Score {score} submitted successfully to leaderboard {LEADERBOARD_ID}");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to submit score: {e.Message}");
            return false;
        }
#else
        Debug.LogWarning("UGS not available. Score not submitted.");
        return false;
#endif
    }

    /// <summary>
    /// トップランキングを取得
    /// </summary>
    /// <param name="limit">取得する件数</param>
    public async Task<LeaderboardEntry[]> GetScores(int limit = 10)
    {
#if UNITY_SERVICES_ENABLED
        if (!await Initialize())
        {
            Debug.LogError("Cannot get scores: UGS not initialized");
            return new LeaderboardEntry[0];
        }

        try
        {
            var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(
                LEADERBOARD_ID,
                new GetScoresOptions { Limit = limit }
            );

            List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

            foreach (var entry in scoresResponse.Results)
            {
                entries.Add(new LeaderboardEntry
                {
                    Rank = entry.Rank + 1, // 0-indexed なので +1
                    PlayerName = GetPlayerName(entry.PlayerId),
                    Score = (int)entry.Score
                });
            }

            Debug.Log($"Retrieved {entries.Count} leaderboard entries");
            return entries.ToArray();
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to get scores: {e.Message}");
            return new LeaderboardEntry[0];
        }
#else
        Debug.LogWarning("UGS not available. Returning empty leaderboard.");
        return new LeaderboardEntry[0];
#endif
    }

    /// <summary>
    /// プレイヤーの自己ベストスコアと順位を取得
    /// </summary>
    public async Task<LeaderboardEntry> GetPlayerScore()
    {
#if UNITY_SERVICES_ENABLED
        if (!await Initialize())
        {
            Debug.LogError("Cannot get player score: UGS not initialized");
            return null;
        }

        try
        {
            var playerScoreResponse = await LeaderboardsService.Instance.GetPlayerScoreAsync(LEADERBOARD_ID);

            if (playerScoreResponse != null)
            {
                return new LeaderboardEntry
                {
                    Rank = playerScoreResponse.Rank + 1,
                    PlayerName = "You",
                    Score = (int)playerScoreResponse.Score
                };
            }

            return null;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to get player score: {e.Message}");
            return null;
        }
#else
        Debug.LogWarning("UGS not available. Returning null.");
        return null;
#endif
    }

    /// <summary>
    /// プレイヤーIDから表示名を生成
    /// （実際のゲームではプレイヤー名の設定機能を実装することを推奨）
    /// </summary>
    private string GetPlayerName(string playerId)
    {
        // Player ID の最後の8文字を使用
        if (playerId.Length > 8)
        {
            return "Player_" + playerId.Substring(playerId.Length - 8);
        }
        return "Player_" + playerId;
    }
}

/// <summary>
/// リーダーボードのエントリーデータ
/// </summary>
[Serializable]
public class LeaderboardEntry
{
    public int Rank;
    public string PlayerName;
    public int Score;
}
