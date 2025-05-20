using UnityEngine;
using System.Collections.Generic;

public class DualAttackEnemy : MonoBehaviour
{
    // 一度非表示にされたかどうかのフラグ（繰り返し処理防止）
    private bool destroyed = false;

    // 現在この敵に触れているプレイヤーIDを記録（0 または 1）
    private HashSet<int> touchingPlayers = new HashSet<int>();

    // プレイヤーがこの敵に触れたときに呼ばれる
    void OnTriggerEnter(Collider other)
    {
        // すでに非表示状態なら処理をスキップ
        if (destroyed) return;

        // プレイヤーのスクリプトを取得
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            // プレイヤーIDを取得（0 または 1）
            int id = player.GetPlayerID();

            // 現在触れているプレイヤーとして登録
            touchingPlayers.Add(id);

            Debug.Log($"Player_{id} がDualエネミーに触れました");

            // 同時に2人が触れているかをチェック
            CheckDestroyCondition();
        }
    }

    // プレイヤーがこの敵から離れたときに呼ばれる
    void OnTriggerExit(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            int id = player.GetPlayerID();

            // 登録されていた場合、セットから削除
            if (touchingPlayers.Contains(id))
            {
                touchingPlayers.Remove(id);
                Debug.Log($"Player_{id} がDualエネミーから離れました");
            }
        }
    }

    // 2人のプレイヤーが同時に触れているかチェック
    private void CheckDestroyCondition()
    {
        // Player_0 と Player_1 の両方が接触中であれば破壊処理へ
        if (touchingPlayers.Contains(0) && touchingPlayers.Contains(1))
        {
            DestroySelf();
        }
    }

    // 敵の非表示（実質的な撃破処理）
    private void DestroySelf()
    {
        destroyed = true; // フラグを立てて二重処理を防止
        gameObject.SetActive(false); // このオブジェクトを非表示にする
        Debug.Log("Player_0 と Player_1 が同時に触れたため、Dualエネミーを非表示にしました");
    }
}
