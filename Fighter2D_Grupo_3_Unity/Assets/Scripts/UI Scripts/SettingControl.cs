using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

    [System.Serializable]
public class SettingControl : MonoBehaviour
{
    private static float musicV;
    private static float sfxV;


    public static int i = 0;
    public int timeConfirm = 0;
    public List<int> widthList = new List<int>();
    public List<int> heightList = new List<int>();
    public List<int> refreshList = new List<int>();
    public TextMeshProUGUI timer;
    public TextMeshProUGUI resObj;
    public int j = 0;
    public bool contador = false;
    public GameObject confirmOBJ;
    public GameObject optionsOBJ;

    public static int width;
    public static int height;
    public static bool fullscreen = true;
    

    public void zSetMusicV(Slider slide){
        musicV = slide.value;
    }
    public float zGetMusicV(){
        return musicV;
    }

    public void zSetSfxV(Slider slide){
        sfxV = slide.value;
    }
    public float zGetSfxV(){
        return sfxV;
    }

    private void Start() {
        Resolution[] resolutions = Screen.resolutions;

        // Print the resolutions
        foreach (var res in resolutions)
        {
            widthList.Add(res.width);
            heightList.Add(res.height);
            refreshList.Add(res.refreshRate);
        }

        width = widthList[i];
        height = heightList[i];
        
        resObj.text = widthList[i] + "x" + heightList[i];  
        Screen.SetResolution(width,height,fullscreen); 
    }

    public void zSiguienteRes(){
        i++;
        if(i > widthList.Count - 1){
            i = 0;
        }
        resObj.text = widthList[i] + "x" + heightList[i];  
    }

    public void zAnteriorRes(){
        i--;
        if(i < 0){
            i = widthList.Count - 1;
        }
        resObj.text = widthList[i] + "x" + heightList[i];  
    }

    public void zApplyRes(int timeCon){
        Screen.SetResolution(widthList[i],heightList[i],fullscreen);
        timeConfirm = timeCon;
        contador = true;
        timer.text ="" + timeConfirm;   
        ConfirmSpawn(confirmOBJ);
        ConfirmSpawn(optionsOBJ);
        StartCoroutine("ResCountDown");
    }

    private void Update() {
        if(contador == true){
            timer.text ="" + j;   
        }
    }

    private void ConfirmSpawn(GameObject target){
        target.SetActive(!target.activeSelf);
    }

    IEnumerator ResCountDown(){
        for(j = timeConfirm;j>=0;j--){
            yield return new WaitForSeconds(1f);
            if (j == 0){
                Screen.SetResolution(width,height,fullscreen);
                ConfirmSpawn(confirmOBJ);
                ConfirmSpawn(optionsOBJ);
                break;
            }
        }
    }

    public void zConfirmRes(){
        StopCoroutine("ResCountDown");        
        ConfirmSpawn(confirmOBJ);
        ConfirmSpawn(optionsOBJ);
        contador = false;
        width = widthList[i];
        height = heightList[i];
    }

    public void zCancelRes(){
        StopCoroutine("ResCountDown");   
        ConfirmSpawn(confirmOBJ);
        ConfirmSpawn(optionsOBJ);
        contador = false;
        Screen.SetResolution(width,height,fullscreen);
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
}
