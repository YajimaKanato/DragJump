using UnityEngine;

public class BoxController : MonoBehaviour
{
    Animator animator;
    BoxCollider2D box;
    public GameObject player;
    public int hp = 2;//�{�b�N�X�̑ϋv�l
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
        if (collision.gameObject.tag == "CheckGround" || collision.gameObject.tag == "CheckSideR")//�v���C���[�ȊO�̏Փ˂𖳎�
        {
            if (Mathf.Abs(player.GetComponent<PlayerController>().force.y) > 2.0f || Mathf.Abs(player.GetComponent<PlayerController>().force.x) > 2.0f)
            {//�v���C���[���󒆂��牺�����ɉ���������

                hp--;
                if (hp > 0)//�{�b�N�X�̑ϋv�l��0�ł͂Ȃ��Ƃ�
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
