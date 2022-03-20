using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWeapon : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles +=  new Vector3(0,0,(-transform.eulerAngles.z + Player.transform.eulerAngles.z)/30);
    }
    public void Attact()
    {

    }
}
