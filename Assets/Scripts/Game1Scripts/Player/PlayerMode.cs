using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMode : MonoBehaviour
{
    public enum PlayerState
    {
        Idle = 1,
        Move = 2,
        Stan = 3,
    }

    //アイドル状態に変更
    public PlayerState ToIdleMode(ref PlayerState moveMode)
    {
        moveMode = PlayerState.Idle;
        return moveMode;
    }
    //動く状態に変更
    public PlayerState ToMoveMode(ref PlayerState moveMode)
    {
        moveMode = PlayerState.Move;
        return moveMode;
    }
    //スタン状態に変更
    public PlayerState ToStanMode(ref PlayerState moveMode)
    {
        moveMode = PlayerState.Stan;
        return moveMode;
    }
}
