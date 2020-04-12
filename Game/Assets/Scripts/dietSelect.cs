using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class dietSelect : MonoBehaviour
{
    public TextMeshProUGUI intake;
    public Button Meager;
    public Button Moderate;
    public Button Plentiful;
    public Button cont;

    void Update()
    {
        Image plentifulImage = GameObject.FindGameObjectWithTag("plentiful").GetComponent<Image>();
        Image moderateImage = GameObject.FindGameObjectWithTag("moderate").GetComponent<Image>();
        Image meagerImage = GameObject.FindGameObjectWithTag("meager").GetComponent<Image>();
        Color32 grey = new Color32(0x7B, 0x78, 0x78, 0xFF);
        if (meagerImage.color == grey & moderateImage.color == grey & plentifulImage.color == grey)
        {
            cont.enabled = false;
        }
        else
        {
            cont.enabled = true;
        }
    }

    public void meager()
    {
        Image plentifulImage = GameObject.FindGameObjectWithTag("plentiful").GetComponent<Image>();
        Image moderateImage = GameObject.FindGameObjectWithTag("moderate").GetComponent<Image>();
        Image meagerImage = GameObject.FindGameObjectWithTag("meager").GetComponent<Image>();

        SaveSystem.SaveFood("meager");
        intake.text = "Intake: " + "meager";

        Color32 grey = new Color32(0x7B, 0x78, 0x78, 0xFF);
        Color32 red = new Color32(0xE9, 0X0E, 0x0E, 0xFF);

        plentifulImage.color = grey;
        moderateImage.color = grey;
        meagerImage.color = red;
    }
    public void moderate()
    {
        Image plentifulImage = GameObject.FindGameObjectWithTag("plentiful").GetComponent<Image>();
        Image moderateImage = GameObject.FindGameObjectWithTag("moderate").GetComponent<Image>();
        Image meagerImage = GameObject.FindGameObjectWithTag("meager").GetComponent<Image>();

        SaveSystem.SaveFood("moderate");
        intake.text = "Intake: " + "moderate";

        Color32 grey = new Color32(0x7B, 0x78, 0x78, 0xFF);
        Color32 yellow = new Color32(0xFA, 0XF2, 0x1E, 0xFF);

        plentifulImage.color = grey;
        moderateImage.color = yellow;
        meagerImage.color = grey;
    }
    public void plentiful()
    {
        Image plentifulImage = GameObject.FindGameObjectWithTag("plentiful").GetComponent<Image>();
        Image moderateImage = GameObject.FindGameObjectWithTag("moderate").GetComponent<Image>();
        Image meagerImage = GameObject.FindGameObjectWithTag("meager").GetComponent<Image>();

        SaveSystem.SaveFood("plentiful");
        intake.text = "Intake: " + "plentiful";

        Color32 grey = new Color32(0x7B, 0x78, 0x78, 0xFF);
        Color32 green = new Color32(0x0F, 0XF8, 0x06, 0xFF);

        plentifulImage.color = green;
        moderateImage.color = grey;
        meagerImage.color = grey;
    }

    public void beginGame()
    {
        SceneManager.LoadScene("Kirnys");
    }

}
