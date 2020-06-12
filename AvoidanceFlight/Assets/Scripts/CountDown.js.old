#pragma strict

var CountTime : int;

function Start () {
	CountTime = 3;
	CountDown();
}

function CountDown () {
	while (true) {
		GetComponent.<Text>().text = "カイシマデ アト " + CountTime.ToString() + "ビョウ";
		CountTime--;

		yield new WaitForSeconds(1);

		if (CountTime == 0) {
			Destroy(this.gameObject);
		}
	}
}