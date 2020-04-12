using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text : MonoBehaviour
{
    private int health;
    private int energy;
    private int stat1;
    private int stat2;
    private int stat3;
    private int stat4;
    private string mainText;
    private string button1;
    private string button2;

    

    public int Health {
        get{ return health; }
        set {
            if (value.GetType() != health.GetType())
            {
                Debug.Log("Value for Health must be an int");
            }
            health = value;
            GameObject.Find("Canvas/Health").GetComponent<Text>().text = "Health: " + Health.ToString();
        }
    }

    public int Energy
    {
        get { return energy; }
        set
        {
            energy = value;
            GameObject.Find("Canvas/Energy").GetComponent<Text>().text = "Energy: " + Energy.ToString();
        }
    }

    public int Stat1
    {
        get { return stat1; }
        set
        {
            stat1 = value;
            GameObject.Find("Canvas/Stat1").GetComponent<Text>().text = "Stat 1: " + Stat1.ToString();
        }
    }

    public int Stat2
    {
        get { return stat2; }
        set
        {
            stat2 = value;
            GameObject.Find("Canvas/Stat2").GetComponent<Text>().text = "Stat 2: " + Stat2.ToString();
        }
    }

    public int Stat3
    {
        get { return stat3; }
        set
        {
            stat3 = value;
            GameObject.Find("Canvas/Stat3").GetComponent<Text>().text = "Stat 3: " + Stat3.ToString();
        }
    }

    public int Stat4
    {
        get { return stat4; }
        set
        {
            stat4 = value;
            GameObject.Find("Canvas/Stat4").GetComponent<Text>().text = "Stat 4: " + Stat4.ToString();
        }
    }

    public string MainText
    {
        get { return mainText; }
        set
        {
            mainText = value;
            GameObject.Find("Canvas/MainText").GetComponent<Text>().text = MainText;
        }
    }

    public string Button1
    {
        get { return button1; }
        set
        {
            button1 = value;
            GameObject.Find("Canvas/Button1/Text").GetComponent<Text>().text = Button1;
        }
    }

    public string Button2
    {
        get { return button2; }
        set
        {
            button2 = value;
            GameObject.Find("Canvas/Button2/Text").GetComponent<Text>().text = Button2;
        }
    }
    /*
    public int Health = 100;
    public int Energy = 100;

    public int Stat1 = 5;
    public int Stat2 = 5;
    public int Stat3 = 5;
    public int Stat4 = 5;*/

    // Start is called before the first frame update
    void Start()
    {
        Health = 100;
        Energy = 100;
        
        Stat1 = 5;
        Stat2 = 6;
        Stat3 = 2;
        Stat4 = 7;

        MainText = "On your journey you have come across an abandoned house. It looks as if it has remained relatively untouched since the outbreak. Do you venture inside?" +
            "(Exploring will allow you to find loots but also will consume 10 Energy with the possibility of encountering Zombies)";

        Button1 = "Yes";
        Button2 = "No";
        /*
        Text HealthText = GameObject.Find("Canvas/Health").GetComponent<Text>();
        Text EnergyText = GameObject.Find("Canvas/Energy").GetComponent<Text>();

        HealthText.text = "Health: " + Health.ToString();
        EnergyText.text = "Energy: " + Energy.ToString();

        Text Stat1Text = GameObject.Find("Canvas/Stat1").GetComponent<Text>();
        Text Stat2Text = GameObject.Find("Canvas/Stat2").GetComponent<Text>();
        Text Stat3Text = GameObject.Find("Canvas/Stat3").GetComponent<Text>();
        Text Stat4Text = GameObject.Find("Canvas/Stat4").GetComponent<Text>();

        Stat1Text.text = "Stat 1: " + Stat1.ToString();
        Stat2Text.text = "Stat 2: " + Stat2.ToString();
        Stat3Text.text = "Stat 3: " + Stat3.ToString();
        Stat4Text.text = "Stat 4: " + Stat4.ToString();

        Text Option1 = GameObject.Find("Canvas/Option1/Text").GetComponent<Text>();
        Text Option2 = GameObject.Find("Canvas/Option2/Text").GetComponent<Text>();

        Option1.text = "Yes";
        Option2.text = "No";

        Text MainText = GameObject.Find("Canvas/MainText").GetComponent<Text>();
        MainText.text = "On your journey you have come across an abandoned house. It looks as if it has remained relatively untouched since the outbreak. Do you venture inside?" +
            "(Exploring will allow you to find loots but also will consume 10 Energy with the possibility of encountering Zombies)";
*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
