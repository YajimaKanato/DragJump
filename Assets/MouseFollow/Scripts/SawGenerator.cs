using UnityEngine;

public class SawGenerator : MonoBehaviour
{
    public GameObject saw;
    public float delta = 0.0f;//�I�u�W�F�N�g�����^�C�~���O
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;
        if (delta > 4.0f)
        {
            delta = 0.0f;
            Instantiate(saw).transform.position = this.transform.position;
        }
    }
}
