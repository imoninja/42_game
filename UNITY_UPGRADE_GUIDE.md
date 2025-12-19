# Unity 2022.3.21f1 LTS アップグレードガイド

このプロジェクトは Unity 2020.3.17f1 から Unity 2022.3.21f1 LTS にアップグレードされました。

## アップグレード完了内容

### ✅ 実施済みの作業

1. **Unity バージョンアップグレード**
   - Unity 2020.3.17f1 → Unity 2022.3.21f1 LTS

2. **2D パッケージの更新**
   - 2D Animation: 5.0.6 → 9.0.x
   - 2D PSD Importer: 4.1.0 → 8.0.x
   - 2D Common: 自動更新

3. **Unity Gaming Services (UGS) の統合**
   - Authentication パッケージ: インストール済み
   - Leaderboards パッケージ: インストール済み
   - Services Core: 自動インストール済み

4. **プロジェクト設定**
   - Scripting Define Symbol に `UNITY_SERVICES_ENABLED` を追加
   - すべてのプラットフォーム（Standalone, iOS, Android, WebGL）に適用済み

5. **動作確認**
   - タイトル画面: ✅ 正常動作
   - ゲームプレイ: ✅ 正常動作
   - リザルト画面: ✅ 正常動作
   - オンラインランキング送信: ✅ 正常動作
   - Unity Dashboard でスコア確認: ✅ 確認済み

## Unity Gaming Services セットアップ状況

### ✅ 完了済み

- Unity Dashboard でプロジェクト作成
- Leaderboard ID: `42_game_highscore` 作成済み
- Leaderboard 設定:
  - Sort Order: Highest to lowest（降順）
  - Update Strategy: Best score（最高スコアを保持）
  - Tiers: なし
- Unity エディタでプロジェクトIDリンク完了
- テストスコア送信成功（86400点）

## プロジェクトで使用中のバージョン

- **Unity**: 2022.3.21f1 LTS
- **パッケージ**:
  - Authentication: 3.5.2
  - Leaderboards: 2.3.3
  - Services Core: 1.x
  - 2D Animation: 9.x
  - 2D PSD Importer: 8.x

## 新規開発者向けセットアップ手順

このプロジェクトを初めて開く場合:

### 1. Unity のインストール

1. Unity Hub をインストール
2. Unity 2022.3.21f1 をインストール
3. 必要なモジュール:
   - WebGL Build Support（推奨）
   - Android Build Support（モバイル向け）
   - iOS Build Support（モバイル向け）

### 2. プロジェクトを開く

1. Unity Hub の「Projects」タブ
2. 「Add」→ プロジェクトフォルダを選択
3. Unity 2022.3.21f1 で開く
4. 初回起動時にアセットのインポート（5〜15分）

### 3. Unity Gaming Services の設定確認

**Scripting Define Symbol の確認:**
1. `Edit` → `Project Settings` → `Player` → `Other Settings`
2. `Scripting Define Symbols` に `UNITY_SERVICES_ENABLED` があることを確認

**プロジェクトIDの確認:**
1. `Edit` → `Project Settings` → `Services`
2. プロジェクトがリンク済みか確認
3. 未リンクの場合は、Unity Dashboard でプロジェクトを作成してリンク

### 4. テストプレイ

1. シーンを開く: `Assets/Scenes/` から任意のシーンを開く
2. Play ボタンを押してゲームをテスト
3. Console でエラーがないか確認（`Window` → `General` → `Console`）

## トラブルシューティング

### コンパイルエラー: "UGS not available"

**原因:** `UNITY_SERVICES_ENABLED` シンボルが設定されていない

**解決方法:**
1. Unity エディタを完全に閉じる
2. `Edit` → `Project Settings` → `Player` → `Other Settings`
3. `Scripting Define Symbols` に `UNITY_SERVICES_ENABLED` を追加
4. Enter キーを押して Apply
5. Unity エディタを再起動

### Safe Mode で起動してしまう

**原因:** 2D パッケージのバージョン不一致

