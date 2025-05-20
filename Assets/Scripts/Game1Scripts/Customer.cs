using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public enum MoveMode //客の状態を管理
    {
        Idle = 1,
        Follow = 2
    }
    public float moveSpeed; //速度
    Rigidbody rb;
    Transform followTarget; //追いかけるターゲット
    MoveMode currentMoveMode; //Enum型の変数

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentMoveMode = MoveMode.Idle; //Enumの初期化
    }
    void Update()
    {
        DoAutoMoveMent();
    }
    void DoAutoMoveMent()
    {
        switch (currentMoveMode)
        {
            case MoveMode.Idle: //止まっている時
                break;
            case MoveMode.Follow: //動いている時
                if (followTarget != null)
                {
                    //LookRotationとLerpで徐々に振り向くように
                    Quaternion moveRotation = Quaternion.LookRotation(followTarget.transform.position - transform.position, Vector3.up);
                    transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);
                    rb.velocity = transform.forward * moveSpeed;
                }
                break;
        }
    }

    //追跡モードの状態を切り替える関数
    public void OnEnterFollowTarget()
    {
        followTarget = null;
        if (currentMoveMode == MoveMode.Follow)
        {
            currentMoveMode = MoveMode.Idle;
            rb.velocity = Vector3.zero; //止める
        }
    }
    public void OnExitFollowTarget(Transform target)
    {
        followTarget = target;
        if (currentMoveMode == MoveMode.Idle)
            currentMoveMode = MoveMode.Follow;
    }

    void OnTriggerEnter(Collider c)
    {
        
    }
}