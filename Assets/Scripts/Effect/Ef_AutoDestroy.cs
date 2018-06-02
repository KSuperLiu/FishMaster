using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_AutoDestroy : MonoBehaviour {
    public int destroyTime=1;
	void Start () {
        Destroy(gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
