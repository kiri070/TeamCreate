using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalManager : MonoBehaviour
{
    public static bool isGoalZero = false; //ゴール0が触れられているか
    public static bool isGoalOne = false; //ゴール1が触れられているか


    // Start is called before the first frame update
    void Start()
    {
        //リセット時の初期化
        isGoalZero = false;
        isGoalOne = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGoalZero)
        {
            Debug.Log("プレイヤー0、検知");
        }
        if (isGoalOne)
        {
            Debug.Log("プレイヤー1、検知");
        }
        if (isGoalZero && isGoalOne == true)
        {
            SceneManager.LoadScene("ClearScene"); //クリア画面に変遷
            Debug.Log("G A M E C L E A R");
        }
    }

    /*
    別スクリプトへ分割移行:GoalZero,GoalOne
    念の為残す
    void OnTriggerEnter(Collider other)
    {
        string objectName = gameObject.name;
        if (objectName == "Player_0" && other.gameObject.name == "Goal0")
        {
            isGoalOne = true;
            Debug.Log("Player_0 ゴールしたよーーーーーー");
        }
        if (objectName == "Player_1" && other.gameObject.name == "Goal1")
        {
            isGoalTwo = true;
            Debug.Log("Player_1 ゴールしたよーーーーーー");
        }
    }

    void OnTriggerExit(Collider other)
    {
        string objectName = gameObject.name;
        if (objectName == "Player_0" && other.gameObject.name == "Goal0")
        {
            isGoalOne = false;
            Debug.Log("Player_0 ゴールからはなれたよーーーーーー");
        }
        if (objectName == "Player_1" && other.gameObject.name == "Goal1")
        {
            isGoalTwo = false;
            Debug.Log("Player_1 ゴールからはなれたよーーーーー");
        }
    }
    */
}
