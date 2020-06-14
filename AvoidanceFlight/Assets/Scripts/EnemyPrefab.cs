using UnityEngine;
using System.Collections;

[System.Serializable]
public partial class EnemyPrefab : MonoBehaviour
{
    // PrefabのEnemyをUnity側で設定する
    public GameObject enemyPrefab;
    public GameObject enemyBox;
    
    // Enemyの出現時間を設定
    public float times;
    public Vector2 min;
    public Vector2 max;
    
    // Enemyのスピード
    public float speed;
    
    // 一度に敵を生成する数
    public int emenyNum;
    
    // 難易度
    public bool difficulty;
    
    public virtual void Awake()
    {
        this.times = 2.3f;
        this.speed = 2.2f;
        this.emenyNum = 3;
        this.difficulty = false;
    }

    public virtual void Start()
    {
        CameraPosition cameraObj = (CameraPosition) GameObject.Find("Main Camera").GetComponent(typeof(CameraPosition));
        this.min = (Vector2) cameraObj.minVisibleList;
        this.max = (Vector2) cameraObj.maxVisibleList;
        this.StartCoroutine(this.EnemyGeneration());
        this.StartCoroutine(this.EnemyIncrease());
    }

    // Enemyを一定時間ごとに発生させる
    public virtual IEnumerator EnemyGeneration()
    {
        yield return new WaitForSeconds(3);
        
        while (true)
        {
            int i = 0;
            while (i < this.emenyNum)
            {
                Vector2 position = new Vector2(Random.Range(this.max.x + 0.5f, this.max.x + 3), Random.Range(this.min.y + 0.35f, this.max.y - 0.35f));
                GameObject enemy = UnityEngine.Object.Instantiate(this.enemyPrefab, position, this.transform.rotation);
                enemy.transform.parent = this.enemyBox.transform;
                i++;
            }
            
            yield return new WaitForSeconds(this.times);
        }
    }

    // Enemyの数を一定時間で増やす
    public virtual IEnumerator EnemyIncrease()
    {
        while (true)
        {
            if (this.emenyNum < 10)
            {
                this.emenyNum++;
            }
            
            else
            {
                this.difficulty = true;
                break;
            }
            
            yield return new WaitForSeconds(7);
        }
    }

}