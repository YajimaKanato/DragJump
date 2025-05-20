using UnityEngine;

public class FireController : MonoBehaviour
{
    Animator animator;
    GameObject child;
    CapsuleCollider2D capsule;
    public float delta;//fireがオフ（false）の時は1に設定
    float span = 0.0f;
    public bool fire, cap;//初期値をインスペクターから設定、どちらも同じものになる
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
        if (delta > span)//spanごとにアニメーションとコライダーの有効無効を切り替える
        {
            delta = 0.0f;
            fire = !fire;
            animator.SetBool("Fire", fire);
            cap = !cap;
            capsule.enabled = cap;
        }
    }
}
