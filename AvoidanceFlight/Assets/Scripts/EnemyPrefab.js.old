#pragma strict

// PrefabのEnemyをUnity側で設定する
var EnemyPrefab : GameObject;
var EnemyBox : GameObject;

// Enemyの出現時間を設定
var Times : float;

var Min : Vector2;
var Max : Vector2;

// Enemyのスピード
var Speed : float;
// 一度に敵を生成する数
var EmenyNum : int;
// 難易度
var Difficulty : boolean;

function Awake () {
	Times = 2.3f;
	Speed = 2.2;
	EmenyNum = 3;
	Difficulty = false;
}

function Start () {
	var CameraObj = GameObject.Find("Main Camera").GetComponent(CameraPosition);

	Min = CameraObj.MINVisivleList;
	Max = CameraObj.MAXVisivleList;


	EnemyGeneration();
	EmenyIncrease();
}

// Enemyを一定時間ごとに発生させる
function EnemyGeneration () {
	yield new WaitForSeconds(3);

	while (true) {
		for (var i = 0; i < EmenyNum; i++) {
			var Position : Vector2 = new Vector2 (Random.Range((Max.x + 0.5), (Max.x + 3)), Random.Range((Min.y + 0.35), (Max.y - 0.35)));
			var Enemy : GameObject = Instantiate (EnemyPrefab, Position, transform.rotation);
			Enemy.transform.parent = EnemyBox.transform;
		}

		yield new WaitForSeconds (Times);
	}
}

// Enemyの数を一定時間で増やす
function EmenyIncrease () {
	while (true) {
		if (EmenyNum < 10) {
			EmenyNum++;
		} else {
			Difficulty = true;
			break;
		}

		yield new WaitForSeconds (7);
	}
}