using System.Collections;
using UnityEngine;

public class FishMaker : MonoBehaviour {
    public Transform[] birthPos;     //鱼出生的位置
    public Transform fishHolder;
    public GameObject[] fishPrefabs;  //鱼

    public float waveGenWaitTime = 0.3f;
    private float fishGenWaitTime=0.5f;

    void MakeFishes()
    {
        //随机生出鱼的种类
        int birthPosIndex = Random.Range(0, birthPos.Length);
        //随机生成鱼的位置
        int fishPreIndex = Random.Range(0, fishPrefabs.Length);
        //鱼的最大数量
        int maxNum = fishPrefabs[fishPreIndex].GetComponent<FishAttr>().maxNum;
        //鱼的最大速度
        int maxSpeed = fishPrefabs[fishPreIndex].GetComponent<FishAttr>().maxSpeed;
        
        int num = Random.Range((maxNum / 2) + 1, maxNum);
        if (maxNum == 2)
        {
            num = 1;
        }
        int speed = Random.Range(maxSpeed / 2, maxSpeed);
        int type = Random.Range(0, 2);
        int angOffset;    //仅直走生效，直走的倾斜角
        int angSpeed;    //仅转弯生效，转弯的角速度
        if (type == 1) //鱼直走
        {
            angOffset = Random.Range(-22, 22);

            StartCoroutine(GenStraightFish(birthPosIndex, fishPreIndex, num, speed, angOffset)) ;
        }
        else  //鱼转弯  
        {
            int dir = Random.Range(0, 2);
            if(dir == 0)
            {
                angSpeed = Random.Range(-15, 9);
            }
            else
            {
                angSpeed = Random.Range(9, -15);
            }
            StartCoroutine(GenRotateFish(birthPosIndex, fishPreIndex, num, speed, angSpeed));
        }
    }

    private IEnumerator GenRotateFish(int birthPosIndex, int fishPreIndex, int num, int speed, int angSpeed)
    {
        for (int i = 0; i < num; i++)
        {
            GameObject fish = Instantiate(fishPrefabs[fishPreIndex]);
            //需要设置父物体
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = birthPos[birthPosIndex].localPosition;
            fish.transform.localRotation = birthPos[birthPosIndex].localRotation;
           
        
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            //让鱼动起来
            fish.AddComponent<AutoMove>().speed = speed;
            fish.AddComponent<Ef_AutoRotate>().speed = angSpeed;
            //如果不暂停 鱼就会重复生成在一个地方
            yield return new WaitForSeconds(fishGenWaitTime);
        }
        yield return new WaitForSeconds(fishGenWaitTime);
    }

    void Start () {
      InvokeRepeating("MakeFishes", 0, waveGenWaitTime);

    }

    IEnumerator GenStraightFish(int posIndex,int preIndex,int num,int speed,int angOffest)
    {
        for(int i = 0; i < num; i++)
        {
           GameObject fish= Instantiate(fishPrefabs[preIndex]);
            //需要设置父物体
            fish.transform.SetParent(fishHolder, false);
            fish.transform.localPosition = birthPos[posIndex].localPosition;
            fish.transform.localRotation = birthPos[posIndex].localRotation;
            //围绕轴旋转角度角度。
            fish.transform.Rotate(0, 0, angOffest);
            fish.GetComponent<SpriteRenderer>().sortingOrder += i;
            //让鱼动起来
            fish.AddComponent<AutoMove>().speed = speed;
            //如果不暂停 鱼就会重复生成在一个地方
            yield return new WaitForSeconds(fishGenWaitTime);
        }


    }


    void Update () {
		
	}
}
