using TMPro;
using UnityEngine;

public class UIGameOver : MonoBehaviour {

    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Awake() {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    void Start() {
        finalScoreText.text = ("Score = " + scoreKeeper.GetScore());
    }

}
