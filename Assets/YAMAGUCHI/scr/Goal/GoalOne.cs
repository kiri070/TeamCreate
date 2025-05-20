using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalOne : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        // プレイヤー0がゴール0に触れているかどうか
        Player player = other.GetComponent<Player>(); // Playerスクリプト格納用の変数
        if (player != null) // 接触したオブジェクトのPlayerスクリプト有無
        {
            int id = player.GetPlayerID(); // Playerスクリプト内の[playerID]を取得
            if (id == 1)
            {
                GoalManager.isGoalOne = true;
                Debug.Log("Player_1 ゴールしたよーーーーーー");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // プレイヤー0がゴール0から離れたかどうか
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            int id = player.GetPlayerID();
            if (id == 1)
            {
                GoalManager.isGoalOne = false;
                Debug.Log("Player_ 1 ゴールからはなれたよーーーーーー");
            }
        }
    }
}
