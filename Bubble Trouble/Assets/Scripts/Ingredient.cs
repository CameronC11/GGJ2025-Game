using UnityEngine;

public class Ingredient : MonoBehaviour
{

    /***************************\
    -----------Variables---------
    \***************************/

    //init colors
    public int color = -1;

    //init start position
    public Vector2 startPos;

    /***************************\
    -----------Methods-----------
    \***************************/

    private void Start()
    {
        startPos = transform.position;
    }

    public void moveToStart()
    {
        this.transform.position = startPos;
    }


}
