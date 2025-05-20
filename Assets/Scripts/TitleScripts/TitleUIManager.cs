using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIManager : MonoBehaviour
{
    public GameObject nowLoadingUI; //ロード画面
    public GameObject mainUI; //メインUI
    public Text loadingText; //ロード状況

    //ゲームプレイボタン
    public void PlayButton()
    {
        //ゲームシーンに変遷
        // SceneManager.LoadScene("Game1");

        StartCoroutine(LoadScene("Game1"));
    }

    //ロード画面を表示
    IEnumerator LoadScene(string sceneName)
    {
        //UIの切り替え
        mainUI.SetActive(false);
        nowLoadingUI.SetActive(true);

        loadingText = GameObject.Find("LoadingText").GetComponent<Text>(); //ロード状況のテキストを取得
        loadingText.text = "NowLoading..." + "0%"; //ロード%を表示
        yield return new WaitForSeconds(1f);
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName); //シーンを裏で読み込み
        op.allowSceneActivation = false;

        //シーンが読み込まれるまで
        while (op.progress < 0.9f)
        {
            loadingText.text = "NowLoading..." + (op.progress * 100f).ToString() + "%"; //ロード%を表示
            yield return null; //読み込みが完了まで毎フレーム待機
        }
        loadingText.text = "NowLoading...100%";
        yield return new WaitForSeconds(0.5f);
        op.allowSceneActivation = true; //シーン変遷
    }
}
