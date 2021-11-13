using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSizer : MonoBehaviour
{
    public static int ancho = 900;
    public static int altura = 600;
    public static bool full = true;

    
    private void Start() {
        ancho = Screen.width;
        altura = Screen.height;
        Screen.SetResolution(ancho,altura,full); 
    }

    public void setAncho(int nuevo){
        ancho = nuevo;
    }

    public void setAltura(int nuevo){
        altura = nuevo;
    }

    public void setToggle(Toggle t){
        if (full){
            t.isOn = true;
        }else{
            t.isOn = false;
        }
    }

    public void setFull(Toggle t){
        if (t.isOn){
            full = true;
        }else{
            full = false;
        }
    }
        
    public void setRes(){
        Screen.SetResolution(ancho,altura,full);
    }
}
