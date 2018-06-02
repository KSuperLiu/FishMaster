using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollow : MonoBehaviour {
    public RectTransform rect;
    public Camera cam;
    //1.得到将屏幕上的坐标和原点的那条直线和y轴的夹角
	//2.将炮旋转夹角
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //
        Vector3 ret;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, new Vector2(Input.mousePosition.x, Input.mousePosition.y), cam, out ret);
        float z;
        //transform 就是本身的
        if (ret.x > transform.position.x)
        {
            //z将会是负的   而Vector3.Angle是返回[0,180)
            z = -Vector3.Angle(Vector3.up, ret - transform.position);
        }
        else
        {
            z = Vector3.Angle(Vector3.up, ret - (Vector3)transform.position);
        }
         transform.localRotation = Quaternion.Euler(0, 0, z);
      

    }
}
