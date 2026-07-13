### 🎯 プロジェクト名：「GunMan」
## 🧩 基本コンセプト
1対1の対戦型TPS/FPSシューティング
TPS視点で銃を掴んで、FPS視点で狙撃
銃を抜くタイミングと回避の読みあい、精度が勝負を分ける、駆け引き系のゲーム性

# オブジェクトの配置予定
- 原点（0,0,0）
- 自キャラ(0,0,-7)
    - EyePoint
    - Gun
        - MuzzlePoint
        - Bullet
            - BulletBack
            - BulletFront
    - HandPoint
    - ArmReach
    - Shoulder
- 相手キャラ(0,0,7)
- TPSRoot(0,1.2,0) Rotation(0,-5?,0)
    - TPSAnchor(0,0,-8)
    - OpeningAnchor(0,17,0) Rotation(90,0,0)
- InputReceiver
    - InputPosition
        - TPS
            - MouseNavi
        - FPS
    - InputButton
- ScoreCanvas
    - ScoreUI
- MenuCanvas
    - MenuUI

# ラウンドの流れと操作仕様

- 常時可能操作
    - WASD（無制限）
        - 微回避
    - WASD+Space（ラウンド毎に1回のみ）
        - 回避

0. Opening(演出)

1. Duel
    1. TPS
        - 視点
            TPSAnchorから原点を見るカメラ。
            TPSRootの回転に従って円周上を周って、徐々に自キャラと相手キャラが被さっていく。
        - マウス移動
            - MouseNaviがマウスに追従。
            - MouseNaviの位置に向かって腕を伸ばし続ける。
            - MouseNaviの位置（OnGun,OnArmReach,TooFar）に応じてMouseNaviの色変更。
        - （OnGun時）右クリック押しっぱなし
            - 銃をHandPointの子にしてFPSモードに移行

    2. FPS
        - 視点
            EyePointからMuzzlePointを見るカメラ
            腕が動く→銃が動く→視点が動く
        - マウス移動
            - 操作量に応じて腕が動く
        - 右クリック離す
            - 銃を親から放してTPSモードに戻る
        - 左クリック
            - 発砲してShotFiteに移行

2. ShotFire（一瞬）
    - EyePointからMuzzlePointに向けてRaycastを飛ばして、弾が相手に当たるか確認
        - 当たる場合
            相手に当たったことを伝えて（GotShotに強制移行させて）BulletTimeに移行
        - 外れた場合
            相手に何も伝えずBulletTimeに移行

    1. BulletTiem（撃った側,弾の当たり外れに関わらず実施）※実装後回し
        - 視点
            BulletBackからBulletFrontを見るカメラ
            スロー再生（内部的にはリプレイ的な処理だけど、プレイヤー的にはリアルタイムっぽい）
        - マウス移動
            - 操作量に応じてBulletBackが動く
        - マウスホイール
            - ズーム
        弾が相手に当たったタイミングでスコア加算。
        当たる当たらないに関わらず、通り越して数秒でリプレイに移行

    2. GotShot（撃たれた側,弾を当てられた場合のみ実施）
        - 視点
            TPSモード同様
        - 操作不可
        自キャラが倒れる演出。
        相手のBulletCameraが終わったらリプレイに移行

3. リプレイ

4. Result
    ラウンド毎の両者のスコアを表示
    お互いにボタンクリックで次のラウンドに移行

# ✅やらない予定
InputSystem

# ✅ スコア・判定
- 銃弾を当てた箇所に応じて得点
    - 頭　100点
    - 顔　20点
    - 首　20点
    - 心臓　20点
    - 胴　10点
    - 腕,足　5点
- 後抜き先撃ちで5倍の得点
    - 「先に銃を抜いた」側にフラグ
    - 「銃を落とした」場合は相手が既に銃を持っていたら相手にフラグを渡す
    - フラグ持ってない側が当てたら得点5倍
- 合計スコアは常に画面左上に表示

# ✅ リプレイ対応（NEW）
プレイヤーの**入力履歴（マウス位置・ボタン）**を記録
再生時は記録された入力データを元に再現
必要に応じてキャラクターの状態同期（再生とネット対戦との整合性も意識）

# ✅ オンライン対戦
Steamworksでマッチング
過去にStart()の同期ミスで構造崩れた経験 → 早期にネットテストで構造チェック


