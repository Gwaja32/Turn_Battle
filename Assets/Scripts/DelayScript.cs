using UnityEngine;

public class DelayScript : MonoBehaviour
{
    public float Timer { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        Timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Timer += Time.deltaTime;
    }
}
