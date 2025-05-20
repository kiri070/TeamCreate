using UnityEngine;

public class P0Enemy : MonoBehaviour
{
    private bool destroyed = false; // Desyroyが作動したか

    void OnTriggerEnter(Collider other)
    {
        if (destroyed) return; // 作動したらここで終了

        Player player = other.GetComponent<Player>();
        if (player != null && player.GetPlayerID() == 0) // PlayerIDの確認
        {
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        destroyed = true;
        gameObject.SetActive(false); // 非表示
        Debug.Log($"{gameObject.name}：Player_0 に接触して非表示になりました");
    }
}
