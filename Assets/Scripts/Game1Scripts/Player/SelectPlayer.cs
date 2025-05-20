using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSelector : MonoBehaviour
{
    private Player selectedPlayer; // 選択されたプレイヤーを管理(Player Script)
    private Color originalColor;


    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 左クリックで選択
        {
            SelectPlayer();
        }
    }

    void SelectPlayer()
    {
        // マウスのスクリーン座標をワールド座標に変換
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //Playerタグがついているオブジェクトを選択
            if (hit.collider.CompareTag("Player"))
            {
                Player newPlayer = hit.collider.GetComponent<Player>(); //選択したplayer

                if (newPlayer != null)
                {
                    // 以前のプレイヤーの選択解除
                    if (selectedPlayer != null)
                    {
                        selectedPlayer.selectPlayer = false;
                        selectedPlayer.GetComponent<Renderer>().material.color = originalColor;
                    }

                    // 新しく選択したプレイヤーを設定
                    selectedPlayer = newPlayer;
                    selectedPlayer.selectPlayer = true;

                    Renderer renderer = selectedPlayer.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        originalColor = renderer.material.color; // 元の色を保存
                        renderer.material.color = Color.blue;  // 色を変更
                    }
                }
            }
        }
    }
}