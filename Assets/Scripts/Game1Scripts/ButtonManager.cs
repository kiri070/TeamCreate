using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class ButtonManager : MonoBehaviour
{
    Player player1;
    Player player2;

    float player1MoveSpeed; //プレイヤー1のスピードを保存する変数
    float player2MoveSpeed; //プレイヤー2のスピードを保存する変数
    public bool pause; //一時停止しているかどうか
    RouteVisual routeVisual; //RouteVisualのインスタンス

    [Header("プレイヤーを格納")]
    public GameObject player1_Obj;
    public GameObject player2_Obj;

    [Header("UI関連")]
    public Button startButton;
    PlayerMode playerMode;
    PlayerMode.PlayerState playerState;
    public GameObject pauseButton; //一時停止ボタン


    [Header("スタートボタンの不透明度")]
    public float startButtonAlpha; //スタートボタンの透明度
    Image buttonImage; //スタートボタンのImage型
    Color color; //スタートボタンのImageカラー型

    void Start()
    {
        player1 = player1_Obj.GetComponent<Player>(); //プレイヤー1スクリプトを取得
        player2 = player2_Obj.GetComponent<Player>(); //プレイヤー2スクリプトを取得
        playerMode = GetComponent<PlayerMode>(); //PlayerModeスクリプトを取得

        buttonImage = startButton.GetComponent<Image>(); //スタートボタンのイメージを取得
        //不透明度を設定
        color = buttonImage.color;
        color.a = startButtonAlpha;
        buttonImage.color = color;

        routeVisual = FindObjectOfType<RouteVisual>();
    }

    void Update()
    {
        //ルート設計が終わったらスタートボタンを表示
        if (player1.isRouteSet == true && player2.isRouteSet == true)
        {
            //透明度を消す
            startButtonAlpha = 1f;
            color.a = startButtonAlpha;
            buttonImage.color = color;
            Debug.Log("ルート設計完了");
        }
    }

    //スタートボタン
    public void OnStartButton()
    {
        player1.playerMode.ToMoveMode(ref player1.playerState);
        player2.playerMode.ToMoveMode(ref player2.playerState);

        player1.isRouteSet = false;
        player2.isRouteSet = false;

        player1.StartMoving();
        player2.StartMoving();
    }

    //リセットボタン
    public void ResetButton()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        PlayerPrefs.SetFloat("TimeLimit", gameManager.timeLimit); //タイムリミットを保存
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        GameManager.resetCount++; //リセット回数
    }

    //一時停止するボタン
    public void PauseButton()
    {
        // プレイヤーの移動停止処理
        player1.StopMoving();
        player2.StopMoving();

        // 移動速度保存して止める（今後のResumeで使う）
        player1MoveSpeed = player1.moveSpeed;
        player2MoveSpeed = player2.moveSpeed;
        player1.moveSpeed = 0f;
        player2.moveSpeed = 0f;

        pause = true;

        // ルート削除（中断後の再設計のため）
        player1.ClearRoute();
        player2.ClearRoute();

        // ポーズ中なら自動で再開処理を先に行う
        if (pause)
        {
            Resume();  // 再開処理を先に実行
        }
    }

    //再開する関数
    public void Resume()
    {
        // 速度を元に戻す
        player1.moveSpeed = player1MoveSpeed;
        player2.moveSpeed = player2MoveSpeed;

        pause = false;
    }

    //スタートボタン
    // public void OnStartButton()
    // {
    //     player1.playerMode.ToMoveMode(ref player1.playerState);
    //     player2.playerMode.ToMoveMode(ref player2.playerState);

    //     player1.isRouteSet = false;
    //     player2.isRouteSet = false;
    //     Debug.Log("player1=" + player1.isRouteSet);
    //     Debug.Log("player2=" + player2.isRouteSet);

    //     StartCoroutine(player1.MoveAlongPositions()); //プレイヤー1の移動コルーチン開始
    //     StartCoroutine(player2.MoveAlongPositions()); //プレイヤー2の移動コルーチン開始
    // }

    //再開ボタン
    // public void ResumeButton()
    // {
    //     //速度を戻す
    //     player1.moveSpeed = player1MoveSpeed;
    //     player2.moveSpeed = player2MoveSpeed;

    //     //ポーズボタンを表示・再開ボタンを非表示
    //     pauseButton.SetActive(true);
    //     resumeButton.SetActive(false);

    //     pause = false;
    // }


    //一時停止ボタン
    // public void PauseButton()
    // {
    //     //プレイヤー1,2の速度を保存
    //     player1MoveSpeed = player1.moveSpeed;
    //     player2MoveSpeed = player2.moveSpeed;

    //     player1.moveSpeed = 0;
    //     player2.moveSpeed = 0;

    //     //ポーズボタンを非表示・再開ボタン表示
    //     pauseButton.SetActive(false);
    //     resumeButton.SetActive(true);

    //     pause = true;

    //     //ルートを削除
    //     StopCoroutine(player1.MoveAlongPositions());
    //     StopCoroutine(player2.MoveAlongPositions());

    //     player1.blockCenters.Clear();
    //     player2.blockCenters.Clear();

    //     //ルートの描画を削除
    //     routeVisual.RemovePoint();
    //     routeVisual.RemovePoint2();
    // }

}
