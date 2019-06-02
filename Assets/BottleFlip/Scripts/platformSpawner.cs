using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformSpawner : MonoBehaviour
{
    JumpFlip jf;
    public Transform[] position;
    public GameObject[] platform;
    public GameObject winp;

    bool win;
    void Start()
    {
        jf = GetComponent<JumpFlip>();
    }

    void Update()
    {
        if (jf.time!=0&&jf.time % 500==0)
        {
            if(Random.Range(0, 10) > 5)
            {
                Color background = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f)
                );
                GameObject item = platform[0];
                item.GetComponent<SpriteRenderer>().color = background;
                Instantiate(item, position[0].position, Quaternion.identity);
            }
            else
            {
                Color background = new Color(
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f),
                    Random.Range(0f, 1f)
                );
                GameObject item = platform[1];
                item.GetComponent<SpriteRenderer>().color = background;
                Instantiate(item, position[0].position, Quaternion.identity);
            }
        }
        if (jf.time > 5000 && win==false)
        {
            win = true;
            GameData.getInstance().main.gameWin();
            //Instantiate(winp);
            jf.enabled = false;
        }
    }
}
