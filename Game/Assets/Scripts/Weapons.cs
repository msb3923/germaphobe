using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapons
{
    public string name;
    public int power;
    public int weight;
    public int range;
    public int noise;

    private void Start()
    {
        saveAllWeapons();
    }

    public static void saveAllWeapons ()
    {
        Weapons[] all_weapons;

        Weapons bat = new Weapons();
        bat.name = "bat";
        bat.power = 4;
        bat.weight = 10;
        bat.range = 2;
        bat.noise = 2;

        Weapons trash_can_lid = new Weapons();
        trash_can_lid.name = "trash can lid";
        trash_can_lid.power = 2;
        trash_can_lid.weight = 15;
        trash_can_lid.range = 1;
        trash_can_lid.noise = 0;

        Weapons chainsaw = new Weapons();
        chainsaw.name = "chainsaw";
        chainsaw.power = 8;
        chainsaw.weight = 30;
        chainsaw.range = 4;
        chainsaw.noise = 7;

        all_weapons = new Weapons[3];

        all_weapons[0] = bat;
        all_weapons[1] = trash_can_lid;
        all_weapons[2] = chainsaw;

        SaveSystem.SaveWeapons(all_weapons);

    }
}




