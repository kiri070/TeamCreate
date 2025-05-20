// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyManager : MonoBehaviour
// {
//     public static bool DplayerZero = false; //DualEnemy P0
//     public static bool DplayerOne = false; //DualEnemy P1
//     public static bool playerZero = false; //P0Enemy
//     public static bool playerOne = false; //P1Enemy
//     DualAttackEnemy dualscr;
//     P0Enemy p0scr;
//     P1Enemy p1scr;
//     GameObject p1EnemyObject;
//     GameObject p0EnemyObject;
//     GameObject dualEnemyObject;
//     // Start is called before the first frame update
//     void Start()
//     {
//         //リセット時の初期化
//         DplayerZero = false;
//         DplayerOne = false;
//         playerZero = false;
//         playerOne = false;
//         p1EnemyObject = GameObject.Find("P1Enemy");//P1エネミー処理
//         p1scr = p1EnemyObject.GetComponent<P1Enemy>();
//         p0EnemyObject = GameObject.Find("P0Enemy");// P0エネミースクリプトの取得と設定
//         p0scr = p0EnemyObject.GetComponent<P0Enemy>();
//         dualEnemyObject = GameObject.Find("DualAttackEnemy"); // デュアルエネミースクリプトの取得と設定
//         dualscr = dualEnemyObject.GetComponent<DualAttackEnemy>();
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         //同時攻撃処理
//         if (DplayerZero && DplayerOne == true)
//         {
//             if (dualEnemyObject != null)
//             {
//                 dualscr.Destroy();
//             }
//             //Debug.Log("同時攻撃によりエネミー撃破");
//         }
//         //P0エネミー処理
//         if (playerZero)
//         {
//             if (p0EnemyObject != null)
//             {
//                 p0scr.Destroy();
//             }
//             //Debug.Log("P0がP0Enemy接触によりエネミー撃破");
//         }

//         if (p1EnemyObject != null)
//         {
//             if (playerOne)
//             {
//                 // P1エネミースクリプトの取得と設定
//                 p1scr.Destroy();
//                 //Debug.Log("P1がP1Enemy接触によりエネミー撃破");
//             }
//         }
//     }
// }
