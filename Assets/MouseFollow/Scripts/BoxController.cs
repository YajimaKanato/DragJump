using UnityEngine;

public class BoxController : MonoBehaviour
{
    Animator animator;
    BoxCollider2D box;
    public GameObject player;
    public int hp = 2;//ボックスの耐久値
    bool boxbreak = false;
    float delta = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (boxbreak)
        {
            player.GetComponent<PlayerController>().force = Vector3.zero;
            delta -= Time.deltaTime;
            if (delta < 0.0f)
            {

                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CheckGround" || collision.gameObject.tag == "CheckSideR")//プレイヤー以外の衝突を無視
        {
            if (Mathf.Abs(player.GetComponent<PlayerController>().force.y) > 2.0f || Mathf.Abs(player.GetComponent<PlayerController>().force.x) > 2.0f)
            {//プレイヤーが空中から下方向に加速した時

                hp--;
                if (hp > 0)//ボックスの耐久値が0ではないとき
                {
                    animator.SetTrigger("Hit");
                }
                if (hp == 0)
                {
                    box.isTrigger = true;
                    animator.SetBool("Break", true);
                    boxbreak = true;
                    delta = 1 / 2f;
                }
            }
        }
    }
}
