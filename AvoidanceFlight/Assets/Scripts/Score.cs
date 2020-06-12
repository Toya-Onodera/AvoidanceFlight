using UnityEngine.UI;
using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Score : MonoBehaviour
{
    public static int PlayerScore;
    private Text _text;

    public virtual void Start()
    {
        _text = this.GetComponent<Text>();
        Score.PlayerScore = 0;
        this.StartCoroutine(this.ScoreCount());
    }

    public virtual void Update()
    {
        _text.text = "SCORE : " + Score.PlayerScore.ToString();
    }

    public virtual IEnumerator ScoreCount()
    {
        yield return new WaitForSeconds(3);
        
        while (true)
        {
            if (Player.GameOverFlg)
            {
                break;
            }
            
            Score.PlayerScore++;
            
            yield return new WaitForSeconds(0.05f);
        }
    }

}