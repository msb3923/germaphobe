using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class deathScript : MonoBehaviour
{
    public TextMeshProUGUI deathMessage;
    // Start is called before the first frame update
    void Start()
    {
        deathMessage.text = SaveSystem.LoadDeathMessage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
