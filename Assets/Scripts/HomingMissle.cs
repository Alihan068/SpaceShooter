using System.Collections;
using UnityEngine;

public class HomingMissle : MonoBehaviour {
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float homingTimer = 10f;

    bool chase;

    Transform target;

    void OnEnable() {
        target = FindFirstObjectByType<PlayerControler>().transform;
    }

    // Update is called once per frame
    void FixedUpdate() {
        TargetHoming();
    }

    void TargetHoming() {
        var step = moveSpeed * Time.deltaTime; // calculate distance to move
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        transform.LookAt(target.position);

    }

    //IEnumerator HomingTimer() {
    //    chase = true;

    //    yield return new WaitForSeconds(homingTimer);

    //}
}
