using UnityEngine;

public class CheckController : MonoBehaviour
{
    Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")//ÉvÉåÉCÉÑÅ[Ç™êGÇÍÇΩÇÁ
        {
            animator.SetBool("Check",true);
        }
    }
}
