using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float food;
    public int health;
    public int distance;
    public Weapons weapon;
    public float x;
    public float diet_per_mile;
    public Hashtable inventory = new Hashtable();
    public float curr_weight;

    public PlayerData (Player player)
    {
        food = player.Food;
        health = player.health;
        distance = player.Distance;
        x = player.transform.position.x;
        weapon = player.weapon;
        inventory = player.inventory;
        curr_weight = player.curr_weight;
    }
}