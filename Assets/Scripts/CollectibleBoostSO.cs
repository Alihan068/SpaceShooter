using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(menuName = "CollectibleBoost", fileName = "Create Boost")]
public class CollectibleBoostSO : MonoBehaviour
{
    [SerializeField] Sprite sprite;
    [SerializeField] float boost;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.gameObject.CompareTag("Player")) return;

        
    }
}
