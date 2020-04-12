using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    private int Strength;
    private int Sight;
    private int Speed;
    private int Stealth;

    public Text strengthText;
    public Text sightText;
    public Text speedText;
    public Text stealthText;
    public Text melee_dmg;
    public Text max_weight;
    public Text item_chance;
    public Text intake;
    // Start is called before the first frame update
    void Start()
    {
        Strength = Player.BaseStrength;
        Sight = Player.BaseSight;
        Speed = Player.BaseSpeed;
        Stealth = Player.BaseStealth;
        PlayerData player = SaveSystem.LoadPlayerData();

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

        strengthText.text = "Strength: " + Strength.ToString() + " + " + strengthBuff.ToString() + " = " + (Strength + strengthBuff).ToString();
        sightText.text = "Sight: " + Sight.ToString() + " + " + sightBuff.ToString() + " = " + (Sight + sightBuff).ToString();
        speedText.text = "Speed: " + Speed.ToString() + " + " + speedBuff.ToString() + " = " + (Speed + speedBuff).ToString();
        stealthText.text = "Stealth: " + Stealth.ToString() + " + " + stealthBuff.ToString() + " = " + (Stealth + stealthBuff).ToString();
        melee_dmg.text = "Melee Damage: " + Player.melee_dmg.ToString();
        max_weight.text = "Max Weight: " + Player.max_weight.ToString() + " + " + maxWeightBuff.ToString() + " = " + (Player.max_weight + maxWeightBuff).ToString();
        item_chance.text = "Luck: " + Player.find_chance_per_mile.ToString();
        intake.text = "Intake: " + SaveSystem.LoadFood();

    }

    public void returnToGame()
    {
        SceneManager.LoadScene(0);
    }
    public void meager()
    {
        SaveSystem.SaveFood("meager");
        intake.text = "Intake: " + "meager \n Health decreases by 1 every 5 miles";
    }
    public void moderate()
    {
        SaveSystem.SaveFood("moderate");
        intake.text = "Intake: " + "moderate \n Health stays the same";
    }
    public void plentiful()
    {
        SaveSystem.SaveFood("plentiful");
        intake.text = "Intake: " + "plentiful \n Health increases by 1 every 5 miles";
    }
}
