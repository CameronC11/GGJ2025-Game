using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    /***************************\
    -----------Variables---------
    \***************************/

    //declare variables
    public int color;
    private float speed;
    private Vector2 startPos;
    private Vector2 target;

    private SpriteRenderer spriteRenderer;

    /***************************\
    -----------Methods-----------
    \***************************/

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        color = Random.Range(0, 4);
        speed = 0.25f;
        target = new Vector2(0, 2);

        startPos = new Vector3((Random.Range(0, 2) * 2 - 1) * 15, Random.Range(-6, 6)); 
        transform.position = startPos; 

        spriteRenderer = GetComponent<SpriteRenderer>();
        setColor();
    }

    // Update is called once per frame
    void Update()
    {
        moveToPlayer();
    }

    //Move to player
    private void moveToPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime);
    }

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
