using UnityEngine;
using System.Collections;

public class AgentFollow : MonoBehaviour {

	public Transform player;
	NavMeshAgent agent;
	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		agent.SetDestination(player.position);
	}
}
