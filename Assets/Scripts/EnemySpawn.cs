using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public RectTransform parentCanvas;
    public int enemyNum = 5;
    public Vector2 spawnXRange = new Vector2(-200f, 200f);
    public Vector2 spawnYRange = new Vector2(-100f, 100f);
    // Start is called before the first frame update
    // public GameObject SpawnEnemy()
    // {
    //     
    // }
    public void SpawnEnemies(int count)
    {
        for (int i = 0; i < count; i++)
        {
            // spawn enemy method call
        }
    }
}
