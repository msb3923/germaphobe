using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class abandonedhouse : MonoBehaviour
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

    public bool lured = false;

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
    public string death_message;


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

    // Start is called before the first frame update
    void Start()
    {
        a1.enabled = true;
        a2.enabled = true;
        a1.gameObject.SetActive(true);
        a2.gameObject.SetActive(true);
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
        message.text = "You enter the abandoned house and instantly see 5 zombies in the next room!\nWhat do you do?";
        a1.GetComponentInChildren<TextMeshProUGUI>().text = "Lure Zombies Away with 5lb of Food";
        a2.GetComponentInChildren<TextMeshProUGUI>().text = "Carefully Sneak Upstairs";
        Food = data.food;
        Health = data.health;
        inventory = data.inventory;
        setBuffs();
        melee_dmg = BaseStrength + myweapon.power + strengthBuff;
        BaseMaxWeight = (int)50f + (int)(50f * ((BaseStrength + strengthBuff) / 10f));
        //health.text = "Health: " + Health.ToString();
        strength.text = "Damage: " + (melee_dmg).ToString();
        melee.text = "Weapon: " + myweapon.name;
        sight.text = "Sight: " + (BaseSight + sightBuff).ToString();
        speed.text = "Speed: " + (BaseSpeed + speedBuff).ToString();
        stealth.text = "Stealth: " + (BaseStealth + stealthBuff).ToString();
        
        //food.text = "Food: " + Food.ToString();
        //curr_weight.text = "Curr Weight: " + calcCurrWeight().ToString();
        //max_weight.text = "Max Weight: " + (BaseMaxWeight+maxWeightBuff).ToString();
        options = "start";
        
        if (Food < 5)
        {
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Leave";
        }
        leave.enabled = false;
        leave.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        setWeight();
        setFood();
        setHealth();
    }

    public void Option1()
    {
        if (options == "start")
        {
            if (a1.GetComponentInChildren<TextMeshProUGUI>().text == "Leave")
            {
                returnToRoad();
            }
            else
            {
                Food = Food - 5;
                //food.text = "Food: " + Food.ToString();
                message.text = "You Lured the Zombies Away!\n You found a bandage\nYou go to the second floor and see a chest across the room, but the floor looks weak.  What should you do?";
                options = "creaky floor";
                a1.GetComponentInChildren<TextMeshProUGUI>().text = "Attempt to cross floor";
                a2.GetComponentInChildren<TextMeshProUGUI>().text = "Leave";
                seenItems["bandages"] = (int)seenItems["bandages"] + 1;
                lured = true;
            }
        }
        else if (options == "creaky floor")
        {
            if (calcCurrWeight() > (BaseMaxWeight/2))
            {
                if (lured)
                {
                    message.text = "You fell, but luckily the zombies were lured away.  Your leg was injured so your health took a 10 point hit, and your speed was reduced by 1";
                    Health = Health - 10;
                    BaseSpeed = BaseSpeed - 1;
                    //health.text = "Health: " + Health.ToString();
                    speed.text = "Speed: " + (BaseSpeed + speedBuff).ToString();
                    leave.enabled = true;
                    leave.gameObject.SetActive(true);
                    a1.enabled = false;
                    a2.enabled = false;
                    a1.gameObject.SetActive(false);
                    a2.gameObject.SetActive(false);
                    if (Health <= 0)
                    {
                        death_message = "You fell and your health took a 10 point hit, which killed you";
                        SaveSystem.SaveDeathMessage(death_message);
                        SceneManager.LoadScene(10);
                    }
    
                }
                else
                {
                    message.text = "You fell through and the zombies ate you! If only you had lured them away!";
                    //Need to add an exit button to show that you were killed
                    leave.enabled = true;
                    leave.gameObject.SetActive(true);
                    a1.enabled = false;
                    a2.enabled = false;
                    a1.gameObject.SetActive(false);
                    a2.gameObject.SetActive(false);
                    Health = 0;
                    //health.text = "Health: " + Health.ToString();
                    death_message = "You fell through and the zombies ate you! If only you had lured them away!";
                    SaveSystem.SaveDeathMessage(death_message);
                    SceneManager.LoadScene(10);
                }
            }
            else
            {
                message.text = "You cross the floor and found 2 bandages, running shoes, 20 pounds of food";
                leave.enabled = true;
                leave.gameObject.SetActive(true);
                a1.enabled = false;
                a2.enabled = false;
                a1.gameObject.SetActive(false);
                a2.gameObject.SetActive(false);
                seenItems["bandages"] = (int)seenItems["bandages"] + 2;
                seenItems["shoes"] = (int)seenItems["shoes"] + 1;
                Food += 20;
                //food.text = "Food: " + Food.ToString();
                hasKey = true;
                //Need to exit house button as wells
            }
        }
    }

    public void Option2()
    {
        if (options == "start")
        {
            if (BaseStealth+stealthBuff >= 6)
            {
                message.text = "You successfully snuck upstairs past the zombies.\nYou go to the second floor and see a chest across the room, but the floor looks weak.  What should you do?";
                a1.GetComponentInChildren<TextMeshProUGUI>().text = "Attempt to cross floor";
                a2.GetComponentInChildren<TextMeshProUGUI>().text = "Leave";
                options = "creaky floor";
            }
            else
            {
                message.text = "The zombies noticed you, and you were forced to leave and you lost 15 health";
                Health = Health - 15;
                //health.text = "Health: " + Health.ToString();
                leave.enabled = true;
                leave.gameObject.SetActive(true);
                a1.enabled = false;
                a2.enabled = false;
                a1.gameObject.SetActive(false);
                a2.gameObject.SetActive(false);
                if (Health <= 0)
                {
                    death_message = "Zombies noticed you and you lost 15 health, which killed you";
                    SaveSystem.SaveDeathMessage(death_message);
                }
                //Need button to exit scene
            }
        }
        else if (options == "creaky floor")
        {
            returnToRoad();
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

        SaveSystem.SaveLandmark("1");
        savedPlayer.x = curr_landmark.out_position;
        SaveSystem.SavePlayerData(savedPlayer);


        SceneManager.LoadScene("Kirnys");
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
}

