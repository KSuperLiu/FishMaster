using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_SeaWave : MonoBehaviour {

    private Vector3 ve;
	void Start () {
        ve = -transform.position;
	}
	
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, ve, 10 * Time.deltaTime);
	}
}
