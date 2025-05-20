using UnityEngine;
using UnityEngine.Rendering;

public class FallingMove : MonoBehaviour
{
    float speed = 0.01f;
    public float thetaRad = 0.0f;
    float delta = 0.0f;
    public float span;
    Vector3 vecspeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vecspeed = new Vector3(Mathf.Cos(thetaRad), Mathf.Sin(thetaRad), 0);//thetaRad�ɂ���ď��̈ړ����������߂�
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta > span)//��莞�Ԃ������甽�Ε�����
        {
            delta = 0.0f;
            speed *= -1;
        }
        this.transform.position += vecspeed * speed;
    }
}
