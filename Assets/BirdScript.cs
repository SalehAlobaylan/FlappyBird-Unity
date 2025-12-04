using UnityEngine;
using UnityEngine.InputSystem;


public class BirdScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D myRigidbody;
    public float flapStrength;
    public LogicScript logic;
    public bool isAlive = true;
    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        if (GameManager.Instance != null)
        {
            flapStrength = GameManager.Instance.CurrentFlapStrength;
            // Don't let the bird fall before the game starts
            if (GameManager.Instance.IsRunning == false && myRigidbody != null)
                myRigidbody.simulated = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // If game just started, re-enable physics
        if (GameManager.Instance != null && GameManager.Instance.IsRunning == true && myRigidbody != null && myRigidbody.simulated == false)
        {
            myRigidbody.linearVelocity = Vector2.zero;
            myRigidbody.simulated = true;
        }

        if (GameManager.Instance != null && GameManager.Instance.IsRunning == false)
            return;
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isAlive == true)
        {
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
        }
        if (transform.position.y > 40 || transform.position.y < -40)
        {
            logic.gameOver();
            isAlive = false;
        }
        // myRigidbody2D.velocity = Vector2.up * 5;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GameManager.Instance != null && GameManager.Instance.IsRunning == false)
            return;
        // Play collision then game over sounds
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayCollisionThenGameOver();
        }
        logic.gameOver();
        isAlive = false;
    }
}
