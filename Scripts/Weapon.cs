using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform Player;
    const float StartX = 1.8f, StartY = -0.42f;
    public GameObject Bullet;
    public Sprite[] SpWeapon = new Sprite[2];
    Vector3 StartPos = new Vector3(StartX, StartY,0);
    public int WeaponAttact = 3;
    // Start is called before the first frame update
    void Start()
    {
        SpWeapon[0] = GetComponent<SpriteRenderer>().sprite;
    }
    int Direction;
    bool IsMouseDown = false;
    void FixedUpdate()
    {
        // 获取鼠标位置相对移动向量
        Vector2 translation = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            // 鼠标坐标默认是屏幕坐标，首先要转换到世界坐标
            // 物体坐标默认就是世界坐标
            // 两者取差得到方向向量
            Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Player.transform.position;
            // 方向向量转换为角度值
            //========================================================================

            float angle = 360 - Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
        //========================================================================
        // 将当前物体的角度设置为对应角度
        if (Player.transform.localScale.x > 0)
            Direction = 1;
        else Direction = -1;
        transform.Translate(new Vector3(-0.5f, 0, 0));
        transform.eulerAngles = new Vector3(0, 0, angle+90*Direction);
        transform.Translate(new Vector3(0.5f,0,0));
        if (Input.GetMouseButton(0)&&!IsMouseDown)
        {
            IsMouseDown = true;
            StartCoroutine(MouseDown());
            Instantiate(Bullet, transform.position, transform.rotation);
            PlayerEnergyDown();
        }
        GetComponent<SpriteRenderer>().sprite = SpWeapon[0];
        if (Input.GetKeyDown(KeyCode.R))
        {
            SwapWeapon();
        }
    }
    void PlayerEnergyDown()
    {
        Player.gameObject.GetComponent<Movement>().PlayerEnergy--;
    }
    IEnumerator MouseDown()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForEndOfFrame();
        }
        IsMouseDown = false;
    }
    void SwapWeapon()
    {
        Sprite a = SpWeapon[0];
        SpWeapon[0] = SpWeapon[1];
        SpWeapon[1] = a;
    }

    // Update is called once per frame
}
