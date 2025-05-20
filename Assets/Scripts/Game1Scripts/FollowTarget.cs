using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    Customer customer; //Customerスクリプト型
    void Start()
    {
        customer = transform.GetComponentInParent<Customer>();
    }

    //一定範囲内にいる場合は止まる
    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Player")
            customer.OnEnterFollowTarget();
    }
    //一定範囲外なら追従する
    void OnTriggerExit(Collider c)
    {
        if (c.gameObject.tag == "Player")
        {
            customer.OnExitFollowTarget(c.transform);
        }
    }
}
