using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [Tooltip("このスイッチで制御するエネミーたち（EnemyToggle を持っている必要があります）")]
    public EnemyToggle[] controlledEnemies; // このスイッチで制御対象となる敵のリスト

    // プレイヤーがスイッチに触れたときに呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        // Player スクリプトを持っているか確認
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            int id = player.GetPlayerID(); // プレイヤーID（0 または 1）を取得

            if (id == 0 || id == 1)
            {
                Debug.Log($"Player_{id} がスイッチを踏みました");

                // 制御対象のすべての敵に対して表示/非表示をトグル
                foreach (EnemyToggle enemy in controlledEnemies)
                {
                    if (enemy != null)
                    {
                        enemy.Toggle();
                    }
                }
            }
        }
    }
}

