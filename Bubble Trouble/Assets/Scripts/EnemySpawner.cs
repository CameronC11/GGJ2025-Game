using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    /***************************\
    -----------Variables---------
    \***************************/

    //init cooldown start time
    private const float COOLDOWN_TIME = 4f;

    //declare enemy prefab
    public GameObject enemy;

    //declare variables
    [SerializeField]
    private float cooldown;
    public List<GameObject> enemies;

    /***************************\
    -----------Methods-----------
    \***************************/

    void Start()
    {
        //init variables
        cooldown = COOLDOWN_TIME;
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnEnemy();
    }

    private void spawnEnemy()
    {
        if(cooldown < 0)
        {
            GameObject newEnemy = Instantiate(enemy, transform.position, transform.rotation);

            enemies.Add(newEnemy);
            cooldown = COOLDOWN_TIME;

        }
        cooldown -= Time.deltaTime;
    }

}
