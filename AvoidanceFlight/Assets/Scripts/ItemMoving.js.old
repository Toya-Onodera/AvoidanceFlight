#pragma strict

// 自分 (Item)
var Mine : GameObject;
var Speed : float;

var Min : Vector2;
var Max : Vector2;

function Start () {
	var CameraObj = GameObject.Find("Main Camera").GetComponent(CameraPosition);

	Min = CameraObj.MINVisivleList;
	Max = CameraObj.MAXVisivleList;

	Speed = GameObject.Find("ItemBox").GetComponent(ItemPrefab).Speed;
}

function Update () {
	Mine.transform.position.x -= Speed * Time.deltaTime;

	// 可視領域から消えた場合、要素を削除
	if ((transform.position.x < (Min.x - 0.5))) {
		Destroy(this.gameObject);
	}
}

function OnTriggerEnter2D (Obj : Collider2D) {
	if (Obj.gameObject.tag == "Player" && !Player.GameOver_flg) {
		Score.Score += 150;
		Destroy(this.gameObject);
	}
}