using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizer : MonoBehaviour
{
    public static int ancho = 900;
    public static int altura = 600;
    public static bool full = false;

    private void Start() {
       Screen.SetResolution(ancho,altura,full); 
    }

    public void setAncho(int nuevo){
        ancho = nuevo;
    }

    public void setAltura(int nuevo){
        altura = nuevo;
    }

    public void setFull(bool nuevo){
        if (full){
            full = false;
        }else{
            full = true;
        }
    }
        
    public void setRes(){
        Screen.SetResolution(ancho,altura,full);
    }
}
