using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    GameManager gameManager; //ゲームマネージャーのインスタンス
    Rigidbody rb;
    Coroutine moveCoroutine; //こるーちんを代入する変数


    [Header("プレイヤーの情報")]
    public float moveSpeed = 5f; // 移動速度
    public float StunTime = 0f; //スタンする時間

    public List<Vector3> blockCenters = new List<Vector3>(); // ブロックの中心位置を保存するリスト
    private bool isDragging = false; // ドラッグ中かどうか

    float currentStanTime = 0f; // スタン時間計測に扱う変数

    private Vector3 lastPosition; // 直前に追加した地点
    private bool crushWall; //壁にぶつかったかどうか

    [Header("触らない")]
    public bool selectPlayer = false; // 選択されたかどうか
    public PlayerMode playerMode; //プレイヤーの状態を管理するスクリプトの型
    public PlayerMode.PlayerState playerState; //現在のプレイヤーの状態を格納する変数
    PlayerAudio playerAudio; //音を管理するスクリプト型
    RouteVisual routeVisual; //ルートを描画するスクリプト型
    public bool isRouteSet;
    [Header("プレイヤーIDを設定(0and1)")]
    [SerializeField] private int playerID; //プレイヤーIDを設定(インスペクター上)

    void Awake()
    {
        Debug.Log($"{gameObject.name}のID: {playerID}");
        gameObject.name = "Player_" + playerID;
    }

    public int GetPlayerID() => playerID; //プレイヤーIDを取得する関数

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = FindObjectOfType<GameManager>(); //GameMangaerスクリプトを取得
        rb.interpolation = RigidbodyInterpolation.Interpolate; //Rigidbodyの補間をインターポレート（補間）に設定
        playerMode = GetComponent<PlayerMode>();
        playerState = PlayerMode.PlayerState.Idle; //アイドル状態で初期化   
        playerAudio = GetComponentInChildren<PlayerAudio>();
        routeVisual = FindObjectOfType<RouteVisual>();
    }

    void Update()
    {
        //Idle状態の場合のみ操作を許可(停止時のみ)
        if (selectPlayer && !gameManager.timeOver) //rb.velocity == Vector3.zero(移動している時は動かないようにしたい時)
        {
            if (Input.GetMouseButtonDown(0)) // 右クリックでドラッグ開始
            {
                isDragging = true;
                crushWall = false;
                Debug.Log("クリック");
            }
            if (Input.GetMouseButton(0) && isDragging) // 右クリック中
            {
                SaveBlockCenterPosition();
                Debug.Log("クリック中");
            }
            if (Input.GetMouseButtonUp(0)) // 右クリックを離すと移動開始
            {
                Debug.Log("クリック終了");
                isDragging = false;
                //ルート設計が終わったら
                if (blockCenters.Count > 0)
                    isRouteSet = true;
            }
        }

        // スタン時間の経過を計測
            if (playerState == PlayerMode.PlayerState.Stan)
            {
                CalclateStunTime(StunTime); //スタン時間を設定
            }
    }

    //タイルの位置を保存
    void SaveBlockCenterPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = ~(1 << LayerMask.NameToLayer("IgnoreRaycast")); //無視するraycast

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                Vector3 centerPosition = hit.collider.transform.position;
                // 既に同じ場所が選ばれているか確認
                if (blockCenters.Any(p => p == centerPosition))
                {
                    Debug.Log("重複あり");
                }
                else
                {
                    // 最初のタイルの場合、プレイヤーの真下かをチェック
                    if (blockCenters.Count == 0)
                    {
                        Vector3 playerPos = transform.position;
                        Vector3 tilePos = centerPosition;

                        float distance = Vector2.Distance(new Vector2(playerPos.x, playerPos.z), new Vector2(tilePos.x, tilePos.z));
                        if (distance > 1.5f) //0.5f
                        {
                            Debug.Log("最初のタイルはプレイヤーの真下でなければなりません");
                            return; // 追加せずに終了
                        }
                    }

                    if (blockCenters.Count == 0 || Vector3.Distance(centerPosition, lastPosition) <= 2.6f) //tileの大きさ+0.1fに設定(タイルの大きさをTile(temp)と合わせている)
                    {
                        blockCenters.Add(centerPosition);
                        lastPosition = centerPosition;

                        //playerIDごとにルートを表示
                        if (playerID == 0)
                            routeVisual.AddPoint(centerPosition);
                        else if (playerID == 1)
                            routeVisual.AddPoint2(centerPosition);

                        // Debug.Log("ブロック追加: " + centerPosition);
                    }
                }
            }

            // if (hit.collider.CompareTag("Tile"))
            // {
            //     Vector3 centerPosition = hit.collider.transform.position; // タイルの中心に修正

            //     //ルートの重複がある場合
            //     if (blockCenters.Any(p => p == centerPosition))
            //     {
            //         Debug.Log("重複あり");
            //     }
            //     //ルートの重複がない場合
            //     else
            //     {
            //         Debug.Log("重複なし");
            //         if (blockCenters.Count == 0 || Vector3.Distance(centerPosition, lastPosition) > 0.5f)
            //         {
            //             blockCenters.Add(centerPosition);
            //             lastPosition = centerPosition;

            //             //ルートを描画
            //             if (playerID == 0)
            //                 routeVisual.AddPoint(centerPosition);
            //             else if (playerID == 1)
            //                 routeVisual.AddPoint2(centerPosition);
            //         }
            //     }


            //     //一応保存
            //     // if (blockCenters.Count == 0 || Vector3.Distance(centerPosition, lastPosition) > 0.5f)
            //     // {
            //     //     blockCenters.Add(centerPosition);
            //     //     lastPosition = centerPosition;

            //     //     //ルートを描画
            //     //     if (playerID == 0)
            //     //         routeVisual.AddPoint(centerPosition);
            //     //     else if (playerID == 1)
            //     //         routeVisual.AddPoint2(centerPosition);
            //     // }
            // }

        }
    }

    // ルート順に進む
    public IEnumerator MoveAlongPositions()
    {
        if (playerState == PlayerMode.PlayerState.Move)
        {
            Debug.Log("こるーちん開始");
            for (int i = 0; i < blockCenters.Count; i++)
            {
                Vector3 targetPosition = blockCenters[i];
                targetPosition.y = transform.position.y; // Y座標を固定

                while (Vector3.Distance(transform.position, targetPosition) > 0.1f && !crushWall && playerState == PlayerMode.PlayerState.Move)
                {
                    // 移動方向と速度の計算
                    Vector3 direction = (targetPosition - transform.position).normalized;
                    rb.velocity = direction * moveSpeed; // Rigidbodyで速度を指定

                    // 方向転換を追加(動いていない時は何もしない)
                    if (direction != Vector3.zero)
                    {
                        Quaternion rotation = Quaternion.LookRotation(direction);
                        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * 10f);
                    }

                    yield return null;
                }
                rb.velocity = Vector3.zero; // 目的地に到達したら停止

                //スタン状態ではない場合はIdle状態にする(ボタンスタートのため削除)
                // if (playerState != PlayerMode.PlayerState.Stan)
                //     playerMode.ToIdleMode(ref playerState);
            }
            blockCenters.Clear();
            //スタン状態ではない場合はIdle状態にする
            if (playerState != PlayerMode.PlayerState.Stan)
                playerMode.ToIdleMode(ref playerState);
            Debug.Log("こるーちん終了");
        }
    }

    // スタン時間計測メソッド
    void CalclateStunTime(float time)
    {
        currentStanTime += Time.deltaTime;
        if (currentStanTime > time)
        {
            playerMode.ToIdleMode(ref playerState); // スタン解除
            currentStanTime = 0f; // 時間リセット
            Debug.Log("スタン解除");
        }
    }

    //ButtonManagerから操作する
    //ルートを消す関数
    public void ClearRoute()
    {
        blockCenters.Clear();
        lastPosition = Vector3.zero;
        isRouteSet = false;

        if (playerID == 0)
            routeVisual.RemovePoint();
        else if (playerID == 1)
            routeVisual.RemovePoint2();
    }
    //移動こるーちんを開始する関数
    public void StartMoving()
    {
        moveCoroutine = StartCoroutine(MoveAlongPositions());
    }
    //こるーちんを停止する関数
    public void StopMoving()
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
        //滑らないように
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }


    //衝突処理
    private void OnTriggerEnter(Collider other)
    {
        //落とし穴(仮) 透明のprefabに当たったら落ちる
        // if (other.gameObject.CompareTag("PitFall"))
        // {
        //     Debug.Log("落とし穴のトリガーが起動");
        //     rb.constraints &= ~RigidbodyConstraints.FreezePositionY; //Y軸固定をオフ
        //     playerMode.ToIdleMode(ref playerState);
        //     StopCoroutine(MoveAlongPositions());
        // }
    }

    // 衝突時の処理
    // void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         Debug.Log("プレイヤー同士が衝突");
    //         // 衝突した相手をスタンさせる
    //         Player otherPlayer = other.gameObject.GetComponent<Player>();
    //         if (otherPlayer != null && otherPlayer.playerState != PlayerMode.PlayerState.Stan) // 相手がスタンしていない場合
    //         {
    //             otherPlayer.playerMode.ToStanMode(ref otherPlayer.playerState); // 相手をスタン状態にする
    //             otherPlayer.currentStanTime = 0f; // スタン時間をリセット

    //             // 自分自身もスタン状態にする
    //             playerMode.ToStanMode(ref playerState);
    //             currentStanTime = 0f; // スタン時間リセット

    //             Debug.Log("プレイヤー同士をスタン状態にしました");

    //             // 道のりをリセット
    //             blockCenters.Clear();
    //             otherPlayer.blockCenters.Clear();
    //             lastPosition = Vector3.zero;
    //             otherPlayer.lastPosition = Vector3.zero;

    //             // 反発力を加える
    //             Vector3 directionToOtherPlayer = (transform.position - other.transform.position).normalized;

    //             // 反発力の強さを調整
    //             float reboundForce = 3f;

    //             // 反発力を両方のプレイヤーに加える
    //             rb.AddForce(directionToOtherPlayer * reboundForce, ForceMode.Impulse); // 自分に反発力を加える
    //             otherPlayer.rb.AddForce(-directionToOtherPlayer * reboundForce, ForceMode.Impulse); // 相手に反発力を加える

    //             playerAudio.OnSound(playerAudio.pickMoney); //ぶつかった時の音
    //         }
    //     }
    //     //壁にぶつかったら
    //     if (other.gameObject.CompareTag("Wall"))
    //     {
    //         rb.velocity = Vector3.zero;
    //         crushWall = true;
    //         blockCenters.Clear();
    //         Debug.Log("壁にぶつかった");
    //     }
    // }


}
