using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class inventory1 : MonoBehaviour
{
    public Button object0;
    public Button object1;
    public Button object2;
    public Button object3;
    public Button object4;
    public Button object5;
    public Button object6;
    public Button object7;
    public Button object8;
    public Button dropItem;
    public Button useItem;
    public Slider weightSlider;
    public Slider foodSlider;
    public Slider healthSlider;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI healthyText;
    public TextMeshProUGUI weightText;
    public int allocator;
    public TextMeshProUGUI itemInfo;
    public TextMeshProUGUI strengthBuff;
    public TextMeshProUGUI sightBuff;
    public TextMeshProUGUI stealthBuff;
    public TextMeshProUGUI speedBuff;
    public TextMeshProUGUI maxWeightBuff;
    public Button[] allButtons;
    public Button road;

    public string curr_found_item_name;

    public Button accept;
    public Button reject;

    public PlayerData player;

    public void allocateButtons()
    {
        player = SaveSystem.LoadPlayerData();

        foreach(string key in player.inventory.Keys)
        {
            if ((int)player.inventory[key] > 0)
            {
                allButtons[allocator].enabled = true;
                allButtons[allocator].gameObject.SetActive(true);
                allButtons[allocator].GetComponentInChildren<TextMeshProUGUI>().text = key + ": " + player.inventory[key];
                allocator += 1;
            }
        }

        for (int i = allocator; i < 9; i++)
        {
            allButtons[i].enabled = false;
            allButtons[i].gameObject.SetActive(false);
        }

        allocator = 0;
    }

    public void clearWhenOut()
    {
        player = SaveSystem.LoadPlayerData();
        if (itemInfo.text != "")
        {
            string item_name = itemInfo.text.Substring(0, itemInfo.text.IndexOf(" ", StringComparison.CurrentCulture));
            if ((int)player.inventory[item_name.ToLower()] <= 0)
            {
                dropItem.enabled = false;
                dropItem.gameObject.SetActive(false);
                useItem.enabled = false;
                useItem.gameObject.SetActive(false);
                itemInfo.text = "";
            }

        }

    }

    public void SelectItem()
    {
        string button_text = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        int colon_idx = button_text.IndexOf(":", StringComparison.CurrentCulture);

        string item_name = button_text.Substring(0, colon_idx);

        Items curr_item = SaveSystem.LoadItem(item_name);

        itemInfo.text = curr_item.description;

        dropItem.enabled = true;
        dropItem.gameObject.SetActive(true);

        if (curr_item.always_active == false)
        {
            useItem.enabled = true;
            useItem.gameObject.SetActive(true);
        }
        else
        {
            useItem.enabled = false;
            useItem.gameObject.SetActive(false);
        }
    }

    public void setHealth()
    {
        player = SaveSystem.LoadPlayerData();
        healthSlider.value = player.health;
        healthyText.text = healthSlider.value.ToString() + "/50";
    }


    public void dropItems()
    {
        player = SaveSystem.LoadPlayerData();
        string item_name = itemInfo.text.Substring(0, itemInfo.text.IndexOf(" ", StringComparison.CurrentCulture));

        player.inventory[item_name.ToLower()] = (int)player.inventory[item_name.ToLower()] - 1;

        SaveSystem.SavePlayerData(player);

    }

    public void dropFood()
    {
        player = SaveSystem.LoadPlayerData();

        if (player.food > 0)
        {
            player.food = player.food - 1;
        }

        SaveSystem.SavePlayerData(player);
    }

    public void applyTempItem(string item)
    {
        player = SaveSystem.LoadPlayerData();
        string item_name = itemInfo.text.Substring(0, itemInfo.text.IndexOf(" ", StringComparison.CurrentCulture));
        int[] stats = SaveSystem.LoadStats();

        if (item == "bandages")
        {
            if (player.health < 50)
            {
                player.health = player.health + SaveSystem.LoadItem(item).health;
                player.inventory[item_name.ToLower()] = (int)player.inventory[item_name.ToLower()] - 1;
            }
            else
            {

            }
        }
        if (item == "steroids")
        {
            stats[0] += 1;
            SaveSystem.SaveStats(stats);
            player.health = player.health - 15;
            player.inventory[item_name.ToLower()] = (int)player.inventory[item_name.ToLower()] - 1;
        }

        SaveSystem.SavePlayerData(player);
    }

    public void useItems()
    {
        player = SaveSystem.LoadPlayerData();
        string item_name = itemInfo.text.Substring(0, itemInfo.text.IndexOf(" ", StringComparison.CurrentCulture));
        Debug.Log(item_name.ToLower());

        Debug.Log(player.inventory[item_name.ToLower()]);

        applyTempItem(item_name.ToLower());

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

    public void setWeight()
    {
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
        foodSlider.value = player.food;
        foodText.text = foodSlider.value.ToString() + "/" + foodSlider.maxValue.ToString();
    }

    public void displayBuffs()
    {
        int speedBuffs = 0;
        int stealthBuffs = 0;
        int strengthBuffs = 0;
        int sightBuffs = 0;
        int maxWeightBuffs = 0;


        player = SaveSystem.LoadPlayerData();
        foreach (string key in player.inventory.Keys)
        {
            int num_items = 0;
            num_items = (int)player.inventory[key];
            //Debug.Log(SaveSystem.LoadItem(key).name);
            //Debug.Log(num_items);
            //Debug.Log(SaveSystem.LoadItem(key).stealth);
            //Debug.Log((SaveSystem.LoadItem(key).stealth * num_items).ToString());

         

            speedBuffs = speedBuffs + SaveSystem.LoadItem(key).speed * num_items;
            stealthBuffs = stealthBuffs + SaveSystem.LoadItem(key).stealth * num_items;
            strengthBuffs = strengthBuffs + SaveSystem.LoadItem(key).strength * num_items;
            sightBuffs = sightBuffs + SaveSystem.LoadItem(key).sight * num_items;
            maxWeightBuffs = maxWeightBuffs + SaveSystem.LoadItem(key).max_weight * num_items;


            //stealthB = SaveSystem.LoadItem(key).stealth * num_items;

            sightBuff.text = "Sight: + " + sightBuffs.ToString();
            strengthBuff.text = "Strength: + " + strengthBuffs.ToString();
            speedBuff.text = "Speed: + " + speedBuffs.ToString();
            stealthBuff.text = "Stealth: + " + stealthBuffs.ToString();
            maxWeightBuff.text = "Max Weight: + " + maxWeightBuffs.ToString();
        }
    }

    public void returnToRoad()
    {
        SceneManager.LoadScene("Kirnys");
    }

    // Start is called before the first frame update
    void Start()
    {
        player = SaveSystem.LoadPlayerData();
        allocator = 0;
        allButtons = new Button[9];
        allButtons[0] = object0;
        allButtons[1] = object1;
        allButtons[2] = object2;
        allButtons[3] = object3;
        allButtons[4] = object4;
        allButtons[5] = object5;
        allButtons[6] = object6;
        allButtons[7] = object7;
        allButtons[8] = object8;
        allocateButtons();
        dropItem.enabled = false;
        dropItem.gameObject.SetActive(false);
        useItem.enabled = false;
        useItem.gameObject.SetActive(false);

        //curr_found_item_name = SaveSystem.LoadFoundItem();


    }

    // Update is called once per frame
    void Update()
    {
        allocateButtons();
        displayBuffs();
        setWeight();
        setFood();
        setHealth();
        player = SaveSystem.LoadPlayerData();
        if (player.health <= 0)
        {
            SaveSystem.SaveDeathMessage("You should've stayed off the juice");
            SceneManager.LoadScene(10);
        }
        int strength = SaveSystem.LoadStats()[0];
        if (calcCurrWeight() > (int)((int)50f + (50f * (strength / 10f))) + SaveSystem.LoadItem("backpack").max_weight * (int)player.inventory["backpack"])
        {
            road.gameObject.SetActive(false);
            road.enabled = false;
        }
        else
        {
            road.gameObject.SetActive(true);
            road.enabled = true;
        }


        //if (curr_found_item_name != "nothing")
        //{
        //    itemInfo.text = "Found Item: " + curr_found_item_name + "\n\n" + "Item Weight: " + SaveSystem.LoadItem(curr_found_item_name).weight.ToString();
        //    if (((int)((int)50f + (50f * (strength / 10f))) + SaveSystem.LoadItem("backpack").max_weight * (int)player.inventory["backpack"]) - (calcCurrWeight() + SaveSystem.LoadItem(curr_found_item_name).weight) > 0)
        //    {
        //        accept.enabled = true;
        //    }
        //    accept.gameObject.SetActive(true);
        //    reject.enabled = true;
        //    reject.gameObject.SetActive(true);
        //}
        //else
        //{
        //    accept.gameObject.SetActive(false);
        //    reject.enabled = false;
        //    reject.gameObject.SetActive(false);

        clearWhenOut();
        //}

    }


    //public void Take()
    //{
    //    player = SaveSystem.LoadPlayerData();
    //    int strength = SaveSystem.LoadStats()[0];
    //    if (((int)((int)50f + (50f * (strength / 10f))) + SaveSystem.LoadItem("backpack").max_weight * (int)player.inventory["backpack"]) - (calcCurrWeight() + SaveSystem.LoadItem(curr_found_item_name).weight) > 0)
    //    {
    //        player.inventory[curr_found_item_name] = (int)player.inventory[curr_found_item_name] + 1;
    //    }

    //    curr_found_item_name = "nothing";
    //    SaveSystem.SavePlayerData(player);
    //    SaveSystem.SaveFoundItem(curr_found_item_name);

    //    itemInfo.text = "";

    //    Start();
    //}

    //public void Drop()
    //{
    //    curr_found_item_name = "nothing";
    //    SaveSystem.SavePlayerData(player);
    //    SaveSystem.SaveFoundItem(curr_found_item_name);

    //    itemInfo.text = "";

    //    Start();
    //}

}
