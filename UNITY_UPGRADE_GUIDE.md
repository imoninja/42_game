# Unity バージョンアップグレードガイド

## 現在のバージョン
- Unity 2020.3.17f1 (LTS)

## 推奨アップグレード先

### オプション1: Unity 2021 LTS（推奨）
- **バージョン**: Unity 2021.3.x (最新のLTS)
- **リリース**: 2022年4月
- **サポート終了**: 2024年5月まで

**メリット:**
- 安定したLTSバージョン
- 2020からの移行が比較的スムーズ
- UGS統合が改善されている
- パフォーマンス向上

**互換性:**
- ほぼすべてのアセットが動作する
- TextMeshProが標準パッケージに
- 大きな破壊的変更は少ない

### オプション2: Unity 2022 LTS
- **バージョン**: Unity 2022.3.x (現在のLTS)
- **リリース**: 2023年6月
- **サポート終了**: 2025年6月まで

**メリット:**
- 最新のLTS版
- 長期サポート
- 最新機能が使える
- UGSネイティブサポート

**注意点:**
- アセットの互換性確認が必要
- 一部APIが変更されている可能性

### オプション3: Unity 6 (旧2023 LTS)
- **バージョン**: Unity 6.0.x
- **リリース**: 2024年10月
- **サポート終了**: 2026年まで

**メリット:**
- 最新版
- Unity 6へのリブランディング
- 最高のパフォーマンス

**注意点:**
- 新しすぎて安定性が未知数
- アセットの互換性問題の可能性

## 推奨: Unity 2022.3.x LTS（現在の最新LTS）

理由:
1. ✅ 最新のLTSバージョン（長期サポート）
2. ✅ 2020からの移行も安定している
3. ✅ UGSがネイティブサポート
4. ✅ パフォーマンスが大幅向上
5. ✅ サポート期限が最も長い（2025年まで）

## アップグレード手順

### 1. バックアップを作成（重要！）

#### Gitでコミット
```bash
git add .
git commit -m "Backup before Unity upgrade to 2021 LTS"
git push
```

#### プロジェクトフォルダをコピー（推奨）
```
C:\Unity_Project\AtA_after → C:\Unity_Project\AtA_after_backup
```

### 2. Unity 2022.3.21f1 を使用（既にインストール済み）

既に Unity 2022.3.21f1 がインストールされているので、新規インストールは不要です。

確認方法:
1. Unity Hub を起動
2. 「Installs」タブで Unity 2022.3.21f1 が表示されているか確認
5. モジュールを選択:
   - ✅ Visual Studio (または既存のIDEを使用)
   - ✅ Android Build Support (モバイルビルド用)
     - Android SDK & NDK Tools
     - OpenJDK
   - ✅ iOS Build Support (iOSビルド用)
   - ✅ WebGL Build Support
   - ✅ Documentation (オフラインドキュメント)
6. 「Install」をクリック

### 3. プロジェクトを Unity 2022.3.21f1 で開く

1. Unity Hub の「Projects」タブ
2. プロジェクトの右側の「...」メニュー
3. 「Open with」→ **Unity 2022.3.21f1** を選択
4. 初回起動時に警告が出ます:
   ```
   "This project was created with an older version of Unity..."
   ```
   → 「Continue」をクリック

### 4. アップグレードプロセス

Unity が自動的に以下を実行します:
- プロジェクト設定の更新
- アセットの再インポート（時間がかかります）
- スクリプトの再コンパイル

**注意**: 初回起動は5〜15分かかる場合があります。

### 5. エラーチェック

Unity Editorが開いたら:

#### Consoleを確認
- `Window` → `General` → `Console`
- エラー（赤）や警告（黄）を確認

#### 主な互換性問題と修正

##### 問題1: TextMeshPro パッケージエラー
```
TextMeshPro is not installed
```
**解決策:**
- `Window` → `Package Manager`
- 「TextMeshPro」を検索してインストール

