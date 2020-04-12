using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Items
{
    public string name;
    public int sight;
    public int strength;
    public int speed;
    public int stealth;
    public int weight;
    public int health;
    public int max_weight;
    public bool always_active;
    public string description;
    public int count;
    public int maxCount;


    public void initAtZero(Items item)
    {
        item.sight = 0;
        item.strength = 0;
        item.speed = 0;
        item.stealth = 0;
        item.weight = 0;
        item.health = 0;
        item.max_weight = 0;
        item.description = "";
        item.maxCount = 0;
    }

    public static void saveAllItems ()
    {
        Items[] all_items;

        all_items = new Items[9];

        Items bandage = new Items();
        bandage.name = "bandages";
        bandage.sight = 0;
        bandage.strength = 0;
        bandage.speed = 0;
        bandage.stealth = 0;
        bandage.weight = 4;
        bandage.health = 5;
        bandage.max_weight = 0;
        bandage.always_active = false;
        bandage.description = "Bandages Info:\n\n Use once \n health: +5";
        bandage.maxCount = 100;//Doesn't matter here

        Items glasses = new Items();
        glasses.name = "glasses";
        glasses.sight = 2;
        glasses.strength = 0;
        glasses.speed = 0;
        glasses.stealth = 0;
        glasses.weight = 2;
        glasses.health = 0;
        glasses.max_weight = 0;
        glasses.always_active = true;
        glasses.description = "Glasses Info: \n\n Sight: +2 \n Can only have one";
        glasses.maxCount = 1;


        Items camo = new Items();
        camo.name = "camo";
        camo.sight = 0;
        camo.strength = 0;
        camo.speed = 0;
        camo.stealth = 2;
        camo.weight = 10;
        camo.health = 0;
        camo.max_weight = 0;
        camo.always_active = true;
        camo.description = "Camo Info:\n\n Stealth: +2 \n Can only have one";
        camo.maxCount = 1;

        Items backpack = new Items();
        backpack.initAtZero(backpack);
        backpack.name = "backpack";
        backpack.max_weight = 20;
        backpack.speed = -2;
        backpack.weight = 10;
        backpack.always_active = true;
        backpack.description = "Backpack Info:\n\n Max Weight: +20 \n Speed: -2 \n Can only have one";
        backpack.maxCount = 1;

        Items shoes = new Items();
        shoes.initAtZero(shoes);
        shoes.name = "shoes";
        shoes.speed = 1;
        shoes.weight = 5;
        shoes.always_active = true;
        shoes.description = "Shoes Info:\n\n Speed: +1 \n Can only have one";
        shoes.maxCount = 1;

        Items flashlight = new Items();
        flashlight.initAtZero(flashlight);
        flashlight.name = "flashlight";
        flashlight.sight = 1;
        flashlight.weight = 5;
        flashlight.always_active = true;
        flashlight.description = "Flashlight Info: Sight: +1 \n Can only have one";
        flashlight.maxCount = 1;

        Items steroids = new Items();
        steroids.initAtZero(steroids);
        //steroids.health = -10;
        steroids.name = "steroids";
        steroids.strength = 0;
        steroids.weight = 1;
        steroids.always_active = false;
        steroids.description = "Steroids Info:\n\n Strength: +1 to your Base Strength \n Health: -15 \n One use per steroid";
        steroids.maxCount = 100;

        Items dumbell = new Items();
        dumbell.initAtZero(dumbell);
        dumbell.health = 0;
        dumbell.name = "dumbell";
        dumbell.strength = 2;
        dumbell.weight = 30;
        dumbell.always_active = true;
        dumbell.description = "Dumbell Info:\n\n Strength: +2";
        dumbell.maxCount = 100;

        all_items[0] = bandage;
        all_items[1] = glasses;
        all_items[2] = camo;
        all_items[3] = backpack;
        all_items[4] = shoes;
        all_items[5] = flashlight;
        all_items[6] = steroids;
        all_items[7] = dumbell;

        SaveSystem.SaveAllItems(all_items);
    }

}
