using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void playGame(){
        SceneManager.LoadScene(1);
    }

    public void endGame(){
        Application.Quit();
        Debug.Log("Adios!");
    }
}
