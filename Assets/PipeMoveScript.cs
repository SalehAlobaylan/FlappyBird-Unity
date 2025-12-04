using UnityEngine;

public class PipeMoveScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float moveSpeed = 5;
    public float deadZone = -50;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = moveSpeed;
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.IsRunning == false) return;
            speed = GameManager.Instance.CurrentMoveSpeed;
        }
        transform.position = transform.position + (Vector3.left * speed) * Time.deltaTime;

        if (transform.position.x < deadZone)
        {
            Debug.Log("Pipe Destroyed");
            Destroy(gameObject);
        }

    }
}
