using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonSwap : MonoBehaviour
{
    public static Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    public TextMeshProUGUI JumpP1, LeftP1, RightP1;
    public TextMeshProUGUI JumpP2, LeftP2, RightP2;

    private GameObject currentKey;


    private Color32 normal = new Color32(39, 171, 249, 255);
    private Color32 selected = new Color32(239, 116, 36, 255);


    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Use this for initialization
    void Start () {
        keys.Add("JumpA", KeyCode.W);
        keys.Add("LeftA", KeyCode.A);
        keys.Add("RightA", KeyCode.D);

        keys.Add("JumpB", KeyCode.UpArrow);
        keys.Add("LeftB", KeyCode.LeftArrow);
        keys.Add("RightB", KeyCode.RightArrow);


        JumpP1.text = keys["JumpA"].ToString();
        LeftP1.text = keys["LeftA"].ToString();
        RightP1.text = keys["RightA"].ToString();

        JumpP2.text = keys["JumpB"].ToString();
        LeftP2.text = keys["LeftB"].ToString();
        RightP2.text = keys["RightB"].ToString();

    }
    
    
    public void defaultButton()
    {

        keys["JumpA"] = KeyCode.W;
        keys["LeftA"] = KeyCode.A;
        keys["RightA"] = KeyCode.D;
        
        keys["JumpB"] = KeyCode.UpArrow;
        keys["LeftB"] = KeyCode.LeftArrow;
        keys["RightB"] = KeyCode.RightArrow;


        JumpP1.text = keys["JumpA"].ToString();
        LeftP1.text = keys["LeftA"].ToString();
        RightP1.text = keys["RightA"].ToString();
        
        JumpP2.text = keys["JumpB"].ToString();
        LeftP2.text = keys["LeftB"].ToString();
        RightP2.text = keys["RightB"].ToString();

    }

    void OnGUI(){
        if(currentKey != null){
            Event e = Event.current;
            if(e.isKey){
                bool used = false;
                foreach (var k in keys.Values){//Revision repeticion de input
                    if(k == e.keyCode){
                        used = true;
                    }
                }
                if (used == false){//Sistema anti repeticion de input
                    keys[currentKey.name] = e.keyCode;
                    currentKey.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = e.keyCode.ToString();
                    currentKey.GetComponent<Image>().color = normal;
                    currentKey = null;
                }else{
                    keys[currentKey.name] = keys[currentKey.name];
                    currentKey.GetComponent<Image>().color = normal;
                    currentKey = null;
                }
            }
        }
    }

    public void ChangeKey(GameObject clicked){
        if(currentKey != null){
            currentKey.GetComponent<Image>().color = normal;
        }
        currentKey = clicked;
        currentKey.GetComponent<Image>().color = selected;
    }
}