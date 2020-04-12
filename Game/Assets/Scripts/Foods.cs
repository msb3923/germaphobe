using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Foods
{
    public string name;
    public int weight;
    public int health;

    public static void saveAllFoods()
    {
        Foods[] all_foods;
        all_foods = new Foods[9];

        Foods strawberries = new Foods();
        strawberries.name = "strawberries";
        strawberries.weight = 2;
        strawberries.health = 1;

        Foods twinkies = new Foods();
        twinkies.name = "twinkies";
        twinkies.weight = 3;
        twinkies.health = -1;

        Foods squirrel = new Foods();
        squirrel.name = "squirrel";
        squirrel.weight = 2;
        squirrel.health = 0;

        Foods rabbit = new Foods();
        rabbit.name = "rabbit";
        rabbit.weight = 4;
        rabbit.health = 0;

        Foods obese_rabbit = new Foods();
        obese_rabbit.name = "obese rabbit";
        obese_rabbit.weight = 8;
        obese_rabbit.health = 0;

        Foods coyote = new Foods();
        coyote.name = "coyote";
        coyote.weight = 15;
        coyote.health = 0;

        Foods raccoon = new Foods();
        raccoon.name = "raccoon";
        raccoon.weight = 10;
        raccoon.health = 0;

        Foods possum = new Foods();
        possum.name = "possum";
        possum.weight = 5;
        possum.health = 0;

        Foods fox = new Foods();
        fox.name = "fox";
        fox.weight = 7;
        fox.health = 0;

        all_foods[0] = strawberries;
        all_foods[1] = twinkies;
        all_foods[2] = squirrel;
        all_foods[3] = rabbit;
        all_foods[4] = obese_rabbit;
        all_foods[5] = coyote;
        all_foods[6] = raccoon;
        all_foods[7] = possum;
        all_foods[8] = fox;


        SaveSystem.SaveFoods(all_foods);

    }
}