#pragma strict
import UnityEngine.UI;

static var Score : int;

function Start () {
	Score = 0;
	ScoreCount();
}

function Update () {
	GetComponent.<Text>().text = "SCORE : " + Score.ToString();
}

function ScoreCount () {
	yield new WaitForSeconds (3);
	
	while (true) {
		if (Player.GameOver_flg) { break; }
		Score++;
		yield new WaitForSeconds (0.05);
	}
}