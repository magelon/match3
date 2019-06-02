/*
 * code by guojin dong may 29 2019
 * bottle img reference
 * https://opengameart.org/content/bottle-icons
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpFlip : MonoBehaviour
{
    public GameObject bottle;
    Rigidbody2D bottleBody;

    [Range(300,1000)]
    public float thrust;

    [Range(200, 1000)]
    public float torque;

    //velocity on bottle
    [SerializeField]
    private float ve;


    public int time;

    [Range(0,5)]
    public float offset;

    public int jumpCount = 0;

    float oldY;
    float newY;

    void Start()
    {
        bottleBody = bottle.GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        newY= bottle.transform.position.y;
        Time.timeScale = 1;
        //check ve
        ve = bottleBody.velocity.magnitude;

        if (Input.GetMouseButtonDown(0)&&(newY-oldY)>0.5f)
        {
            jumpCount++;
        }

        if (jumpCount > 0 && (newY - oldY) > 0.5f)
        {
            time++;
        }

    }

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0)&&(jumpCount<2)&&ve<10f)
        {
            oldY = bottle.transform.position.y;
            //to avoid the collision detection and jump at same time 
            //change jump position a little bit above
            bottle.transform.position = new Vector2(bottle.transform.position.x, bottle.transform.position.y + offset);
            //change bodyType to dynamic
            bottleBody.bodyType = 0;
            //add forward force. don't need if obstacle move
            //bottleBody.AddForce(transform.right * thrust);
            //add upward force
            bottleBody.AddForce(transform.up * thrust);
            //add torque force
            bottleBody.AddTorque(torque);
            GameManager.getInstance().playSfx("flip");
        }
    }
}