**解決方法:**
1. Safe Mode ダイアログで「Ignore」をクリック
2. `Window` → `Package Manager`
3. 左上を「Packages: In Project」に変更
4. 2D Animation と 2D PSD Importer を最新版に更新
5. Unity エディタを再起動

### Library フォルダの再構築が必要な場合

**手順:**
1. Unity エディタを完全に閉じる
2. プロジェクトフォルダの `Library` フォルダを削除
3. Unity エディタで再度プロジェクトを開く
4. アセットの再インポートが自動的に始まる（5〜15分）

## Unity Gaming Services の使い方

### スコアをオンラインランキングに送信

コードは既に実装済み。ゲームをプレイして Result 画面に到達すると自動的に送信されます。

### ランキングの確認

1. [Unity Dashboard](https://dashboard.unity3d.com/) にアクセス
2. プロジェクトを選択
3. `Leaderboards` → `42 Game High Score` → `Entries` タブ
4. 送信されたスコアが表示される

### 認証について

現在は**匿名認証**を使用しています:
- プレイヤーは自動的に匿名IDで認証される
- デバイスごとに異なるIDが生成される
- アカウント登録不要

## アップグレード時に発生した問題と解決方法

### 問題1: 2D パッケージのバージョン不一致

**エラー:**
```
Library\PackageCache\com.unity.2d.psdimporter@4.1.0\Editor\PSDImporterEditor.cs(19,62): error CS0535
```

**解決方法:**
- Package Manager で 2D Animation と 2D PSD Importer を Unity 2022.3 互換バージョンに更新

### 問題2: Library フォルダのロック

**エラー:**
```
rm: cannot remove 'Library/ArtifactDB': Device or resource busy
```

**解決方法:**
- Unity エディタを完全に閉じてから Library フォルダを削除

### 問題3: UNITY_SERVICES_ENABLED が認識されない

**エラー:**
```
UGS not available. Score not submitted.
```

**解決方法:**
- ProjectSettings.asset を手動編集して Scripting Define Symbol を追加
- Unity エディタを再起動

## 検証チェックリスト

アップグレード後の確認事項:
- [x] プロジェクトがエラーなく開く
- [x] Console にコンパイルエラーがない
- [x] ゲームが正常にプレイできる
- [x] タイトル画面が表示される
- [x] ゲームプレイが正常に動作
- [x] スコア計算が正常
- [x] リザルト画面が表示される
- [x] UGS が初期化される
- [x] 匿名認証が成功する
- [x] スコアがオンラインランキングに送信される
- [x] Unity Dashboard でスコアを確認できる

## 参考ファイル

- **UGS セットアップガイド**: [UGS_SETUP.md](UGS_SETUP.md)
- **マイグレーションノート**: [MIGRATION_NOTES.md](MIGRATION_NOTES.md)
- **UGS マネージャー**: [Assets/Script/UGSLeaderboardManager.cs](Assets/Script/UGSLeaderboardManager.cs)
- **リザルトマネージャー**: [Assets/Script/ResultManager.cs](Assets/Script/ResultManager.cs)

## 今後の推奨事項

1. **ビルドテスト**
   - WebGL ビルドの動作確認
   - モバイル（Android/iOS）ビルドの動作確認

2. **UI の改善（任意）**
   - LeaderboardUI.cs を使ってゲーム内でランキング表示
   - ランキング表示画面をシーンに追加

3. **将来のアップグレード**
   - Unity 2022.3 LTS のサポート期限: 2025年6月
   - 2025年以降に Unity 6 LTS への移行を検討

## 参考リンク

- [Unity 2022.3 LTS リリースノート](https://unity.com/releases/editor/whats-new/2022.3.0)
- [Unity アップグレードガイド](https://docs.unity3d.com/Manual/UpgradeGuides.html)
- [Unity Gaming Services ドキュメント](https://docs.unity.com/ugs/)
- [Unity Leaderboards API](https://docs.unity.com/ugs/manual/leaderboards/manual/overview)
- [Unity Authentication API](https://docs.unity.com/ugs/manual/authentication/manual/intro-unity-authentication)
