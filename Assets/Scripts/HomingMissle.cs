using System.Collections;
using UnityEngine;

public class HomingMissle : MonoBehaviour {
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float homingTimer = 10f;
    [SerializeField] float turnSpeed = 100f;

    bool chase;

    Transform target;
    Rigidbody2D rb2d;

    void OnEnable() {
        target = FindFirstObjectByType<PlayerControler>().transform;
        rb2d = GetComponent<Rigidbody2D>();
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

    }

    //IEnumerator HomingTimer() {
    //    chase = true;

    //    yield return new WaitForSeconds(homingTimer);

    //}
}
