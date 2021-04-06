using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class father_walking : MonoBehaviour {
    
    public List<Transform> waypoints = new List<Transform>();
    private Transform targetWaypoint;
    private int targetWaypointIndex = 0;
    private float minDistance = 0.1f;
    private int lastWaypointIndex;

    private float movementSpeed = 1.8f;
    private float rotateSpeed = 1.5f;

    Animator anim;
    //NavMeshAgent agent;

    // Start is called before the first frame update
    void Start() {
        lastWaypointIndex = waypoints.Count - 1;
        targetWaypoint = waypoints[targetWaypointIndex];
        anim = GetComponent<Animator>();
        //agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update() {
        float movementStep = movementSpeed * Time.deltaTime;
        float rotationStep = rotateSpeed * Time.deltaTime;

        anim.SetInteger ("Condition", 1);
        Vector3 directionToTarget = targetWaypoint.position - transform.position;
        Quaternion rotationToTarget = Quaternion.LookRotation(directionToTarget);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToTarget, rotationStep);

        float distance = Vector3.Distance(transform.position, targetWaypoint.position);
        CheckDistanceToWaypoint(distance);
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, movementStep);
    }

    void CheckDistanceToWaypoint(float currentDistance) {
        if (currentDistance <= minDistance) {
            targetWaypointIndex++;
            UpdateTargetWaypoint();
        }
    }

    void UpdateTargetWaypoint() {
        if (targetWaypointIndex > lastWaypointIndex) {
            anim.SetInteger ("Condition", 0);
        }
        targetWaypoint = waypoints[targetWaypointIndex];
    }
}
