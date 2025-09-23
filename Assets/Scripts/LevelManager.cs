using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float loadDelay = 2f;
    ScoreKeeper scoreKeeper;

    private void Awake() {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    public  void LoadGame() {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Level_1");
    }

    public void LoadMainMenu() {
        SceneManager.LoadScene("Menu");
    }

    public void LoadGameOver() {
        StartCoroutine(WaitAndLoad("GameOver", loadDelay));
    }
    public void QuitGame() {
        Application.Quit(); // this works on all devices and platforms
        Debug.Log("Quit Button Pressed");
    }

    IEnumerator WaitAndLoad(string sceneName, float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
