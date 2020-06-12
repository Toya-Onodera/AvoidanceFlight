#pragma strict

var Camera : GameObject;
var _Camera : Camera;

static var Start_flg : boolean;

var MINVisivleList;
var MAXVisivleList;

function Awake () {
	Camera = GameObject.Find("Main Camera");
	_Camera = Camera.GetComponent.<Camera>();

	// カメラに写っている座標のリスト
	MINVisivleList = GetScreenTopLeft();
 	MAXVisivleList = GetScreenBottomRight();

 	Start_flg = false;
 	StartGo ();
}

// 画面の左上を取得
function GetScreenTopLeft() {
    var topLeft : Vector2 = _Camera.ScreenToWorldPoint(Vector2.zero);
    topLeft.Scale(new Vector2(1f, -1f));
    return topLeft;
}

// 画面の右下を取得
function GetScreenBottomRight() {
    var bottomRight : Vector2 = _Camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    bottomRight.Scale(new Vector2(1f, -1f));
    return bottomRight;
}

// 3秒後にゲームスタート
function StartGo () {
	yield new WaitForSeconds (3);
	Start_flg = true;
}