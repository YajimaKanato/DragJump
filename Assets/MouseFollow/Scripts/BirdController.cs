using UnityEngine;

public class BirdController : MonoBehaviour
{
    float delta = 0.0f, dropdelta = 0.0f;
    public GameObject drop;
    public Transform player;//ターゲット（プレイヤー）との距離を測る
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;

        if (delta > 8.0f)
        {
            delta = 0.0f;
            transform.localScale = new Vector3(transform.localScale.x * (-1), 1, 1);
        }
        else if (delta > 7.0f)
        {
            transform.position += new Vector3(0, 0, 0);
        }
        else
        {
            transform.position += new Vector3(transform.localScale.x * (-1) * 0.01f, 0, 0);
        }


        if (Mathf.Abs(player.position.x - this.transform.position.x) < 10.0f)//ターゲットを追従する
        {
            dropdelta += Time.deltaTime;
            if (dropdelta > 1.5f)
            {
                dropdelta = 0.0f;
                Instantiate(drop).transform.position = this.transform.position + new Vector3(0.2f* transform.localScale.x, -0.9f, 0);
            }
        }
    }
}
