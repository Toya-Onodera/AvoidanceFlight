using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public partial class CountDown : MonoBehaviour
{
    private int _countTime;
    private Text _targetText;
    
    public virtual void Start()
    {
        this._targetText = this.GetComponent<Text>();
        this._countTime = 3;
        this.StartCoroutine(this.CountDownStart());
    }

    public virtual IEnumerator CountDownStart()
    {
        while (true)
        {
            _targetText.text = ("カイシマデ アト " + this._countTime.ToString()) + "ビョウ";
            this._countTime--;

            yield return new WaitForSeconds(1);
            
            if (this._countTime == 0)
            {
                UnityEngine.Object.Destroy(this.gameObject);
            }
        }
    }

}