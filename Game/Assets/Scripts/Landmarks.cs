using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Landmarks
{
    public int in_position;
    public int out_position;
    public int build_number;
    public string message;

    public static void saveAllLandmarks()
    {
        Landmarks[] all_landmarks;

        Landmarks abandonedHouse = new Landmarks();
        abandonedHouse.in_position = 95;
        abandonedHouse.out_position = 185;
        abandonedHouse.build_number = 11;
        abandonedHouse.message = "You've come to an abandoned house.";

        Landmarks dustyTown = new Landmarks();
        dustyTown.in_position = 310;
        dustyTown.out_position = 450; //just a guess
        dustyTown.build_number = 13;
        dustyTown.message = "You approach a dusty town.";

        Landmarks campfire = new Landmarks();
        campfire.in_position = 600;
        campfire.out_position = 670; //guess
        campfire.build_number = 14;
        campfire.message = "You see smoke in the distance.";

        Landmarks cabin = new Landmarks();
        cabin.in_position = 800;
        cabin.out_position = 870;
        cabin.build_number = 15;
        cabin.message = "You see a cabin in the distance.";

        Landmarks arena = new Landmarks();
        arena.in_position = 970;
        arena.out_position = 1050;
        arena.build_number = 16;
        arena.message = "A dark figure grabs you!";

        Landmarks safehouse = new Landmarks();
        safehouse.in_position = 1100;
        safehouse.out_position = 1170;
        safehouse.build_number = 17;
        safehouse.message = "You see a gleaming light ahead";

        all_landmarks = new Landmarks[6];

        all_landmarks[0] = abandonedHouse;
        all_landmarks[1] = dustyTown;
        all_landmarks[2] = campfire;
        all_landmarks[3] = cabin;
        all_landmarks[4] = arena;
        all_landmarks[5] = safehouse;


        SaveSystem.SaveLandmarks(all_landmarks);

    }
}