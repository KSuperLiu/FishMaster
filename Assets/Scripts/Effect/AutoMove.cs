using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour {
   public int speed;
    public Vector3 vec = Vector3.right;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //鱼全部往上走，因为鱼的出生点的X轴都对准了屏幕
        transform.Translate(vec * Time.deltaTime * speed);

    }
}
