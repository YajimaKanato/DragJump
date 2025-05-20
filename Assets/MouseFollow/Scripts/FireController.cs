using UnityEngine;

public class FireController : MonoBehaviour
{
    Animator animator;
    GameObject child;
    CapsuleCollider2D capsule;
    public float delta;//fire���I�t�ifalse�j�̎���1�ɐݒ�
    float span = 0.0f;
    public bool fire, cap;//�����l���C���X�y�N�^�[����ݒ�A�ǂ�����������̂ɂȂ�
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("Fire", fire);
        child = transform.GetChild(0).gameObject;
        capsule = child.GetComponent<CapsuleCollider2D>();
        capsule.enabled = cap;
    }

    // Update is called once per frame
    void Update()
    {
        if (fire)
        {
            span = 3.0f;
        }
        else
        {
            span = 5.0f;
        }

        delta += Time.deltaTime;
        if (delta > span)//span���ƂɃA�j���[�V�����ƃR���C�_�[�̗L��������؂�ւ���
        {
            delta = 0.0f;
            fire = !fire;
            animator.SetBool("Fire", fire);
            cap = !cap;
            capsule.enabled = cap;
        }
    }
}
