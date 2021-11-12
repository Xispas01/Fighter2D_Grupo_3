using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour{

    public static bool IsPaused;
    public GameObject pauseMenuCanvas;

    void Start (){    
        pauseMenuCanvas.SetActive(false);
    }


    void Update(){

        if (Input.GetKeyDown (KeyCode.Escape)){
            if(!IsPaused){
                Time.timeScale=0;    
                IsPaused = true;                 
            }
            else{
                Time.timeScale=1;
                IsPaused = false;
            }
        }

        if (IsPaused){
            pauseMenuCanvas.SetActive(true);
        } else{
            pauseMenuCanvas.SetActive (false);
        }
    }


    public void Resume(){
        Time.timeScale=1;
        IsPaused = false;     
    }
     
    public void ExitGame(){
        SceneManager.LoadScene(0);
    }
}
