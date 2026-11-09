using UnityEngine;

public class Collectible : MonoBehaviour
{
    public int points = 10;   // points this leaf is worth

    // Unity calls this when something enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // Only react to Qee
        if (other.CompareTag("Player"))
        {
            GameData.score += points;
            Destroy(gameObject);   // the leaf disappears
        }
    }
}