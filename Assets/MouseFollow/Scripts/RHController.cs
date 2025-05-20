using UnityEngine;

public class RHController : MonoBehaviour
{
    Vector3 basepos;//初期位置
    Rigidbody2D rigid2d;
    Animator animator;
    GameObject child;
    public string hit;
    public float thetaRad;//移動する角度
    float delta = 0.0f, speed, defspeed = 20;//時間管理、移動速度、デフォルトの移動速度
    bool falling = false, targetin = false;//落ちているかどうか、ターゲット（プレイヤー）が侵入したかどうか
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        child = transform.GetChild(0).gameObject;
        child.transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg*(thetaRad+Mathf.PI/2)));//子オブジェクトを親の進む方向に応じて回転
        basepos = transform.position;
        speed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(basepos, transform.position) <= 0.01)//初期位置にいる時
        {
            if (targetin)//ターゲット（プレイヤー）が侵入したか
            {
                speed = defspeed;
            }
            else
            {
                speed = 0.0f;
            }
        }

        if (falling)
        {
            delta -= Time.deltaTime;
            if (delta < 0.0f)
            {
                speed = -2.0f;
                delta = 0.0f;
            }
        }


        if (Vector2.Distance(basepos, transform.position) <= 1.0 && falling)//落下した後に初期位置に近づいたとき
        {
            speed = -1.0f;
            falling = false;
        }
        rigid2d.linearVelocity = new Vector2(Mathf.Cos(thetaRad) * speed, Mathf.Sin(thetaRad) * speed);//速度をかける
    }

    private void OnCollisionEnter2D(Collision2D collision)//落下して何かにぶつかったら
    {
        speed = 0.0f;
        delta = 3.0f;
        falling = true;
        animator.SetTrigger(hit);
    }

    private void OnTriggerEnter2D(Collider2D collision)//領域内に入ったかどうか
    {
        if (collision.gameObject.tag == "Player")
        {
            targetin = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)//領域を出たかどうか
    {
        if (collision.gameObject.tag == "Player")
        {
            targetin = false;
        }
    }
}
