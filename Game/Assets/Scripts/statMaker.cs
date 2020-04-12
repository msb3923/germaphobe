using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class statMaker : MonoBehaviour
{
    public int Strength;
    public int Sight;
    public int Speed;
    public int Stealth;
    public int pointsLeft;
    public TextMeshProUGUI totalPoints;
    public TextMeshProUGUI strength;
    public TextMeshProUGUI sight;
    public TextMeshProUGUI speed;
    public TextMeshProUGUI stealth;

    public Slider StrengthSlider;
    public Slider SightSlider;
    public Slider SpeedSlider;
    public Slider StealthSlider;
    public Button done;
    public int[] stats;



    public void up(string type)
    {
        if (pointsLeft > 0)
        {
            if (type == "Strength")
            {
                if (Strength == 10)
                {

                }
                else
                {
                    Strength += 1;
                    strength.text = Strength.ToString();
                }

            }
            else if (type == "Sight")
            {
                if (Sight == 10)
                {

                }
                else
                {
                    Sight += 1;
                    sight.text = Sight.ToString();
                }
            }
            else if (type == "Stealth")
            {
                if (Stealth == 10)
                { }
                else
                {
                    Stealth += 1;
                    stealth.text = Stealth.ToString();
                }
            }
            else
            {
                if (Speed == 10)
                { }
                else
                {
                    Speed += 1;
                    speed.text = Speed.ToString();
                }
            }
        }
    }
    public void down(string type)
    {
        if (pointsLeft < 20)
        {
            if (type == "Strength")
            {
                if (Strength == 0)
                {

                }
                else
                {
                    Strength -= 1;
                    strength.text = Strength.ToString();
                }

            }
            else if (type == "Sight")
            {
                if (Sight == 0)
                {

                }
                else
                {
                    Sight -= 1;
                    sight.text = Sight.ToString();
                }
            }
            else if (type == "Stealth")
            {
                if (Stealth == 0)
                { }
                else
                {
                    Stealth -= 1;
                    stealth.text = Stealth.ToString();
                }
            }
            else
            {
                if (Speed == 0)
                { }
                else
                {
                    Speed -= 1;
                    speed.text = Speed.ToString();
                }
            }
        }
    }



    private void Update()
    {
        Image back = GameObject.FindGameObjectWithTag("cont").GetComponent<Image>();
        if (pointsLeft != 0)
        {
            done.enabled = false;
            back.color = Color.red;
        }
        else
        {
            done.enabled = true;
            Color32 grey = new Color32(0x7B, 0x78, 0x78, 0xFF);
            back.color = grey;
        }

        pointsLeft = 20 - Strength - Sight - Speed - Stealth;

        totalPoints.text = "Points Left: " + pointsLeft.ToString();

        
    }

    public void goToWeapon ()
    {
        stats = new int[4];
        stats[0] = Strength;
        stats[1] = Sight;
        stats[2] = Speed;
        stats[3] = Stealth;
        SaveSystem.SaveStats(stats);
        SceneManager.LoadScene(4);
    }

}
