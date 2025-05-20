using UnityEngine;

public class CheckSideEnemy : MonoBehaviour
{
    ChickenController chickenController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        chickenController = transform.parent.gameObject.GetComponent<ChickenController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)//‘O•û‚É‰½‚©‚ª‚ ‚é‚©
    {
        chickenController.side = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        chickenController.side = false;
    }
}
