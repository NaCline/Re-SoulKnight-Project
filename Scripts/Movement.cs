using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    Vector2 movement;
    public float speed;
    public Text HpText, HdText, EnerText;
    public GameObject RectHP, RectHD, RectEner;
    const int PlayerHPMax = 6;
    const int PlayerHDMax = 5;
    const int PlayerEnergyMax = 180;
    [Header("角色状态")]
    public int PlayerHP = 6;
    public int PlayerHD = 5;
    public int PlayerEnergy = 180;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x != 0)
        {
            transform.localScale = new Vector3(movement.x* 0.166953f, transform.localScale.y, 0);
        }
        HpText.text = PlayerHP.ToString() + "/" + PlayerHPMax.ToString();
        HdText.text = PlayerHD.ToString() + "/" + PlayerHDMax.ToString();
        EnerText.text = PlayerEnergy.ToString() + "/" + PlayerEnergyMax.ToString();
        RectHP.GetComponent<RectTransform>().sizeDelta = new Vector2(90.58f/PlayerHPMax * PlayerHP, 10.943f);
        RectHD.GetComponent<RectTransform>().sizeDelta = new Vector2(90.58f / PlayerHDMax * PlayerHD, 10.943f);
        RectEner.GetComponent<RectTransform>().sizeDelta = new Vector2(90.58f / PlayerEnergyMax * PlayerEnergy, 10.943f);
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        anim.SetFloat("Speed", movement.magnitude);
    }
}
