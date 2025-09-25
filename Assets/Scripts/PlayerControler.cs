using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour {
    [SerializeField] float moveSpeed = 5f;
    Vector2 rawInput;

    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;

    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;

    private void Awake() {
        shooter = GetComponent<Shooter>();
    }
    void Start() {
        InitBounds();
    }

    // Update is called once per frame
    void Update() {
        Move();
    }

    void Move() {
        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector3 newPos = new Vector2(); //Vector3 can be converted to vector2 without "z"

        //Limit player movement by boders
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value) {
        rawInput = value.Get<Vector2>();
        //Debug.Log(rawInput);
    }

    void OnAttack(InputValue value) {
        if (shooter != null) {
            shooter.isFiring = value.isPressed;
        }
    }

    void InitBounds() {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));  //0,0 Bottom right corner
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1)); //1,1 Top left corner
    }
}
