using UnityEngine;

public class ChickenController : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigid2d;
    public GameObject player;
    PlayerController plycon;
    CheckSideEnemy checks;
    CheckFallEnemy checkf;
    int hp = 2;
    int rand, left = 34, right = 69;//動きをランダムで決める
    float delta = 0.0f;
    bool hit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rigid2d = GetComponent<Rigidbody2D>();
        plycon = player.GetComponent<PlayerController>();
        checks=transform.GetChild(0).GetComponent<CheckSideEnemy>();
        checkf=transform.GetChild(1).GetComponent<CheckFallEnemy>();
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
            if(!checkf.fall && !checks.side)//崖にいないかつ前方に何もない
            {
                animator.SetBool("Run", true);
                transform.localScale = new Vector3(1, 1, 1);
                transform.position -= new Vector3(0.05f, 0, 0);//左移動
            }
            else
            {//方向転換
                rand = Random.Range(left,right);
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        else if (left <= rand && rand < right && !hit)
        {
            if(!checkf.fall && !checks.side)
            {
                animator.SetBool("Run", true);
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position += new Vector3(0.05f, 0, 0);//右移動
            }
            else
            {
                rand = Random.Range(0, left);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            animator.SetBool("Run", false);
        }

        if (hp <= 0)//hpがなくなったときの処理
        {
            animator.SetBool("Death", true);
            if (delta > 0.57f)
            {
                Destroy(gameObject);
            }

        }

        if (transform.position.y < -8)//落下した（落とされた）時の処理
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (plycon.force.magnitude > 2.0f)//一定の速度でプレイヤーがぶつかってきたとき
            {
                hp--;
                hit = true;
                delta = 0.0f;
                animator.SetTrigger("Hit");
                rigid2d.AddForce(new Vector3(0.3f * (plycon.force.x / Mathf.Abs(plycon.force.x)), 2.0f, 0), ForceMode2D.Impulse);//のけぞり演出
            }
            rand = 90;
        }
    }
}
