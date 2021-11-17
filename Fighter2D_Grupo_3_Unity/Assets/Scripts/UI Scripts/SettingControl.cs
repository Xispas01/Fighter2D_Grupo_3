using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

    [System.Serializable]
public class SettingControl : MonoBehaviour
{
    public static float musicV = 1;
    public static float sfxV = 1;


    public static int resol = 0;
    public static bool full = true;

    private int j = 0;
    private bool contador = false;
    private int timeConfirm = 0;

    private List<int> widthList = new List<int>();
    private List<int> heightList = new List<int>();
    private List<int> refreshList = new List<int>();

    public TextMeshProUGUI timer;
    public TextMeshProUGUI resObj;


    public GameObject confirmOBJ;
    public GameObject optionsOBJ;

    public static int width;
    public static int height;
    public static bool fullscreen = true;
    
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

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

        width = widthList[resol];
        height = heightList[resol];
        
        resObj.text = widthList[resol] + "x" + heightList[resol];

        Screen.SetResolution(width,height,fullscreen); 
    }

    public void zSiguienteRes(){
        resol++;
        if(resol > widthList.Count - 1){
            resol = 0;
        }
        resObj.text = widthList[resol] + "x" + heightList[resol];  
    }

    public void zAnteriorRes(){
        resol--;
        if(resol < 0){
            resol = widthList.Count - 1;
        }
        resObj.text = widthList[resol] + "x" + heightList[resol];  
    }

    public void zApplyRes(int timeCon){
        Screen.SetResolution(widthList[resol],heightList[resol],full);
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
    
    IEnumerator ResCountDown(){
        for(j = timeConfirm;j>=0;j--){
            yield return new WaitForSeconds(1f);
            if (j == 0){
                zCancelRes();
                break;
            }
        }
    }

    private void ConfirmSpawn(GameObject target){
        target.SetActive(!target.activeSelf);
    }


    public void zConfirmRes(){
        StopCoroutine("ResCountDown");        
        ConfirmSpawn(confirmOBJ);
        ConfirmSpawn(optionsOBJ);
        contador = false;
        width = widthList[resol];
        height = heightList[resol];
        fullscreen = full;
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

    public void zSetSfxSlider(Slider sl){
        sl.value = sfxV;
    }
    
    public void zSetMusicSlider(Slider sl){
        sl.value = musicV;
    }

    public void zSetFullScreen(Toggle t){
        if (t.isOn){
            full = true;
        }else{
            full = false;
        }
    }
}
