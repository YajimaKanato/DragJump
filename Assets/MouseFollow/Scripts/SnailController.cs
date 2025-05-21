using UnityEngine;

public class SnailController : MonoBehaviour
{
    Vector3 center;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(center, new Vector3(0, 0, 1), 2 * Mathf.PI * Mathf.Rad2Deg / (Time.deltaTime * 60000));//触れているブロックの中心座標を中心に円運動
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            center = collision.gameObject.transform.position;
        }
    }
}
