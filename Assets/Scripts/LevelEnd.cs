using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelEnd : MonoBehaviour
{
    public GameObject endGroup;        // the EndSequence object
    public GameObject qee;             // the player
    public CameraFollow cameraFollow;  // the camera script we need to stop
    public string nextScene = "";      // scene to load afterwards (empty = stay here)
    public float showTime = 5f;        // seconds the ending is shown

    private bool started = false;

    // The Goal calls this when Qee reaches it
    public void StartEnding()
    {
        if (started) return;           // only run once
        started = true;
        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence()
    {
        // Stop the camera from following Qee
        cameraFollow.enabled = false;

        // Stop and hide the real Qee
        qee.GetComponent<QeeController>().enabled = false;
        Rigidbody2D rb = qee.GetComponent<Rigidbody2D>();
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;
        qee.GetComponent<SpriteRenderer>().enabled = false;

        // Center the ending on wherever the camera stopped, then show it
        Vector3 cam = Camera.main.transform.position;
        endGroup.transform.position = new Vector3(cam.x, 0f, 0f);
        endGroup.SetActive(true);

        // Wait, then load the next scene if there is one
        yield return new WaitForSeconds(showTime);
        if (nextScene != "")
            SceneManager.LoadScene(nextScene);
    }
}