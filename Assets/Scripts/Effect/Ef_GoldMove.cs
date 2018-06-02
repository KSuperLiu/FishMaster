using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_GoldMove : MonoBehaviour {
    private GameObject goldCollect;
	
	void Start () {
        //找到一个物体
        goldCollect = GameObject.Find("GoldCollect");

	}
	

	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, goldCollect.transform.position, 8 * Time.deltaTime);
	}
}
