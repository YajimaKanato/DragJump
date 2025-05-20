using UnityEngine;

public class CheckSideEnemy : MonoBehaviour
{
    public bool side = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)//‘O•û‚É‰½‚©‚ª‚ ‚é‚©
    {
        if (collision.gameObject.tag != "Player")
        {
            side = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        side = false;
    }
}
