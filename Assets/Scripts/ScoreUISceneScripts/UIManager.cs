using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    void Start()
    {
    }
    void Update()
    {
        //デバック用
        if (Input.GetKeyDown(KeyCode.P))
        {
            //全データの削除
            PlayerPrefs.DeleteAll();
        }
    }
    //ゲームをもう一度プレイするボタン
    public void RetryButton()
    {
        GameManager.resetCount = 0; //リセットカウントをリセット
        SceneManager.LoadScene("Game1");
    }
    //タイトルに戻るボタン
    public void TitleButton() => SceneManager.LoadScene("Title");
}
