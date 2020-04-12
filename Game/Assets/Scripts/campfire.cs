using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class campfire : MonoBehaviour
{
    public Slider weightSlider;
    public Slider foodSlider;
    public Slider healthSlider;
    public TextMeshProUGUI weightText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI healthText;
    public PlayerData player;
    public TextMeshProUGUI melee;

    public TextMeshProUGUI message;
    public TextMeshProUGUI health;
    public TextMeshProUGUI strength;
    public TextMeshProUGUI sight;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI stealth;
    public TextMeshProUGUI food;
    public TextMeshProUGUI curr_weight;
    public TextMeshProUGUI max_weight;

    public Button a1;
    public Button a2;
    public Button leave;

    public string options;


    public PlayerData data;
    public Weapons myweapon;
    public static int[] stats;
    public int BaseStrength;
    public int BaseSight;
    public int BaseSpeed;
    public int BaseStealth;
    public int BaseMaxWeight;
    public int sightBuff;
    public int strengthBuff;
    public int stealthBuff;
    public int speedBuff;
    public int maxWeightBuff;
    public float Food;
    public int Health;
    public Hashtable inventory;
    public Hashtable seenItems;
    public int seenFood;
    public bool hasKey;
    public float melee_dmg;

    public void setWeight()
    {
        player = SaveSystem.LoadPlayerData();
        int strength = SaveSystem.LoadStats()[0];
        int baseMaxWeight = (int)((int)50f + (50f * (strength / 10f))) + SaveSystem.LoadItem("backpack").max_weight * (int)player.inventory["backpack"];

        weightSlider.maxValue = baseMaxWeight;
        weightSlider.value = calcCurrWeight();
        weightText.text = calcCurrWeight().ToString() + "/" + weightSlider.maxValue.ToString();
    }

    public void setFood()
    {
        player = SaveSystem.LoadPlayerData();
        int strength = SaveSystem.LoadStats()[0];
        int baseMaxWeight = (int)((int)50f + (50f * (strength / 10f))) + SaveSystem.LoadItem("backpack").max_weight * (int)player.inventory["backpack"];

        foodSlider.maxValue = baseMaxWeight;
        foodSlider.value = Food;
        foodText.text = foodSlider.value.ToString() + "/" + foodSlider.maxValue.ToString();
    }

    public void setHealth()
    {
        player = SaveSystem.LoadPlayerData();
        healthSlider.maxValue = 50;
        healthSlider.value = Health;
        healthText.text = Health.ToString() + "/50";
    }


    public void setBuffs()
    {
        PlayerData player = SaveSystem.LoadPlayerData();

        sightBuff = 0;
        strengthBuff = 0;
        speedBuff = 0;
        stealthBuff = 0;
        maxWeightBuff = 0;

        foreach (string key in player.inventory.Keys)
        {
            int num_items = 0;
            num_items = (int)player.inventory[key];

            sightBuff += SaveSystem.LoadItem(key).sight * num_items;
            strengthBuff += SaveSystem.LoadItem(key).strength * num_items;
            speedBuff += SaveSystem.LoadItem(key).speed * num_items;
            stealthBuff += SaveSystem.LoadItem(key).stealth * num_items;
            maxWeightBuff += SaveSystem.LoadItem(key).max_weight * num_items;
        }
    }

    public void addSeenItems()
    {
        string[] keys = new string[inventory.Keys.Count];

        int i = 0;
        foreach (string key in inventory.Keys)
        {
            keys[i] = key;
            i++;
        }

        foreach (string key in keys)
        {
            inventory[key] = (int)inventory[key] + (int)seenItems[key];
        }
    }

    public float calcCurrWeight()
    {
        float temp_weight = 0f;

        temp_weight += myweapon.weight;
        temp_weight += Food;

        foreach (string key in inventory.Keys)
        {
            int num_items = 0;
            num_items = (int)inventory[key];

            temp_weight += num_items * SaveSystem.LoadItem(key).weight;
        }

        return temp_weight;
    }

    public void makeInventory()
    {
        seenItems.Add("bandages", 0);
        seenItems.Add("glasses", 0);
        seenItems.Add("camo", 0);
        seenItems.Add("backpack", 0);
        seenItems.Add("shoes", 0);
        seenItems.Add("flashlight", 0);
        seenItems.Add("steroids", 0);
        seenItems.Add("dumbell", 0);
    }

    public void setDefaultInventory()
    {
        seenItems["bandages"] = 0;
        seenItems["glasses"] = 0;
        seenItems["camo"] = 0;
        seenItems["backpack"] = 0;
        seenItems["shoes"] = 0;
        seenItems["flashlight"] = 0;
        seenItems["dumbell"] = 0;
    }

    public void loseItems()
    {
        inventory["bandages"] = 0;
        inventory["glasses"] = 0;
        inventory["camo"] = 0;
        inventory["backpack"] = 0;
        inventory["shoes"] = 0;
        inventory["flashlight"] = 0;
        inventory["dumbell"] = 0;
        inventory["steroids"] = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        seenItems = new Hashtable();
        makeInventory();
        setDefaultInventory();
        data = SaveSystem.LoadPlayerData();
        myweapon = SaveSystem.LoadWeapons(SaveSystem.LoadWeapon());
        stats = SaveSystem.LoadStats();
        BaseStrength = stats[0];
        BaseSight = stats[1];
        BaseSpeed = stats[2];
        BaseStealth = stats[3];
        if (BaseSight + sightBuff >= 6)
        {
            message.text = "As you're walking down the path you see smoke in the distance.  It appears to be coming from a campfire.  Maybe there are people there?  Do you want to check it out?";
        }
        message.text = "As you're walking down the path you see smoke in the distance.  Do you want to check it out?";
        a1.GetComponentInChildren<TextMeshProUGUI>().text = "Yes";
        a2.GetComponentInChildren<TextMeshProUGUI>().text = "No";
        Food = data.food;
        Health = data.health;
        inventory = data.inventory;
        setBuffs();
        melee_dmg = BaseStrength + myweapon.power + strengthBuff;
        BaseMaxWeight = (int)50f + (int)(50f * ((BaseStrength + strengthBuff) / 10f));
        melee_dmg = BaseStrength + myweapon.power + strengthBuff;
        BaseMaxWeight = (int)50f + (int)(50f * ((BaseStrength + strengthBuff) / 10f));
        //health.text = "Health: " + Health.ToString();
        strength.text = "Strength: " + (BaseStrength + strengthBuff).ToString();
        melee.text = "Weapon: " + myweapon.name;
        sight.text = "Sight: " + (BaseSight + sightBuff).ToString();
        speed.text = "Speed: " + (BaseSpeed + speedBuff).ToString();
        stealth.text = "Stealth: " + (BaseStealth + stealthBuff).ToString();
        options = "start";

        leave.enabled = false;
        leave.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            SceneManager.LoadScene(10);
        }
        setWeight();
        setFood();
        setHealth();
        //curr_weight.text = "Curr Weight: " + calcCurrWeight().ToString();
    }

    public void Option1()
    {
        if (options == "start")
        {
            message.text = "As you get closer, you know the smoke is coming from a campfire, but you don't see anyone.\n\nAll of a sudden you hear someone yell, 'Put your hands up!'  It was a trap.  The man grabs your " + myweapon.name + ", so you are unable to use it.\nWhat's your plan?";
            options = "run";
            strength.text = "Damage: " + (BaseStrength+strengthBuff).ToString();
            melee.text = "Weapon: NONE";
            
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Run";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "Comply with the man";
        }
        else if (options == "run")
        {
            if (BaseSpeed + speedBuff >= 6)
            {
                message.text = "You grabbed your " + myweapon.name + " and ran. You were quick enough to get away!";
                leave.enabled = true;
                leave.gameObject.SetActive(true);
                melee.text = "Weapon:" + myweapon.name;
                a1.enabled = false;
                a2.enabled = false;
                a1.gameObject.SetActive(false);
                a2.gameObject.SetActive(false);
            }
            else
            {
                message.text = "You weren't quick enough and the man shot you in the back.  You died";
                SaveSystem.SaveDeathMessage("You weren't quick enough and the man shot you in the back.  You died");
                Health = 0;
            }
        }
        else if (options == "comply")
        {
            if (BaseStrength + strengthBuff >= 6)
            {
                message.text = "You decide to fight back.  Before he can react, you knock the gun out of the man's hand.  In a panic, the man turns and runs away.  You inspect the gun, but appears to have been a fake gun this whole time.\nYou loot his camp and find 30 lbs of food, some camoflague and you retrieve your " + myweapon.name + ".";
                strength.text = "Damage: " + melee_dmg.ToString();
                melee.text = "Weapon: " + myweapon.name;
                Food = Food + 30;
                //food.text = "Food: " + Food.ToString();
                seenItems["camo"] = (int)seenItems["camo"] + 1;
                leave.enabled = true;
                leave.gameObject.SetActive(true);
                a1.enabled = false;
                a2.enabled = false;
                a1.gameObject.SetActive(false);
                a2.gameObject.SetActive(false);
            }
            else
            {
                message.text = "You attempt to knock the gun out of his hand, but you are not strong enough.  He shoots you through the skull.";
                SaveSystem.SaveDeathMessage("You attempt to knock the gun out of his hand, but you are not strong enough.  He shoots you through the skull.");

                Health = 0;
                leave.enabled = true;
                leave.gameObject.SetActive(true);
                a1.enabled = false;
                a2.enabled = false;
                a1.gameObject.SetActive(false);
                a2.gameObject.SetActive(false);
            }
        }
    }

    public void Option2()
    {
        if (options == "start")
        {
            returnToRoad();
        }
        else if (options == "run")
        {
            message.text = "The man says, 'Drop everything and you'll be free to leave'  What's your plan?";
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Attempt to overpower him";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "Comply with the man";
            options = "comply";
        }
        else if (options == "comply")
        {
            if (Food > 5)
            {
                message.text = "You comply with the man and give him all of your things.  He allows" + " you to keep your " + myweapon.name + " and 5 lbs of food";
                Food = 5;
                //food.text = "Food: " + Food.ToString();
            }
            else
            {
                Food = (float)Math.Floor(Food);
                message.text = "You comply with the man and give him all of your things.  He allows" + " you to keep your " + myweapon.name + " and " + Food.ToString() + " lbs of food";
            }
            //Need to remove everything else from inventory
            loseItems();


            leave.enabled = true;
            leave.gameObject.SetActive(true);
            a1.enabled = false;
            a2.enabled = false;
            a1.gameObject.SetActive(false);
            a2.gameObject.SetActive(false);
        }
    }

    public void returnToRoad()
    {
        //Save All Changes Here
        //Also need to figure out how to change location of kid so he cant visit house again
        Landmarks curr_landmark = SaveSystem.LoadLandmarks(SaveSystem.LoadLandmark());
        PlayerData savedPlayer = SaveSystem.LoadPlayerData();

        savedPlayer.curr_weight = calcCurrWeight();
        savedPlayer.health = Health;
        savedPlayer.food = Food;
        addSeenItems();
        savedPlayer.inventory = inventory;

        SaveSystem.SaveLandmark("3");
        savedPlayer.x = curr_landmark.out_position;
        SaveSystem.SavePlayerData(savedPlayer);


        SceneManager.LoadScene("Kirnys");
    }
}
