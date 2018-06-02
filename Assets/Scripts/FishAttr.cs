using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAttr : MonoBehaviour {
    public int maxNum;
    public int maxSpeed;
    public int hp;
    public GameObject diePre;
    public int exp;
    public int gold;
    public GameObject goldPre;//鱼死后掉的金币
    public int type;  //鱼的种类 1小鱼  2乌龟等  3彩云等  4蝴蝶鱼等 5银鲨   6金鲨

    private void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Board")
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int value)
    {
        print("fish 受到伤害：" + value);
        hp -= value;
        if (hp <= 0)
        {
            GameController.Instance.exp += exp;
            GameController.Instance.gold += gold;

            GameObject goldObj = Instantiate(goldPre);
            goldObj.transform.SetParent(gameObject.transform.parent, true);
            goldObj.transform.position = transform.position;
            goldObj.transform.rotation = transform.rotation;
           

            GameObject dieObj = Instantiate(diePre);
            dieObj.transform.SetParent(gameObject.transform.parent, false);
            dieObj.transform.position = transform.position;
            dieObj.transform.rotation = transform.rotation;
            if (gameObject.GetComponent<Ef_PlayEffect>() != null)
            {
                gameObject. GetComponent<Ef_PlayEffect>().PlayEffect();
            }
            Destroy(gameObject);

            int audioIndex = 0;
            switch (type)
            {
               
                //case 3:
                //    audioIndex = Random.Range(0, 3);
                //    AudioManager.Instance.PlayEffectSound(AudioManager.Instance.fishSpeakClip[audioIndex]);
                //    break;
                //case 4:
                //    audioIndex = Random.Range(4, 7);
                //    AudioManager.Instance.PlayEffectSound(AudioManager.Instance.fishSpeakClip[audioIndex]);
                //    break;
                case 5:
                    audioIndex = Random.Range(8, 11);
                    AudioManager.Instance.PlayEffectSound(AudioManager.Instance.fishSpeakClip[audioIndex]);
                    break;
                case 6:
                    audioIndex = Random.Range(12, 16);
                    AudioManager.Instance.PlayEffectSound(AudioManager.Instance.fishSpeakClip[audioIndex]);
                    break;



            }

        }
    }
}
