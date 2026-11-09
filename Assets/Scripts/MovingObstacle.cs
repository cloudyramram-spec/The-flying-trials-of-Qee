using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    public float distance = 2f;   // how far it moves
    public float speed = 1.5f;    // how fast it moves

    private Vector3 startPos;     // where the log was placed
    private float offset;         // random start, so logs don't move in sync

    void Start()
    {
        // The generator has already placed the log, so remember this position
        startPos = transform.position;
        offset = Random.Range(0f, 10f);
    }

    void Update()
    {
        // PingPong goes back and forth between 0 and distance
        float move = Mathf.PingPong((Time.time + offset) * speed, distance);
        transform.position = startPos + Vector3.up * move;
    }
}