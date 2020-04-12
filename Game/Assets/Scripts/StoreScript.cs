using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoreScript : MonoBehaviour
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

    public float time;
    public string opposite_store_choice;
    public bool safe = false;


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
        message.text = "You come across a small town.  In the distance you can see a hoard of zombies heading your way.  If you're quick enough, you probably have enough time to loot a building and slip away.  What do you want to do?";
        a1.GetComponentInChildren<TextMeshProUGUI>().text = "Take your chances and look around";
        a2.GetComponentInChildren<TextMeshProUGUI>().text = "Leave the town and continue";
        Food = data.food;
        Health = data.health;
        inventory = data.inventory;
        setBuffs();
        melee_dmg = BaseStrength + myweapon.power;
        BaseMaxWeight = (int)50f + (int)(50f * ((BaseStrength + strengthBuff) / 10f));
        //health.text = "Health: " + Health.ToString();
        strength.text = "Strength: " + (BaseStrength + strengthBuff).ToString();
        melee.text = "Weapon:" + myweapon.name;
        sight.text = "Sight: " + (BaseSight + sightBuff).ToString();
        speed.text = "Speed: " + (BaseSpeed + speedBuff).ToString();
        stealth.text = "Stealth: " + (BaseStealth + stealthBuff).ToString();
        //food.text = "Food: " + Food.ToString();
        //curr_weight.text = "Curr Weight: " + calcCurrWeight().ToString();
        //max_weight.text = "Max Weight: " + (BaseMaxWeight + maxWeightBuff).ToString();
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
        if (time > BaseSpeed+speedBuff)
        {
            //deathMessage = "You spent too much time in the town and the hoarde caught up to you.  You have died and become one with the hoard";
            //You die here
            message.text = "You spent too much time in the town and the hoard caught up to you.  You have died and become one with the hoard";
            SaveSystem.SaveDeathMessage(message.text);
            SceneManager.LoadScene(10);
        }
        //curr_weight.text = "Curr Weight: " + calcCurrWeight().ToString();
        setWeight();
        setFood();
        setHealth();
    }

    public void Option1()
    {
        if (options == "start")
        {
            message.text = "Of all the stores you see in the town, only two seem intact enough to raid.  An old pharmacy and a grocery store.  Which one do you want to take a look at?";
            options = "store choice";
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "The Pharmacy";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "The Grocery Store";
        }
        else if (options == "store choice")
        {
            opposite_store_choice = "the grocery store";
            message.text = "In the pharmacy, you are able to find 2 bandages, a syringe of streroids, and a flashlight.  You add them to your inventory.\nIn the backroom you notice a safe, but it looks locked.  Do you want to take the time to open it?";
            seenItems["bandages"] = (int)seenItems["bandages"] + 2;
            seenItems["flashlight"] = (int)seenItems["flashlight"] + 1;
            seenItems["steroids"] = (int)seenItems["steroids"] + 1;
            time = time + 2f;
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Yes";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "No";
            options = "safe";
        }
        else if (options == "safe")
        {
            message.text = "You got the safe open, but it was empty.  Hopefully you didnt waste too much time on it.  Would you like to leave the town or visit " + opposite_store_choice + "?";
            time = time + 1f;
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Leave the town";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "Visit the " + opposite_store_choice;
            options = "leave or stay";
        }
        else if (options == "leave or stay")
        {
            returnToRoad();
        }
    }

    public void Option2()
    {
        if (options == "start")
        {
            returnToRoad();
        }
        else if (options == "store choice")
        {
            opposite_store_choice = "the pharmacy";
            message.text = "In the grocery store you find 20 pounds of food and you add it to your inventory.\nIn the backroom you notice a safe, but it looks locked.  Do you want to take time to open it?";
            time = time + 2f;
            Food = Food + 20;
            //food.text = "Food: " + Food.ToString();
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Yes";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "No";
            options = "safe";
        }
        else if (options == "safe")
        {
            message.text = "Would you like to leave the town or visit " + opposite_store_choice + "?";
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Leave the town";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "Visit the " + opposite_store_choice;
            options = "leave or stay";
        }
        else if (options == "leave or stay")
        {
            time = time + 2.5f;
            if (opposite_store_choice == "the grocery store")
            {
                message.text = "In the grocery store you find 20 pounds of food and you add it to your inventory.  You can hear the hoard coming, so you know it is time to leave.";
                time = time + 2f;
                Food = Food + 20;
                leave.enabled = true;
                leave.gameObject.SetActive(true);
                a1.enabled = false;
                a2.enabled = false;
                a1.gameObject.SetActive(false);
                a2.gameObject.SetActive(false);
            }
            else
            {
                message.text = "In the pharmacy, you are able to find 2 bandages, a syringe of streroids, and a flashlight.  You add them to your inventory.  You can hear the hoard coming, so you know it is time to leave";
                seenItems["bandages"] = (int)seenItems["bandages"] + 2;
                seenItems["flashlight"] = (int)seenItems["flashlight"] + 1;
                seenItems["steroids"] = (int)seenItems["steroids"] + 1;
                leave.enabled = true;
                leave.gameObject.SetActive(true);
                a1.enabled = false;
                a2.enabled = false;
                a1.gameObject.SetActive(false);
                a2.gameObject.SetActive(false);
            }
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

        SaveSystem.SaveLandmark("2");
        Debug.Log(SaveSystem.LoadLandmark());
        Debug.Log(curr_landmark.out_position);
        savedPlayer.x = curr_landmark.out_position;
        SaveSystem.SavePlayerData(savedPlayer);


        SceneManager.LoadScene("Kirnys");
    }
}
