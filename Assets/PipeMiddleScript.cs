using UnityEngine;


public class PipeMiddleScript : MonoBehaviour
{

    public LogicScript logic;
    // Initialize early so logic is set before any trigger fires
    void Awake()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            logic.addScore(1);
        }
    }
} 
