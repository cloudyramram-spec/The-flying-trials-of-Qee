using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;       // the object to follow (Qee)
    public float xOffset = 3f;     // keeps Qee on the left side of the screen

    // LateUpdate runs after all movement, so the camera never jitters
    void LateUpdate()
    {
        if (target == null) return;   // do nothing if no target is set

        // Follow only on the X axis. Y and Z stay the same.
        transform.position = new Vector3(
            target.position.x + xOffset,
            transform.position.y,
            transform.position.z
        );
    }
}