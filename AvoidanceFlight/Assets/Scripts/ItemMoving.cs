using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ItemMoving : MonoBehaviour
{
    // 自分 (Item)
    //private GameObject _mine;
    private float _speed;
    private Vector2 _min;
    private Vector2 _max;
    
    public virtual void Start()
    {
        var cameraObj = (CameraPosition) GameObject.Find("Main Camera").GetComponent(typeof(CameraPosition));
        this._min = (Vector2) cameraObj.minVisibleList;
        this._max = (Vector2) cameraObj.maxVisibleList;
        this._speed = ((ItemPrefab) GameObject.Find("ItemBox").GetComponent(typeof(ItemPrefab))).speed;
    }

    public virtual void Update()
    {
        var mTra = this.transform;
        var mPos = mTra.position;
        mPos.x -= (this._speed * Time.deltaTime);
        mTra.position = mPos;

        // 可視領域から消えた場合、要素を削除
        if (this.transform.position.x < (this._min.x - 0.5f))
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.gameObject.CompareTag("Player") && !Player.GameOverFlg)
        {
            Score.PlayerScore += 150;
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

}