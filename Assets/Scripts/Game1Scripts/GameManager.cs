using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float timeLimit = 0f; //残り時間
    public bool timeOver = false; //タイムリミットが残っているか

    public Text timeLimitUI;
    public static int resetCount = 0; //リセットカウント
    private float[] save_timeLimit = new float[1]; //残り時間を保存する配列

    void Start()
    {
        //リセットした際の処理(時間処理)
        if (resetCount >= 1)
        {
            timeLimit = PlayerPrefs.GetFloat("TimeLimit");
        }
        else if (resetCount == 0)
        {
            save_timeLimit[0] = timeLimit; //初期の残り時間を保存
            timeLimit = save_timeLimit[0];
        }

        //ライティングを明示的に設定
        RenderSettings.ambientLight = Color.gray;
        DynamicGI.UpdateEnvironment(); // ライティング反映

    }

    void Update()
    {
        //タイムリミットを表示
        timeLimit -= Time.deltaTime;
        timeLimitUI.text = "<color=white>" + "残り時間:" + Mathf.Max(0f, Mathf.Floor(timeLimit)).ToString() + "秒" + "</color>";
        //タイムが0秒になったら
        if (timeLimit <= 0)
        {
            timeOver = true; //タイムオーバーフラグをtrue
            SceneManager.LoadScene("GameOver"); //ゲームオーバーのシーンに変遷
        }
    }
}
