using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript:MonoBehaviour
{


public GameObject restartButton;
public GameObject menuButton;


public void restartScene(){
    SceneManager.LoadScene("SampleScene");
}

public void MenuScene(){
    SceneManager.LoadScene("Title");
}



    
}
