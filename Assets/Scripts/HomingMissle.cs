using System.Collections;
using UnityEngine;

public class HomingMissle : MonoBehaviour {
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float homingTimer = 10f;
    [SerializeField] float turnSpeed = 100f;
    [SerializeField] float rotateSpeed = 360f;

    bool chase;

    Transform target;
    Rigidbody2D rb2d;

    void Start() {
        target = FindFirstObjectByType<PlayerControler>().transform;
        rb2d = GetComponent<Rigidbody2D>();
        StartCoroutine(HomingTimer());
    }

    // Update is called once per frame
    void FixedUpdate() {
        TargetHoming();
    }

    void TargetHoming() {
        if (target == null) {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        else {
            Vector2 direction = (Vector2)target.position - rb2d.position;

            direction.Normalize();

            float rotateAmount = Vector3.Cross(direction, transform.up).z;

            rb2d.angularVelocity = -rotateAmount * turnSpeed; //if you change rotateamount to positive, it avoids you instead

            rb2d.linearVelocity = transform.up * moveSpeed;
        }
        if (!chase || target == null) return;

        var step = moveSpeed * Time.fixedDeltaTime;

        Vector2 direction = (Vector2)(target.position - transform.position);

        if (direction.sqrMagnitude < Mathf.Epsilon)
            return;

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // If your sprite faces right by default, use targetAngle - 90
        // If your sprite faces left by default, use targetAngle + 90
        // If your sprite faces down by default, use targetAngle + 180
        targetAngle -= 90; // Assuming sprite faces up by default

        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotateSpeed * Time.fixedDeltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newAngle);

        Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, step);
        
        if (rb2d != null) {
            rb2d.MovePosition(newPosition);
        }
        else {
            transform.position = newPosition;
        }
    }

    IEnumerator HomingTimer() {
        chase = true;
        yield return new WaitForSeconds(homingTimer);
        chase = false;

        if (rb2d != null)
        {
            rb2d.linearVelocity = transform.up * moveSpeed;
        }
    }

    void OnDisable() {
        chase = false;
        StopAllCoroutines();
    }
}