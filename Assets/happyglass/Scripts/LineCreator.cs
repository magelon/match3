using UnityEngine;
using System.Collections;

public class LineCreator : MonoBehaviour
{
    public GameObject linePrefab;
    private GameObject lineGo;
    DrawLineWithCollider activeLine;

    private void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lineGo = Instantiate(linePrefab);
            activeLine = lineGo.GetComponent<DrawLineWithCollider>();
        }

        if (Input.GetMouseButtonUp(0))
        {
            activeLine = null;
            lineGo.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            Time.timeScale = 1;
        }

        if (activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            activeLine.UpdateLine(mousePos);
        }

    }
}
