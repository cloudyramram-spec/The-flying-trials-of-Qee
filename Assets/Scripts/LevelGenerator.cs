using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] bottomObstacles;  // the 3 "down.obs" prefabs
    public GameObject[] topObstacles;     // the 3 "upwards.bs" prefabs
    public GameObject goalPrefab;         // the Goal prefab

    public int obstacleCount = 12;   // how many obstacles in this level
    public float startX = 12f;       // where the first obstacle goes
    public float spacing = 6f;       // distance between obstacles
    public float bottomY = -6f;      // Y position of the bottom logs
    public float topY = 5f;          // Y position of the hanging logs
    public float goalGap = 4f;       // extra empty space before the goal

    void Start()
    {
        for (int i = 0; i < obstacleCount; i++)
        {
            float x = startX + i * spacing;

            if (Random.value > 0.5f)
            {
                int index = Random.Range(0, bottomObstacles.Length);
                Instantiate(bottomObstacles[index], new Vector3(x, bottomY, 0f),
                            Quaternion.identity, transform);
            }
            else
            {
                int index = Random.Range(0, topObstacles.Length);
                Instantiate(topObstacles[index], new Vector3(x, topY, 0f),
                            Quaternion.identity, transform);
            }
        }

        // Place the goal after the last obstacle, plus the extra gap
        if (goalPrefab != null)
        {
            float goalX = startX + obstacleCount * spacing + goalGap;
            Instantiate(goalPrefab, new Vector3(goalX, 0f, 0f),
                        Quaternion.identity, transform);
        }
    }
}