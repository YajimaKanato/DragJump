using UnityEngine;

public class CheckFallEnemy : MonoBehaviour
{
    public bool fall = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)//�R�ɂ��邩�ǂ���
    {
        fall = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        fall = true;
    }
}
