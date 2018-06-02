using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_PlayEffect : MonoBehaviour {
    public GameObject[] effectPre;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayEffect()
    {
        foreach(GameObject effect in effectPre)
        {
           GameObject go = Instantiate(effect);
           go.transform.SetParent( GameObject.Find("Order90Canvas").transform, false);
        }
    }
}
