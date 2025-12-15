# Unity Gaming Services (UGS) Leaderboard セットアップ手順

このゲームは Unity Gaming Services のLeaderboard機能を使用してオンラインランキングを実装しています。

## 前提条件
- Unity 2020.3 以降
- Unity アカウント（無料）
- インターネット接続

## セットアップ手順

### 1. Unity Dashboard でプロジェクトを作成

1. [Unity Dashboard](https://dashboard.unity3d.com/) にアクセス
2. 「Create Project」をクリック
3. プロジェクト名を入力（例: "42_Game"）
4. Organization を選択
5. プロジェクトIDをコピーして保存

### 2. Unity Editor でプロジェクトをリンク

1. Unity Editor を開く
2. `Edit` → `Project Settings` → `Services`
3. 「Create Unity Project ID」または既存のプロジェクトを選択
4. Organization を選択してリンク

### 3. UGS パッケージのインストール

1. `Window` → `Package Manager` を開く
2. 左上のドロップダウンから「Unity Registry」を選択
3. 以下のパッケージをインストール:
   - **Authentication** (com.unity.services.authentication)
   - **Leaderboards** (com.unity.services.leaderboards)
   - **Core** (com.unity.services.core) - 自動でインストールされる

### 4. Leaderboard の作成

1. [Unity Dashboard](https://dashboard.unity3d.com/) に戻る
2. プロジェクトを選択
3. 左メニューから「Leaderboards」を選択
4. 「Create Leaderboard」をクリック
5. 設定:
   - **Leaderboard ID**: `42_game_highscore`
   - **Name**: "High Score Ranking"
   - **Sort Order**: Descending (降順 - 高い方が上位)
   - **Update Type**: Keep Best (ベストスコアを保持)
6. 「Create」をクリック

### 5. コードの設定

プロジェクト内の `UGSLeaderboardManager.cs` を確認し、必要に応じて設定を調整してください。

```csharp
// Leaderboard ID（Dashboard で作成したもの）
private const string LEADERBOARD_ID = "42_game_highscore";
```

## 使用方法

### 初期化
ゲーム開始時に自動的に初期化されます（匿名認証）。

### スコアの送信
```csharp
await UGSLeaderboardManager.Instance.SubmitScore(score);
```

### ランキングの取得
```csharp
var entries = await UGSLeaderboardManager.Instance.GetScores(limit: 10);
```

## 料金について

Unity Gaming Services の無料枠:
- 月間アクティブユーザー数（MAU）: 100万まで無料
- Leaderboard エントリ: 無制限
- API リクエスト: 無制限

個人開発や小規模ゲームであれば、無料枠で十分です。

## トラブルシューティング

### 認証エラー
- Unity Dashboard でプロジェクトが正しくリンクされているか確認
- インターネット接続を確認

### Leaderboard が表示されない
- Dashboard で Leaderboard ID が正しく作成されているか確認
- コード内の LEADERBOARD_ID が一致しているか確認

### ビルドエラー
- 必要なパッケージがすべてインストールされているか確認
- `Packages/manifest.json` を確認

## 参考リンク

- [Unity Gaming Services 公式ドキュメント](https://docs.unity.com/ugs/)
- [Leaderboards サービス](https://docs.unity.com/ugs/manual/leaderboards/manual/unity-leaderboards-service)
- [Unity Dashboard](https://dashboard.unity3d.com/)
