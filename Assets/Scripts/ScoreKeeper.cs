using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    //health health;

    int score; 

    static ScoreKeeper instance;

    private void Awake() {
        ManageSingleton();
    }
    void ManageSingleton() {
        if (instance != null) {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ModifyScore(int value) {
        score += value;
        Mathf.Clamp(score, 0, int.MaxValue);
        Debug.Log("Score = "+ score);
    }

    public int GetScore() { return score; }

    public void ResetScore() {
        score = 0;
    }
}
