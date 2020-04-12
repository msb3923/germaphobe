using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        Weapons.saveAllWeapons();
        Items.saveAllItems();
        Landmarks.saveAllLandmarks();
        Foods.saveAllFoods();
        Scene currScene = SceneManager.GetActiveScene();
        if (currScene.buildIndex == 10 || currScene.buildIndex == 17)
        {
            PlayerData player = SaveSystem.LoadDefault();
            setDefaultInventory(player.inventory);
            SaveSystem.SavePlayerData(player);
            SaveSystem.SaveLandmark("0");
        }
    }

    public void setDefaultInventory(Hashtable inventory)
    {
        inventory["bandages"] = 0;
        inventory["glasses"] = 0;
        inventory["camo"] = 0;
        inventory["backpack"] = 0;
        inventory["shoes"] = 0;
        inventory["flashlight"] = 0;
        inventory["dumbell"] = 0;
    }


    public void PlayGame ()
    {
        SceneManager.LoadScene(8);
    }

    public void ViewInstructions ()
    {
        SceneManager.LoadScene(2);
    }

    public void ReturnToMenu ()
    {
        SceneManager.LoadScene("Main Menu 1");
    }

    public void SelectSword ()
    {
        SaveSystem.SaveWeapon("bat");
        SceneManager.LoadScene(12);
    }

    public void SelectShield()
    {
        SaveSystem.SaveWeapon("trash can lid");
        SceneManager.LoadScene(12);
    }

    public void SelectChainsaw()
    {
        SaveSystem.SaveWeapon("chainsaw");
        SceneManager.LoadScene(12);
    }

    public void ReturnToWeapons()
    {
        SceneManager.LoadScene(4);
    }


    public void quit()
    {
        Application.Quit();
    }
}
