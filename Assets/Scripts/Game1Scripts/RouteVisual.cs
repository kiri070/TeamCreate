using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class RouteVisual : MonoBehaviour
{
    [Header("LineRendererをそれぞれ格納-プレイヤーごとに分けるため")]
    public LineRenderer lineRenderer;
    public LineRenderer lineRenderer2;

    [Header("触らない")]
    public List<Vector3> routePoints = new List<Vector3>(); //位置を保存するリスト
    public List<Vector3> routePoints2 = new List<Vector3>(); //位置を保存するリスト

    void Start()
    {
        lineRenderer.startWidth = 0.8f;
        lineRenderer2.startWidth = 0.8f;
    }

    //プレイヤーID:0の線を描画する関数
    public void AddPoint(Vector3 point)
    {
        Vector3 adjusted = point + new Vector3(0, 0.3f, 0); //y値の調整
        routePoints.Add(adjusted);
        lineRenderer.positionCount = routePoints.Count; //点の数を代入
        lineRenderer.SetPositions(routePoints.ToArray()); //点の位置をセット
    }

    //プレイヤーID:1の線を描画する関数
    public void AddPoint2(Vector3 point)
    {
        Vector3 adjusted = point + new Vector3(0, 0.3f, 0); //y値の調整
        routePoints2.Add(adjusted);
        lineRenderer2.positionCount = routePoints2.Count; //点の数を代入
        lineRenderer2.SetPositions(routePoints2.ToArray()); //点の位置をセット
    }

    //プレイヤーID:0の線を削除する関数
    public void RemovePoint()
    {
        routePoints.Clear();
        lineRenderer.positionCount = 0;
    }

    //プレイヤーID:1の線を削除する関数
    public void RemovePoint2()
    {
        routePoints2.Clear();
        lineRenderer2.positionCount = 0;
    }
}
