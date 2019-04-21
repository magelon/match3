using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cupFillState : MonoBehaviour
{
    public float time;
    public GameObject sad;
    public GameObject happy;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.name.ToString().Substring(0, 4) == "Drop")
        {
            time += Time.time;
        }
        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        time = 0;
    }

    private void Update()
    {
        if (time > 1000)
        {
            if (GameData.getInstance().isLock)//If the game locked,you can not control the game.
                return;
            sad.SetActive(false);
            happy.SetActive(true);
            GameData.getInstance().main.gameWin();

            }
        }

}
