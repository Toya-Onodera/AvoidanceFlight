using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class GameOver : MonoBehaviour
{
    public AudioSource GameOverTheme;
    public bool SoundOnce;
    public virtual void Start()
    {
        AudioSource[] AudioSources = this.GetComponents<AudioSource>();
        this.GameOverTheme = AudioSources[0];
        this.SoundOnce = true;
    }

    public virtual void Update()
    {
        if ((this.GetComponent<UnityEngine.UI.Text>().enabled == true) && this.SoundOnce)
        {
            this.GameOverTheme.Play();
            this.SoundOnce = false;
        }
    }

}