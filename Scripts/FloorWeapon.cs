using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    bool IsAppear = false;
    public LayerMask PlayerL;
    public GameObject FloorWeapons;
    GameObject Player;
    Weapon Weapon;
    GameObject PlayerTouchObj;
    public GameObject TextCanvas;
    Sprite PlayerTouchWeapon;
    bool IsTouchPlayer  = false;
    void Start()
    {
        //transform.position = new Vector3(-0.009f, -0.075f,0);
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        //gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
        Player = GameObject.Find("Player");
        Weapon = GameObject.Find("Weapon").GetComponent<Weapon>();
        TextCanvas.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (IsAppear)
        {
            if (Physics2D.OverlapCircle(transform.position, 1f, PlayerL))
            {
                TextCanvas.SetActive(true);
                if (PlayerTouchWeapon == null)
                {
                    PlayerTouchWeapon = GetComponent<SpriteRenderer>().sprite;
                    PlayerTouchObj = gameObject;
                }
                IsTouchPlayer = true;
            }
            else TextCanvas.SetActive(false);

            if (IsTouchPlayer && !Physics2D.OverlapCircle(transform.position, 1f, PlayerL))
            {
                IsTouchPlayer = false;
                PlayerTouchWeapon = null;
                PlayerTouchObj = null;
            }
            if (IsTouchPlayer && PlayerTouchObj == gameObject)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    SwitchWeapon();
                }
            }
        }
    }

    public void ChestOpen()
    {
        StartCoroutine(ProgressiveAppear());
    }
    IEnumerator ProgressiveAppear()
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(color.r, color.g, color.b, 0);
        float a = transform.position.y;
        for (int i = 0; i < 40; i++)
        {
            gameObject.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255/40);
            transform.position += new Vector3(0,(0.25f-0.075f-(transform.position.y-a))/15,0);
            yield return new WaitForEndOfFrame();
        }
        IsAppear = true;
    }
    public void SwitchWeapon()
    {
        if(Weapon.SpWeapon[1] == null)
        {
            Weapon.SpWeapon[1] = Weapon.SpWeapon[0];
            Weapon.SpWeapon[0] = GetComponent<SpriteRenderer>().sprite;
            Destroy(gameObject);
        }
        else
        {
            Sprite a = GetComponent<SpriteRenderer>().sprite;
            GetComponent<SpriteRenderer>().sprite = Weapon.SpWeapon[0];
            Weapon.SpWeapon[0] = a;
            GameObject obj = Instantiate(FloorWeapons,Player.transform.position,Quaternion.identity);
            obj.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            obj.GetComponent<FloorWeapon>().IsAppear = true;
            Destroy(gameObject);
        }
    }
}
