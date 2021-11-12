using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_UI : MonoBehaviour
{
    public void Testing(){Debug.Log("Funciona !!");}

    public void visible(GameObject target){target.SetActive(true);}
    public void inVisible(GameObject target){target.SetActive(false);}

}
