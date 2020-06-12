using System;
using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class Buttons : MonoBehaviour
{
    private AudioSource _chooseSe;
    private AudioSource _decisionSe;
    
    public virtual void Start()
    {
        var cam = GameObject.Find("Main Camera");
        var audioSources = cam.GetComponents<AudioSource>();
        this._chooseSe = audioSources[0];
        this._decisionSe = audioSources[1];
    }

    // hoverされたとき
    public virtual void OnPointerEnter()
    {
        this._chooseSe.PlayOneShot(this._chooseSe.clip);
    }

    // クリックされたとき
    public virtual void ClickSound()
    {
        this._decisionSe.PlayOneShot(this._decisionSe.clip);
        switch (this.gameObject.name)
        {
            case "StartButton":
            case "RetryButton":
                this.StartCoroutine(this.Try());
                break;
            
            case "DescriptionButton":
                this.StartCoroutine(this.Description());
                break;
            
            case "CreditButton":
                this.StartCoroutine(this.Credit());
                break;
            
            case "TitleButton":
                this.StartCoroutine(this.Title());
                break;
        }
    }

    public virtual IEnumerator Description()
    {
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("description");
    }

    public virtual IEnumerator Credit()
    {
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("credit");
    }

    public virtual IEnumerator Try()
    {
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }

    public virtual IEnumerator Title()
    {
        yield return new WaitForSeconds(0.5f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("title");
    }

}