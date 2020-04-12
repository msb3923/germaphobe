using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class arena : MonoBehaviour
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
        Food = data.food;
        message.text = "A dark figure grabs you and throws you into a cell. Your weapon is gone. Inside, a crazed looking man is glaring at you.\nWhat do you want to do?";
        if (Food > 5)
        {
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Give them 5 lbs of food";
        }
        else
        {
            a1.enabled = false;
            a1.gameObject.SetActive(false);
        }
        a2.GetComponentInChildren<TextMeshProUGUI>().text = "Ignore them";
        Health = data.health;
        inventory = data.inventory;
        setBuffs();
        melee_dmg = BaseStrength + myweapon.power + strengthBuff;
        BaseMaxWeight = (int)50f + (int)(50f * ((BaseStrength + strengthBuff) / 10f));
        //health.text = "Health: " + Health.ToString();
        strength.text = "Damage: " + (BaseStrength).ToString();
        melee.text = "Weapon: NONE";
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
    }

    public void Option1()
    {
        if (options == "start")
        {
            message.text = "The man scarf down the food.  He looks content.\n\nThe dark figure returns. He grabs you and starts dragging you down a hallway.\nWhat do you want to do?";
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Try to Escape";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "Comply";
            options = "escape";
            Food = Food - 5;
            //food.text = "Food: " + Food.ToString();
        }
        else if (options == "escape")
        {
            if ((BaseSpeed + speedBuff >=7) & (BaseStealth + stealthBuff >=7))
            {
                message.text = "You broke free and ran away from the figure";
                a2.enabled = false;
                a2.gameObject.SetActive(false);
                a1.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
                options = "square";
            }
            else
            {
                message.text = "The figure slams you against a wall, costing you 20 Health.\n\nYou reach your destination and the figure throws you into a large dirt arena.  You see your " + myweapon.name + " on the ground and pick it up.";
                Health = Health - 20;
                SaveSystem.SaveDeathMessage("The figure slamed you into the wall, costing you 20 Health");
                //health.text = "Health: " + Health.ToString();
                strength.text = "Damage: " + (melee_dmg).ToString();
                melee.text = "Weapon: " + myweapon.name;
                a2.enabled = false;
                a2.gameObject.SetActive(false);
                a1.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
                options = "zombie";
            }
        }
        else if (options == "zombie")
        {
            a2.enabled = true;
            a2.gameObject.SetActive(true);
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "Run and stall";
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Attack the zombies";
            message.text = "Suddenly a pair of zombies emerge from a hidden door.  They start running towards you.  What's your plan?";
            options = "attack";
        }
        else if (options == "attack")
        {
            a2.enabled = false;
            a2.gameObject.SetActive(false);
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
            if (melee_dmg > 6) // Should change to incorporate strength
            {
                message.text = "You strike and kill the zombies";
                options = "triangle";
            }
            else
            {
                message.text = "Your attacks aren't strong enough and the zombies hit you for 20 health points.";
                Health = Health - 20;
                SaveSystem.SaveDeathMessage("The Attacks from the zombies cost you 20 health");
                //health.text = "Health: " + Health.ToString();
                if (BaseSight + sightBuff > 4)
                {
                    message.text = message.text + "  You notice a trap, and lure the zombies there, killing them";
                    options = "triangle";
                }
                else
                {
                    message.text = message.text + "\n\nBut with nowhere to run in sight, the zombies cornered and ate you.";
                    SaveSystem.SaveDeathMessage(message.text);
                    Health = 0;
                }
                
            }
        }
        else if (options == "triangle")
        {
            message.text = "You look around and notice a hole where you can escape the arena.  What do you do?";
            a2.enabled = true;
            a2.gameObject.SetActive(true);
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Run for it";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "Stay in the center of the arena";
            options = "run for it";
        }
        else if (options == "run for it")
        {
            options = "stay";
            message.text = "A zombie emerges from a trap door near the hole and hits you for 10 health points.";
            Health = Health - 10;
            SaveSystem.SaveDeathMessage("A zombie emerged from a trap door and attacks you.  This cost you 10 Health");
            //health.text = "Health: " + Health.ToString();
            a2.enabled = false;
            a2.gameObject.SetActive(false);
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        }
        else if (options == "stay")
        {
            a2.enabled = true;
            a2.gameObject.SetActive(true);
            message.text = "A massive door opens, revealing a huge zombie bear.  Behind it a huge pile of food and survial equipment.  What do you want to do?";
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Charge the bear";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "Taunt the bear";
            options = "bear";
        }
        else if (options == "bear")
        {
            if ((BaseSpeed + speedBuff >=5) & (melee_dmg >= 8))
            {
                message.text = "You catch the bear off guard and kill it";
            }
            else if ((BaseSpeed + speedBuff >=5) & (melee_dmg >= 10))
            {
                message.text = "The bear charges you, but you heroically slaughter it.";
            }
            else
            {
                SaveSystem.SaveDeathMessage("The bear was too strong and he slaughted you.");
                Health = 0;
            }
            options = "perks";
            a2.enabled = false;
            a2.gameObject.SetActive(false);
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
        }
        else if (options == "perks")
        {
            message.text = "In a backpack you find, 50 lbs of food, 20 bandages, 4 syringes of steroids, and 1 dumbell.\nFrom this experience you've also gained +1 Strength, +1 Stealth, +1 Speed, and +1 Sight";
            a1.enabled = false;
            a1.gameObject.SetActive(false);
            a2.enabled = false;
            a2.gameObject.SetActive(false);
            leave.enabled = true;
            leave.gameObject.SetActive(true);
            Food = Food + 50;
            //food.text = "Food: " + Food.ToString();
            seenItems["bandages"] = (int)seenItems["bandages"] + 20;
            seenItems["steroids"] = (int)seenItems["steroids"] + 4;
            seenItems["dumbell"] = (int)seenItems["dumbell"] + 1;
            seenItems["backpack"] = (int)seenItems["backpack"] + 1;
            BaseStrength += 1;
            BaseSpeed += 1;
            BaseSight += 1;
            BaseStealth += 1;
            BaseMaxWeight = (int)50f + (int)(50f * ((BaseStrength + strengthBuff) / 10f));
            //health.text = "Health: " + Health.ToString();
            melee_dmg = BaseStrength + myweapon.power + strengthBuff;
            strength.text = "Damage: " + (melee_dmg).ToString();
            sight.text = "Sight: " + (BaseSight + sightBuff).ToString();
            speed.text = "Speed: " + (BaseSpeed + speedBuff).ToString();
            stealth.text = "Stealth: " + (BaseStealth + stealthBuff).ToString();
            //food.text = "Food: " + Food.ToString();
            //curr_weight.text = "Curr Weight: " + calcCurrWeight().ToString();
            //max_weight.text = "Max Weight: " + (BaseMaxWeight + maxWeightBuff + 20).ToString();
        }
        else if (options == "square")
        {
            a2.enabled = false;
            a2.gameObject.SetActive(false);
            message.text = "While wandering you sneak up onto the skybox of a large dirt arena and watch as a zombie bear mauls the dark figure, and then falls into a trap and dies.";
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
            options = "next";
        }
        else if (options == "next")
        {
            a2.enabled = true;
            a2.gameObject.SetActive(true);
            message.text = "You notice a massive open door with lots of survival gear on the other side.  What do you want to do?";
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Go through the door";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "Find another way out";
            options = "door";
        }
        else if (options == "door")
        {
            message.text = "In a backpack you find, 50 lbs of food, 5 bandages, 4 syringes of steroids, and 1 dumbell.\nFrom this experience you've also gained +1 Strength, +1 Stealth, +1 Speed, and +1 Sight";
            a1.enabled = false;
            a1.gameObject.SetActive(false);
            a2.enabled = false;
            a2.gameObject.SetActive(false);
            leave.enabled = true;
            leave.gameObject.SetActive(true);
            Food = Food + 50;
            //food.text = "Food: " + Food.ToString();
            seenItems["bandages"] = (int)seenItems["bandages"] + 5;
            seenItems["steroids"] = (int)seenItems["steroids"] + 4;
            seenItems["dumbell"] = (int)seenItems["dumbell"] + 1;
            seenItems["backpack"] = (int)seenItems["backpack"] + 1;
            BaseStrength += 1;
            BaseSpeed += 1;
            BaseSight += 1;
            BaseStealth += 1;
            BaseMaxWeight = (int)50f + (int)(50f * ((BaseStrength + strengthBuff) / 10f));
            //health.text = "Health: " + Health.ToString();
            strength.text = "Damage: " + (melee_dmg).ToString();
            sight.text = "Sight: " + (BaseSight + sightBuff).ToString();
            speed.text = "Speed: " + (BaseSpeed + speedBuff).ToString();
            stealth.text = "Stealth: " + (BaseStealth + stealthBuff).ToString();
            //food.text = "Food: " + Food.ToString();
            //curr_weight.text = "Curr Weight: " + calcCurrWeight().ToString();
            //max_weight.text = "Max Weight: " + (BaseMaxWeight + maxWeightBuff + 20).ToString();
        }
    }

    public void Option2()
    {
        if (options == "start")
        {
            if ((BaseStrength + strengthBuff) >= 4)
            {
                message.text = "The man swipes at you, but you grab his had, crushing his brittle bones.";
            }
            else if ((BaseSpeed + speedBuff) >= 7 & BaseStealth + stealthBuff >= 4)
            {
                message.text = "The man swipes at you, but you swiftly dodge. His hand hits the bar and he cowers.";
            }
            else
            {
                message.text = "The man swipes at you, causing you to lose 10 health and all your food";
                Food = 0;
                Health = Health - 10;
                SaveSystem.SaveDeathMessage(message.text);
                //food.text = "Food: " + Food.ToString();
                //health.text = "Health: " + Health.ToString();

            }
            a1.enabled = true;
            a1.gameObject.SetActive(true);
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Try to Escape";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "Comply";
            options = "escape";
            message.text = message.text + "\n\nThe dark figure returns. He grabs you and starts dragging you down a hallway.\nWhat do you want to do?";
        }
        else if (options == "escape")
        {
            message.text = "You reach your destination and the figure throws you into a large dirt arena.  You see your " + myweapon.name + " on the ground and pick it up.";
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
            melee.text = "Weapon: " + myweapon.name;
            strength.text = "Damage: " + (melee_dmg).ToString();

            a2.enabled = false;
            a2.gameObject.SetActive(false);
            options = "zombie";
        }
        else if (options == "attack")
        {
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
            a2.enabled = false;
            a2.gameObject.SetActive(false);
            if (BaseSpeed + speedBuff >=6)
            {
                message.text = "You're faster than the zombies, and have time to scan the arena.";
                if (BaseSight + sightBuff > 4)
                {
                    message.text = message.text + "  You notice a trap, and lure the zombies there, killing them";
                    options = "triangle";
                }
                else
                {
                    message.text = message.text + "\n\nBut with nowhere to run in sight, the zombies cornered and ate you.";
                    SaveSystem.SaveDeathMessage(message.text);
                    Health = 0;
                }
            }
            else
            {
                SaveSystem.SaveDeathMessage("You were unable to outrun the zombies and they ate you.");
                Health = 0;
            }
        }
        else if (options == "run for it")
        {
            a2.enabled = true;
            a2.gameObject.SetActive(true);
            message.text = "A massive door opens, revealing a huge zombie bear.  Behind it a huge pile of food and survial equipment.  What do you want to do?";
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Charge the bear";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "Taunt the bear";
            options = "bear";
        }
        else if (options == "bear")
        {
            if ((BaseStealth + stealthBuff >=5) & (BaseSpeed + speedBuff >= 8))
            {
                message.text = "You evade the bear and exit the arena through the massive door, closing it behind you";
                options = "perks";
                a2.enabled = false;
                a2.gameObject.SetActive(false);
                a1.GetComponentInChildren<TextMeshProUGUI>().text = "Continue";
            }
            else
            {
                SaveSystem.SaveDeathMessage("The bear wasn't pleased with your taunting and he slaughters you.");
                Health = 0;
            }
        }
        else if (options == "door")
        {
            message.text = "You were able to find a way out.\nFrom this experience you've also gained +1 Strength, +1 Stealth, +1 Speed, and +1 Sight";
            BaseStrength += 1;
            BaseSpeed += 1;
            BaseSight += 1;
            BaseStealth += 1;
            BaseMaxWeight = (int)50f + (int)(50f * ((BaseStrength + strengthBuff) / 10f));
            //health.text = "Health: " + Health.ToString();
            strength.text = "Damage: " + (melee_dmg).ToString();
            sight.text = "Sight: " + (BaseSight + sightBuff).ToString();
            speed.text = "Speed: " + (BaseSpeed + speedBuff).ToString();
            stealth.text = "Stealth: " + (BaseStealth + stealthBuff).ToString();
            //food.text = "Food: " + Food.ToString();
            //curr_weight.text = "Curr Weight: " + calcCurrWeight().ToString();
            //max_weight.text = "Max Weight: " + (BaseMaxWeight + maxWeightBuff + 20).ToString();
            a1.enabled = false;
            a1.gameObject.SetActive(false);
            a2.enabled = false;
            a2.gameObject.SetActive(false);
            leave.enabled = true;
            leave.gameObject.SetActive(true);
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

        SaveSystem.SaveLandmark("5");
        savedPlayer.x = curr_landmark.out_position;
        SaveSystem.SavePlayerData(savedPlayer);


        SceneManager.LoadScene("Kirnys");
    }
}
