using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour {
    public void returnToStartScreen() {
        SceneManager.LoadScene("Start Screen");
    }
}
