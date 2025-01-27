using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemySpawner : MonoBehaviour
{

    /***************************\
    -----------Variables---------
    \***************************/

    //init cooldown start time
    private float cooldownTime;//reduce as time goes on

    //declare enemy prefab
    public GameObject enemy;

    //declare variables
    [SerializeField]
    private float cooldown;
    public List<GameObject> enemies;

    //declare other objects
    public GameObject playerObj;
    private Player player;

    /***************************\
    -----------Methods-----------
    \***************************/

    void Start()
    {
        //init variables
        cooldownTime = 4.0f;
        cooldown = cooldownTime;
        enemies = new List<GameObject>();
        //init other
        player = playerObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        //set the cooldown and spawn the enemies
        setCooldownTime();
        spawnEnemy();
    }

    private void spawnEnemy()
    {
        if(cooldown < 0)
        {
            GameObject newEnemy = Instantiate(enemy, transform.position, transform.rotation);
            setEnemySpeed(newEnemy);
            enemies.Add(newEnemy);
            cooldown = cooldownTime;

        }
        cooldown -= Time.deltaTime;
    }

    //Sets the cooldown time
    private void setCooldownTime()
    {
        cooldownTime = 4f;
        if (player.score > 10000)
        {
            cooldownTime = 1f;
        }
        else if (player.score > 5000)
        {
            cooldownTime = 1.5f;
        }
        else if (player.score > 3000)
        {
            cooldownTime = 1.75f;
        }
         else if (player.score > 2000)
        {
            cooldownTime = 2f;
        }
    
        else if(player.score > 1000)
        {
            cooldownTime = 3f;
        }
        
         
        
    }

    //Sets enemy speed
    private void setEnemySpeed(GameObject enemyObj)
    {
        Enemy enemy = enemyObj.GetComponent<Enemy>();

        enemy.speed = 10.25f;

        if (player.score > 1000)
        {
            enemy.speed = 0.50f;
        }
        if (player.score > 2000)
        {
            enemy.speed = 0.75f;
        }
        if (player.score > 3000)
        {
            enemy.speed = 1f;
        }
        if (player.score > 5000)
        {
            enemy.speed = 2f;
        }
    }

}
