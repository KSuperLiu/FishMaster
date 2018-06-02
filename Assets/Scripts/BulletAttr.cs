using UnityEngine;

public class BulletAttr : MonoBehaviour {
    public int speed;
    public int damage;
    public GameObject webPre;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Board")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Fish")
        {
            GameObject web = Instantiate(webPre);
            //和子弹同级
            web.transform.SetParent(gameObject.transform.parent, false);
            //不被ugui托管
            web.transform.position = gameObject.transform.position;
            web.transform.rotation = gameObject.transform.rotation;
            web.GetComponent<WebAttr>().damage = damage;
            Destroy(gameObject);

        }
    }
}
