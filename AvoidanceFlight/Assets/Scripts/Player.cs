using System;
using UnityEngine.EventSystems;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public partial class Player : MonoBehaviour
{
    public static bool GameOverFlg;
    private int _mode;
    private Vector2 _min;
    private Vector2 _max;
    public Rigidbody2D _force;
    public GameObject _gameOverText;
    public GameObject _bom;
    public GameObject _pointCanvas;
    public GameObject _point;
    public GameObject[] _buttons;
    private AudioSource _mainTheme;
    private AudioSource _getSe;
    private AudioSource _bombSe;
    private AudioSource _spaceshipSe;
    private Renderer _renderer;
    private EventSystem _eventSystem;

    public virtual void Awake()
    {
        Player.GameOverFlg = false;
    }

    public virtual void Start()
    {
        _eventSystem = GameObject.FindObjectOfType<EventSystem>();
        _renderer = this.GetComponent<Renderer>();
        
        var cameraObj = (CameraPosition) GameObject.Find("Main Camera").GetComponent(typeof(CameraPosition));
        this._min = (Vector2) cameraObj.minVisibleList;
        this._max = (Vector2) cameraObj.maxVisibleList;
        this._gameOverText.GetComponent<UnityEngine.UI.Text>().enabled = false;
        this._buttons = GameObject.FindGameObjectsWithTag("Buttons");
        this.ButtonSetVisible(false);
        
        var audioSources = this.GetComponents<AudioSource>();
        this._mainTheme = audioSources[0];
        this._getSe = audioSources[1];
        this._bombSe = audioSources[2];

        this._mode = 0;
    }

    public virtual void Update()
    {
        this.MyBehavior();
    }

    // Player のX移動速度を二段階に設定
    public virtual float SpeedChanger(float speed)
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            speed = 0.08f;
        }
        
        return speed;
    }

    public virtual void MyBehavior()
    {
        var playerSpeed = 0.04f;
        Vector3 mPos;
        
        // 最初に Player をスタート位置へ移動させる
        if (this._mode == 0)
        {
            if (this.transform.position.x <= -8f)
            {
                var mTra = this.transform;
                mPos = mTra.position;
                mPos.x += 0.02f;
                mTra.position = mPos;
            }
            
            if (CameraPosition.StartFlg)
            {
                this._mode += 1;
                this._force.WakeUp();
                this._mainTheme.Play();
            }

            else
            {
                this._force.Sleep();
            }
            
        }
        
        // Player をキーボード入力で移動 (横)、左クリックで上昇
        else
        {
            
            if (Input.GetMouseButton(0) || (Input.touchCount > 0))
            {
                this._force.AddForce(this.transform.up * 1.5f);
            }
            
            if (Input.GetKey(KeyCode.RightArrow))
            {
                mPos = this.transform.position;
                mPos.x += this.SpeedChanger(playerSpeed);
                this.transform.position = mPos;
            }
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                mPos = this.transform.position;
                mPos.x -= this.SpeedChanger(playerSpeed);
                this.transform.position = mPos;
            }
            
            mPos = this.transform.position;
            mPos.x = Mathf.Clamp(mPos.x, this._min.x, this._max.x - 1.05f);;
            this.transform.position = mPos;
        }
    }

    // 衝突判定
    public virtual void OnTriggerEnter2D(Collider2D obj)
    {
        if (Player.GameOverFlg == false)
        {
            // 上か下の壁、もしくは Enemy に衝突した場合 ゲームオーバー
            if (obj.gameObject.CompareTag("Enemy") || obj.gameObject.CompareTag("Wall"))
            {
                this.StartCoroutine(this.GameOverMode());
            }
            
            // アイテム取得
            if (obj.gameObject.CompareTag("Item"))
            {
                this.StartCoroutine(this.GetItem());
            }
        }
    }

    public virtual IEnumerator GameOverMode()
    {
        Player.GameOverFlg = true;
        _renderer.enabled = false;

        var mTra = this.transform;
        var mPos = mTra.position;
        var position = new Vector2(mPos.x + 0.5f, mPos.y - 0.25f);
        var createBom = UnityEngine.Object.Instantiate(this._bom, position, mTra.rotation);
        
        this._force.Sleep();
        this._mainTheme.Stop();
        this._bombSe.PlayOneShot(this._bombSe.clip);
        yield return new WaitForSeconds(0.35f);
        
        UnityEngine.Object.Destroy(createBom.gameObject);
        yield return new WaitForSeconds(2.65f);
        
        this._gameOverText.GetComponent<Text>().enabled = true;
        this.ButtonSetVisible(true);
        UnityEngine.Object.Destroy(this.gameObject);
    }

    // アイテムを取得したときに +150 と表示
    public virtual IEnumerator GetItem()
    {
        this._getSe.PlayOneShot(this._getSe.clip);
        
        var mPos = this.transform.position;
        var position = new Vector2(mPos.x + 0.45f, mPos.y);
        
        var plusPoint = UnityEngine.Object.Instantiate(this._point, Camera.main.WorldToScreenPoint(position), this.transform.rotation);
        plusPoint.transform.SetParent(this._pointCanvas.transform);
        yield return new WaitForSeconds(0.8f);
        
        UnityEngine.Object.Destroy(plusPoint.gameObject);
    }

    // リトライ & タイトルへ のボタン表示切り替え
    public virtual void ButtonSetVisible(bool set)
    {
        var setAlpha = set ? 1 : 0;
        var i = 0;

        while (i < this._buttons.Length)
        {
            var button = this._buttons[i].GetComponent<UnityEngine.UI.Button>();
            var text = this._buttons[i].transform.Find("Text");
            var canvasRenderer1 = this._buttons[i].GetComponent<CanvasRenderer>();
            var canvasRenderer2 = text.GetComponent<CanvasRenderer>();
            
            button.enabled = set;
            canvasRenderer1.SetAlpha(setAlpha);
            canvasRenderer2.SetAlpha(setAlpha);
            i++;
        }
        
        // hoverの判定を切り替える
        _eventSystem.enabled = set;
    }

}