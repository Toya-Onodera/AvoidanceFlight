#pragma strict

// PrefabのItemをUnity側で設定する
var ItemPrefab : GameObject;
var ItemBox : GameObject;

// Itemの出現時間を設定
var Times : float;

var Min : Vector2;
var Max : Vector2;

// Itemのスピード
var Speed : float;
// 難易度
var difficulty : float;

function Awake () {
	Times = 10.0f;
	Speed = 2.2;
	difficulty = 1.0005;
}

function Start () {
	var CameraObj = GameObject.Find("Main Camera").GetComponent(CameraPosition);

	Min = CameraObj.MINVisivleList;
	Max = CameraObj.MAXVisivleList;

	ItemGeneration();
}

// Itemを一定時間ごとに発生させる
function ItemGeneration () {
	yield new WaitForSeconds(3);

	while (true) {
		for (var i = 0; i < Random.Range(1, 5); i++) {
			var Position : Vector2 = new Vector2 (Random.Range((Max.x + 0.5), (Max.x + 2)), Random.Range((Min.y + 0.5), (Max.y - 0.5)));
			var Item : GameObject = Instantiate (ItemPrefab, Position, transform.rotation);
			Item.transform.parent = ItemBox.transform;
		}

		yield new WaitForSeconds (Times);
	}
}