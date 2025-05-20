using UnityEngine;

public class RHController : MonoBehaviour
{
    Vector3 basepos;//�����ʒu
    Rigidbody2D rigid2d;
    Animator animator;
    GameObject child;
    public string hit;
    public float thetaRad;//�ړ�����p�x
    float delta = 0.0f, speed, defspeed = 20;//���ԊǗ��A�ړ����x�A�f�t�H���g�̈ړ����x
    bool falling = false, targetin = false;//�����Ă��邩�ǂ����A�^�[�Q�b�g�i�v���C���[�j���N���������ǂ���
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigid2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        child = transform.GetChild(0).gameObject;
        child.transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg*(thetaRad+Mathf.PI/2)));//�q�I�u�W�F�N�g��e�̐i�ޕ����ɉ����ĉ�]
        basepos = transform.position;
        speed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(basepos, transform.position) <= 0.01)//�����ʒu�ɂ��鎞
        {
            if (targetin)//�^�[�Q�b�g�i�v���C���[�j���N��������
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


        if (Vector2.Distance(basepos, transform.position) <= 1.0 && falling)//����������ɏ����ʒu�ɋ߂Â����Ƃ�
        {
            speed = -1.0f;
            falling = false;
        }
        rigid2d.linearVelocity = new Vector2(Mathf.Cos(thetaRad) * speed, Mathf.Sin(thetaRad) * speed);//���x��������
    }

    private void OnCollisionEnter2D(Collision2D collision)//�������ĉ����ɂԂ�������
    {
        speed = 0.0f;
        delta = 3.0f;
        falling = true;
        animator.SetTrigger(hit);
    }

    private void OnTriggerEnter2D(Collider2D collision)//�̈���ɓ��������ǂ���
    {
        if (collision.gameObject.tag == "Player")
        {
            targetin = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)//�̈���o�����ǂ���
    {
        if (collision.gameObject.tag == "Player")
        {
            targetin = false;
        }
    }
}
