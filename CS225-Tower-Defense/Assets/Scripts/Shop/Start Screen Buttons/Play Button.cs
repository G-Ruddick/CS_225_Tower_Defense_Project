using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {
    public void BeginGame() {
        SceneManager.LoadScene("Game");
    }
}