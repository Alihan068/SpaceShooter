using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour {

    [Header("Health")]
    [SerializeField] Slider healthSlider;
    [SerializeField] Health playerHealth;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

   
    private void Awake() {
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
    }

    void Start() {
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    // Update is called once per frame
    void Update() {
        healthSlider.value = playerHealth.GetHealth();
        scoreText.text = "Score: " + scoreKeeper.GetScore().ToString("0000000000");
    }
    
}
