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

using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;

public class RaysController : MonoBehaviour
{
    public IdleGame game;

    [Header("Buttons")]
    public GameObject omegaUpgradesButton;
    public GameObject PiButton;
    public GameObject TauButton;
    public GameObject ChiButton;
    [Header("Texts")]
    public Text[] rayAmountTexts = new Text[12];
    public Text[] ionizingTexts = new Text[11];
    public Text[] acceleratorTexts = new Text[6];
    public Text[] fabricatorTexts = new Text[6];
    public Text[] rayPerSecTexts = new Text[12];
    public string[] tiers;
    [Header("Objects")]
    public GameObject[] raysObjects = new GameObject[12];
    [Header("Other Array Stuff")]
    public BigDouble[] ionizeRewards = new BigDouble[11];
    private BigDouble[] acceleratorBaseCost = new BigDouble[] {10};
    private BigDouble[] fabBaseCost = new BigDouble[] {100};
    private BigDouble costMult = 1.25;
    public BigDouble[] acceleratorCosts = new BigDouble[6];
    public BigDouble[] fabCosts = new BigDouble[6];

    public void Start()
    {
        var data = game.data;
        tiers = new string[]{"<color=#797979>α</color>", "<color=#825454>β</color>", "<color=#A8AB58>γ</color>"," <color=#77C854>δ</color>", "<color=#63F128>ε</color>", "<color=#28F1D3>ζ</color>", "<color=#28C9F1>η</color>",
        "<color=#2848F1>θ</color>", "<color=#A728F1>ι</color>", "<color=#F128E4>κ</color>", "<color=#F12895>λ</color>", "<color=#F1282F>μ</color>", "<color=#F19028>ν</color>", "<color=#F1F128>ξ</color>", "<color=#81F128>ο</color>",
        "<color=#00C814>π</color>", "<color=#00B9C8>ρ</color>", "<color=#0088C8>σ</color>", "<color=#0500C8>τ</color>", "<color=#8E00CD>υ</color>", "<color=#FF00CD>φ</color>", "<color=#FF007F>χ</color>", "<color=#FF0015>ψ</color>",
        "<color=#FFFFFF>ω</color>"};
    }

