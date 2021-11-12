using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour
{
    private static float volume;

    
    public static int width = 900;
    public static int height = 600;
    public static bool fullscreen = true;

    public void zSetVolume(Slider slide){
        volume = slide.value;
    }
    public float zGetVolume(){
        return volume;
    }

private void Start() {
        width = Screen.width;
        height = Screen.height;
        Screen.SetResolution(width,height,fullscreen); 
    }

    public void setAncho(int nuevo){
        width = nuevo;
    }

    public void setAltura(int nuevo){
        height = nuevo;
    }

    public void zSetToggle(Toggle t){
        if (fullscreen){
            t.isOn = true;
        }else{
            t.isOn = false;
        }
    }

    public void zSetFullScreen(Toggle t){
        if (t.isOn){
            fullscreen = true;
        }else{
            fullscreen = false;
        }
    }
        
    public void zSetRes(){
        Screen.SetResolution(width,height,fullscreen);
    }
}
