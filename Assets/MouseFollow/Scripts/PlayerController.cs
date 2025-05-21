using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator animator;
    Vector3 start, end, mousepos;//�}�E�X�̃h���b�O���擾
    public Vector3 force;//BoxController�Ŏg����悤�ɂ���
    Rigidbody2D rigid2d;
    GameObject flag;
    public int jump = 0, fall = 0, falling = 0, sidetouchR = 0;//�W�����v�����񐔁A���ɗ��������ǂ����A�I�u�W�F�N�g���痣�ꂽ���ǂ���
    public float defhp;
    float hp, delta = 0.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = 60;
        animator = GetComponent<Animator>();//Animator�R���|�[�l���g���擾
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
            if (transform.position.y < -8)//���ɗ��������̏���
            {
                this.transform.position = flag.transform.position + new Vector3(0.5f, 0, 0);
                fall = 1;
            }

            /*if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetMouseButton(0))//���L�[�ňړ�
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);//�i�s����������
                this.transform.position += new Vector3(-0.1f, 0, 0);
                if (jump < 1)
                {
                    animator.SetBool("Walk", true);//�A�j���[�V�����؂�ւ�
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
                force = Vector3.zero;//���Z�b�g����

                mousepos = Input.mousePosition;
                start = Camera.main.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, 10));
                fall = 0;
                rigid2d.linearVelocity = Vector3.zero;//���x���[���ɂ���
                rigid2d.bodyType = RigidbodyType2D.Kinematic;//�������Z���I�t�ɂ���
            }

            if (Input.GetMouseButtonUp(0) && jump < 2)
            {
                mousepos = Input.mousePosition;
                end = Camera.main.ScreenToWorldPoint(new Vector3(mousepos.x, mousepos.y, 10));
                force = start - end;
                if (Vector3.Distance(start, end) > 3.0f)//�}�E�X�h���b�O�����̒����𒴂������̏���
                {
                    force = force * 3.0f / Vector3.Distance(start, end);
                }

                if (fall == 1)//���ɗ��������ɁA���X�|�[����ɃW�����v���Ȃ��悤�ɂ���
                {
                    force = Vector3.zero;
                }

                if (force.x > 0)//�����������ɃX�v���C�g�����킹��
                {
                    this.transform.localScale = new Vector3(1, 1, 1);
                }
                else
                {
                    this.transform.localScale = new Vector3(-1, 1, 1);
                }

                rigid2d.bodyType = RigidbodyType2D.Dynamic;//�������Z���I���ɂ���
                this.rigid2d.AddForce(force * 4.5f, ForceMode2D.Impulse);//�}�E�X�̃N���b�N�͂��߂ƃN���b�N�I���̍��W�̍����Ƃ��āA���̑傫���ɉ����ė͂�������

                if (fall != 1 && force.y > 1.0f)//���X�|�[����Ə�����̗͂��ア�Ƃ��ȊO�̓A�j���[�V������ύX
                {
                    animator.SetBool("Jump", true);
                    animator.SetBool("WallJump", false);
                }
                fall = 0;//���X�|�[���������Ƃ𔻒�
                sidetouchR = 0;//�ǂ��痣�ꂽ�Ƃ݂Ȃ�


                if (jump == 1)
                {
                    jump++;
                }
            }

            if (falling == 1 && sidetouchR == 1)//�n�ʂɐG��Ă��Ȃ����ǂɐG��Ă���
            {
                animator.SetBool("WallJump", true);
                animator.SetBool("Jump", false);
                rigid2d.linearVelocity = Vector3.zero;//���x���[���ɂ���
                rigid2d.bodyType = RigidbodyType2D.Kinematic;//�������Z���I�t�ɂ���
                jump = 1;
            }
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*foreach (var contact in collision.contacts)//�ڒn����
        {//contact�͐ڒn�n�_�̏���Ԃ��Acontacts�͂��̏��𕡐��Ԃ�
            Vector2 contactPoint = contact.point - (Vector2)this.transform.position;//contact.point�Őڒn�n�_�̍��W�𓾂�
            Vector2 upVec = contact.collider.gameObject.transform.up;//�ڒn�n�_�ɂ���R���C�_�[�����Q�[���I�u�W�F�N�g�̒��S���������̃x�N�g��

            Debug.Log(contact.point);
            if (Vector2.Angle(contactPoint, upVec) > 170)
            {
                
            }
        }*/

        animator.SetBool("Jump", false);

        if (transform.parent == null && collision.gameObject.tag == "Falling")//�ړ����ɏ������
        {
            if (collision.gameObject.transform.position.y + 0.2f + 0.6f <= this.transform.position.y)
            {//�Փ˂����I�u�W�F�N�g���m��y���W�̋������R���C�_�[���l���������̂ɂȂ��Ă��邩
                GameObject emptyObject = new GameObject();
                emptyObject.transform.parent = collision.gameObject.transform;
                transform.parent = emptyObject.transform;//�G���v�e�B�I�u�W�F�N�g����Đe�q�֌W�ɂ��邱�Ƃœ����Ɉړ�����
            }
        }

        if (collision.gameObject.tag == "RH"||collision.gameObject.tag=="Snail")
        {
            hp--;
            animator.SetTrigger("Hit");
            rigid2d.AddForce(new Vector3(3.0f * transform.localScale.x * (-1), 5.0f, 0), ForceMode2D.Impulse);//�̂����艉�o
        }

        if (collision.gameObject.tag != "Block"&&collision.gameObject.tag!="Start" && collision.gameObject.tag != "Check" && collision.gameObject.tag != "Goal" && collision.gameObject.tag != "Falling" && collision.gameObject.tag != "Jumping")
        {
            if (Vector3.Distance(start, end) <= 2.0f)
            {
                hp--;
                animator.SetTrigger("Hit");
                rigid2d.AddForce(new Vector3(3.0f * transform.localScale.x * (-1), 5.0f, 0), ForceMode2D.Impulse);//�̂����艉�o
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //falling = 1;//�I�u�W�F�N�g���痣�ꂽ
        if (transform.parent != null && collision.gameObject.tag == "Falling")//�ړ�������~�肽��
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
            flag = GameObject.Find("Checkpoint");//�`�F�b�N�|�C���g�ɐG�ꂽ�烊�X�|�[���n�_���X�V
        }

        if (collision.gameObject.tag == "Fire" || collision.gameObject.tag == "Saw")//���ɐG�ꂽ��
        {
            hp--;
            animator.SetTrigger("Hit");
        }

        if (collision.gameObject.tag == "")//�t���[�c���̂����Ƃ��̏���
        {

        }
    }
}
