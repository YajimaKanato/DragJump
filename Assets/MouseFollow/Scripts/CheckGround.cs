using UnityEngine;

public class CheckGround : MonoBehaviour
{
    GameObject player;
    PlayerController playcon;
    Rigidbody2D rigid2d;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = transform.parent.gameObject;
        playcon = player.GetComponent<PlayerController>();
        rigid2d = player.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Jumping")//ジャンプ台に上から、もしくは下からジャンプした時にプレイヤーの上方向に力を加える
        {
            rigid2d.AddForce(Vector3.up * 20, ForceMode2D.Impulse);
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)//足元のトリガーが何かに触れている間
    {
        playcon.jump = 0;
        playcon.falling = 0;
    }

    private void OnTriggerExit2D(Collider2D collision)//足元のトリガーが何かから離れた時
    {
        playcon.jump = 1;
        playcon.falling = 1;
    }
}
