using UnityEngine;

public class FruitsController : MonoBehaviour
{
    bool collect;
    float delta = 0.0f;
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collect)
        {
            delta += Time.deltaTime;
            if (delta > 0.5f)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collect = true;
            animator.SetTrigger("Collect");
        }
    }
}
