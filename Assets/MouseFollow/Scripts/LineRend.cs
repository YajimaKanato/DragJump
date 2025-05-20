using Unity.VisualScripting;
using UnityEngine;

public class LineRend : MonoBehaviour
{
    LineRenderer line;
    Vector3 start, end, mousepos;
    int jump, fall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        jump = transform.parent.gameObject.GetComponent<PlayerController>().jump;
        fall = transform.parent.gameObject.GetComponent<PlayerController>().fall;
        if (Input.GetMouseButtonDown(0) && jump < 2)//ガイドを表示
        {
            mousepos = Input.mousePosition;
            start = Camera.main.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, 10));
        }
        if (Input.GetMouseButton(0) && jump < 2 && fall != 1)
        {
            mousepos = Input.mousePosition;
            end = Camera.main.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, 10));
            Vector3 vec = end - start;
            if (Vector3.Distance(start, end) > 3.0f)
            {
                vec = vec * 3 / Vector3.Distance(start, end);
            }
            line.SetPosition(0, transform.parent.gameObject.transform.position);
            line.SetPosition(1, transform.parent.gameObject.transform.position - vec);
        }
        if (Input.GetMouseButtonUp(0))//ガイドを消す
        {
            line.SetPosition(0, new Vector3(0, 0, 0));
            line.SetPosition(1, new Vector3(0, 0, 0));
        }
        if (jump == 2)
        {
            jump = 0;
        }
    }
}
