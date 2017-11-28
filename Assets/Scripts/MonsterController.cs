using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter Tag: " + collision.gameObject.tag);
    }
}
