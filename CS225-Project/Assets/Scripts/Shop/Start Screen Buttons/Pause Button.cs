using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseButton : MonoBehaviour {
    public Button pauseButton;
    public TMP_Text pauseButtonText;
    public static bool gamePaused;
    
    void Start()
    {
        gamePaused = true;
    }

    // Update is called once per frame
    void Update()
    {
        // updating button text
        if (gamePaused) {
            pauseButtonText.text = "►";
            pauseButton.onClick.AddListener(() => unpauseGame());
        }
        else {
            pauseButtonText.text = "||";
            pauseButton.onClick.AddListener(() => pauseGame());
        }        
    }

    public void unpauseGame() {
        gamePaused = false;
    }
    public void pauseGame() {
        gamePaused = true;
    }
}
