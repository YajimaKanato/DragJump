using UnityEngine;

public class ChickenController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid2d;
    public GameObject player;
    PlayerController plycon;
    int hp = 2;
    int rand, left = 34, right = 69;//“®‚«‚ğƒ‰ƒ“ƒ_ƒ€‚ÅŒˆ‚ß‚é
    float delta = 0.0f;
    bool hit;
    public bool fall, side;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid2d = GetComponent<Rigidbody2D>();
        plycon = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta > 1.0f)
        {
            delta = 0.0f;
            rand = Random.Range(0, 100);
            hit = false;
        }

        if (0 <= rand && rand < left && !hit)
        {
            animator.SetBool("Run", true);
            transform.localScale = new Vector3(1, 1, 1);
            transform.position -= new Vector3(0.05f, 0, 0);//¶ˆÚ“®
        }
        else if (left <= rand && rand < right && !hit)
        {
            animator.SetBool("Run", true);
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position += new Vector3(0.05f, 0, 0);//‰EˆÚ“®
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (hp <= 0)
        {
            animator.SetBool("Death", true);
            if (delta > 0.57f)
            {
                Destroy(gameObject);
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (plycon.force.magnitude > 2.0f)
            {
                hp--;
                hit = true;
                delta = 0.0f;
                animator.SetTrigger("Hit");
                rigid2d.AddForce(new Vector3(0.3f * (plycon.force.x / Mathf.Abs(plycon.force.x)), 2.0f, 0), ForceMode2D.Impulse);//‚Ì‚¯‚¼‚è‰‰o
                rand = 90;
            }
            else
            {
                rand = 90;
            }

        }
    }
}
