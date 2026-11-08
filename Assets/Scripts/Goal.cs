using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the LevelEnd object in the scene and start the ending
            LevelEnd end = FindAnyObjectByType<LevelEnd>();
            if (end != null) end.StartEnding();
        }
    }
}