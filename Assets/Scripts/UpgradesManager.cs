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
        productionUpgradeCostMults = new BigDouble[] { .25, .50, .75, 1.25, 1.5, 1.75, 2, 2.5 };
        productionUpgradePower = new BigDouble[] { 10, 50, 100, 1e3, 1e4, 1e5, 1e7, 1e10};
        productionUpgradeBaseCosts = new BigDouble[] { 10, 100, 1e3, 1e4, 1.5e4, 1e5, 1e6, 1e8 };
        productionUpgradeCost = new BigDouble[8];
        productionUpgradeLevels = new BigDouble[8];
    }

    public void RunUpgrades()
    {
        var data = game.data;
        ArrayManager();

        productionUpgradeCost[0] = productionUpgradeBaseCosts[0] * Pow(productionUpgradeCostMults[0], data.productionUpgrade1Level);
        productionUpgradeCost[1] = productionUpgradeBaseCosts[1] * Pow(productionUpgradeCostMults[1], data.productionUpgrade2Level);
        productionUpgradeCost[2] = productionUpgradeBaseCosts[2] * Pow(productionUpgradeCostMults[2], data.productionUpgrade3Level);
        productionUpgradeCost[3] = productionUpgradeBaseCosts[3] * Pow(productionUpgradeCostMults[3], data.productionUpgrade4Level);
        productionUpgradeCost[4] = productionUpgradeBaseCosts[4] * Pow(productionUpgradeCostMults[4], data.productionUpgrade5Level);
        productionUpgradeCost[5] = productionUpgradeBaseCosts[5] * Pow(productionUpgradeCostMults[5], data.productionUpgrade6Level);
        productionUpgradeCost[6] = productionUpgradeBaseCosts[6] * Pow(productionUpgradeCostMults[6], data.productionUpgrade7Level);
        productionUpgradeCost[7] = productionUpgradeBaseCosts[7] * Pow(productionUpgradeCostMults[7], data.productionUpgrade8Level); 

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

            for (var i = 0; i < 10; i++)
            {
                productionUpgradeMaxText[i].text = $"Buy Max ({BuyProductionUpgradeMaxCount(i)})";
            }
            

            productionUpgradeText[0].text = $"Manual Generator\nCost: {GetUpgradeCost(0, productionUpgradeCost)} Power\nPower: {Methods.NotationMethod(productionUpgradePower[0], "F2")} Power/s\nLevel: {GetUpgradeLevel(0, productionUpgradeLevels)}";
            productionUpgradeText[1].text = $"Wood Burner\nCost   {GetUpgradeCost(1, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[1], "F2")}   Power/s\nLevel: {GetUpgradeLevel(1, productionUpgradeLevels)}";
            productionUpgradeText[2].text = $"Coal Generator\nCost   {GetUpgradeCost(2, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[2], "F2")}   Power/s\nLevel: {GetUpgradeLevel(2, productionUpgradeLevels)}";
            productionUpgradeText[3].text = $"Oil Generator\nCost   {GetUpgradeCost(3, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[3], "F2")}   Power/s\nLevel: {GetUpgradeLevel(3, productionUpgradeLevels)}";
            productionUpgradeText[4].text = $"Natural Gas Generator\nCost   {GetUpgradeCost(4, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[4], "F2")}   Plasma/s\nLevel: {GetUpgradeLevel(4, productionUpgradeLevels)}";
            productionUpgradeText[5].text = $"Steam Turbine\nCost   {GetUpgradeCost(5, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[5], "F2")}   Power/s\nLevel: {GetUpgradeLevel(5, productionUpgradeLevels)}";
            productionUpgradeText[6].text = $"Nuclear Reactor\nCost   {GetUpgradeCost(6, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[6], "F2")}   Power/s\nLevel: {GetUpgradeLevel(6, productionUpgradeLevels)}";
            productionUpgradeText[7].text = $"Fusion Reactor\nCost   {GetUpgradeCost(7, productionUpgradeCost)}   Power\nPower:  {Methods.NotationMethod(productionUpgradePower[7], "F2")}   Power/s\nLevel: {GetUpgradeLevel(7, productionUpgradeLevels)}";

            game.SmoothNumber(ref game.plasmaTemp, data.power);
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

}
