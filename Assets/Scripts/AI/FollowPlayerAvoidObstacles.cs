using UnityEngine;
using System.Collections;

public class FollowPlayerAvoidObstacles : MonoBehaviour {

    public float rotationSpeed = 6.0f;
    public float moveSpeed = 3.0f;
    public float raycastDistance = 20.0f;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        // find direction to player
        Vector3 dir = (player.transform.position - transform.position).normalized;

        // check for obstacles
        float offset = 5.0f;
        AdjustDirectionForObstacle(dir, transform.position);
        AdjustDirectionForObstacle(dir, transform.position + (transform.right * -offset));
        AdjustDirectionForObstacle(dir, transform.position + (transform.right * offset));

        // set rotation to face player
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        // move towards player
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    private Vector3 AdjustDirectionForObstacle(Vector3 dir, Vector3 origin)
    {
        Debug.DrawRay(origin, transform.forward * raycastDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(origin, transform.forward, out hit, raycastDistance))
        {
            if (hit.transform != transform)
            {
                return dir + hit.normal * rotationSpeed;
            }
        }
        return dir;
    }
}
