using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class cabin : MonoBehaviour
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
        int Strength = SaveSystem.LoadStats()[0];
        int baseMaxWeight = (int)((int)50f + (50f * (Strength / 10f))) + SaveSystem.LoadItem("backpack").max_weight * (int)player.inventory["backpack"];

        weightSlider.maxValue = baseMaxWeight;
        weightSlider.value = calcCurrWeight();
        weightText.text = calcCurrWeight().ToString() + "/" + weightSlider.maxValue.ToString();
    }

    public void setFood()
    {
        player = SaveSystem.LoadPlayerData();
        int Strength = SaveSystem.LoadStats()[0];
        int baseMaxWeight = (int)((int)50f + (50f * (Strength / 10f))) + SaveSystem.LoadItem("backpack").max_weight * (int)player.inventory["backpack"];

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
        player = SaveSystem.LoadPlayerData();

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
        message.text = "you come across some old tire tracks leading off into the forest.\nThey seem oddly familiar. What do you do?";
        a1.GetComponentInChildren<TextMeshProUGUI>().text = "Follow Tire Tracks";
        a2.GetComponentInChildren<TextMeshProUGUI>().text = "Continue Along Trail";
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
        options = "start";
        
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
        if(options == "start")
        {
            message.text = "Futher along the trail you spot a small group of zombies.\nYou can try to avoid them by sneaking away and taking a different route or you can try to fight them.";
            a1.GetComponentInChildren<TextMeshProUGUI>().text = "Take a different path";
            a2.GetComponentInChildren<TextMeshProUGUI>().text = "Fight them!";

            options = "seeZombies";
        }
        else if(options == "seeZombies")
        {
            if(Food > 10 && (BaseStealth + strengthBuff) < 5)
            {
                message.text = "Due to your lack of stealth knowledge, you try to sneak away upwind of the zombies.\nThey catch the scent of your food on the wind and immediately begin to charge in your direction.\nUnprepared for this encounter you take 15 damage as one of them hits you before you can escape.";

                a1.enabled = false;
                a2.enabled = false;
                a1.gameObject.SetActive(false);
                a2.gameObject.SetActive(false);

                Health = Health - 15;

                leave.enabled = true;
                leave.gameObject.SetActive(true);

                if (Health <= 0)
                {
                    death_message = "The blow from the zombies proved to be fatal.\nIf only you weren't so bad at sneaking...\nor went into encounters with more health?";
                    SaveSystem.SaveDeathMessage(death_message);
                    SceneManager.LoadScene(10);
                }
            }
            else
            {
                message.text = "Being the super sneaky person you are, you succuesfully sneak off and find the tracks on the other side of the zombies. As you continue to follow long the tracks, they lead you to what seems to be a small abandoned cabin.";

                a1.GetComponentInChildren<TextMeshProUGUI>().text = "Check out the cabin";
                a2.GetComponentInChildren<TextMeshProUGUI>().text = "Return to the trail";

                options = "seeCabin";
            }
        }
        else if (options == "seeCabin")
        {
            message.text = "The cabin appears to strangely secure for a cabin.\nThe only way in is the front door and its locked tight.\nMaybe you have something to cut down the door?";

            a2.enabled = false;
            a2.gameObject.SetActive(false);

            if (SaveSystem.LoadWeapon() == "chainsaw")
            {
                a1.GetComponentInChildren<TextMeshProUGUI>().text = "Break in with your chainsaw";
            }
            else
            {
                a1.enabled = false;
                a1.gameObject.SetActive(false);

                leave.enabled = true;
                leave.gameObject.SetActive(true);
            }

            options = "enterCabin";
        }
        else if(options == "enterCabin")
        {
            message.text = "You cut through the door! Upon entering it seems like the cabin was an old zombie survival shelter.\nYou find 50 food, 4 bandages, and a dumbell!";
            Food = Food + 50;
            //food.text = "Food: " + Food.ToString();
            seenItems["bandages"] = (int)seenItems["bandages"] + 4;
            seenItems["dumbell"] = (int)seenItems["dumbell"] + 1;

            options = "leave";
            a1.enabled = false;
            a1.gameObject.SetActive(false);

            leave.enabled = true;
            leave.gameObject.SetActive(true);
        }
        else if(options == "leave")
        {
            a1.enabled = false;
            a1.gameObject.SetActive(false);

            leave.enabled = true;
            leave.gameObject.SetActive(true);
        }
        else
        {
            message.text = "Option 1 was clicked without a function assigned";
        }
    }

    public void Option2()
    {
        if(options == "start")
        {
            returnToRoad();
        }
        else if(options == "seeZombies")
        {
            //the fight them option

            if(melee_dmg > 6 /*|| myweapon.name = "chainsaw"*/)
            {
                //put in different text for chainsaw
                message.text = "You charge head first at the group of zombies, downing the first two immediately due to your sheer strength. The last couple don't pose a problem as you finish them off easily. With the zombies death, you continue along the road until you come to an old cabin.";

                a1.GetComponentInChildren<TextMeshProUGUI>().text = "Check out the cabin";
                a2.GetComponentInChildren<TextMeshProUGUI>().text = "Return to the trail";

                options = "seeCabin";
            }
            else
            {
                message.text = "You weren't able to overcome the zombies, and you lost 15 health points.";
                a1.enabled = false;
                a1.gameObject.SetActive(false);
                a2.enabled = false;
                a2.gameObject.SetActive(false);
                leave.enabled = true;
                leave.gameObject.SetActive(true);
                Health = Health - 15;
                //health.text = "Health: " + Health.ToString();
                if (Health <= 0)
                {
                    SaveSystem.SaveDeathMessage("You weren't able to overcome the zombies and they killed you");
                    SceneManager.LoadScene(10);
                }

            }

        }
        else if(options == "seeCabin")
        {
            returnToRoad();
        }
        else
        {
            message.text = "Option 2 was clicked without a function assigned";
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

        SaveSystem.SaveLandmark("4");
        savedPlayer.x = curr_landmark.out_position;
        SaveSystem.SavePlayerData(savedPlayer);


        SceneManager.LoadScene("Kirnys");
    }
}

