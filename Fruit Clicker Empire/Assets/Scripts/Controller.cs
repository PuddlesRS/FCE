using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BreakInfinity;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    public static Controller instance;
    private void Awake()
    {
        instance = this;
    }

    public TMP_Text moneyText;
    public GameData data;

    GameObject AppleTree;
    
    //Determines clickPower
     public BigDouble ClickPower()
    {
        BigDouble total = 1;
        for (int i = 0; i < data.clickUpgradeLevel.Count; i++)
        {
            total += UpgradesManager.instance.clickUpgradesBasePower[i] * data.clickUpgradeLevel[i];
        }

        return total;
    }

    public void Start()
    {
        data = new GameData();
        UpgradesManager.instance.StartUpgradeManager();
    }

    public void Update()
    {
        moneyText.text = "$" + data.money;
    }

    //Increase money by clickPower
    public void HarvestApples()
    {
        data.money += ClickPower();
    }
}
