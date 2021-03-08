using UnityEngine;
using System.Collections;
using System.Collections.Generic;
// using UnityEngine.AI;

public class FatherFollowing_Official : MonoBehaviour
{
    public GameObject Player;
    public float TargetDist;
    public float allowedDist = 5;
    public GameObject Father;
    public float followSpeed;
    public RaycastHit Shot;
    float gravity = 8;

    Vector3 moveDir = Vector3.zero;
    CharacterController controller;
    Animator anim;

    void Start () {
        controller = GetComponent<CharacterController> ();
        anim = GetComponent<Animator> ();
    }

    void Update () {

        transform.LookAt(Player.transform);
        
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Shot)) {
                TargetDist = Shot.distance;
                if (TargetDist >= allowedDist) {
                    followSpeed = 2.5f;
                    anim.SetInteger ("Condition", 1);
                    moveDir = new Vector3 (0,0,1); 
                    moveDir *= followSpeed;
                    moveDir = transform.TransformDirection (moveDir);
                    //transform.position += new Vector3(0,0,1) * followSpeed * Time.deltaTime;
                    //moveDir = new Vector3 (0,0,1);
                    //transform.Translate (Vector3.forward * (followSpeed) * Time.deltaTime);
                //transform.position += transform.forward * followSpeed * Time.deltaTime;
                    //transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, followSpeed);  
                }
                else {
                    followSpeed = 0;
                    anim.SetInteger ("Condition", 0);
                    moveDir = new Vector3 (0,0,0);
                }
            }
 
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move (moveDir * Time.deltaTime);
    }




//     public Transform Player;
//     public int maxDistance = 5;
//     public int minDistance = 2;
//     float speed = 4;
//     float gravity = 8;

//     Vector3 moveDir = Vector3.zero;

//     CharacterController controller;
//     Animator anim;

//     void Start()
//     {
//         controller = GetComponent<CharacterController> ();
//         anim = GetComponent<Animator> ();
//     }

//     void Update()
//     {
//         if (controller.isGrounded) {
//             transform.LookAt(Player);

//             // if (Vector3.Distance(transform.position, Player.position) >= maxDistance) {
//             //     anim.SetInteger ("Condition", 1);
//             //     // moveDir = new Vector3 (0,0,1); 
//             //     // moveDir *= speed;
//             //     transform.position += transform.forward * speed * Time.deltaTime;
                
//             //     if (Vector3.Distance(transform.position, Player.position) <= minDistance) {
//             //         anim.SetInteger ("Condition", 0);
//             //         moveDir = new Vector3 (0,0,0);
//             //     }
//             // }
            
//             if(Input.GetKey(KeyCode.W)) {
//                 anim.SetInteger ("Condition", 1);
//                 moveDir = new Vector3 (0,0,1); 
//                 moveDir *= speed;
//                 moveDir = transform.TransformDirection (moveDir);
//             }
//             if (Input.GetKeyUp(KeyCode.W)) {
//                 anim.SetInteger ("Condition", 0);
//                 moveDir = new Vector3 (0,0,0); 
//             }
//         }
//         moveDir.y -= gravity * Time.deltaTime;
//         controller.Move (moveDir * Time.deltaTime);
//     }
// }
}