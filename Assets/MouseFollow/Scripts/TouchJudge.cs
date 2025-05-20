using UnityEngine;

public class TouchJudge : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)//地面との接地判定、斜めにも対応可能
    {
        foreach(var contact in collision.contacts)
        {
            Vector2 contactPoint = contact.point - (Vector2)this.transform.position;
            Vector2 upVec = contact.collider.gameObject.transform.up;

            if (Vector2.Angle(contactPoint, upVec) > 135)
            {
                Debug.Log("Touch");
                break;
            }
        }
    }
}
