using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public float rotationSpeed = 3.0f;
    public float moveSpeed = 3.0f;

    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        // find direction to player
        Vector3 dir = (player.transform.position - transform.position).normalized;
        // set rotation to face player
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
        // move towards player
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }
}
