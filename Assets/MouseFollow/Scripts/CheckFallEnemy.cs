using UnityEngine;

public class CheckFallEnemy : MonoBehaviour
{
    ChickenController chickenController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chickenController=transform.parent.gameObject.GetComponent<ChickenController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)//�R�ɂ��邩�ǂ���
    {
        chickenController.fall = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        chickenController.fall = true;
    }
}
