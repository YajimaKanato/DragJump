using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    Vector3 pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (0 <= player.transform.position.x && player.transform.position.x <= 220)//ˆê’èðŒ‰º‚Å‚Ì‚ÝƒJƒƒ‰‚ð“®‚©‚·
        {
            pos = this.transform.position;
            pos.x = player.transform.position.x;
            this.transform.position = pos;
        }
        else if (player.transform.position.x<0)
        {
            pos = this.transform.position;
            pos.x = 0;
            this.transform.position = pos;
        }
        else if (player.transform.position.x > 220)
        {
            pos = this.transform.position;
            pos.x = 220;
            this.transform.position = pos;
        }

        if (-4 <= player.transform.position.y && player.transform.position.y <= 10)
        {
            pos = this.transform.position;
            pos.y = player.transform.position.y + 2;
            this.transform.position = pos;
        }
    }
}
