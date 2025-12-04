using UnityEngine;

public class PipeSpawnScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject pipe;          // default prefab
    public GameObject pipeEasy;      // optional: assign a prefab with a larger gap
    public GameObject pipeHard;      // optional: assign a prefab with a smaller gap
    public float spawnRate = 2;
    private float timer = 0;
    public float heightOffset = 10;
    void Start()
    {
        // Initialize spawn rate from difficulty if available
        if (GameManager.Instance != null)
        {
            spawnRate = GameManager.Instance.CurrentSpawnRate;
        }
        // Do not spawn immediately; wait for Play
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsRunning == false) return;
        if (timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            SpawnPipe(); 
            timer = 0;
            // Refresh spawn rate in case difficulty changed just before starting
            if (GameManager.Instance != null)
                spawnRate = GameManager.Instance.CurrentSpawnRate;
        }


    }

    void SpawnPipe()
    {
        float lowestPoint = transform.position.y - heightOffset;
        float highestPoint = transform.position.y + heightOffset;
        Vector3 spawnPosition = new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), transform.position.z);
        GameObject prefabToSpawn = pipe;
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.SelectedDifficulty == Difficulty.Easy && pipeEasy != null)
                prefabToSpawn = pipeEasy;
            else if (GameManager.Instance.SelectedDifficulty == Difficulty.Hard && pipeHard != null)
                prefabToSpawn = pipeHard;
        }
        Instantiate(prefabToSpawn, spawnPosition, transform.rotation);
    }
}
