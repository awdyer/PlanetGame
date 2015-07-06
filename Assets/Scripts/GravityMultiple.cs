using UnityEngine;
using System.Collections;

public class GravityMultiple : MonoBehaviour {

    // tweak these to change gravitational effect
    public float gravitationalConstant = 10; // gravitational constant
    public float myMass = 1;
    public float massMultiplier = 25;

    private GameObject[] planets;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // disable regular gravity and lock rotation
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;

        // find planets
        planets = GameObject.FindGameObjectsWithTag("Planet");
    }

    private void FixedUpdate()
    {
        // use Newton's Law of Universal Gravitation to calculate force between player and each planet

        float closestDis = -1;
        Vector3 dirUp = transform.up;   

        for (int i = 0; i < planets.Length; i++)
        {
            GameObject planet = planets[i];

            float size = planet.GetComponent<Renderer>().bounds.size.x;
            float mass = size * massMultiplier;
            float dis = Vector3.Distance(planet.transform.position, transform.position);
            float force = (gravitationalConstant * myMass * mass) / (dis * dis);
            Vector3 dir = (transform.position - planet.transform.position).normalized;
            Vector3 gravity = force * dir * -1;

            //Debug.Log(planet.name + " - distance: " + dis + ", force: " + force + ", mass: " + mass);
            rb.AddForce(gravity);

            // find closest planet, to set rotation appropriately
            if (closestDis == -1 || dis < closestDis)
            {
                closestDis = dis;
                dirUp = dir;              
            }
        }

        // set rotation towards nearest planet
        HandleRotation(dirUp);
    }

    private void HandleRotation(Vector3 dirUp)
    {
        float rotSpeed = GetRotationSpeed(dirUp);
        Quaternion targetRotation = Quaternion.FromToRotation(transform.up, dirUp) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }

    private float GetRotationSpeed(Vector3 dirUp)
    {
        float rotSpeed = 0;
        float dirDiff = Vector3.Dot(transform.up.normalized, dirUp.normalized);
        if (dirDiff < 0)
        {
            rotSpeed = 2;
        }
        else if (dirDiff >= 0.9)
        {
            rotSpeed = 50;
        }

        return rotSpeed;
    }

}
