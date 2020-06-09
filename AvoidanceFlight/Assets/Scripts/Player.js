#pragma strict
import UnityEngine.EventSystems;

var EVENT = new Array ("First", "Second");
var Min : Vector2;
var Max : Vector2;
var Force : Rigidbody2D;
var GameOverText : GameObject;
var Bom : GameObject;
var Canvas : GameObject;
var Point : GameObject;
var Buttons : GameObject[];

static var GameOver_flg : boolean;

var MainTheme : AudioSource;
var GetSE : AudioSource;
var BombSE : AudioSource;
var SpaceshipSE : AudioSource;

function Awake () {
	GameOver_flg = false;
}

function Start () {
	var CameraObj = GameObject.Find("Main Camera").GetComponent(CameraPosition);

	Min = CameraObj.MINVisivleList;
	Max = CameraObj.MAXVisivleList;

	Force = GetComponent.<Rigidbody2D>();

	GameOverText = GameObject.Find("GameOver");
	GameOverText.GetComponent.<Text>().enabled = false;

	Buttons = GameObject.FindGameObjectsWithTag("Buttons");
	ButtonSetVisible(false);	

	var AudioSources = GetComponents.<AudioSource>();
	MainTheme = AudioSources[0];
	GetSE = AudioSources[1];
	BombSE = AudioSources[2];
}

function Update () {
	MYBehavior(EVENT[0]);
}

// Player のX移動速度を二段階に設定
function SpeedChanger (speed : float) {
	if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) { speed = 0.08; }
	return speed;
}

function MYBehavior (event) {
	var PlayerSpeed : float = 0.04;

	// 最初に Player をスタート位置へ移動させる
	if (event === "First") {
		if (transform.position.x <= -8.0) {
			transform.position.x += 0.02;
			Force.Sleep();
		} else {
			if (CameraPosition.Start_flg) {
				EVENT.Shift();
				Force.WakeUp();
				MainTheme.Play();
			}
		}
	}

	// Player をキーボード入力で移動 (横)、左クリックで上昇
	else if (event === "Second") {
    	if( Input.GetMouseButton(0) || Input.touchCount > 0 ) {
    		Force.AddForce(transform.up * 1.5f);
    	}

    	if(Input.GetKey( KeyCode.RightArrow )) {
    		transform.position.x += SpeedChanger(PlayerSpeed);
    	}

    	else if(Input.GetKey( KeyCode.LeftArrow )) {
    		transform.position.x -= SpeedChanger(PlayerSpeed);
    	}

		// 移動領域を制限
		transform.position.x = Mathf.Clamp(transform.position.x, Min.x, (Max.x - 1.05));
	}
}

// 衝突判定
function OnTriggerEnter2D (Obj : Collider2D) {
	if (GameOver_flg == false) {
		// 上か下の壁、もしくはEnemyに衝突した場合 ゲームオーバー
		if (Obj.gameObject.tag == "Enemy" || Obj.gameObject.tag == "Wall") {
			GameOverMode();
		}

		// アイテム取得
		if (Obj.gameObject.tag == "Item") {
			GetItem();
		}
	}
}

function GameOverMode () {
	GameOver_flg = true;
	GetComponent.<Renderer>().enabled = false;
	var position = new Vector2((transform.position.x + 0.5), (transform.position.y - 0.25));
	var CreateBom = Instantiate (Bom, position, transform.rotation);
	Force.Sleep();
	MainTheme.Stop();
	BombSE.PlayOneShot(BombSE.clip);

	yield new WaitForSeconds (0.35);
	Destroy(CreateBom.gameObject);
	
	yield new WaitForSeconds (2.65);
	
	GameOverText.GetComponent.<Text>().enabled = true;
	ButtonSetVisible(true);

	Destroy(this.gameObject);
}

// アイテムを取得したときに +150 と表示
function GetItem () {
	GetSE.PlayOneShot(GetSE.clip);

	var position = new Vector2((transform.position.x + 0.45), transform.position.y);
	var PlusPoint = Instantiate (Point, Camera.main.WorldToScreenPoint(position), transform.rotation);
	PlusPoint.transform.SetParent(Canvas.transform);

	yield new WaitForSeconds (0.8);
	Destroy(PlusPoint.gameObject);
}

// リトライ & タイトルへ のボタン表示切り替え
function ButtonSetVisible (Set : boolean) {
	var SetAlpha : int = Set ? 1 : 0;

	for (var i = 0; i < Buttons.Length; i++) {
		Buttons[i].GetComponent.<Button>().enabled = Set;
		Buttons[i].GetComponent.<CanvasRenderer>().SetAlpha(SetAlpha);
		Buttons[i].transform.FindChild("Text").GetComponent.<CanvasRenderer>().SetAlpha(SetAlpha);
	}

	// hoverの判定を切り替える
	var eventSystem = GameObject.FindObjectOfType.<EventSystem>();
	eventSystem.enabled = Set;
}