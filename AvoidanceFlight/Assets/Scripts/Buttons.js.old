#pragma strict
import UnityEngine.SceneManagement;

var ChooseSE : AudioSource;
var DecisionSE : AudioSource;

function Start () {
	var Camera = GameObject.Find("Main Camera");
	var AudioSources = Camera.GetComponents.<AudioSource>();
	ChooseSE = AudioSources[0];
	DecisionSE = AudioSources[1];
}

// hoverされたとき
function OnPointerEnter () {
	ChooseSE.PlayOneShot(ChooseSE.clip);
}

// クリックされたとき
function ClickSound () {
	DecisionSE.PlayOneShot(DecisionSE.clip);

	switch (gameObject.name) {
		case "StartButton" : 
		case "RetryButton" :
			Try();
			break;
		case "DescriptionButton" :
			Description();
			break;
		case "CreditButton" :
			Credit();
			break;
		case "TitleButton" :
			Title();
			break;
	};
}

function Description () {
	yield new WaitForSeconds(0.5f);
	SceneManager.LoadScene("description");
}

function Credit () {
	yield new WaitForSeconds(0.5f);
	SceneManager.LoadScene("credit");
}

function Try () {
	yield new WaitForSeconds(0.5f);
	SceneManager.LoadScene("main");
}

function Title () {
	yield new WaitForSeconds(0.5f);
	SceneManager.LoadScene("title");
}