using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public float speed;
    GameObject gm;
    int jf;

    private void Start()
    {
        gm = GameObject.Find("GameManager");
    }

    void Update()
    {
        jf = gm.GetComponent<JumpFlip>().jumpCount;
        if (jf > 0) { 
        transform.Translate(Vector2.left * Time.deltaTime * speed); 
        }
    }
}
