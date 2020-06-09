#pragma strict

var GameOverTheme : AudioSource;
var SoundOnce : boolean;

function Start () {
	var AudioSources = GetComponents.<AudioSource>();
	GameOverTheme = AudioSources[0];
	SoundOnce = true;
}

function Update () {
	if (GetComponent.<Text>().enabled == true && SoundOnce) {
		GameOverTheme.Play();
		SoundOnce = false;
	}
}