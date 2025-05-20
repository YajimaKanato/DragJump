using UnityEngine;

public class PigController : MonoBehaviour
{
    Animator animator;
    public Transform player;//�^�[�Q�b�g�i�v���C���[�j�Ƃ̋����𑪂�
    public GameObject plycon;
    CheckSideEnemy checks;
    CheckFallEnemy checkf;//�R�ɂ��Ȃ����O���ɉ����Ȃ��̔���𓾂�
    Rigidbody2D rigid2d;
    int hp = 3;
    int rand, left = 34, right = 69;//�����������_���Ō��߂�
    float delta = 0.0f;
    bool hit, targetLock;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        checks = transform.GetChild(0).GetComponent<CheckSideEnemy>();
        checkf = transform.GetChild(1).GetComponent<CheckFallEnemy>();
        rigid2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (targetLock)//�^�[�Q�b�g���͈͂ɓ����Ă��邩�ǂ���
        {
            if(player.position.x - this.transform.position.x > 0)//�^�[�Q�b�g��Ǐ]����
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position += new Vector3(0.07f, 0, 0);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position -= new Vector3(0.07f, 0, 0);
            }
        }
        else
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
                if (!checkf.fall && !checks.side)//�R�ɂ��Ȃ����O���ɉ����Ȃ�
                {
                    animator.SetBool("Walk", true);
                    transform.localScale = new Vector3(1, 1, 1);
                    transform.position -= new Vector3(0.05f, 0, 0);//���ړ�
                }
                else
                {//�����]��
                    rand = Random.Range(left, right);
                    transform.localScale = new Vector3(-1, 1, 1);
                }
            }
            else if (left <= rand && rand < right && !hit)
            {
                if (!checkf.fall && !checks.side)
                {
                    animator.SetBool("Walk", true);
                    transform.localScale = new Vector3(-1, 1, 1);
                    transform.position += new Vector3(0.05f, 0, 0);//�E�ړ�
                }
                else
                {
                    rand = Random.Range(0, left);
                    transform.localScale = new Vector3(1, 1, 1);
                }
            }
            else
            {
                animator.SetBool("Walk", false);
            }

            if (Mathf.Abs(player.position.x - this.transform.position.x) < 6.0f)//�^�[�Q�b�g���͈͂ɓ�������
            {
                delta = 0.0f;
                targetLock = true;
                animator.SetBool("Run", true);
            }
        }

        if (hp <= 0)//hp���Ȃ��Ȃ����Ƃ��̏���
        {
            delta += Time.deltaTime;
            animator.SetBool("Death", true);
            if (delta > 0.57f)
            {
                Destroy(gameObject);
            }

        }

        if (transform.position.y < -8)//���������i���Ƃ��ꂽ�j���̏���
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (plycon.GetComponent<PlayerController>().force.magnitude > 2.0f)//���̑��x�Ńv���C���[���Ԃ����Ă����Ƃ�
            {
                hp--;
                hit = true;
                animator.SetTrigger("Hit");
                rigid2d.AddForce(new Vector3(1.0f * (plycon.GetComponent<PlayerController>().force.x / Mathf.Abs(plycon.GetComponent<PlayerController>().force.x)), 2.0f, 0), ForceMode2D.Impulse);//�̂����艉�o
            }
        }
    }
}
