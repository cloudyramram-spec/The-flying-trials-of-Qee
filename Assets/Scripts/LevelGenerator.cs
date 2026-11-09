using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject[] bottomObstacles;  // the 3 "down.obs" prefabs
    public GameObject[] topObstacles;     // the 3 "upwards.bs" prefabs
    public GameObject goalPrefab;         // the Goal prefab
    public GameObject leafPrefab;         // the Leaf collectible prefab

    public bool alternate = true;    // true = bottom, top, bottom, top...
    public int obstacleCount = 12;   // how many obstacles in this level
    public float startX = 12f;       // where the first obstacle goes
    public float spacing = 6f;       // distance between obstacles
    public float bottomY = -6f;      // Y position of the bottom logs
    public float topY = 5f;          // Y position of the hanging logs
    public float goalGap = 4f;       // extra empty space before the goal

    public float leafChance = 0.8f;  // chance of a leaf in each gap (0 to 1)
    public float leafMinY = -2f;     // lowest height for a leaf
    public float leafMaxY = 2f;      // highest height for a leaf
    public float leafOffsetX = 0.5f; // horizontal offset for leaf placement

    void Start()
    {
        bool nextIsBottom = Random.value > 0.5f;

        for (int i = 0; i < obstacleCount; i++)
        {
            float x = startX + i * spacing;

            if (!alternate)
                nextIsBottom = Random.value > 0.5f;

            if (nextIsBottom)
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

            nextIsBottom = !nextIsBottom;

            // Place a leaf halfway between this log and the next one
            if (leafPrefab != null && Random.value < leafChance)
            {
                float leafX = x + spacing * leafOffsetX;
                float leafY = Random.Range(leafMinY, leafMaxY);
                Instantiate(leafPrefab, new Vector3(leafX, leafY, 0f),
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