using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Player : MonoBehaviour
{

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            healthText.text = "Health: " + Health.ToString();
        }
    }

    public void SavePlayer()
    {
        PlayerData pd = new PlayerData(this);
        SaveSystem.SavePlayerData(pd);
    }

    public void SaveDefault()
    {
        SaveSystem.SaveDefault(this);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayerData();
        Weapons myweapon = SaveSystem.LoadWeapons(SaveSystem.LoadWeapon());
        curr_landmark = SaveSystem.LoadLandmarks(SaveSystem.LoadLandmark());
        int[] stats = SaveSystem.LoadStats();
        BaseStrength = stats[0];
        BaseSight = stats[1];
        BaseSpeed = stats[2];
        BaseStealth = stats[3];
        Food = data.food;
        Distance = data.distance;
        health = data.health;
        mytransform.position = new Vector3(data.x, mytransform.position.y, mytransform.position.z);
        weapon = myweapon;
        weapon_weight = weapon.weight;
        inventory = data.inventory;
    }

    public void LoadDefault()
    {
        PlayerData data = SaveSystem.LoadDefault();
        Food = data.food;
        Distance = data.distance;
        health = data.health;
        mytransform.position = new Vector3(data.x, mytransform.position.y, mytransform.position.z);
        inventory = data.inventory;
    }

    public void checkStats()
    {
        SavePlayer();
        SceneManager.LoadScene(1);
    }

    public void checkInventory()
    {
        SavePlayer();
        SceneManager.LoadScene(9);
    }




    public void makeInventory()
    {
        inventory.Add("bandages", 1);
        inventory.Add("glasses", 0);
        inventory.Add("camo", 0);
        inventory.Add("backpack", 0);
        inventory.Add("shoes", 0);
        inventory.Add("flashlight", 0);
        inventory.Add("steroids", 0);
        inventory.Add("dumbell", 0);
    }
    public void setDefaultInventory()
    {
        inventory["bandages"] = 0;
        inventory["glasses"] = 0;
        inventory["camo"] = 0;
        inventory["backpack"] = 0;
        inventory["shoes"] = 0;
        inventory["flashlight"] = 0;
        inventory["dumbell"] = 0;
    }

    public float calcCurrWeight()
    {
        float temp_weight = 0f;

        temp_weight += weapon_weight;
        temp_weight += Food;

        foreach (string key in inventory.Keys)
        {
            int num_items = 0;
            num_items = (int)inventory[key];

            temp_weight += num_items * SaveSystem.LoadItem(key).weight;
        }

        return temp_weight;
    }

    public Transform mytransform;
    public float position;
    public static int Strength;
    public static int Sight;
    public static int Speed;
    public static int Stealth;
    public static int BaseStrength;
    public static int BaseSight;
    public static int BaseSpeed;
    public static int BaseStealth;
    public static int BaseMax_Weight;
    public int sightBuff = 0;
    public int strengthBuff = 0;
    public int speedBuff = 0;
    public int stealthBuff = 0;
    public int maxWeightBuff = 0;
    public static float melee_dmg;
    public static float max_weight;
    public static float moderate_diet_per_mile;
    public static float meager_diet_per_mile;
    public static float plentiful_diet_per_mile;
    public static float curr_diet;
    public static float find_chance_per_mile;
    public static float danger_sense;
    public static float trail_speed_per_frame;
    public float curr_weight;
    public Hashtable inventory = new Hashtable();
    public Items curr_found_item = new Items();
    public float curr_found_food_weight;
    public Button acceptStuff;
    public Button rejectStuff;
    public Button yesHunt;
    public Button noHunt;
    public Button moveAlong;
    public Button huntButton;
    public Slider distanceSlider;
    public Slider weightSlider;
    public Slider healthSlider;
    public Slider foodSlider;
    public TextMeshProUGUI intake;
    public TextMeshProUGUI strengthShow;
    public TextMeshProUGUI sightShow;
    public TextMeshProUGUI speedShow;
    public TextMeshProUGUI stealthShow;
    public TextMeshProUGUI meleeShow;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI weightText;
    public TextMeshProUGUI healthyText;
    public TextMeshProUGUI foodyText;
    public int oldDistance;
    public Button Inventory;
    public Button bandageButton;
    public bool zombieAttack;
    public string deathMessage;
    public Landmarks curr_landmark = new Landmarks();

    public Button goToInventory;
    public Button encounter;

    public GameObject modal;

    public int Distance;
    public int health;

    public Animator animator;
    public float Food = 50f;
    public Weapons weapon;
    public int weapon_weight;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI foodText;
    public TextMeshProUGUI message;
    public bool walk = true;

    public void setDistance()
    {
        distanceSlider.maxValue = 1100;
        distanceSlider.value = curr_landmark.in_position - Distance;
        distanceText.text = distanceSlider.value.ToString() + "/" + distanceSlider.maxValue.ToString();
    }

    public void setWeight()
    {
        weightSlider.maxValue = max_weight;
        weightSlider.value = curr_weight;
        weightText.text = weightSlider.value.ToString() + "/" + weightSlider.maxValue.ToString();
    }

    public void setHealth()
    {
        healthSlider.value = Health;
        healthyText.text = healthSlider.value.ToString()+"/50";
    }

    public void setFood()
    {
        foodSlider.maxValue = max_weight;
        foodSlider.value = Food;
        foodyText.text = foodSlider.value.ToString() + "/" + foodSlider.maxValue.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        makeInventory();
        mytransform = GetComponent<Transform>();
        BaseStrength = 5;
        BaseSight = 5;
        BaseSpeed = 5;
        BaseStealth = 5;
        setBuffs();
        Strength = BaseStrength + strengthBuff;
        Sight = BaseSight + sightBuff;
        Speed = BaseSpeed + speedBuff;
        Stealth = BaseStealth + stealthBuff;
        max_weight = BaseMax_Weight + maxWeightBuff;
        Health = 50;
        Distance = 95;
        Food = 20f;
        curr_weight = calcCurrWeight();
        mytransform.position = new Vector3(0f, mytransform.position.y, mytransform.position.z);
        SaveDefault();
        LoadPlayer();
        SavePlayer();
        setBuffs();
        Strength = BaseStrength + strengthBuff;
        Sight = BaseSight + sightBuff;
        Speed = BaseSpeed + speedBuff;
        Stealth = BaseStealth + stealthBuff;
        melee_dmg = weapon.power + Strength;
        max_weight = 50f + (50f * (Strength / 10f));
        moderate_diet_per_mile = (.25f + (.04f * Strength) - (.1f * Speed / 10))/2f;
        plentiful_diet_per_mile = moderate_diet_per_mile * 2;
        meager_diet_per_mile = moderate_diet_per_mile * .5f;
        curr_diet = moderate_diet_per_mile;
        find_chance_per_mile = (.01f + (Sight / 220f))/2;
        danger_sense = .2f + (Sight / 10);
        trail_speed_per_frame = (.1f * (Speed+1)) / 4;
        max_weight = BaseMax_Weight + maxWeightBuff;
        strengthShow.text = "Strength: " + Strength.ToString();
        sightShow.text = "Sight: " + Sight.ToString();
        speedShow.text = "Speed: " + Speed.ToString();
        stealthShow.text = "Stealth: " + Stealth.ToString();
        meleeShow.text = "Melee Damage: " + melee_dmg.ToString();
        curr_found_item.name = "nothing";
        curr_landmark = SaveSystem.LoadLandmarks(SaveSystem.LoadLandmark());
        SaveSystem.SaveFoundItem(curr_found_item.name);

        if (SaveSystem.LoadFood() == "meager")
        {
            curr_diet = meager_diet_per_mile;
            foodText.color = Color.white;
        }
        else if (SaveSystem.LoadFood() == "moderate")
        {
            curr_diet = moderate_diet_per_mile;
            foodText.color = Color.yellow;

        }
        else
        {
            curr_diet = plentiful_diet_per_mile;
            foodText.color = Color.white;
        }
        modal.gameObject.SetActive(false);
        message.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //setBuffs();
        //Strength = BaseStrength + strengthBuff;
        //Sight = BaseSight + sightBuff;
        //Speed = BaseSpeed + speedBuff;
        //Stealth = BaseSpeed + stealthBuff;
        //max_weight = BaseMax_Weight + maxWeightBuff;
        if (Health > 50)
        {
            Health = 50;
        }
        foodText.text = "Food: " + Food.ToString() + " Lbs";
        healthText.text = "Health: " + Health.ToString();

        if ((int)inventory["bandages"] > 0 & Health != 50)
        {
            bandageButton.enabled = true;
            bandageButton.gameObject.SetActive(true);
        }
        else
        {
            bandageButton.enabled = false;
            bandageButton.gameObject.SetActive(false);
        }

            //Section here is used to view your current stats
            //change to clicking a button to view stats
            if (Input.GetKeyDown("return"))
        {

            SceneManager.LoadScene(1);
        }

        //Used while character is traveling
        if (walk == true)
        {
            modal.SetActive(false);
            huntButton.enabled = true;
            acceptStuff.enabled = false;
            rejectStuff.enabled = false;
            acceptStuff.gameObject.SetActive(false);
            rejectStuff.gameObject.SetActive(false);
            yesHunt.enabled = false;
            noHunt.enabled = false;
            yesHunt.gameObject.SetActive(false);
            noHunt.gameObject.SetActive(false);
            moveAlong.enabled = false;
            moveAlong.gameObject.SetActive(false);
            oldDistance = Distance;
            Distance = curr_landmark.in_position - (int)Mathf.Floor(transform.position.x);
            if (Distance > 0)
            {
                animator.SetBool("stop", false);
                transform.position = new Vector3(transform.position.x + trail_speed_per_frame, transform.position.y, transform.position.z);


                //indicates a mile has been travelled
                if (oldDistance != Distance)
                {
                    updateHealth();
                    randomFind();

                    //update food per mile
                    if (Food > 0)
                    {
                        if (SaveSystem.LoadFood() == "meager")
                        {
                            curr_diet = meager_diet_per_mile;
                            Image plentifulImage = GameObject.FindGameObjectWithTag("plentiful").GetComponent<Image>();
                            Image moderateImage = GameObject.FindGameObjectWithTag("moderate").GetComponent<Image>();
                            Image meagerImage = GameObject.FindGameObjectWithTag("meager").GetComponent<Image>();

                            Color32 grey = new Color32(0x7B, 0x78, 0x78, 0xFF);
                            Color32 red = new Color32(0xE9, 0X0E, 0x0E, 0xFF);

                            plentifulImage.color = grey;
                            moderateImage.color = grey;
                            meagerImage.color = red;
                        }
                        else if (SaveSystem.LoadFood() == "moderate")
                        {
                            curr_diet = moderate_diet_per_mile;
                            Image plentifulImage = GameObject.FindGameObjectWithTag("plentiful").GetComponent<Image>();
                            Image moderateImage = GameObject.FindGameObjectWithTag("moderate").GetComponent<Image>();
                            Image meagerImage = GameObject.FindGameObjectWithTag("meager").GetComponent<Image>();

                            Color32 grey = new Color32(0x7B, 0x78, 0x78, 0xFF);
                            Color32 yellow = new Color32(0xFA, 0XF2, 0x1E, 0xFF);

                            plentifulImage.color = grey;
                            moderateImage.color = yellow;
                            meagerImage.color = grey;

                        }
                        else if (SaveSystem.LoadFood() == "plentiful")
                        {
                            curr_diet = plentiful_diet_per_mile;
                            Image plentifulImage = GameObject.FindGameObjectWithTag("plentiful").GetComponent<Image>();
                            Image moderateImage = GameObject.FindGameObjectWithTag("moderate").GetComponent<Image>();
                            Image meagerImage = GameObject.FindGameObjectWithTag("meager").GetComponent<Image>();

                            Color32 grey = new Color32(0x7B, 0x78, 0x78, 0xFF);
                            Color32 green = new Color32(0x0F, 0XF8, 0x06, 0xFF);

                            plentifulImage.color = green;
                            moderateImage.color = grey;
                            meagerImage.color = grey;
                        }

                        if ((Food - curr_diet) < 0)
                        {
                            Food = 0;
                        }
                        else
                        {
                            Food = Food - curr_diet;
                            Food = (float)System.Math.Round(Food, 1);
                        }
                    }


                }
            }
        }

        //Sets character's animation to idle once the player is no longer moving
        if (Distance == 0)
        {
            modal.SetActive(true);
            walk = false;
            message.text = curr_landmark.message;
            encounter.gameObject.SetActive(true);
            encounter.enabled = true;
            SavePlayer();
        }
        else
        {
            encounter.gameObject.SetActive(false);
            encounter.enabled = false;
        }

        if (walk == false)
        {
            animator.SetBool("stop", true);
            huntButton.enabled = false;
        }

        if (message.text == curr_landmark.message)
        {
            if (Input.GetKeyDown("space"))
            {
                SceneManager.LoadScene(curr_landmark.build_number);
            }
        }
        curr_weight = calcCurrWeight();
        max_weight = (int)((int)50f + (50f * (Strength / 10f))) + SaveSystem.LoadItem("backpack").max_weight * (int)inventory["backpack"];
        setDistance();
        setWeight();
        setHealth();
        setFood();
        SavePlayer();
        if (Health <= 0)
        {
            LoadDefault();
            SaveSystem.SaveFood("moderate");
            setDefaultInventory();
            SavePlayer();
            SceneManager.LoadScene(10);
        }
    }

    void updateHealth()
    {
        if (Distance % 5 == 0)
        {
            if (SaveSystem.LoadFood() == "meager")
            {
                healthText.color = new Color(200, 0, 0);
                Health--;
            }
            if (SaveSystem.LoadFood() == "plentiful")
            {
                if ((Health < 50) & Food > 0)
                {
                    healthText.color = Color.green;
                    Health++;
                }
            }
        }

        else if (Food <= 0)
        {
            healthText.color = new Color(200, 0, 0);
            foodText.color = Color.white;
            Health -= 1;
        }

        else
        {
            healthText.color = Color.white;

        }

        if (Health <= 0)
        {
            deathMessage = "You starved to death";
            SaveSystem.SaveDeathMessage(deathMessage);
            LoadDefault();
            SaveSystem.SaveFood("moderate");
            setDefaultInventory();
            SavePlayer();
            SceneManager.LoadScene(10);
        }

        if ((curr_weight > max_weight) && (max_weight != 0))
        {
            modal.SetActive(true);
            message.text = "Your Current Weight is greater than your max weight.  Please go into your inventory and drop some items.";
            goToInventory.gameObject.SetActive(true);
            goToInventory.enabled = true;
            acceptStuff.gameObject.SetActive(false);
            acceptStuff.enabled = false;
            rejectStuff.gameObject.SetActive(false);
            rejectStuff.enabled = false;
            walk = false;
        }
        else
        {
            goToInventory.gameObject.SetActive(false);
            goToInventory.enabled = false;
            walk = true;
        }
    }

    public void acceptItem()
    {
        if (zombieAttack == false)
        {
            if (!message.text.Contains("food"))
            {
                //if ((curr_weight + curr_found_item.weight) <= max_weight)
                //{
                inventory[curr_found_item.name] = (int)inventory[curr_found_item.name] + 1;

                walk = true;
                message.text = "";
                curr_found_food_weight = 0f;
                curr_found_item.name = "nothing";
                modal.SetActive(false);
                acceptStuff.enabled = false;
                rejectStuff.enabled = false;
                acceptStuff.gameObject.SetActive(false);
                rejectStuff.gameObject.SetActive(false);
                Inventory.enabled = true;
                //}
                //else
                //{
                //    SaveSystem.SaveFoundItem(curr_found_item.name);
                //    SceneManager.LoadScene(9);
                //}
            }

            else
            {
                if (message.text.Contains(curr_found_food_weight.ToString() + " lbs of food."))
                {
                    Food += curr_found_food_weight;
                    foodText.text = "Food : " + Food.ToString() + " lbs";

                    walk = true;
                    message.text = "";
                    curr_found_item.name = "nothing";
                    modal.SetActive(false);
                    curr_found_food_weight = 0f;
                    acceptStuff.enabled = false;
                    rejectStuff.enabled = false;
                    acceptStuff.gameObject.SetActive(false);
                    rejectStuff.gameObject.SetActive(false);
                }
                else
                {
                    Food += max_weight - curr_weight;
                    foodText.text = "Food : " + Food.ToString() + " lbs";

                    walk = true;
                    message.text = "";
                    curr_found_item.name = "nothing";
                    curr_found_food_weight = 0f;
                    modal.SetActive(false);
                    acceptStuff.enabled = false;
                    rejectStuff.enabled = false;
                    acceptStuff.gameObject.SetActive(false);
                    rejectStuff.gameObject.SetActive(false);
                }
                Inventory.enabled = true;
            }
        }
        else
        {
            acceptStuff.enabled = false;
            rejectStuff.enabled = false;
            acceptStuff.gameObject.SetActive(false);
            rejectStuff.gameObject.SetActive(false);
            int zombie_damage = (int)Random.Range(5f, 10f);
            int pass_chance;
            if ((Sight + Stealth) < 8)
            {
                if (melee_dmg > 10)
                {
                    pass_chance = (int)Random.Range(2f, 6f);
                }
                else
                {
                    pass_chance = (int)Random.Range(0f, 4f);
                }
            }
            else
            {
                if(melee_dmg > 10)
                {
                    pass_chance = 10;
                }
                else
                {
                    pass_chance = (int)Random.Range(2f, 6f);
                }
            }
            if (pass_chance < 3)
            {
                modal.SetActive(true);
                message.text = "A hidden zombie attacked you for " + zombie_damage.ToString() + " damage!.  You lost the loot.";
                Health = Health - zombie_damage;
            }
            else
            {
                if (melee_dmg > 10)
                {
                    modal.SetActive(true);
                    message.text = "You spotted a hidden zombie and killed it, but lost the loot";
                }
                else
                {
                    modal.SetActive(true);
                    message.text = "You spotted a hidden zombie and escaped with no damage, but lost the loot.";
                }
                
            }
            if (Health <= 0)
            {
                deathMessage = "A hidden zombie attacked you for " + zombie_damage.ToString() + " damage and it killed you";
                SaveSystem.SaveDeathMessage(deathMessage);
                LoadDefault();
                SaveSystem.SaveFood("moderate");
                setDefaultInventory();
                SavePlayer();
                SceneManager.LoadScene(10);
            }
            curr_found_item.name = "nothing";
            curr_found_food_weight = 0f;
            modal.SetActive(true);
            acceptStuff.enabled = false;
            rejectStuff.enabled = false;
            acceptStuff.gameObject.SetActive(false);
            rejectStuff.gameObject.SetActive(false);
            moveAlong.enabled = true;
            moveAlong.gameObject.SetActive(true);
        }

    }
        

    public void rejectItem()
    {
        zombieAttack = false;
        walk = true;
        message.text = "";
        curr_found_food_weight = 0f;
        curr_found_item.name = "nothing";
        modal.SetActive(false);
        acceptStuff.enabled = false;
        rejectStuff.enabled = false;
        acceptStuff.gameObject.SetActive(false);
        rejectStuff.gameObject.SetActive(false);

    }


    void randomFind()
    {
        zombieAttack = false;
        float find_chance = Random.Range(0.0f, 1.0f);
        float item_chance = Random.Range(0f, 1f);
        int food_idx = Random.Range(0, 9);
        Foods found_food = SaveSystem.LoadFoods(food_idx);
        float food_weight = found_food.weight;
        
        string found_item_name = "";



        bool found = false;

        float find_range = 1f - find_chance_per_mile;

        if (find_chance > find_range)
        {
            found = true;
            Inventory.enabled = false;
            //Inventory.gameObject.SetActive(false);
        }

        if (found == true)
        {
            if (item_chance < .5f)
            {
                found_item_name = "food";
                if (item_chance < .1f)
                {
                    zombieAttack = true;
                }
            }
            else if (item_chance >= .5f && item_chance < .6f)
            {
                found_item_name = "bandages";
            }
            else if (item_chance >= .6f && item_chance < .65f)
            {
                found_item_name = "bandages";
            }
            else if (item_chance >= .65f && item_chance < .7f)
            {
                found_item_name = "bandages";
                zombieAttack = true;
            }
            else if (item_chance >= .7f && item_chance < .8f)
            {
                found_item_name = "bandages";
                zombieAttack = true;
            }
            else if (item_chance >= .8f && item_chance < .9f)
            {
                found_item_name = "bandages";
            }
            else if (item_chance >= .9f && item_chance < .95f)
            {
                found_item_name = "bandages";
            }
            else if (item_chance >= .95f && item_chance < .98f)
            {
                found_item_name = "bandages";
            }
            else if (item_chance >= .98f && item_chance < 1f)
            {
                found_item_name = "bandages";
            }

            if (found_item_name != "food")
            {
                curr_found_food_weight = 0;
                curr_found_item = SaveSystem.LoadItem(found_item_name);
                modal.SetActive(true);
                acceptStuff.enabled = true;
                rejectStuff.enabled = true;
                acceptStuff.gameObject.SetActive(true);
                rejectStuff.gameObject.SetActive(true);
                message.text = "You found " + found_item_name;

                walk = false;
            }
            else
            {
                modal.SetActive(true);
                acceptStuff.enabled = true;
                rejectStuff.enabled = true;
                acceptStuff.gameObject.SetActive(true);
                rejectStuff.gameObject.SetActive(true);
                curr_found_food_weight = food_weight;
                if (food_weight + curr_weight <= max_weight)
                {
                    message.text = "You found " + found_food.name + "...\n" + food_weight.ToString() + " lbs of food.";
                }
                else
                {
                    curr_found_food_weight = max_weight - curr_weight;
                    message.text = "You found " + found_food.name + "...\n" + food_weight.ToString() + " lbs of food." + "\n accepting will reach your weight limit";
                }

                walk = false;
            }
        }



    }


    public void hunt()
    {
        Inventory.enabled = false;
        walk = false;
        message.text = "Hunt for food? \n Stats affect chance to find wild animals or encounter zombies";
        modal.SetActive(true);
        yesHunt.enabled = true;
        noHunt.enabled = true;
        yesHunt.gameObject.SetActive(true);
        noHunt.gameObject.SetActive(true);
        moveAlong.enabled = false;
        moveAlong.gameObject.SetActive(false);
        huntButton.enabled = false;
    }
    public void dontHunt()
    {
        modal.SetActive(false);
        walk = true;
        yesHunt.enabled = false;
        noHunt.enabled = false;
        yesHunt.gameObject.SetActive(false);
        noHunt.gameObject.SetActive(false);
        message.text = "";
        huntButton.enabled = true;
        Inventory.enabled = true;
    }

    public void moveOn()
    {
        message.text = "";
        modal.SetActive(false);
        moveAlong.enabled = false;
        moveAlong.gameObject.SetActive(false);
        zombieAttack = false;
        walk = true;
        yesHunt.enabled = false;
        noHunt.enabled = false;
        yesHunt.gameObject.SetActive(false);
        noHunt.gameObject.SetActive(false);
        huntButton.enabled = true;

    }

    public float calcFightChance()
    {
        float fightChance = 0f;

        for (int i=0; i < melee_dmg; i+= 1)
        {
            if (i < 7)
            {
                fightChance = Mathf.Pow(i, 2);
            }
            else if (i < 10)
            {
                fightChance += 10;
            }
            else
            {
                fightChance += 1;
            }
        }

        fightChance = fightChance / 100f;

        return fightChance;
    }

    public void doHunt()
    {
        float goodEncounterProb = (float) 0.2f + (Stealth * .06f);

        float rand = Random.Range(0.0f, 1.0f);

        int food_idx = Random.Range(0, 9);
        Foods found_food = SaveSystem.LoadFoods(food_idx);
        float hunt_food_weight = found_food.weight;
        int hunt_damage = Mathf.FloorToInt(Random.Range(5f, 10f));

        if (goodEncounterProb > rand)
        {
            message.text = "You found " + found_food.name + "\n You got " + hunt_food_weight.ToString() + "lbs of food";
            if (curr_weight + hunt_food_weight > max_weight)
            {
                Food += max_weight - curr_weight;
            }
            else
            {
                Food = Food + hunt_food_weight;
            }
            SavePlayer();
        }
        else
        {
            float fightChance = calcFightChance();
            float win = Random.Range(0.0f, 1.0f);
            if (fightChance > win)
            {
                message.text = "You didn't see a Zombie, but it saw you and attacked... \n But you killed it with your " + weapon.name;
                SavePlayer();
            }
            else
            {
                message.text = "You didn't see a Zombie, but it saw you \n It attacked and hurt you for " + ((int)hunt_damage).ToString() + " damage";
                Health = Health - (int)hunt_damage;
                SavePlayer();
            }
        }

        if (Health <= 0)
        {
            deathMessage = "A hidden zombie attacked you for " + hunt_damage.ToString() + " damage and it killed you";
            SaveSystem.SaveDeathMessage(deathMessage);
            LoadDefault();
            SaveSystem.SaveFood("moderate");
            setDefaultInventory();
            SavePlayer();
            SceneManager.LoadScene(10);
        }
        //modal.SetActive(false);
        yesHunt.enabled = false;
        noHunt.enabled = false;
        yesHunt.gameObject.SetActive(false);
        noHunt.gameObject.SetActive(false);
        moveAlong.enabled = true;
        moveAlong.gameObject.SetActive(true);
        Inventory.enabled = true;
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

    public void meager()
    {
        SaveSystem.SaveFood("meager");

        Image plentifulImage = GameObject.FindGameObjectWithTag("plentiful").GetComponent<Image>();
        Image moderateImage = GameObject.FindGameObjectWithTag("moderate").GetComponent<Image>();
        Image meagerImage = GameObject.FindGameObjectWithTag("meager").GetComponent<Image>();

        Color32 grey = new Color32(0x7B, 0x78, 0x78, 0xFF);
        Color32 red = new Color32(0xE9, 0X0E, 0x0E, 0xFF);

        plentifulImage.color = grey;
        moderateImage.color = grey;
        meagerImage.color = red;

    }
    public void moderate()
    {
        SaveSystem.SaveFood("moderate");

        Image plentifulImage = GameObject.FindGameObjectWithTag("plentiful").GetComponent<Image>();
        Image moderateImage = GameObject.FindGameObjectWithTag("moderate").GetComponent<Image>();
        Image meagerImage = GameObject.FindGameObjectWithTag("meager").GetComponent<Image>();

        Color32 grey = new Color32(0x7B, 0x78, 0x78, 0xFF);
        Color32 yellow = new Color32(0xFA, 0XF2, 0x1E, 0xFF);

        plentifulImage.color = grey;
        moderateImage.color = yellow;
        meagerImage.color = grey;
    }
    public void plentiful()
    {
        SaveSystem.SaveFood("plentiful");

        Image plentifulImage = GameObject.FindGameObjectWithTag("plentiful").GetComponent<Image>();
        Image moderateImage = GameObject.FindGameObjectWithTag("moderate").GetComponent<Image>();
        Image meagerImage = GameObject.FindGameObjectWithTag("meager").GetComponent<Image>();

        Color32 grey = new Color32(0x7B, 0x78, 0x78, 0xFF);
        Color32 green = new Color32(0x0F, 0XF8, 0x06, 0xFF);

        plentifulImage.color = green;
        moderateImage.color = grey;
        meagerImage.color = grey;
    }

    public void useBandage()
    {
        if ((int)inventory["bandages"] > 0)
        {
            inventory["bandages"] = (int)inventory["bandages"] - 1;
            Health += SaveSystem.LoadItem("bandages").health;
            healthText.color = Color.green;
        }
    }

    public void enterEncounter()
    {
        SceneManager.LoadScene(curr_landmark.build_number);
    }


    private void OnApplicationQuit()
    {
        LoadDefault();
        SaveSystem.SaveFood("moderate");
        setDefaultInventory();
        SavePlayer();
    }
}
