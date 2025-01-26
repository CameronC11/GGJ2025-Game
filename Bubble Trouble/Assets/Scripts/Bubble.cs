using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour
{

    /***************************\
    -----------Variables---------
    \***************************/

    //declare variables
    public int color = -1;
    public CircleCollider2D bubbleCollider;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private List<GameObject> enemies;

    //declare other objects
    [SerializeField]
    private GameObject enemySpawner;

    /***************************\
    -----------Methods-----------
    \***************************/

    //START METHOD
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemySpawner = GameObject.Find("EnemySpawner");
        enemies = enemySpawner.GetComponent<EnemySpawner>().enemies;

        setColor();
    }

    //UPDATE METHOD
    void Update()
    {
        moveToEnemy();
    }

    //Move to enemy
    void moveToEnemy()
    {
        GameObject closest = closestEnemy();
        if(closest != null && closest.GetComponent<Enemy>().color == color)
        {
            //move towards the closest enemy
            transform.position = Vector2.MoveTowards(transform.position, closest.transform.position, 0.025f);
            
            // Check if we've collided with the enemy
            float distance = Vector2.Distance(transform.position, closest.transform.position);
            if (distance < 0.1f)  // Small threshold for collision
            {
                AudioManager.Instance.PlayPopAndLaugh();
                Destroy(gameObject);
            }
        }
        else
        {
            transform.position += new Vector3(0.0f, 0.1f, 0.0f) * Time.deltaTime;
        }
    }


    //Find the closest enemy
    public GameObject closestEnemy()
    {

        if (enemies.Count <= 0) { return null; }

        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        //find the closest enemy
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestEnemy = enemy;
                closestDistance = distance;
            }
        }

        return closestEnemy;
    }

    //Set the color of the object
    private void setColor()
    {
        switch (color)
        {
            case 0://yellow
                spriteRenderer.color = new Color(1, 0.92f, 0.016f);
                break;
            case 1://red
                spriteRenderer.color = new Color(1.0f, 0.0f, 0.0f);
                break;
            case 2://blue
                spriteRenderer.color = new Color(0.0f, 0.0f, 1.0f);
                break;
            case 3://purple
                spriteRenderer.color = new Color(1.0f, 0.0f, 1.0f);
                break;
            default:
                spriteRenderer.color = new Color(1.0f, 1.0f, 1.0f);
                break;
        }
    }

}
