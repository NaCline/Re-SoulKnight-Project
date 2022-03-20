using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    const float r = 0.04212f;
    GameObject Player,Weapon;
    public LayerMask wall,monsters;
    int BulletAttact;
    // Start is called before the first frame update
    void Start()
    {
       Player =  GameObject.Find("Player");
        Weapon = GameObject.Find("Weapon");
        BulletAttact = Weapon.GetComponent<Weapon>().WeaponAttact;
        if (Player.transform.localScale.x > 0)
            Direction = 1;
        else Direction = -1;
       // transform.Translate(new Vector3(0, 0.1f, 0));
        //调整位置以至美观
        StartCoroutine(BulletMove());
    }
    Vector3 point = new Vector3(r, r,0);
    float speed = 0.1f;
    int Direction;
    // Update is called once per frame
    IEnumerator BulletMove()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.1f, wall))
        {
            Destroy(gameObject);
        }
        else transform.Translate(new Vector3(Direction * speed , 0, 0));
        yield return new WaitForEndOfFrame();
        StartCoroutine(BulletMove());
    }
    /*void FixedUpdate()
    {
        bool IstouchWall= false;
        for (int i = 0; i < 100; i++)
        {
            transform.Translate(new Vector3(Direction * speed * Time.fixedDeltaTime/100, 0, 0));
            if (Physics2D.OverlapCircle(transform.position, 0.1f, wall)) IstouchWall = true;
        }
        transform.Translate(new Vector3(-Direction * speed * Time.fixedDeltaTime, 0, 0));
        if (IstouchWall)
        {
            Destroy(gameObject);
        }
        else transform.Translate(new Vector3(Direction * speed * Time.fixedDeltaTime, 0, 0));
    }*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BulletTouchDestry")
        {
            collision.gameObject.GetComponent<MonsterMove>().StartIETouchBullet(transform.eulerAngles.z, Direction);
            collision.gameObject.GetComponent<MonsterMove>().MonsterHP -= BulletAttact;
            Destroy(gameObject);
        }
    }
}
