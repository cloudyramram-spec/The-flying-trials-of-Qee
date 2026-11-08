using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class QeeController : MonoBehaviour
{
    public float flapForce = 6f;       // how strong each flap is
    public float forwardSpeed = 2f;    // how fast Qee moves right
    public float restartDelay = 1f;    // seconds before the level restarts
    public float minY = -5.5f;         // invisible floor
    public float maxY = 5.5f;          // invisible ceiling

    private Rigidbody2D rb;
    private Animator anim;
    private bool isDead = false;       // true after hitting something

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // After a hit, ignore the player's input
        if (isDead) return;

        // Falling out of the screen counts as a hit (replaces the ground)
        if (transform.position.y < minY || transform.position.y > maxY)
        {
            StartCoroutine(Die());
            return;
        }

        bool flapPressed = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (flapPressed)
        {
            rb.linearVelocity = new Vector2(forwardSpeed, flapForce);
            anim.SetTrigger("Flap");
        }
        else
        {
            rb.linearVelocity = new Vector2(forwardSpeed, rb.linearVelocity.y);
        }
    }

    // Unity calls this when Qee's collider touches another collider
    void OnCollisionEnter2D(Collision2D col)
    {
        if (isDead) return;

        if (col.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        isDead = true;

        // Stop moving forward right away
        rb.linearVelocity = Vector2.zero;

        // Play the hit animation (only works if the "Hit" trigger exists)
        anim.SetTrigger("Hit");

        // Wait so the player can see what happened
        yield return new WaitForSeconds(restartDelay);

        // Restart the current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}