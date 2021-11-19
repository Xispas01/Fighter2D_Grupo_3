using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox_retardada : MonoBehaviour
{
    public int a;
    public bool b;
    public int c;
    public bool d;
    public int e;
    public bool f;
    public bool g;
    public int h;

    // Start is called before the first frame update
    void Start()
    {
        a = 0;
        b = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && a == 0){
            a = 1;
            c = 1;
        }
    }

    IEnumerator HitboxCD() {    
        b = false;
        yield return new WaitForSeconds(0.1f);
        a++;
        b = true;
    }
    
    IEnumerator HitboxCD2() {    
        b = false;
        yield return new WaitForSeconds(0.1f);
        h++;
        b = true;
    }

    IEnumerator Combo() {    
        d = false;
        yield return new WaitForSeconds(0.1f);
        c++;
        d = true;
    }
}
