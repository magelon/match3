using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class staticKnife : MonoBehaviour
{

    public Material capMaterial;

   

    void Start()
    {
       
    }

    private void Update()
    {
       
       

        if (Input.GetMouseButtonUp(0))
        {

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {

                GameObject victim = hit.collider.gameObject;

                GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

                Destroy(pieces[0].GetComponent<MeshCollider>());
                pieces[0].AddComponent<MeshCollider>().convex = true;
                pieces[0].AddComponent<Rigidbody>();

                if (!pieces[1].GetComponent<Rigidbody>())
                {
                    //pieces[1].AddComponent<Rigidbody>();
                    //freeze rotationon on x
                    pieces[1].AddComponent<MeshCollider>().convex = true;
                    pieces[1].AddComponent<Rigidbody>();//.constraints=RigidbodyConstraints.FreezeRotation;
                                                        //pieces[1].AddComponent<BoxCollider2D>();
                                                        // victim.GetComponent<MeshCollider>().enabled = true;
                }
                //Destroy(pieces[1], 1);
            }


        }


        /*
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {

                GameObject victim = hit.collider.gameObject;

                GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

                Destroy(pieces[0].GetComponent<MeshCollider>());
                pieces[0].AddComponent<MeshCollider>().convex=true;
                pieces[0].AddComponent<Rigidbody>();

                if (!pieces[1].GetComponent<Rigidbody>()) {
                    //pieces[1].AddComponent<Rigidbody>();
                    //freeze rotationon on x
                    pieces[1].AddComponent<MeshCollider>().convex = true;
                    pieces[1].AddComponent<Rigidbody>();//.constraints=RigidbodyConstraints.FreezeRotation;
                    //pieces[1].AddComponent<BoxCollider2D>();
                   // victim.GetComponent<MeshCollider>().enabled = true;
                }
                //Destroy(pieces[1], 1);
            }

        }**/
    }

    void OnDrawGizmosSelected()
    {

       Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5.0f);
        Gizmos.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward * 5.0f);
       Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.forward * 5.0f);

        Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + -transform.up * 0.5f);

    }

}