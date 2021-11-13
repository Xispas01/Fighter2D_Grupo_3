using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public static float v;

    public void PercentageValue(Slider slide){
        v = slide.value;
    }
}
