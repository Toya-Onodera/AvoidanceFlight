using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class CameraPosition : MonoBehaviour
{
    public GameObject Camera;
    public Camera _Camera;
    public static bool Start_flg;
    public object MINVisivleList;
    public object MAXVisivleList;
    public virtual void Awake()
    {
        this.Camera = GameObject.Find("Main Camera");
        this._Camera = this.Camera.GetComponent<Camera>();
        // カメラに写っている座標のリスト
        this.MINVisivleList = this.GetScreenTopLeft();
        this.MAXVisivleList = this.GetScreenBottomRight();
        CameraPosition.Start_flg = false;
        this.StartCoroutine(this.StartGo());
    }

    // 画面の左上を取得
    public virtual Vector2 GetScreenTopLeft()
    {
        Vector2 topLeft = this._Camera.ScreenToWorldPoint(Vector2.zero);
        topLeft.Scale(new Vector2(1f, -1f));
        return topLeft;
    }

    // 画面の右下を取得
    public virtual Vector2 GetScreenBottomRight()
    {
        Vector2 bottomRight = this._Camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        bottomRight.Scale(new Vector2(1f, -1f));
        return bottomRight;
    }

    // 3秒後にゲームスタート
    public virtual IEnumerator StartGo()
    {
        yield return new WaitForSeconds(3);
        CameraPosition.Start_flg = true;
    }

}