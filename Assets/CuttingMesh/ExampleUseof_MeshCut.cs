using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExampleUseof_MeshCut : MonoBehaviour
{

    public Material capMaterial;

    private List<Vector2> segments;
    private Vector2 start;
    private Vector2 end;
    private LineRenderer line;
    private Vector2 po;
    //public float angle;
    private Quaternion rotation;
    public float angleZ;
    public GameObject p;
    private MainCharacterMovement mcm;
    public float linelength;
    //public bool rbFirst;

    void Start()
    {
        segments = new List<Vector2>();
        mcm = p.GetComponent<MainCharacterMovement>();
        //gameObject.AddComponent<LineRenderer>();
        line = gameObject.AddComponent<LineRenderer>();

    }

    private void Update()
    {
        po = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //rotation
        RaycastHit hitOn;

        if (Physics.Raycast(ray, out hitOn, 100))
            Debug.DrawLine(ray.origin, hitOn.point);

        if (Input.GetMouseButtonUp(0))
        {
            //drawline
            line.enabled = false;
            transform.position = new Vector3(start.x, start.y, -2f);
            transform.eulerAngles = new Vector3(0, 0, rotation.eulerAngles.z-90);

            linelength=Vector2.Distance(start, end);
            // rotation.eulerAngles;
            // Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime);

            //new Quaternion(0, 0, -transform.rotation.z, 1);
            //transform.eulerAngles = new Vector3(0, 0, -angle);
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Invoke("ToMove", 1f);
                GameManager.getInstance().playSfx("touch1");
                GameObject victim = hit.collider.gameObject;

                GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);

                Destroy(pieces[0].GetComponent<MeshCollider>());
                pieces[0].AddComponent<MeshCollider>().convex = true;
                if (pieces[0].GetComponent<Rigidbody>() == null)
                {
                    pieces[0].AddComponent<Rigidbody>();
                    pieces[0].GetComponent<Rigidbody>().isKinematic = false;
                    //pieces[0].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                }
                if (pieces[0].GetComponent<Rigidbody>())
                {
                    pieces[0].GetComponent<Rigidbody>().isKinematic = false;
                }

               

                if (pieces[0].GetComponent<soundOnCollision>() == null)
                {
                    pieces[0].AddComponent<soundOnCollision>();
                }
               

                if (!pieces[1].GetComponent<Rigidbody>())
                {
                    //pieces[1].AddComponent<Rigidbody>();
                    //freeze rotationon on x
                    pieces[1].AddComponent<MeshCollider>().convex = true;

                    if (pieces[1].GetComponent<Rigidbody>() == null)
                    {
                        pieces[1].AddComponent<Rigidbody>();
                        pieces[1].GetComponent<Rigidbody>().isKinematic = false;
                        //pieces[1].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;

                    }
                    if (pieces[1].GetComponent<soundOnCollision>() == null)
                    {
                        pieces[1].AddComponent<soundOnCollision>();
                    }
                    //.constraints=RigidbodyConstraints.FreezeRotation;
                    //pieces[1].AddComponent<BoxCollider2D>();
                    // victim.GetComponent<MeshCollider>().enabled = true;

                }
                //Destroy(pieces[1], 1);
            }


        }
        //start from no hit
        if (Input.GetMouseButton(0))
        {
            line.SetPosition(1, new Vector3(po.x, po.y, -2));
            if (hitOn.collider!=null&&hitOn.collider.GetComponent<MeshCollider>())
            {
                segments.Add(po);
            }
            if (segments.Count>1&&line)
            {
               
                start = segments[0];
                //transform.position = new Vector3(start.x,start.y,-2);
                end = segments[segments.Count - 1];
                line.SetPosition(0, new Vector3(start.x, start.y, -2));
                //line.SetPosition(1, new Vector3(end.x, end.y, -2));

                transform.position = new Vector3(start.x, start.y, -2f);
                Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
               
                float angleToMouse = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                 rotation = Quaternion.AngleAxis(angleToMouse, Vector3.forward); 
                 angleZ = rotation.eulerAngles.z;


            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            start = new Vector2();
            line.enabled = true;
            segments = new List<Vector2>();
            transform.eulerAngles = new Vector3(0, 0, 0);
           
            //GameObject gObject = new GameObject("LineRender");
            //line = gObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default"));
            line.startColor = Color.red;
            line.endColor = Color.yellow;
            line.startWidth = 0.01f;
            line.endWidth = 0.01f;

            line.SetPosition(0, new Vector3(po.x, po.y, -2));
          
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

    private void ToMove()
    {
        mcm.canWalk = true;
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