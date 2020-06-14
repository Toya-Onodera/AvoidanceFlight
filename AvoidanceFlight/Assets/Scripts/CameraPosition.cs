using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class CameraPosition : MonoBehaviour
{
    //public GameObject Camera;
    private Camera _camera;
    public static bool StartFlg;
    public object minVisibleList;
    public object maxVisibleList;
    
    public virtual void Awake()
    {
        CameraPosition.StartFlg = false;
        this._camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        
        // カメラに写っている座標のリスト
        this.minVisibleList = this.GetScreenTopLeft();
        this.maxVisibleList = this.GetScreenBottomRight();
        this.StartCoroutine(this.StartGo());
    }

    // 画面の左上を取得
    public virtual Vector2 GetScreenTopLeft()
    {
        Vector2 topLeft = this._camera.ScreenToWorldPoint(Vector2.zero);
        topLeft.Scale(new Vector2(1f, -1f));
        return topLeft;
    }

    // 画面の右下を取得
    public virtual Vector2 GetScreenBottomRight()
    {
        Vector2 bottomRight = this._camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        bottomRight.Scale(new Vector2(1f, -1f));
        return bottomRight;
    }

    // 3秒後にゲームスタート
    public virtual IEnumerator StartGo()
    {
        yield return new WaitForSeconds(3);
        CameraPosition.StartFlg = true;
    }

}