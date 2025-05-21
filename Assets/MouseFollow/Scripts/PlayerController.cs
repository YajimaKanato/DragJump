using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Vector3 start, end, mousepos;//マウスのドラッグを取得
    public Vector3 force;//BoxControllerで使えるようにする
    Rigidbody2D rigid2d;
    GameObject flag;
    public int jump = 0, fall = 0, falling = 0, sidetouchR = 0;//ジャンプした回数、穴に落ちたかどうか、オブジェクトから離れたかどうか
    public float defhp;
    float hp, delta = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        animator = GetComponent<Animator>();//Animatorコンポーネントを取得
        rigid2d = GetComponent<Rigidbody2D>();
        flag = GameObject.Find("StartFlag");
        hp = defhp;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            animator.SetTrigger("Death");
            delta += Time.deltaTime;
            if (delta > 0.57 * 2f)
            {
                hp = defhp;
                delta = 0.0f;
            }
            else if (0.65f>=delta&&delta > 0.583f)
            {
                this.transform.position = flag.transform.position + new Vector3(0.5f, 0.5f, 0);
            }
        }
        else
        {
            if (transform.position.y < -8)//穴に落ちた時の処理
            {
                this.transform.position = flag.transform.position + new Vector3(0.5f, 0, 0);
                fall = 1;
            }

            /*if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetMouseButton(0))//矢印キーで移動
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);//進行方向を向く
                this.transform.position += new Vector3(-0.1f, 0, 0);
                if (jump < 1)
                {
                    animator.SetBool("Walk", true);//アニメーション切り替え
                }
            }
            else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetMouseButton(0))
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                this.transform.position += new Vector3(0.1f, 0, 0);
                if (jump < 1)
                {
                    animator.SetBool("Walk", true);
                }
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                animator.SetBool("Walk", false);
            }*/


            if (Input.GetMouseButtonDown(0) && jump < 2)
            {
                start = Vector3.zero;
                end = Vector3.zero;
                force = Vector3.zero;//リセット処理

                mousepos = Input.mousePosition;
                start = Camera.main.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, 10));
                fall = 0;
                rigid2d.linearVelocity = Vector3.zero;//速度をゼロにする
                rigid2d.bodyType = RigidbodyType2D.Kinematic;//物理演算をオフにする
            }

            if (Input.GetMouseButtonUp(0) && jump < 2)
            {
                mousepos = Input.mousePosition;
                end = Camera.main.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, 10));
                force = start - end;
                if (Vector3.Distance(start, end) > 3.0f)//マウスドラッグが一定の長さを超えた時の処理
                {
                    force = force * 3.0f / Vector3.Distance(start, end);
                }

                if (fall == 1)//穴に落ちた時に、リスポーン後にジャンプしないようにする
                {
                    force = Vector3.zero;
                }

                if (force.x > 0)//向いた方向にスプライトを合わせる
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    this.transform.localScale = new Vector3(-1, 1, 1);
                }

                rigid2d.bodyType = RigidbodyType2D.Dynamic;//物理演算をオンにする
                this.rigid2d.AddForce(force * 4.5f, ForceMode2D.Impulse);//マウスのクリックはじめとクリック終わりの座標の差をとって、その大きさに応じて力を加える

                if (fall != 1 && force.y > 1.0f)//リスポーン後と上方向の力が弱いとき以外はアニメーションを変更
                {
                    animator.SetBool("Jump", true);
                    animator.SetBool("WallJump", false);
                }
                fall = 0;//リスポーンしたことを判定
                sidetouchR = 0;//壁から離れたとみなす


                if (jump == 1)
                {
                    jump++;
                }
            }

            if (falling == 1 && sidetouchR == 1)//地面に触れていないかつ壁に触れている
            {
                animator.SetBool("WallJump", true);
                animator.SetBool("Jump", false);
                rigid2d.linearVelocity = Vector3.zero;//速度をゼロにする
                rigid2d.bodyType = RigidbodyType2D.Kinematic;//物理演算をオフにする
                jump = 1;
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*foreach (var contact in collision.contacts)//接地判定
        {//contactは接地地点の情報を返す、contactsはその情報を複数返す
            Vector2 contactPoint = contact.point - (Vector2)this.transform.position;//contact.pointで接地地点の座標を得る
            Vector2 upVec = contact.collider.gameObject.transform.up;//接地地点にあるコライダーを持つゲームオブジェクトの中心から上方向のベクトル

            Debug.Log(contact.point);
            if (Vector2.Angle(contactPoint, upVec) > 170)
            {
                
            }
        }*/

        animator.SetBool("Jump", false);

        if (transform.parent == null && collision.gameObject.tag == "Falling")//移動床に乗ったら
        {
            if (collision.gameObject.transform.position.y + 0.2f + 0.6f <= this.transform.position.y)
            {//衝突したオブジェクト同士のy座標の距離がコライダーを考慮したものになっているか
                GameObject emptyObject = new GameObject();
                emptyObject.transform.parent = collision.gameObject.transform;
                transform.parent = emptyObject.transform;//エンプティオブジェクトを介して親子関係にすることで同時に移動する
            }
        }

        if (collision.gameObject.tag == "RH"||collision.gameObject.tag=="Snail")
        {
            hp--;
            animator.SetTrigger("Hit");
            rigid2d.AddForce(new Vector3(3.0f * transform.localScale.x * (-1), 5.0f, 0), ForceMode2D.Impulse);//のけぞり演出
        }

        if (collision.gameObject.tag != "Block"&&collision.gameObject.tag!="Start" && collision.gameObject.tag != "Check" && collision.gameObject.tag != "Goal" && collision.gameObject.tag != "Falling" && collision.gameObject.tag != "Jumping")
        {
            if (Vector3.Distance(start, end) <= 2.0f)
            {
                hp--;
                animator.SetTrigger("Hit");
                rigid2d.AddForce(new Vector3(3.0f * transform.localScale.x * (-1), 5.0f, 0), ForceMode2D.Impulse);//のけぞり演出
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //falling = 1;//オブジェクトから離れた
        if (transform.parent != null && collision.gameObject.tag == "Falling")//移動床から降りたら
        {
            transform.parent = null;
        }
        if (jump > 0&&collision.gameObject.tag=="Block")
        {
            animator.SetBool("Jump", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Check")
        {
            flag = GameObject.Find("Checkpoint");//チェックポイントに触れたらリスポーン地点を更新
        }

        if (collision.gameObject.tag == "Fire" || collision.gameObject.tag == "Saw")//炎に触れたら
        {
            hp--;
            animator.SetTrigger("Hit");
        }

        if (collision.gameObject.tag == "")//フルーツを採ったときの処理
        {

        }
    }
}
