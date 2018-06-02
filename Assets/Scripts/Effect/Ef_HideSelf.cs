using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ef_HideSelf : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
	}
    

    

    public  IEnumerator Hideself()
    {
        yield return new WaitForSeconds(0.8f);
        gameObject.SetActive(false);
    }
    
}
