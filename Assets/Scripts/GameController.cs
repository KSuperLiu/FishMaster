using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {
    private static GameController _instance;
    public static GameController Instance
    {
        get
        {
            return _instance;
        }
    }

    public Sprite[] bgSprites;
    public Image bgImage;
    public GameObject seaWaveEff;

    public GameObject goldEffect;
    public GameObject FireEffect;
    public GameObject changeFireEff;
    public GameObject lvUPEffect;


    public GameObject shengji;
    public Color goldColor;
    public Text goldText;
    public Text lvText;
    public Text lvNameText;
    public Text smallCountdownText;
    public Text bigCountdownText;
    public Button bigCountdownButton;
    public Button backButton;
    public Button settingButton;
    public Slider slide;


    public int lv = 1;  //用户等级
    public int exp = 0;
    public int gold = 1000;  //初始化钱
    public const float bigCountdown = 240;
    public const float smallCountdown = 60;
    public float bigTmer = bigCountdown;
    public float smallTimer = smallCountdown ;
    public string[] lvName={"新手","入门","钢铁","青铜","白银","黄金","白金","钻石","大师","王者"};

    public Text costText; 
    public GameObject[] Bullet1Gos;
    public GameObject[] Bullet2Gos;
    public GameObject[] Bullet3Gos;
    public GameObject[] Bullet4Gos;
    public GameObject[] Bullet5Gos;
    public GameObject[] guns;  //炮
    public int costIndex = 0;
    private int[] oneShootCostS = { 5,10,20,30,  40,50,60,70,  80,90,100 ,200,  300,400,500,600,  700,800,900,1000};
    public Transform bulletHolder;



    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {

        gold = PlayerPrefs.GetInt("gold", gold);
        lv = PlayerPrefs.GetInt("lv", lv);
        smallTimer = PlayerPrefs.GetFloat("smallTimer", smallTimer);
        bigTmer = PlayerPrefs.GetFloat("bigTmer", bigTmer);
        exp = PlayerPrefs.GetInt("exp", exp);
        costIndex= PlayerPrefs.GetInt("costIndex", costIndex);
        print("costIndex:" + costIndex);
        InitUI();
    }


    private void Update()
    {
        InitUI();
        ChangeBulletCost();  //滚轮控制
        Fire();
        ChangeBg();
    }

    public int bgIndex=0;
    void ChangeBg()
    {
        //0 !=1/20=0 就是1~20级都满足为0 到达20级后 lv/20=1 
        if(bgIndex != lv / 20)
        {
            bgIndex = lv / 20; //1
           
            print("bgIndex的值为：" + bgIndex);
            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.seaWaveClip);
            Instantiate(seaWaveEff);
            if (bgIndex > 3)
            {
                bgImage.sprite = bgSprites[3];
            }
            else
            {
                bgImage.sprite = bgSprites[bgIndex];
            }
          
        }
    }

    private void InitUI()
    {
      //经验等级换算公式
      while(exp> 1000 + lv * 100)
        {
            //限定最高99级
            if (lv < 99) {
            lv++;

            shengji.SetActive(true);
            shengji.transform.Find("Text").GetComponent<Text>().text = lv.ToString();
            StartCoroutine(shengji.GetComponent<Ef_HideSelf>().Hideself());

            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.lvUpClip);
            Instantiate(lvUPEffect);
            }

            exp -= (1000 + lv * 100);
        }
        goldText.text = "$" + gold;
        lvText.text = lv+"" ;
        lvNameText.text = lvName[lv / 10];
        slide.value = ((float)exp) / (1000 + lv * 200);
        if (lv == 99) slide.value = 1f;

        smallTimer -= Time.deltaTime;
        bigTmer -= Time.deltaTime;
        smallCountdownText.text = (int)smallTimer + "";
        bigCountdownText.text = (int)bigTmer + "S";
        if (bigTmer<=0 && bigCountdownButton.gameObject.activeSelf ==false)
        {
            bigCountdownText.gameObject.SetActive(false);
            bigCountdownButton.gameObject.SetActive(true);

        }
        if (smallTimer <= 0)
        {
            smallTimer = smallCountdown;
            gold+=50;

        }

        //初始化应该显示那个炮 显示一炮打多少金币
        costText.text = "$" + oneShootCostS[costIndex];
        switch (costIndex / 4)
        {
            case 0: guns[0].SetActive(true); guns[1].SetActive(false); guns[2].SetActive(false); guns[3].SetActive(false); guns[4].SetActive(false); break;
            case 1: guns[0].SetActive(false); guns[1].SetActive(true); guns[2].SetActive(false); guns[3].SetActive(false); guns[4].SetActive(false); break;
            case 2: guns[0].SetActive(false); guns[1].SetActive(false); guns[2].SetActive(true); guns[3].SetActive(false); guns[4].SetActive(false); break;
            case 3: guns[0].SetActive(false); guns[1].SetActive(false); guns[2].SetActive(false); guns[3].SetActive(true); guns[4].SetActive(false); break;
            case 4: guns[0].SetActive(false); guns[1].SetActive(false); guns[2].SetActive(false); guns[3].SetActive(false); guns[4].SetActive(true); break;
        }
    }

    void ChangeBulletCost()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            SubGun();
        }
        if (Input.GetAxis("Mouse ScrollWheel")>0){
            AddGun();
        }
    }
    public void AddGun()
    {
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.changeClip);
        GameObject go = Instantiate(changeFireEff);
        go.transform.SetParent(GameObject.Find("Order90Canvas").transform, false);
      
        guns[costIndex / 4].SetActive(false);
        costIndex++;
        costIndex = (costIndex >= oneShootCostS.Length) ? 0 : costIndex;
        guns[costIndex / 4].SetActive(true);
        costText.text = "$" + oneShootCostS[costIndex];
    }

    public void SubGun()
    {
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.changeClip);
        GameObject go = Instantiate(changeFireEff);
        go.transform.SetParent(GameObject.Find("Order90Canvas").transform, false);

        guns[costIndex / 4].SetActive(false);
        costIndex--;
        costIndex = (costIndex<0) ? oneShootCostS.Length-1 : costIndex;
        guns[costIndex / 4].SetActive(true);
        costText.text = "$" + oneShootCostS[costIndex];
    }


    public void OnBigReward()
    {
        AudioManager.Instance.PlayEffectSound(AudioManager.Instance.goldClip);
        gold += 500;
        bigCountdownText.gameObject.SetActive(true);
        bigCountdownButton.gameObject.SetActive(false);
        bigTmer = bigCountdown;

        GameObject go =Instantiate(goldEffect);
        go.transform.SetParent(GameObject.Find("Order90Canvas").transform, false);
    }


    void Fire()
    {
       
        GameObject[] useBullet=Bullet5Gos;
        int bulletIndex; //子弹开火应该长什么样
        if (Input.GetMouseButtonDown(0)&& EventSystem.current.IsPointerOverGameObject() ==false)//让点在UI上面不开火
        {
            //如果钱不够打炮  停止
            if (gold < oneShootCostS[costIndex])
            {
                print("钱不够了");
                StartCoroutine(GoldNotEnough());
                return;
            }

          
            //炮越大  子弹颜色越不一样
            switch (costIndex / 4)
            {
                case 0:useBullet = Bullet1Gos;break;
                case 1: useBullet = Bullet2Gos; break;
                case 2: useBullet = Bullet3Gos; break;
                case 3: useBullet = Bullet4Gos; break;
                case 4: useBullet = Bullet5Gos; break;
            }
            //用户等级越高 子弹也不一样
            bulletIndex = lv % 10;
            GameObject go=Instantiate(useBullet[bulletIndex]);
            go.transform.SetParent(bulletHolder, false);
            //位置保持一致 子弹的Transform不属于UGUI 不能用 go.transform.localPosition 
            go.transform.position = guns[costIndex / 4].transform.Find("FirePos").transform.position;
            go.transform.rotation = guns[costIndex / 4].transform.Find("FirePos").transform.rotation;
            //让只有一半伤害
            go.GetComponent<BulletAttr>().damage =(int) (oneShootCostS[costIndex] *2);
            go.AddComponent<AutoMove>().vec = Vector3.up;
            go.GetComponent<AutoMove>().speed = go.GetComponent<BulletAttr>().speed;


            AudioManager.Instance.PlayEffectSound(AudioManager.Instance.FireClip);
            //加特效
            GameObject goeff = Instantiate(FireEffect);
            goeff.transform.SetParent(guns[costIndex / 4].transform, false);
            goeff.transform.position = guns[costIndex / 4].transform.Find("FirePos").transform.position;
            goeff.transform.rotation = guns[costIndex / 4].transform.Find("FirePos").transform.rotation;
            //扣钱
            gold -= oneShootCostS[costIndex];
        }
    }


    //钱不够了提示
    IEnumerator GoldNotEnough()
    {
        goldText.color = goldColor;
        goldText.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        goldText.color = goldColor;
    }
}
