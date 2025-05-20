using UnityEngine;

public class PlantController : MonoBehaviour
{
    Animator animator;
    public GameObject bullet;
    public GameObject player;
    PlayerController plycon;
    float delta = 0.0f;
    int hp = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        plycon = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta > 1.8f)
        {
            animator.SetBool("Attack", false);
            delta = 0.0f;
            Instantiate(bullet).transform.position = this.transform.position + new Vector3(0.7f * this.transform.localScale.x * (-1), 0, 0);
        }
        else if (delta > 1.5f)
        {
            animator.SetBool("Attack", true);
        }

        if (hp <= 0)//hpがなくなったときの処理
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
            if (plycon.force.magnitude > 2.0f)//一定の速度でプレイヤーがぶつかってきたとき
            {
                hp--;
                delta = 0.0f;
                animator.SetTrigger("Hit");
            }
        }
    }
}
