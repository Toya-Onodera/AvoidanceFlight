using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class EnemyMoving : MonoBehaviour
{
    // 自分 (Enemy)
    // private GameObject _mine;
    private float _speed;
    private int _angle;
    private Vector2 _min;
    private Vector2 _max;

    // 進む角度 (難易度が高いときに使用する)
    private bool _difficulty;

    public virtual void Start()
    {
        var cameraObj = (CameraPosition) GameObject.Find("Main Camera").GetComponent(typeof(CameraPosition));
        this._min = (Vector2) cameraObj.minVisibleList;
        this._max = (Vector2) cameraObj.maxVisibleList;
        this._speed = ((EnemyPrefab) GameObject.Find("EnemyBox").GetComponent(typeof(EnemyPrefab))).speed;
        this._difficulty = ((EnemyPrefab) GameObject.Find("EnemyBox").GetComponent(typeof(EnemyPrefab))).difficulty;
        
        if (this._difficulty)
        {
            this._angle = Random.Range(0, 359);
            this.transform.Rotate(new Vector3(0, 0, this._angle));
        }
    }

    public virtual void Update()
    {
        // 微妙に移動
        if (this._difficulty)
        {
            this.transform.Translate(this.transform.up * 0.0003f);
        }

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
        if ((obj.gameObject.CompareTag("Player")) && Player.GameOverFlg)
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }

}