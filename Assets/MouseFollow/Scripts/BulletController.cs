using UnityEngine;

public class BulletController : MonoBehaviour
{
    Rigidbody2D rigid2d;
    public Transform plant;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        rigid2d.AddForce(new Vector3(7.0f * plant.localScale.x * (-1), 1.0f, 0), ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -8)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
