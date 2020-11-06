/*
Copyright (c) 2020 MrBacon470

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class UpgradesManager : MonoBehaviour
{
    public IdleGame game;

    public GameObject[] productionUpgrade = new GameObject[8];
    
    public Text[] productionUpgradeMaxText = new Text[8];
    public Text[] productionUpgradeText = new Text[8];

    public BigDouble[] productionUpgradeCost;
    public BigDouble[] productionUpgradeLevels;
    public BigDouble[] productionUpgradeCostMults;
    public BigDouble[] productionUpgradeBaseCosts;
    public BigDouble[] productionUpgradePower;

    

    private void Start()
    {
        productionUpgradeCostMults = new BigDouble[] { 1.05, 1.15, 1.25, 1.35, 1.45, 1.55, 1.65, 1.75 };
        productionUpgradePower = new BigDouble[] { 1, 5, 10, 100, 1e3, 1e4, 1e7, 1e10};
        productionUpgradeBaseCosts = new BigDouble[] { 10, 100, 1e3, 1e4, 1.5e4, 1e5, 1e6, 1e8 };
        productionUpgradeCost = new BigDouble[8];
        productionUpgradeLevels = new BigDouble[8];

        Deactivate();
    }

    public void RunUpgrades()
    {
        var data = game.data;
        ArrayManager();
        if(!data.isChallenge3Active)
        {
            productionUpgradeCost[0] = productionUpgradeBaseCosts[0] * Pow(productionUpgradeCostMults[0], data.productionUpgrade1Level);
            productionUpgradeCost[1] = productionUpgradeBaseCosts[1] * Pow(productionUpgradeCostMults[1], data.productionUpgrade2Level);
            productionUpgradeCost[2] = productionUpgradeBaseCosts[2] * Pow(productionUpgradeCostMults[2], data.productionUpgrade3Level);
            productionUpgradeCost[3] = productionUpgradeBaseCosts[3] * Pow(productionUpgradeCostMults[3], data.productionUpgrade4Level);
            productionUpgradeCost[4] = productionUpgradeBaseCosts[4] * Pow(productionUpgradeCostMults[4], data.productionUpgrade5Level);
            productionUpgradeCost[5] = productionUpgradeBaseCosts[5] * Pow(productionUpgradeCostMults[5], data.productionUpgrade6Level);
            productionUpgradeCost[6] = productionUpgradeBaseCosts[6] * Pow(productionUpgradeCostMults[6], data.productionUpgrade7Level);
            productionUpgradeCost[7] = productionUpgradeBaseCosts[7] * Pow(productionUpgradeCostMults[7], data.productionUpgrade8Level);
        }
        else
        {
            productionUpgradeCost[0] = productionUpgradeBaseCosts[0] * Pow(productionUpgradeCostMults[0] * 2, data.productionUpgrade1Level);
            productionUpgradeCost[1] = productionUpgradeBaseCosts[1] * Pow(productionUpgradeCostMults[1] * 2, data.productionUpgrade2Level);
            productionUpgradeCost[2] = productionUpgradeBaseCosts[2] * Pow(productionUpgradeCostMults[2] * 2, data.productionUpgrade3Level);
            productionUpgradeCost[3] = productionUpgradeBaseCosts[3] * Pow(productionUpgradeCostMults[3] * 2, data.productionUpgrade4Level);
            productionUpgradeCost[4] = productionUpgradeBaseCosts[4] * Pow(productionUpgradeCostMults[4] * 2, data.productionUpgrade5Level);
            productionUpgradeCost[5] = productionUpgradeBaseCosts[5] * Pow(productionUpgradeCostMults[5] * 2, data.productionUpgrade6Level);
            productionUpgradeCost[6] = productionUpgradeBaseCosts[6] * Pow(productionUpgradeCostMults[6] * 2, data.productionUpgrade7Level);
            productionUpgradeCost[7] = productionUpgradeBaseCosts[7] * Pow(productionUpgradeCostMults[7] * 2, data.productionUpgrade8Level);
        }

        data.productionUpgrade1Power = productionUpgradePower[0];
        data.productionUpgrade2Power = productionUpgradePower[1];
        data.productionUpgrade3Power = productionUpgradePower[2];
        data.productionUpgrade4Power = productionUpgradePower[3];
        data.productionUpgrade5Power = productionUpgradePower[4];
        data.productionUpgrade6Power = productionUpgradePower[5];
        data.productionUpgrade7Power = productionUpgradePower[6];
        data.productionUpgrade8Power = productionUpgradePower[7];
    }

    public void RunUpgradesUI()
    {
        var data = game.data;
        if (game.mainMenuGroup.gameObject.activeSelf)
        {
            GameObjects();
            for (var i = 0; i < 8; i++)
            {
                productionUpgradeMaxText[i].text = $"Buy Max ({BuyProductionUpgradeMaxCount(i)})";
            }

            if(data.hyperIndex <= 0)
                productionUpgradeText[0].text = data.isGen1Broken ? "BROKEN PLEASE REPAIR" : $"Manual Generator\nCost: {GetUpgradeCost(0, productionUpgradeCost)} Power\nPower: {Methods.NotationMethod(productionUpgradePower[0], "F2")} Power/s\nLevel: {GetUpgradeLevel(0, productionUpgradeLevels)}";
            else
                productionUpgradeText[0].text = data.isGen1Broken ? "BROKEN PLEASE REPAIR" : $"Hyper Manual Generator\nCost: {GetUpgradeCost(0, productionUpgradeCost)} Power\nPower: {Methods.NotationMethod(1e3, "F2")} Power/s\nLevel: {GetUpgradeLevel(0, productionUpgradeLevels)}";
            if(data.hyperIndex <= 1)
                productionUpgradeText[1].text = data.isGen2Broken ? "BROKEN PLEASE REPAIR" : $"Wood Burner\nCost   {GetUpgradeCost(1, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[1], "F2")} Power/s\nLevel: {GetUpgradeLevel(1, productionUpgradeLevels)}";
            else
                productionUpgradeText[1].text = data.isGen2Broken ? "BROKEN PLEASE REPAIR" : $"Hyper Wood Burner\nCost   {GetUpgradeCost(1, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(5e3, "F2")} Power/s\nLevel: {GetUpgradeLevel(1, productionUpgradeLevels)}";
            if(data.hyperIndex <= 2)
                productionUpgradeText[2].text = data.isGen3Broken ? "BROKEN PLEASE REPAIR" : $"Coal Generator\nCost   {GetUpgradeCost(2, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[2], "F2")} Power/s\nLevel: {GetUpgradeLevel(2, productionUpgradeLevels)}";
            else
                productionUpgradeText[2].text = data.isGen3Broken ? "BROKEN PLEASE REPAIR" : $"Hyper Coal Generator\nCost   {GetUpgradeCost(2, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(1e4, "F2")} Power/s\nLevel: {GetUpgradeLevel(2, productionUpgradeLevels)}";
            if(data.hyperIndex <= 3)
                productionUpgradeText[3].text = data.isGen4Broken ? "BROKEN PLEASE REPAIR" : $"Oil Generator\nCost   {GetUpgradeCost(3, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[3], "F2")} Power/s\nLevel: {GetUpgradeLevel(3, productionUpgradeLevels)}";
            else
                productionUpgradeText[3].text = data.isGen4Broken ? "BROKEN PLEASE REPAIR" : $"Hyper Oil Generator\nCost   {GetUpgradeCost(3, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(1e5, "F2")} Power/s\nLevel: {GetUpgradeLevel(3, productionUpgradeLevels)}";
            if(data.hyperIndex <= 4)
                productionUpgradeText[4].text = data.isGen5Broken ? "BROKEN PLEASE REPAIR" : $"Natural Gas Generator\nCost   {GetUpgradeCost(4, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[4], "F2")} Power/s\nLevel: {GetUpgradeLevel(4, productionUpgradeLevels)}";
            else
                productionUpgradeText[4].text = data.isGen5Broken ? "BROKEN PLEASE REPAIR" : $"Hyper Natural Gas Generator\nCost   {GetUpgradeCost(4, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(1e6, "F2")} Power/s\nLevel: {GetUpgradeLevel(4, productionUpgradeLevels)}";
            if(data.hyperIndex <= 5)
                productionUpgradeText[5].text = data.isGen6Broken ? "BROKEN PLEASE REPAIR" : $"Steam Turbine\nCost   {GetUpgradeCost(5, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[5], "F2")} Power/s\nLevel: {GetUpgradeLevel(5, productionUpgradeLevels)}";
            else
                productionUpgradeText[5].text = data.isGen6Broken ? "BROKEN PLEASE REPAIR" : $"Hyper sSteam Turbine\nCost   {GetUpgradeCost(5, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(1e7, "F2")} Power/s\nLevel: {GetUpgradeLevel(5, productionUpgradeLevels)}";
            if(data.hyperIndex <= 6)
                productionUpgradeText[6].text = data.isGen7Broken ? "BROKEN PLEASE REPAIR" : $"Nuclear Reactor\nCost   {GetUpgradeCost(6, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[6], "F2")} Power/s\nLevel: {GetUpgradeLevel(6, productionUpgradeLevels)}";
            else
                productionUpgradeText[6].text = data.isGen7Broken ? "BROKEN PLEASE REPAIR" : $"Hyper Nuclear Reactor\nCost   {GetUpgradeCost(6, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(1e10, "F2")} Power/s\nLevel: {GetUpgradeLevel(6, productionUpgradeLevels)}";
            if(data.hyperIndex <= 7)
                productionUpgradeText[7].text = data.isGen8Broken ? "BROKEN PLEASE REPAIR" : $"Fusion Reactor\nCost   {GetUpgradeCost(7, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[7], "F2")} Power/s\nLevel: {GetUpgradeLevel(7, productionUpgradeLevels)}";
            else
                productionUpgradeText[7].text = data.isGen8Broken ? "BROKEN PLEASE REPAIR" : $"Hyper Fusion Reactor\nCost   {GetUpgradeCost(7, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(1e20, "F2")} Power/s\nLevel: {GetUpgradeLevel(7, productionUpgradeLevels)}";

        }

        string GetUpgradeCost(int index, BigDouble[] upgrade)
            {
                return Methods.NotationMethod(upgrade[index], "F2");
            }

            string GetUpgradeLevel(int index, BigDouble[] upgradeLevel)
            {
                return Methods.NotationMethod(upgradeLevel[index], "F2");
            }

    }

    public void GameObjects()
    {
        var data = game.data;
        if (data.isCompleted0)
        {
            productionUpgrade[0].gameObject.SetActive(true);
        }
        if (data.isCompleted0) return;
        if (data.isCompleted1)
        {
            productionUpgrade[0].gameObject.SetActive(true);
            if(!data.isChallenge1Active && !data.isChallenge3Active)
                productionUpgrade[1].gameObject.SetActive(true);
        }
        if (data.isCompleted1) return;
        if (data.isCompleted2)
        {
            productionUpgrade[0].gameObject.SetActive(true);
            if (!data.isChallenge1Active && !data.isChallenge3Active)
            {
                productionUpgrade[1].gameObject.SetActive(true);
                productionUpgrade[2].gameObject.SetActive(true);
            }
        }
        if (data.isCompleted2) return;
        if (data.isCompleted3)
        {
            productionUpgrade[0].gameObject.SetActive(true);
            if (!data.isChallenge1Active && !data.isChallenge3Active)
            {
                productionUpgrade[1].gameObject.SetActive(true);
                productionUpgrade[2].gameObject.SetActive(true);
                productionUpgrade[3].gameObject.SetActive(true);
            }
        }
        if (data.isCompleted3) return;
        if (data.isCompleted4)
        {
            productionUpgrade[0].gameObject.SetActive(true);
            if (!data.isChallenge1Active && !data.isChallenge3Active)
            {
                productionUpgrade[1].gameObject.SetActive(true);
                productionUpgrade[2].gameObject.SetActive(true);
                productionUpgrade[3].gameObject.SetActive(true);
                productionUpgrade[4].gameObject.SetActive(true);
            }
        }
        if (data.isCompleted4) return;
        if (data.isCompleted5)
        {
            productionUpgrade[0].gameObject.SetActive(true);
            productionUpgrade[5].gameObject.SetActive(true);
            if (!data.isChallenge1Active && !data.isChallenge3Active)
            {
                productionUpgrade[1].gameObject.SetActive(true);
                productionUpgrade[2].gameObject.SetActive(true);
                productionUpgrade[3].gameObject.SetActive(true);
                productionUpgrade[4].gameObject.SetActive(true);
            }
        }
        if (data.isCompleted5) return;
        if (data.isCompleted6)
        {
            productionUpgrade[0].gameObject.SetActive(true);
            productionUpgrade[5].gameObject.SetActive(true);
            if (!data.isChallenge1Active && !data.isChallenge3Active)
            {
                productionUpgrade[1].gameObject.SetActive(true);
                productionUpgrade[2].gameObject.SetActive(true);
                productionUpgrade[3].gameObject.SetActive(true);
                productionUpgrade[4].gameObject.SetActive(true);
                productionUpgrade[6].gameObject.SetActive(true);
            }
        }
        if (data.isCompleted6) return;
        if (data.isCompleted7)
        {
            productionUpgrade[0].gameObject.SetActive(true);
            productionUpgrade[5].gameObject.SetActive(true);
            productionUpgrade[7].gameObject.SetActive(true);
            if (!data.isChallenge1Active && !data.isChallenge3Active)
            {
                productionUpgrade[1].gameObject.SetActive(true);
                productionUpgrade[2].gameObject.SetActive(true);
                productionUpgrade[3].gameObject.SetActive(true);
                productionUpgrade[4].gameObject.SetActive(true);
                productionUpgrade[6].gameObject.SetActive(true);
            }
        }
    }

    private void ArrayManager()
    {
        var data = game.data;

        productionUpgradeLevels[0] = data.productionUpgrade1Level;
        productionUpgradeLevels[1] = data.productionUpgrade2Level;
        productionUpgradeLevels[2] = data.productionUpgrade3Level;
        productionUpgradeLevels[3] = data.productionUpgrade4Level;
        productionUpgradeLevels[4] = data.productionUpgrade5Level;
        productionUpgradeLevels[5] = data.productionUpgrade6Level;
        productionUpgradeLevels[6] = data.productionUpgrade7Level;
        productionUpgradeLevels[7] = data.productionUpgrade8Level;
    }

    private void NonArrayManager()
    {
        var data = game.data;

        

        data.productionUpgrade1Level = productionUpgradeLevels[0];
        data.productionUpgrade2Level = productionUpgradeLevels[1];
        data.productionUpgrade3Level = productionUpgradeLevels[2];
        data.productionUpgrade4Level = productionUpgradeLevels[3];
        data.productionUpgrade5Level = productionUpgradeLevels[4];
        data.productionUpgrade6Level = productionUpgradeLevels[5];
        data.productionUpgrade7Level = productionUpgradeLevels[6];
        data.productionUpgrade8Level = productionUpgradeLevels[7];
    }

public void BuyProductionUpgradeMax(int index)
{
    var data = game.data;
    var b1 = productionUpgradeBaseCosts[index];
    var c1 = data.power;
    var r1 = productionUpgradeCostMults[index];
    var k1 = productionUpgradeLevels[index];
    var n1 = Floor(Log((c1 * (r1 - 1) / (b1 * Pow(r1, k1))) + 1, r1));

    var cost2 = b1 * (Pow(r1, k1) * (Pow(r1, n1) - 1) / (r1 - 1));

    if (data.power >= cost2)
    {
        productionUpgradeLevels[index] += n1;
        data.power -= cost2;
    }
    NonArrayManager();
}

public void BuyProductionUpgrade(int index)
    {
        var data = game.data;
        if (data.power >= productionUpgradeCost[index])
        {
            productionUpgradeLevels[index]++;
            data.power -= productionUpgradeCost[index];
        }
        NonArrayManager();
    }
        //maxcounts

    public BigDouble BuyProductionUpgradeMaxCount(int index)
    {
        var data = game.data;
        var b = productionUpgradeBaseCosts[index];
        var c = data.power;
        var r = productionUpgradeCostMults[index];
        var k = productionUpgradeLevels[index];
        var n = Floor(Log((c * (r - 1) / (b * Pow(r, k))) + 1, r));
        return n;
    }

    public void Deactivate()
    {
        productionUpgrade[1].gameObject.SetActive(false);
        productionUpgrade[2].gameObject.SetActive(false);
        productionUpgrade[3].gameObject.SetActive(false);
        productionUpgrade[4].gameObject.SetActive(false);
        productionUpgrade[5].gameObject.SetActive(false);
        productionUpgrade[6].gameObject.SetActive(false);
        productionUpgrade[7].gameObject.SetActive(false);
    }
}
