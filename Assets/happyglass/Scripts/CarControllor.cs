using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllor : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1)) {
            rb.bodyType = RigidbodyType2D.Dynamic; 
        }
    }
}
