using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

public static class SaveSystem
{
    public static void SavePlayer (Player player)
	{
		BinaryFormatter formatter = new BinaryFormatter();

		string path = Application.persistentDataPath + "/player.txt";

		FileStream stream = new FileStream(path, FileMode.Create);

		PlayerData data = new PlayerData(player);

		formatter.Serialize(stream, data);
		stream.Close();
	}

    public static void SavePlayerData(PlayerData player)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/playerD.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = player;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveDefault(Player player)
	{
		BinaryFormatter formatter = new BinaryFormatter();

		string path = Application.persistentDataPath + "/def.txt";

		FileStream stream = new FileStream(path, FileMode.Create);

		PlayerData data = new PlayerData(player);

		formatter.Serialize(stream, data);
		stream.Close();
	}

    public static PlayerData LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/playerD.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

    public static PlayerData LoadPlayer()
	{
		string path = Application.persistentDataPath + "/player.txt";

        if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			PlayerData data = formatter.Deserialize(stream) as PlayerData;
			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError("Save file not found");
			return null;
		}
	}

	public static PlayerData LoadDefault()
	{
		string path = Application.persistentDataPath + "/def.txt";

		if (File.Exists(path))
		{
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream stream = new FileStream(path, FileMode.Open);

			PlayerData data = formatter.Deserialize(stream) as PlayerData;
			stream.Close();

			return data;
		}
		else
		{
			Debug.LogError("Save file not found");
			return null;
		}
	}

    public static void SaveWeapon(string weapon)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/weapon.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        string data = weapon;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveWeapons(Weapons[] weapons)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/weapons.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        Weapons[] data = weapons;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveFoods(Foods[] foods)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/foods.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        Foods[] data = foods;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Foods LoadFoods(int idx)
    {
        string path = Application.persistentDataPath + "/foods.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Foods[] data = formatter.Deserialize(stream) as Foods[];

            stream.Close();

            return data[idx];
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

    public static void SaveLandmark(string idx)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/landmarkIdx.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        string data = idx;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveLandmarks(Landmarks[] landmarks)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/landmarks.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        Landmarks[] data = landmarks;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static int LoadLandmark()
    {
        string path = Application.persistentDataPath + "/landmarkIdx.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            string data = formatter.Deserialize(stream) as string;
            stream.Close();

            return System.Int32.Parse(data);
        }
        else
        {
            Debug.LogError("Save file not found");
            return default(int);
        }
    }

    public static Landmarks LoadLandmarks(int landmarkIdx)
    {
        string path = Application.persistentDataPath + "/landmarks.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Landmarks[] data = formatter.Deserialize(stream) as Landmarks[];

            stream.Close();      

            return data[landmarkIdx];
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

    public static string LoadWeapon()
    {
        string path = Application.persistentDataPath + "/weapon.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            string data = formatter.Deserialize(stream) as string;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

    public static Weapons LoadWeapons(string weapon)
    {
        string path = Application.persistentDataPath + "/weapons.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Weapons[] data = formatter.Deserialize(stream) as Weapons[];

            stream.Close();

            Weapons my_weapon = new Weapons();

            foreach(Weapons curr_weapon in data)
            {
                if (curr_weapon.name == weapon)
                {
                    my_weapon = curr_weapon;
                    break;
                }
            }

            return my_weapon;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

    public static void SaveStats(int[] stats)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/stats.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        int[] data = stats;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static int[] LoadStats()
    {
        string path = Application.persistentDataPath + "/stats.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            int[] data = formatter.Deserialize(stream) as int[];
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

    public static void SaveFood(string intake)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/food.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        string data = intake;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static string LoadFood()
    {
        string path = Application.persistentDataPath + "/food.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            string data = formatter.Deserialize(stream) as string;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

    public static void SaveDeathMessage(string death_message)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/death.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        string data = death_message;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static string LoadDeathMessage()
    {
        string path = Application.persistentDataPath + "/death.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            string data = formatter.Deserialize(stream) as string;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

    public static void SaveAllItems(Items[] all_items)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/items.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        Items[] data = all_items;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static Items LoadItem(string item_name)
    {
        string path = Application.persistentDataPath + "/items.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Items[] data = formatter.Deserialize(stream) as Items[];

            stream.Close();

            Items my_item = new Items();

            foreach (Items curr_item in data)
            {
                if (curr_item.name == item_name)
                {
                    my_item = curr_item;
                    break;
                }
            }

            return my_item;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }

    public static void SaveFoundItem(string found_item)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/found.txt";

        FileStream stream = new FileStream(path, FileMode.Create);

        string data = found_item;

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static string LoadFoundItem()
    {
        string path = Application.persistentDataPath + "/found.txt";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            string data = formatter.Deserialize(stream) as string;

            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("Save file not found");
            return null;
        }
    }




}