##### 問題2: 非推奨API警告
```
'GUIText' is obsolete
```
**解決策:**
- このプロジェクトでは使用していないので無視してOK

##### 問題3: アセンブリ定義エラー
**解決策:**
- `Assets` → `Reimport All`

### 6. プロジェクトバージョンファイルの確認

以下のファイルが自動更新されます:
```
ProjectSettings/ProjectVersion.txt
```

内容確認:
```
m_EditorVersion: 2021.3.xx
m_EditorVersionWithRevision: 2021.3.xx (ハッシュ)
```

### 7. テストプレイ

1. Mainシーンを開く
2. Playボタンを押してゲームをテスト
3. すべての機能が動作するか確認:
   - タイル生成
   - 選択
   - スコア計算
   - シーン遷移

### 8. ビルド設定の確認

`File` → `Build Settings`:
- Platform設定を確認
- 必要に応じてSwitch Platform

### 9. UGS パッケージのインストール

Unity 2022では Package Manager から直接インストール:
1. `Window` → `Package Manager`
2. 左上のドロップダウンで「Unity Registry」を選択
3. 以下をインストール:
   - ✅ Authentication
   - ✅ Leaderboards
   - ✅ Core (自動)

### 10. Scripting Define Symbol の追加

`Edit` → `Project Settings` → `Player` → `Other Settings`:

Scripting Define Symbols に追加:
```
UNITY_SERVICES_ENABLED
```

### 11. 変更をコミット

```bash
git add .
git commit -m "Upgrade Unity from 2020.3.17f1 to 2022.3.21f1 LTS

- Update ProjectVersion to 2022.3.21f1
- Reimport all assets
- Install UGS packages (Authentication, Leaderboards)
- Add UNITY_SERVICES_ENABLED define symbol
- Verify all game functionality

🤖 Generated with [Claude Code](https://claude.com/claude-code)

Co-Authored-By: Claude <noreply@anthropic.com>"
git push
```

## トラブルシューティング

### アップグレード後にエディタが開かない
1. プロジェクトフォルダの `Library` フォルダを削除
2. 再度プロジェクトを開く（再インポートが始まる）

### コンパイルエラーが大量に出る
1. `Assets` → `Reimport All`
2. `Edit` → `Preferences` → `External Tools` でIDEを再設定

### シーンが真っ黒
1. `Window` → `Rendering` → `Lighting`
2. 「Generate Lighting」をクリック

### アセットが表示されない
1. Package Managerですべてのパッケージを最新に更新
2. エディタを再起動

## 検証チェックリスト

アップグレード後に確認:
- [ ] プロジェクトがエラーなく開く
- [ ] Consoleにエラーがない（警告は許容）
- [ ] Mainシーンが正常にロード
- [ ] ゲームが正常にプレイできる
- [ ] タイル生成が正常
- [ ] スコア計算が正常
- [ ] シーン遷移が正常
- [ ] UGSパッケージがインストール済み
- [ ] ビルドが成功する

## 元に戻す方法（問題が発生した場合）

### 方法1: Gitから復元
```bash
git reset --hard HEAD~1
```

### 方法2: バックアップから復元
バックアップフォルダをコピーして元に戻す

### 方法3: Unity 2020.3を再インストール
Unity Hub から Unity 2020.3.17f1 を再インストール

## 参考リンク

- [Unity 2021.3 LTS リリースノート](https://unity.com/releases/editor/whats-new/2021.3.0)
- [Unity アップグレードガイド](https://docs.unity3d.com/Manual/UpgradeGuides.html)
- [Unity Gaming Services ドキュメント](https://docs.unity.com/ugs/)

## 推奨事項

1. **Unity 2022.3.21f1 LTS を使用**（既にインストール済み）
2. 安定動作を確認
3. 将来的に Unity 6 LTS への移行を検討（2025年以降）
