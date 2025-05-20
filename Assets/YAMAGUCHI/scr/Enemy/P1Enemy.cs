using UnityEngine;

public class P1Enemy : MonoBehaviour
{
    private bool destroyed = false; //作動したかどうか

    void OnTriggerEnter(Collider other)
    {
        if (destroyed) return; //作動した場合に終了する

        Player player = other.GetComponent<Player>();
        if (player != null && player.GetPlayerID() == 1) //PlayerID確認
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        destroyed = true;
        gameObject.SetActive(false); //非表示
        Debug.Log($"{gameObject.name}：Player_1 に接触して非表示になりました");
    }
}
