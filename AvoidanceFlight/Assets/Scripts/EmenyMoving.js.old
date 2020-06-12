#pragma strict

// 自分 (Enemy)
var Mine : GameObject;
var Speed : float;
var Difficulty : boolean;

// 進む角度 (難易度が高いときに使用する)
var Angle : int;

var Min : Vector2;
var Max : Vector2;

function Start () {
	var CameraObj = GameObject.Find("Main Camera").GetComponent(CameraPosition);

	Min = CameraObj.MINVisivleList;
	Max = CameraObj.MAXVisivleList;

	Speed = GameObject.Find("EnemyBox").GetComponent(EnemyPrefab).Speed;
	Difficulty = GameObject.Find("EnemyBox").GetComponent(EnemyPrefab).Difficulty;

	if (Difficulty === true) {
		Angle = Random.Range(0, 359);
		transform.Rotate(new Vector3(0, 0, Angle));
	}
}

function Update () {
	// 微妙に移動
	if (Difficulty === true) {
		transform.Translate(transform.up * 0.0003f);
	}

	Mine.transform.position.x -= Speed * Time.deltaTime;

	// 可視領域から消えた場合、要素を削除
	if ((transform.position.x < (Min.x - 0.5))) {
		Destroy(this.gameObject);
	}
}

function OnTriggerEnter2D (Obj : Collider2D) {
	if (Obj.gameObject.tag == "Player" && Player.GameOver_flg) {
		Destroy(this.gameObject);
	}
}