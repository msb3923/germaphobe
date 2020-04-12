using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Sprite sword;
    public Sprite shield;
    public Sprite chainsaw;
    public SpriteRenderer myRenderer;
    public Transform myTransform;
    private string weapon_name;
    public static int power;
    public static int range;
    public static int noise;
    public static int weight;
    public Weapons myweapon;

    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>();
        myTransform = GetComponent<Transform>();

        myweapon = SaveSystem.LoadWeapons(SaveSystem.LoadWeapon());


        if (SaveSystem.LoadWeapon() == "bat")
        {
            myRenderer.sprite = sword;
        }
        else if(SaveSystem.LoadWeapon() == "trash can lid")
        {
            myRenderer.sprite = shield;
        }
        else
        {
            myRenderer.sprite = chainsaw;
        }


        power = myweapon.power;
        range = myweapon.range;
        noise = myweapon.noise;
        weight = myweapon.weight;
    }

    // Update is called once per frame
    void Update()
    {
        myTransform.position = new Vector3(GameObject.FindGameObjectWithTag("Joe").GetComponent<Transform>().position.x-13.5f, myTransform.position.y, myTransform.position.z);
    }
}
