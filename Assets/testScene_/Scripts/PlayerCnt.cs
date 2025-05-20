using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCnt : MonoBehaviour
{
    private Vector3 lastPosition;
    private List<Vector3> blockCenters = new List<Vector3>();
    private Rigidbody rb;
    public float moveSpeed = 5f;
    private bool crushWall; //壁にぶつかったかどうか

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Y軸の動きを固定（沈まないように）
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 右クリック開始
        {
            Debug.Log("右クリック開始");
            crushWall = false;
            blockCenters.Clear(); // クリックごとにリストをリセット
        }

        if (Input.GetMouseButton(0)) // 右クリック中
        {
            SaveBlockCenterPosition();
        }

        if (Input.GetMouseButtonUp(0)) // 右クリック終了
        {
            Debug.Log("右クリック終了");
            if (blockCenters.Count > 0)
            {
                StartCoroutine(Move());
            }
        }
    }

    void SaveBlockCenterPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                Vector3 centerPosition = hit.collider.transform.position; // タイルの中心に修正

                if (blockCenters.Count == 0 || Vector3.Distance(centerPosition, lastPosition) > 0.5f)
                {
                    blockCenters.Add(centerPosition);
                    lastPosition = centerPosition;
                }
            }
        }
    }

    IEnumerator Move()
    {
        for (int i = 0; i < blockCenters.Count; i++)
        {
            Vector3 targetPosition = blockCenters[i];
            targetPosition.y = transform.position.y; // Y座標を固定

            while (Vector3.Distance(transform.position, targetPosition) > 0.1f && !crushWall)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
                yield return null;
            }

            rb.velocity = Vector3.zero; // 目的地に到達したら停止
        }
    }

    void OnCollisionEnter(Collision other)
    {
        //壁にぶつかったら
        if (other.gameObject.CompareTag("Wall"))
        {
            rb.velocity = Vector3.zero;
            crushWall = true;
            Debug.Log("壁にぶつかった");
        }
    }
}
