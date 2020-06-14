using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class ItemPrefab : MonoBehaviour
{
    // PrefabのItemをUnity側で設定する
    public GameObject itemPrefab;
    public GameObject itemBox;

    // Itemの出現時間を設定
    public float times;
    public Vector2 min;
    public Vector2 max;
    
    // Itemのスピード
    public float speed;
    
    // 難易度
    public float difficulty;
    
    public virtual void Awake()
    {
        this.times = 10f;
        this.speed = 2.2f;
        this.difficulty = 1.0005f;
    }

    public virtual void Start()
    {
        CameraPosition cameraObj = (CameraPosition) GameObject.Find("Main Camera").GetComponent(typeof(CameraPosition));
        this.min = (Vector2) cameraObj.minVisibleList;
        this.max = (Vector2) cameraObj.maxVisibleList;
        this.StartCoroutine(this.ItemGeneration());
    }

    // Itemを一定時間ごとに発生させる
    public virtual IEnumerator ItemGeneration()
    {
        yield return new WaitForSeconds(3);
        
        while (true)
        {
            int i = 0;
            while (i < Random.Range(1, 5))
            {
                Vector2 position = new Vector2(Random.Range(this.max.x + 0.5f, this.max.x + 2), Random.Range(this.min.y + 0.5f, this.max.y - 0.5f));
                GameObject item = UnityEngine.Object.Instantiate(this.itemPrefab, position, this.transform.rotation);
                item.transform.parent = this.itemBox.transform;
                i++;
            }
            
            yield return new WaitForSeconds(this.times);
        }
    }

}