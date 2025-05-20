using UnityEngine;

public class CheckSideR : MonoBehaviour
{
    PlayerController plycon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        plycon = transform.parent.gameObject.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)//ï«Ç…êGÇÍÇƒÇ¢ÇÈÇ©Ç«Ç§Ç©
    {
        if (collision.gameObject.tag == "Block")
        {
            plycon.sidetouchR = 1;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        plycon.sidetouchR = 0;
    }
}
