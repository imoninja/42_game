# NCMBからUGSへの移行ノート

## 変更概要

Nifty Cloud Mobile Backend (NCMB) サービス終了に伴い、Unity Gaming Services (UGS) Leaderboardsに移行しました。

## 削除/無効化されたファイル

以下のNCMB関連ファイルは残っていますが、現在使用されていません：

### NCMB SDK
- `Assets/NCMB/` - NCMB SDK全体
- `Assets/Plugins/Android/` - Android用NCMBプラグイン
- `Assets/Plugins/iOS/` - iOS用NCMBプラグイン（一部）

### NCMB依存シーン
- `Assets/Scenes/Member/Loginsignin.cs` - ログイン機能（未使用）
- `Assets/Scenes/Member/Logout.cs` - ログアウト機能（未使用）
- `Assets/Scenes/Member/getCurrentUser.cs` - ユーザー取得（未使用）

### Naichilab ランキングライブラリ
- `Assets/naichilab/unity-simple-ranking/` - NCMB用ランキングラッパー（未使用）

## 削除してよいファイル（任意）

プロジェクトサイズを削減したい場合、以下を削除できます：

1. **NCMBフォルダ全体**
   ```
   Assets/NCMB/
   ```

2. **Naichilabランキング**
   ```
   Assets/naichilab/
   ```

3. **NCMB関連シーン**
   ```
   Assets/Scenes/Member/Loginsignin.cs
   Assets/Scenes/Member/Logout.cs
   Assets/Scenes/Member/getCurrentUser.cs
   Assets/Scenes/LoginSignin.unity
   Assets/Scenes/LogOut.unity
   ```

4. **Android/iOSプラグイン（NCMBのみ使用）**
   - Firebase等を使う予定がなければ削除可能
   ```
   Assets/Plugins/Android/AndroidManifest.xml (NCMB設定部分)
   Assets/Plugins/iOS/NCMBAppControllerPushAdditions.mm
   Assets/Plugins/iOS/NCMBRichPushView.*
   Assets/Plugins/iOS/NCMBCloseImageView.*
   Assets/Plugins/iOS/NCMBAppleAuth.*
   ```

## 新しい実装

### 追加されたファイル

1. **UGSLeaderboardManager.cs**
   - Unity Gaming Services との連携
   - スコア送信・取得機能
   - シングルトン実装

2. **LeaderboardUI.cs**
   - リーダーボードUI表示
   - 更新ボタン対応

3. **UGS_SETUP.md**
   - Unity Gaming Services のセットアップ手順

### 変更されたファイル

1. **ResultManager.cs**
   - NCMBランキング呼び出しを削除
   - UGS Leaderboard呼び出しに変更
   - `async/await`パターンを使用

## ゲームフロー変更

### 以前（NCMB）
```
ゲーム終了 → Result画面 → NCMBランキング表示（naichilab）
```

### 現在（UGS）
```
ゲーム終了 → Result画面 → UGSにスコア送信
             ↓
        LeaderboardUI でランキング表示（任意）
```

## 必要な追加作業

### 1. Unity Gaming Services のセットアップ
`UGS_SETUP.md` を参照してください。

### 2. パッケージのインストール
Package Manager から以下をインストール：
- com.unity.services.core
- com.unity.services.authentication
- com.unity.services.leaderboards

### 3. コンパイルシンボルの追加（任意）
UGSパッケージがインストールされている場合のみコンパイルするため、
`UGSLeaderboardManager.cs` では `#if UNITY_SERVICES_ENABLED` を使用しています。

Player Settings → Other Settings → Scripting Define Symbols に追加：
```
UNITY_SERVICES_ENABLED
```

パッケージがない場合はエラーを避けるため、このシンボルを設定しないでください。

### 4. UIの調整
ResultシーンにStatusText（任意）を追加すると、送信結果を表示できます。

## 注意事項

### 匿名認証
- UGSは匿名認証を使用しています
- デバイスを変更するとプレイヤーIDも変わります
- 本格的なゲームでは、Email/SNS認証の実装を推奨

### オフライン対応
- UGSはオンライン接続が必要です
- オフライン時はスコア送信に失敗しますが、ゲームは続行できます
- 必要に応じてローカルハイスコア機能を追加してください

### 既存のNCMBデータ
- 過去のNCMBランキングデータは移行されません
- 新規にUGSでランキングを開始します

## トラブルシューティング

### コンパイルエラーが出る
→ UGSパッケージをインストールするか、`UNITY_SERVICES_ENABLED` シンボルを削除

### スコアが送信されない
→ `UGS_SETUP.md` を確認して、プロジェクトIDとLeaderboard IDが正しいか確認

### プレイヤー名が "Player_xxxxx" になる
→ 仕様です。プレイヤー名設定機能を実装する場合は、`UGSLeaderboardManager.GetPlayerName()` を拡張してください

## 今後の拡張案

1. **プレイヤー名設定機能**
   - PlayerPrefs に名前を保存
   - Cloud Save を使った同期

2. **フレンドランキング**
   - ソーシャル機能の追加
   - フレンドとの順位比較

3. **複数のリーダーボード**
   - 週間ランキング
   - 月間ランキング
   - 総合ランキング

4. **報酬システム**
   - ランキング上位者への報酬
   - デイリーチャレンジ
