using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartButtonAnimation : MonoBehaviour
{
    public Image buttonImage;        // the Image of the button
    public Sprite[] frames;          // the 7 animation frames, in order
    public float frameTime = 0.1f;   // seconds each frame stays on screen
    public string firstLevel = "Level1";

    private bool playing = false;

    // The button calls this when clicked
    public void OnPressed()
    {
        if (playing) return;         // ignore double clicks
        playing = true;
        StartCoroutine(PlayAndStart());
    }

    IEnumerator PlayAndStart()
    {
        GameData.score = 0;          // a new game starts at zero

        // Show each frame one after the other
        foreach (Sprite s in frames)
        {
            buttonImage.sprite = s;
            yield return new WaitForSeconds(frameTime);
        }

        SceneManager.LoadScene(firstLevel);
    }
}