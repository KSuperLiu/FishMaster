using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebAttr : MonoBehaviour {
    public float destroyTime;
    public int damage;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroyTime);
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //事件传递机制
        if (collision.tag == "Fish")
        {
            // 这里注意不是webAttr的gameObject的TakeDamage方法
            collision.SendMessage("TakeDamage", damage);
        }
    }

}
