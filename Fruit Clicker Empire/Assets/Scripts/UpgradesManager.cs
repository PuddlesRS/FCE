using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BreakInfinity;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager instance;
    private Controller controller;
    public float clickPow;
    private void Awake()
    {
        instance = this;
    }

    public List<Upgrades> clickUpgrades;
    public Upgrades clickUpgradePrefab;

    public List<Upgrades> productionUpgrades;
    public Upgrades productionUpgradesPrefab;

    public ScrollRect clickUpgradesScroll;
    public GameObject clickUpgradesPanel;

    public ScrollRect productionUpgradeScroll;
    public Transform productionUpgradesPanel;

    public string[] clickUpgradeNames;

    public BigDouble[] clickUpgradesBaseCost;
    public BigDouble clickUpgradeCostMult;
    public BigDouble[] clickUpgradesBasePower;

    //Initiates upgrade system on start
    public void StartUpgradeManager()
    {

        clickUpgradeNames = new[] { "Click Power + 1", "Click Power +5", "Click Power +10"};
        clickUpgradesBaseCost = new BigDouble[] { 10, 50, 100 };
        clickUpgradeCostMult = new BigDouble(1.1);
        clickUpgradesBasePower = new BigDouble[] { 1, 5, 10 };

        //For every upgrade in clickUpgradeLevel.Count create a new instance of clickUpgradePrefab and assign it an ID
        for (int i = 0; i < Controller.instance.data.clickUpgradeLevel.Count; i++)
        {
            Upgrades upgrade = Instantiate(clickUpgradePrefab, clickUpgradesPanel.transform);
            upgrade.UpgradeID = i;
            clickUpgrades.Add(upgrade);
        }
        clickUpgradesScroll.normalizedPosition = new Vector2(0, 0);

        UpdateclickUpgradeUI();

        Methods.UpgradeCheck(Controller.instance.data.clickUpgradeLevel, 3);
    }

    private static void CheckClickPow()
    {
        clickPow = controller.ClickPower();
    }
    //Updates the upgrade UI when called
    public void UpdateclickUpgradeUI(int UpgradeID = -1)
    {
        var data = Controller.instance.data;
        if (UpgradeID == -1)
        {
            for (int i = 0; i < clickUpgrades.Count; i++) 
            {
                UpdateUI(i);
            }
        }
        else
        {
            UpdateUI(UpgradeID);
        }

        void UpdateUI(int ID)
        {
            clickUpgrades[ID].LevelText.text = data.clickUpgradeLevel[ID].ToString();
            clickUpgrades[ID].CostText.text = "$" + $"Cost: {ClickUpgradeCost(ID):F2}";
            clickUpgrades[ID].NameText.text = clickUpgradeNames[ID];
        }
    }

    //Returns the cost of upgrade
    public BigDouble ClickUpgradeCost(int UpgradeID)
    {
        //return clickUpgradesBaseCost[UpgradeID] * BigDouble.Pow(clickUpgradeCostMult[UpgradeID], Controller.instance.data.clickUpgradeLevel[UpgradeID]);
        return clickUpgradesBaseCost[UpgradeID] * (clickUpgradeCostMult);
    }

    //Purchase upgrade if enough money
    public void BuyUpgrade(int UpgradeID)
    {
        var data = Controller.instance.data;
        if (data.money >= ClickUpgradeCost(UpgradeID))
        {
           data.money -= ClickUpgradeCost(UpgradeID);
           data.clickUpgradeLevel[UpgradeID] += 1;
       }

        UpdateclickUpgradeUI(UpgradeID);
    }
}
