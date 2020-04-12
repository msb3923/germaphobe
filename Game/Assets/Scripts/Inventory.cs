using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;
using TMPro;

public class Inventory : MonoBehaviour
{
    public TextMeshProUGUI ItemInfo;
    public Button Bandage;
    public Button Glasses;
    public Button Camo;
    public Button Backpack;
    public Button Shoes;
    public Button Flashlight;
    public Button Steroids;
    public Button Dumbell;
    public Button Food;
    public Button accept;
    public Button reject;
    public TextMeshProUGUI found_item;
    public TextMeshProUGUI buffs;
    public TextMeshProUGUI Max_weight;
    public int baseMaxWeight;
    public string curr_found_item_name;
    public string[] allItems;

    public TextMeshProUGUI curr_weight;

    public PlayerData player;

    private void Start()
    {
        player = SaveSystem.LoadPlayerData();

        Bandage.GetComponentInChildren<TextMeshProUGUI>().text = "Bandages: " + player.inventory["bandages"].ToString();
        Glasses.GetComponentInChildren<TextMeshProUGUI>().text = "Glasses: " + player.inventory["glasses"].ToString();
        Camo.GetComponentInChildren<TextMeshProUGUI>().text = "Camo: " + player.inventory["camo"].ToString();
        Backpack.GetComponentInChildren<TextMeshProUGUI>().text = "Backpack: " + player.inventory["backpack"].ToString();
        Shoes.GetComponentInChildren<TextMeshProUGUI>().text = "Shoes: " + player.inventory["shoes"].ToString();
        Flashlight.GetComponentInChildren<TextMeshProUGUI>().text = "Flashlight: " + player.inventory["flashlight"].ToString();
        Steroids.GetComponentInChildren<TextMeshProUGUI>().text = "Steroids: " + player.inventory["steroids"].ToString();
        Dumbell.GetComponentInChildren<TextMeshProUGUI>().text = "Dumbells: " + player.inventory["dumbell"].ToString();
        Food.GetComponentInChildren<TextMeshProUGUI>().text = "Food: " + player.food + " lbs";

        int strength = SaveSystem.LoadStats()[0];
        baseMaxWeight = (int)((int)50f + (50f * (strength / 10f)));

        buffs.text = allBuffs();
      
        curr_weight.text = "Current Weight: " + calcCurrWeight().ToString() + " lbs";
        Max_weight.text = "Max Weight: " + baseMaxWeight.ToString() + " lbs";

        curr_found_item_name = SaveSystem.LoadFoundItem();

        found_item.text = "";
        accept.enabled = false;
        reject.enabled = false;
        accept.gameObject.SetActive(false);
        reject.gameObject.SetActive(false);
    }

    private void Update()
    {
       if (curr_found_item_name != "nothing")
        {
            found_item.text = "Found Item: " + curr_found_item_name + "\n\n" + "Item Weight: " + SaveSystem.LoadItem(curr_found_item_name).weight.ToString();
            if (baseMaxWeight - (calcCurrWeight() + SaveSystem.LoadItem(curr_found_item_name).weight) > 0)
            {
                accept.enabled = true;
            }
            accept.gameObject.SetActive(true);
            reject.enabled = true;
            reject.gameObject.SetActive(true);
        }


    }

    public void bandageInfo()
    {
        ItemInfo.text = SaveSystem.LoadItem("bandages").description;
    }
    public void glassesInfo()
    {
        ItemInfo.text = SaveSystem.LoadItem("glasses").description;
    }
    public void camoInfo()
    {
        ItemInfo.text = SaveSystem.LoadItem("camo").description;
    }
    public void backpackInfo()
    {
        ItemInfo.text = SaveSystem.LoadItem("backpack").description;
    }
    public void shoesInfo()
    {
        ItemInfo.text = SaveSystem.LoadItem("shoes").description;
    }
    public void flashlightInfo()
    {
        ItemInfo.text = SaveSystem.LoadItem("flashlight").description;
    }
    public void steroidsInfo()
    {
        ItemInfo.text = SaveSystem.LoadItem("steroids").description;
    }
    public void dumbellInfo()
    {
        ItemInfo.text = SaveSystem.LoadItem("dumbell").description;
    }

    public void Drop(string item)
    {
        player = SaveSystem.LoadPlayerData();
        if ((int)player.inventory[item] > 0)
        {
            player.inventory[item] = (int)player.inventory[item] - 1;
        }
        SaveSystem.SavePlayerData(player);
        Start();
    }

    public void DropFood()
    {
        player = SaveSystem.LoadPlayerData();
        if (player.food - 1 >= 0)
        {
            player.food = player.food - 1;
        }
        SaveSystem.SavePlayerData(player);
        Start();
    }

    public float calcCurrWeight()
    {
        player = SaveSystem.LoadPlayerData();
        float temp_weight = 0f;

        Weapons myweapon = SaveSystem.LoadWeapons(SaveSystem.LoadWeapon());

        temp_weight += myweapon.weight;
        temp_weight += player.food;

        foreach (string key in player.inventory.Keys)
        {
            int num_items = 0;
            num_items = (int)player.inventory[key];

            temp_weight += num_items * SaveSystem.LoadItem(key).weight;
        }

        return temp_weight;
    }

    public string allBuffs()
    {
        player = SaveSystem.LoadPlayerData();
        string buffText = "";

        string sight = "Sight: + ";
        string strength = "Strength: + ";
        string speed = "Speed: + ";
        string stealth = "Stealth: + ";
        string max_weight = "Max Weight: + ";

        int sightBuff = 0;
        int strengthBuff = 0;
        int speedBuff = 0;
        int stealthBuff = 0;
        int maxWeightBuff = 0;

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

        buffText = "All Buffs \n\n" + sight + sightBuff.ToString() + "\n" + strength + strengthBuff.ToString() + "\n" + speed + speedBuff.ToString() + "\n" + stealth + stealthBuff.ToString() + "\n" + max_weight + maxWeightBuff.ToString() + "\n";

        return buffText;
    }

    public void Use(string item)
    {
        player = SaveSystem.LoadPlayerData();
        if ((int)player.inventory[item] > 0)
        {
            player.inventory[item] = (int)player.inventory[item] - 1;
            int[] stats = SaveSystem.LoadStats();

            if (item == "bandages")
            {
                player.health = player.health + SaveSystem.LoadItem(item).health;
                buffs.text = buffs.text + "\n \n Health + 20";
            }
            if (item == "steroids")
            {
                stats[0] += SaveSystem.LoadItem(item).strength;
                SaveSystem.SaveStats(stats);
                player.health = player.health + SaveSystem.LoadItem(item).health;
            }
        }
        SaveSystem.SavePlayerData(player);
        Start();
    }

    public void Take()
    {
        player = SaveSystem.LoadPlayerData();
        if (baseMaxWeight - (calcCurrWeight() + SaveSystem.LoadItem(curr_found_item_name).weight) > 0)
        {
            player.inventory[curr_found_item_name] = (int)player.inventory[curr_found_item_name] + 1;
        }

        curr_found_item_name = "nothing";
        SaveSystem.SavePlayerData(player);
        SaveSystem.SaveFoundItem(curr_found_item_name);

        Start();
    }

    public void Drop()
    {
        curr_found_item_name = "nothing";
        SaveSystem.SavePlayerData(player);
        SaveSystem.SaveFoundItem(curr_found_item_name);

        Start();
    }

    public void returnToRoad()
    {
        SceneManager.LoadScene(0);
    }


}