    public void Update()
    {
        CostCalculator();
        PerSecCalculations();
        var data = game.data;

        if(data.rayTotals[11] > 0)
            omegaUpgradesButton.gameObject.SetActive(true);
        else
            omegaUpgradesButton.gameObject.SetActive(false);

        if(data.rayUnlockBool[3])
            PiButton.gameObject.SetActive(true);
        else
            PiButton.gameObject.SetActive(false);

        if(data.rayUnlockBool[5])
            TauButton.gameObject.SetActive(true);
        else
            TauButton.gameObject.SetActive(false);
        
        if(data.rayUnlockBool[8])
            ChiButton.gameObject.SetActive(true);
        else
            ChiButton.gameObject.SetActive(false);

        for(int i = 0; i < 12; i++)
        {
            if(data.rayTotals[i] > 0 && !data.rayUnlockBool[i])
            {
                data.rayUnlockBool[i] = true;
            }
            
            rayAmountTexts[i].text = $"{Methods.NotationMethod(data.particleTotals[i], "F2")} {tiers[i + 12]}";

            if(data.particleGenLevels[i] > 0)
            {  
                if(i > 0)
                {
                    data.particleGenAmounts[i - 1] += 1 * (data.particleGenLevels[i] + data.particleGenAmounts[i]);
                }
            }
                rayPerSecTexts[i].text = $"Boost to {tiers[i + 11]} {Methods.NotationMethod(particleBoost(i), "F2")}";
        }

        for(int i = 0; i < 11; i++)
        {

            ionizeRewards[i] = 300 * Sqrt(data.rayTotals[i] / 1e12);
            if(data.rayTotals[i] >= 1e12)
                ionizingTexts[i].text = data.volatileIndex <= i + 12 ? $"Ionize +{Methods.NotationMethod(ionizeRewards[i], "F2")} {tiers[i + 1]}" : $"Ionizing +{Methods.NotationMethod(ionizeRewards[i], "F2")} {tiers[i + 1]}/s"; 
            else
            {
                ionizingTexts[i].text = $"Not Enough {tiers[i]}";
            }
        }

        for(int i = 0; i < raysObjects.Length; i++)
        {
            if(data.rayUnlockBool[i])
                raysObjects[i].gameObject.SetActive(true);
            else
                raysObjects[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < 6; i++)
        {
            fabricatorTexts[i].text = i == 0 ? $"<b>Fabrication Unit </b>{tiers[i + 12]}\n<b>Produces </b>{tiers[i + 12]}/s\n<b>Cost:{Methods.NotationMethod(fabCosts[i], "F2")}</b> {tiers[i + 12]}\n<b>Level:{Methods.NotationMethod(data.fabLevels[i], "F2")}({Methods.NotationMethod(data.fabAmounts[i], "F2")})</b>" : 
            $"<b>Fabrication Unit </b>{tiers[i + 12]}\n<b>Produces </b>{tiers[i + 11]} Fabricators/s\n<b>Cost:{Methods.NotationMethod(fabCosts[i], "F2")}</b> {tiers[i + 12]}\n<b>Level:{Methods.NotationMethod(data.fabLevels[i + 12], "F2")}({Methods.NotationMethod(data.fabAmounts[i + 12], "F2")})</b>" ;
            acceleratorTexts[i].text = $"Particle Accelerator {tiers[i + 12]}\nBoosts {tiers[i + 3]} by {Methods.NotationMethod(particleBoost(i), "F2")}\nCost:{Methods.NotationMethod(acceleratorCosts[i], "F2")} {tiers[i + 12]}\nLevel:{Methods.NotationMethod(data.acceleratorLevels[i], "F2")}";
            if(i == 0)
            {
                data.rayTotals[0] += ((data.fabLevels[0] + data.fabAmounts[0]) * particleBoost(1)) * Time.deltaTime;
            }
            else
            {
                data.fabAmounts[i - 1] += ((data.fabLevels[i] + data.fabAmounts[i]) * particleBoost(i + 1)) * Time.deltaTime;
            }
        }
    }

    public void PerSecCalculations()
    {
        var data = game.data;
        //Alpha Actually produces its own currency
        data.rayTotals[0] += ((ionizeRewards[0]) * (Sqrt(data.particleTotals[11]) + 1)) * Time.deltaTime;
        data.rayTotals[1] += data.volatileIndex <= 0 ? 0 : (ionizeRewards[1] * particleBoost(0)) * Time.deltaTime;
        data.rayTotals[2] += data.volatileIndex <= 1 ? 0 : (ionizeRewards[2] * particleBoost(1)) * Time.deltaTime;
        data.rayTotals[3] += data.volatileIndex <= 2 ? 0 : (ionizeRewards[3] * particleBoost(2)) * Time.deltaTime;
        data.rayTotals[4] += data.volatileIndex <= 3 ? 0 : (ionizeRewards[4] * particleBoost(3)) * Time.deltaTime;
        data.rayTotals[5] += data.volatileIndex <= 4 ? 0 : (ionizeRewards[5] * particleBoost(4)) * Time.deltaTime;
        data.rayTotals[6] += data.volatileIndex <= 5 ? 0 : (ionizeRewards[6] * particleBoost(5)) * Time.deltaTime;
        data.rayTotals[7] += data.volatileIndex <= 6 ? 0 : (ionizeRewards[7] * particleBoost(6)) * Time.deltaTime;
        data.rayTotals[8] += data.volatileIndex <= 7 ? 0 : (ionizeRewards[8] * particleBoost(7)) * Time.deltaTime;
        data.rayTotals[9] += data.volatileIndex <= 8 ? 0 : (ionizeRewards[9] * particleBoost(8)) * Time.deltaTime;
        data.rayTotals[10] += data.volatileIndex <= 9 ? 0 : (ionizeRewards[10] * particleBoost(9)) * Time.deltaTime;
        data.rayTotals[11] += data.volatileIndex <= 10 ? 0 : (ionizeRewards[11] * particleBoost(10)) * Time.deltaTime;
    }
    
    public void Ionize(int index)
    {
        var data = game.data;

        if(data.rayTotals[index] < 1e12) return;
        if(data.volatileIndex > index) return;
        if(index < 11)
            data.rayTotals[index + 12] += ionizeRewards[index];
        data.rayTotals[index + 11] = 0;
    }

    public void BuyAccelerator(int index)
    {
        var data = game.data;
        if (data.rayTotals[index] >= acceleratorCosts[index])
        {
            data.acceleratorLevels[index]++;
            data.rayTotals[index] -= acceleratorCosts[index];
        }
    }

    public void BuyFabricator(int index)
    {
        var data = game.data;
        if (data.rayTotals[index] >= fabCosts[index])
        {
            data.fabLevels[index]++;
            data.rayTotals[index] -= fabCosts[index];
        }
    }

    public BigDouble particleBoost(int index)
    {
        BigDouble temp = 1;
        temp += Sqrt(game.data.rayTotals[index]);
        return temp;
    }

    public void CostCalculator()
    {
        var data = game.data;

        acceleratorCosts[0] = acceleratorBaseCost[0] * Pow(costMult, data.acceleratorLevels[0]);
        acceleratorCosts[1] = acceleratorBaseCost[0] * Pow(costMult, data.acceleratorLevels[1]);
        acceleratorCosts[2] = acceleratorBaseCost[0] * Pow(costMult, data.acceleratorLevels[2]);
        acceleratorCosts[3] = acceleratorBaseCost[0] * Pow(costMult, data.acceleratorLevels[3]);
        acceleratorCosts[4] = acceleratorBaseCost[0] * Pow(costMult, data.acceleratorLevels[4]);
        acceleratorCosts[5] = acceleratorBaseCost[0] * Pow(costMult, data.acceleratorLevels[5]);
        fabCosts[0] = fabBaseCost[0] * Pow(costMult, data.fabLevels[0]);
        fabCosts[1] = fabBaseCost[0] * Pow(costMult, data.fabLevels[1]);
        fabCosts[2] = fabBaseCost[0] * Pow(costMult, data.fabLevels[2]);
        fabCosts[3] = fabBaseCost[0] * Pow(costMult, data.fabLevels[3]);
        fabCosts[4] = fabBaseCost[0] * Pow(costMult, data.fabLevels[4]);
        fabCosts[5] = fabBaseCost[0] * Pow(costMult, data.fabLevels[5]);
    }

}
