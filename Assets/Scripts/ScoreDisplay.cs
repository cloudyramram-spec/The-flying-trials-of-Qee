using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    private TMP_Text label;   // the text on the screen

    void Start()
    {
        label = GetComponent<TMP_Text>();
    }

    void Update()
    {
        // Show the current score every frame
        label.text = "Score: " + GameData.score;
    }
}