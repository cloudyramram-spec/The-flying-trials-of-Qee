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
    public bool resetScoreAtStart = false;   // tick this in Level1 only

    private Rigidbody2D rb;
    private Animator anim;
    private bool isDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // A brand new game starts at zero (Level1 only)
        if (resetScoreAtStart)
            GameData.score = 0;

        // Remember the score at the start of this level
        GameData.scoreAtLevelStart = GameData.score;
    }

    void Update()
    {
        if (isDead) return;

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
        rb.linearVelocity = Vector2.zero;
        anim.SetTrigger("Hit");

        // Undo the points collected in this attempt
        GameData.score = GameData.scoreAtLevelStart;

        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}