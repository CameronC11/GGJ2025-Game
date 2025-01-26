using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal.Internal;
using UnityEngine.SocialPlatforms.Impl;

public class Cauldron : MonoBehaviour
{
    /***************************\
    -----------Variables---------
    \***************************/

    //init cooldown start time
    private float COOLDOWN_TIME = 1.5f;//reduce as time goes on
    public const int SCORE_VALUE = 100;

    //declare other objects
    [SerializeField]
    private Ingredient currentIngredient;
    public GameObject enemySpawnerObj;
    public GameObject playerObj;

    //declare prefabs
    public GameObject bubblePrefab;

    //declare variables
    private int color;
    public Vector2 cauldronPos;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float cooldown;
    public List<Bubble> bubbles;
    public List<GameObject> enemies;
    private Player player;
    

    /***************************\
    -----------Methods----------
    \***************************/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //init variables
        cauldronPos = transform.position;
        cooldown = COOLDOWN_TIME;
        color = -1;
        spriteRenderer = GetComponent<SpriteRenderer>();

        player = playerObj.GetComponent<Player>();
        bubbles = new List<Bubble>();
        enemies = enemySpawnerObj.GetComponent<EnemySpawner>().enemies;
    }

    // UPDATE METHOD
    void Update()
    {
        //update the color of the bubbles
        setColor();
        //does the cauldron have an ingredient in it?
        useIngredient();
        //destroy any bubbles that touch enemies
        destroyOnCollide();
    }

    //Adds an ingredient to the cauldron, kicking out any old ingredient
    public void addIngredient(Ingredient ingredient)
    {
        //if there is a current ingredient, kick it out
        if(currentIngredient != null)
        {
            currentIngredient.moveToStart();
            currentIngredient.GetComponent<SpriteRenderer>().enabled = true;
        }

        //add the new ingredient
        currentIngredient = ingredient;
        //reset the cooldown
        cooldown = COOLDOWN_TIME;
        
        // Play boil sound
        AudioManager.Instance.PlayBoilSound();
    }


    //Decrement the cooldown when there's an ingredient
    //and put it back when its done being used
    public void useIngredient()
    {
        if(currentIngredient != null)
        {
            currentIngredient.GetComponent<SpriteRenderer>().enabled = false;
            //change the color to the current ingredient
            color = currentIngredient.color;
            //decrement the cooldown
            cooldown -= Time.deltaTime;
            //if the cooldown runs out, make a bubble, and return the ingredient
            if(cooldown < 0)
            {
                //make a bubble
                createBubble(currentIngredient.color);
                //return ingredient
                currentIngredient.GetComponent<SpriteRenderer>().enabled = true;
                currentIngredient.moveToStart();
                currentIngredient = null;
                //reset cooldown and color
                cooldown = COOLDOWN_TIME;
                color = -1;
            }

        }
    }

    //Creates a new bubble with a specific color
    public void createBubble(int ingredientColor)
    {
        GameObject newBubble = Instantiate(bubblePrefab, cauldronPos, Quaternion.identity);
        newBubble.GetComponent<Bubble>().color = ingredientColor;
        foreach(GameObject enemy in enemies)
        {
            if(newBubble.GetComponent<Bubble>().color == enemy.GetComponent<Enemy>().color)
            {
                bubbles.Add(newBubble.GetComponent<Bubble>());
                return;
            }
        }
        Destroy(newBubble);

    }

    //Destroy
    private void destroyOnCollide()
    {
        if(bubbles.Count <= 0) { return; }
        foreach(Bubble bubble in bubbles)
        {
            if (bubble.closestEnemy() != null)
            {
                //if the bubble is within a certain distance, delete both
                float enemyDistance = Vector2.Distance(bubble.transform.position, bubble.closestEnemy().transform.position);
                if (enemyDistance < 1.0f)
                {
                    GameObject enemyToDestroy = bubble.closestEnemy();
                    Destroy(enemyToDestroy);
                    enemies.Remove(enemyToDestroy);
                    Destroy(bubble.gameObject);
                    bubbles.Remove(bubble);


                    //increase player score
                    player.score += SCORE_VALUE;


                    AudioManager.Instance.PlayWitchLaugh();

                    return;

                }
            }

        }
        
    }

    //Set the color of the object
    private void setColor()
    {
        switch (color)
        {
            case 0://yellow
                spriteRenderer.color = new Color(0.8f, 0.7f, 0.006f);
                break;
            case 1://red
                spriteRenderer.color = new Color(0.8f, 0.0f, 0.0f);
                break;
            case 2://blue
                spriteRenderer.color = new Color(0.0f, 0.0f, 0.8f);
                break;
            case 3://purple
                spriteRenderer.color = new Color(0.6f, 0.0f, 0.8f);
                break;
            default://green
                spriteRenderer.color = new Color(0.0f, 0.8f, 0.2f);
                break;
        }
    }

}
