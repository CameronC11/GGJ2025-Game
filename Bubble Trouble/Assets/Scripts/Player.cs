using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    /***************************\
    -----------Variables---------
    \***************************/

    

    //init other objects
    public Ingredient[] ingredients;
    public GameObject cauldronObj;
    public Cauldron cauldron;
    public GameObject enemySpawner;

     private WitchHealthBar witchHealth;

    //declare positions
    private Vector3 mousePos;

    //declare drag/drop variables
    private bool isDragging;
    [SerializeField]
    private Ingredient draggedIngredient;

    //declare health variables
    [SerializeField]
    //private int health
    private BoxCollider2D boxCollider;
    private List<GameObject> enemies;

    //declare score
    public int score;


    /***************************\
    -----------Methods---------
    \***************************/

    void Start()
    {
        //init variables
        score = 0;
        //health = 3;
        boxCollider = GetComponent<BoxCollider2D>();
        //init others
        cauldron = cauldronObj.GetComponent<Cauldron>();
        enemies = enemySpawner.GetComponent<EnemySpawner>().enemies;
        witchHealth = GetComponent<WitchHealthBar>();
    }

    // UPDATE METHOD
    void Update()
    {
        //is the player holding an ingredient?
        dragAndDrop();
        //is the player hit by an enemy?
        isHit();
    }

    //Perform all calculations relating to 
    //dragging and dropping ingredients.
    private void dragAndDrop()
    {
        //init mouse position
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        //loop through ingredients
        for (int i = 0; i < ingredients.Length; i++)
        {
            //if the mouse is on the ingredient, pick it up
            if (ingredients[i].GetComponent<BoxCollider2D>().OverlapPoint(mousePos))
            {
                ingredients[i].GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f);
                if (Input.GetMouseButtonDown(0))
                {
                    isDragging = true;
                    draggedIngredient = ingredients[i];
                    draggedIngredient.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
                }
            }
            else
            {
                ingredients[i].GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
            }
            
        }

        //move the ingredient to the mouse position
        if (isDragging && draggedIngredient != null)
        {
            draggedIngredient.transform.position = mousePos;
        }

        //drop in correct position
        if (Input.GetMouseButtonUp(0) && draggedIngredient != null)
        {
            //stop dragging ingredient
            isDragging = false;
            draggedIngredient.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);

            //check if the ingredient is on the cauldron or not
            if (isValidDropPosition(draggedIngredient.transform.position))
            {
                draggedIngredient.transform.position = cauldron.cauldronPos;
                cauldron.addIngredient(draggedIngredient);
                draggedIngredient = null;
            }
            else
            {   
                draggedIngredient.moveToStart();
                draggedIngredient = null;
            }
        }
        


    }
    
    //Check if the dragged ingredient is close enough to
    //the cauldron or not.
    private bool isValidDropPosition(Vector2 position)
    {
        // Check if the ingredient is close enough to the cauldron
        float snapDistance = 2.0f; 
        return Vector2.Distance(position, cauldron.cauldronPos) < snapDistance;
    }

    //Check to see if the player has been hit by an enemy
    private void isHit()
    {
        if (enemies.Count <= 0) { return; }
        foreach(GameObject enemy in enemies)
        {
            float enemyDistance = Vector2.Distance(transform.position, enemy.transform.position);
            if (enemyDistance < 2.5f)
            {
                witchHealth.TakeDamage(1);
                Destroy(enemy);
                enemies.Remove(enemy);
                //health--;
                return;
            }
        }
    }



}
