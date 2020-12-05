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
using UnityEngine.Analytics;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;

public class ParticlesController : MonoBehaviour
{
    public IdleGame game;

    [Header("Buttons")]
    public GameObject raysButton;
    public GameObject DeltaButton;
    public GameObject EtaButton;
    public GameObject KappaButton;
    [Header("Texts")]
    public Text[] particleAmountTexts = new Text[12];
    public Text[] ionizingTexts = new Text[12];
    public Text[] particleGenTexts = new Text[12];
    public Text[] irradiatorsText = new Text[6];
    public Text[] particlePerSecTexts = new Text[12];
    public string[] tiers;
    [Header("Objects")]
    public GameObject[] particleObjects = new GameObject[11];
    [Header("Other Array Stuff")]
    public bool[] unlockBools = new bool[11];
    public bool[] isIrridiatorActive = new bool[6];
    public BigDouble[] ionizeRewards = new BigDouble[12];
    private BigDouble[] particleGenBaseCosts = new BigDouble[] {10, 1e3};
    private BigDouble particleGenCostMult = 1.15;
    public BigDouble[] particleGenCosts = new BigDouble[12];

    public void Start()
    {
        var data = game.data;
        tiers = new string[]{"<color=#797979>α</color>", "<color=#825454>β</color>", "<color=#A8AB58>γ</color>"," <color=#77C854>δ</color>", "<color=#63F128>ε</color>", "<color=#28F1D3>ζ</color>", "<color=#28C9F1>η</color>",
        "<color=#2848F1>θ</color>", "<color=#A728F1>ι</color>", "<color=#F128E4>κ</color>", "<color=#F12895>λ</color>", "<color=#F1282F>μ</color>", "<color=#F19028>ν</color>", "<color=#F1F128>ξ</color>", "<color=#81F128>ο</color>",
        "<color=#00C814>π</color>", "<color=#00B9C8>ρ</color>", "<color=#0088C8>σ</color>", "<color=#0500C8>τ</color>", "<color=#8E00CD>υ</color>", "<color=#FF00CD>φ</color>", "<color=#FF007F>χ</color>", "<color=#FF0015>ψ</color>",
        "<color=#FF6500>ω</color>"};
    }

    public void Update()
    {
        NonLoopableTextStorage();
        CostCalculator();
        PerSecCalculations();
        var data = game.data;

        if(data.rayTotals[0] > 0)
            raysButton.gameObject.SetActive(true);
        else
            raysButton.gameObject.SetActive(false);

        if(data.particleUnlockBool[2])
            DeltaButton.gameObject.SetActive(true);
        else
            DeltaButton.gameObject.SetActive(false);

        if(data.particleUnlockBool[5])
            EtaButton.gameObject.SetActive(true);
        else
            EtaButton.gameObject.SetActive(false);
        
        if(data.particleUnlockBool[7])
            KappaButton.gameObject.SetActive(true);
        else
            KappaButton.gameObject.SetActive(false);

        for(int i = 0; i < 12; i++)
        {
            ionizeRewards[i] = 300 * Sqrt(data.particleTotals[i] / 1e12);
            if(data.particleTotals[i] >= 1e12)
                ionizingTexts[i].text = data.volatileIndex <= i ? $"Ionize +{Methods.NotationMethod(ionizeRewards[i], "F2")} {tiers[i + 1]}" : $"Ionizing +{Methods.NotationMethod(ionizeRewards[i], "F2")} {tiers[i + 1]}/s"; 
            else
            {
                ionizingTexts[i].text = $"Not Enough {tiers[i]}";
            }
            particleAmountTexts[i].text = $"{Methods.NotationMethod(data.particleTotals[i], "F2")} {tiers[i]}";



            if(data.particleGenLevels[i] > 0)
            {  
                if(i > 0)
                {
                    data.particleGenAmounts[i - 1] += data.omegaUpgrade1Level > 0 ? 1 * ((data.particleGenLevels[i] + data.particleGenAmounts[i]) * (2.25 * data.omegaUpgrade1Level) ): 1 * (data.particleGenLevels[i] + data.particleGenAmounts[i]);
                }
            }

            if(i == 0)
            {
                particlePerSecTexts[i].text = $"{Methods.NotationMethod(data.particleGenLevels[i] + data.particleGenAmounts[i], "F2")} {tiers[i]}/s";
            }
            else if(i > 0)
            {
                particlePerSecTexts[i].text = $"Boost to {tiers[i - 1]} {Methods.NotationMethod(particleBoost(i), "F2")}";
            }
        }

        for(int i = 0; i < 11; i++)
        {
            if(data.particleTotals[i + 1] > 0 && !data.particleUnlockBool[i])
            {
                data.particleUnlockBool[i] = true;
            }
        }

        for(int i = 0; i < particleObjects.Length; i++)
        {
            if(data.particleUnlockBool[i])
                particleObjects[i].gameObject.SetActive(true);
            else
                particleObjects[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < 6; i++)
        {
            if(isIrridiatorActive[i] && data.particleTotals[i + 6] < 1e24)
                isIrridiatorActive[i] = false;
            
            if(isIrridiatorActive[i])
                irradiatorsText[i].text = $"{tiers[i + 6]} Irridiator Online\nProducing {Methods.NotationMethod((data.particleTotals[i + 6] * .25) * 2, "F2")} {tiers[i + 5]}\nLosing {Methods.NotationMethod(data.particleTotals[i + 6] * .25, "F2")} {tiers[i + 6]}";
            else
                irradiatorsText[i].text = data.particleTotals[i + 6] > 1e24 ? "Irridiator Offline" : $"Not Enough {tiers[i + 6]}\n {Methods.NotationMethod(data.particleTotals[i + 6], "F2")}/{Methods.NotationMethod(1e24, "F2")}";

            if(isIrridiatorActive[i])
            {
                data.particleTotals[i + 5] += (data.particleTotals[i + 6] * .5) * 2;
                data.particleTotals[i + 6] -= data.particleTotals[i + 6] * .5;
            }
        }

        
    }

    public void PerSecCalculations()
    {
        var data = game.data;
        //Alpha Actually produces its own currency
        data.particleTotals[0] += ((data.particleGenLevels[0] + data.particleGenAmounts[0]) * particleBoost(1)) * Time.deltaTime;
        data.particleTotals[1] += data.volatileIndex <= 0 ? 0 : (ionizeRewards[0] * particleBoost(2)) * Time.deltaTime;
        data.particleTotals[2] += data.volatileIndex <= 1 ? 0 : (ionizeRewards[1] * particleBoost(3)) * Time.deltaTime;
        data.particleTotals[3] += data.volatileIndex <= 2 ? 0 : (ionizeRewards[2] * particleBoost(4)) * ((Sqrt(data.rayTotals[0]) + 1) * (data.acceleratorLevels[0] * (2.25 * data.omegaUpgrade1Level) + 1)) * Time.deltaTime;
        data.particleTotals[4] += data.volatileIndex <= 3 ? 0 : (ionizeRewards[3] * particleBoost(5)) * ((Sqrt(data.rayTotals[1]) + 1) * (data.acceleratorLevels[1] * (2.25 * data.omegaUpgrade1Level) + 1)) * Time.deltaTime;
        data.particleTotals[5] += data.volatileIndex <= 4 ? 0 : (ionizeRewards[4] * particleBoost(6)) * ((Sqrt(data.rayTotals[2]) + 1) * (data.acceleratorLevels[2] * (2.25 * data.omegaUpgrade1Level) + 1)) * Time.deltaTime;
        data.particleTotals[6] += data.volatileIndex <= 5 ? 0 : (ionizeRewards[5] * particleBoost(7)) * ((Sqrt(data.rayTotals[3]) + 1) * (data.acceleratorLevels[3] * (2.25 * data.omegaUpgrade1Level) + 1)) * Time.deltaTime;
        data.particleTotals[7] += data.volatileIndex <= 6 ? 0 : (ionizeRewards[6] * particleBoost(8)) * ((Sqrt(data.rayTotals[4]) + 1) * (data.acceleratorLevels[4] * (2.25 * data.omegaUpgrade1Level) + 1)) * Time.deltaTime;
        data.particleTotals[8] += data.volatileIndex <= 7 ? 0 : (ionizeRewards[7] * particleBoost(9)) * ((Sqrt(data.rayTotals[5]) + 1) * (data.acceleratorLevels[5] * (2.25 * data.omegaUpgrade1Level) + 1)) * Time.deltaTime;
        data.particleTotals[9] += data.volatileIndex <= 8 ? 0 : (ionizeRewards[8] * particleBoost(10)) * Time.deltaTime;
        data.particleTotals[10] += data.volatileIndex <= 9 ? 0 : (ionizeRewards[9] * particleBoost(11)) * Time.deltaTime;
        data.particleTotals[11] += data.volatileIndex <= 10 ? 0 : (ionizeRewards[10] * Sqrt(data.rayTotals[0]) + 1) * Time.deltaTime;
    }
    
    public void Ionize(int index)
    {
        var data = game.data;

        if(data.particleTotals[index] < 1e12) return;
        if(data.volatileIndex > index) return;
        if(index < 11)
            data.particleTotals[index + 1] += ionizeRewards[index];
        data.particleTotals[index] = 0;
    }

    public void BuyProductionUpgrade(int index)
    {
        var data = game.data;
        
        if(index == 0 || index == 1)
        {
            if (data.particleTotals[0] >= particleGenCosts[index])
            {
                data.particleGenLevels[index]++;
                data.particleTotals[0] -= particleGenCosts[index];
            }
        }
        if(index == 2 || index == 3)
        {
            if (data.particleTotals[1] >= particleGenCosts[index])
            {
                data.particleGenLevels[index]++;
                data.particleTotals[1] -= particleGenCosts[index];
            }
        }
        if(index == 4 || index == 5)
        {
            if (data.particleTotals[2] >= particleGenCosts[index])
            {
                data.particleGenLevels[index]++;
                data.particleTotals[2] -= particleGenCosts[index];
            }
        }
        if(index == 6 || index == 7)
        {
            if (data.particleTotals[3] >= particleGenCosts[index])
            {
                data.particleGenLevels[index]++;
                data.particleTotals[3] -= particleGenCosts[index];
            }
        }
        if(index == 8 || index == 9)
        {
            if (data.particleTotals[4] >= particleGenCosts[index])
            {
                data.particleGenLevels[index]++;
                data.particleTotals[4] -= particleGenCosts[index];
            }
        }
        if(index == 10 || index == 11)
        {
            if (data.particleTotals[5] >= particleGenCosts[index])
            {
                data.particleGenLevels[index]++;
                data.particleTotals[5] -= particleGenCosts[index];
            }
        }
        
    }


    public BigDouble particleBoost(int index)
    {
        BigDouble temp = 1;
        temp += Sqrt(game.data.particleTotals[index]);
        return temp;
    }

    public void Activate(int index)
    {
        var data = game.data;

        if(!isIrridiatorActive[index] && data.particleTotals[index + 6] > 1e24)
            isIrridiatorActive[index] = true;
        else if(isIrridiatorActive[index])
            isIrridiatorActive[index] = false;

    }

    public void CostCalculator()
    {
        var data = game.data;

        particleGenCosts[0] = particleGenBaseCosts[0] * Pow(particleGenCostMult, data.particleGenLevels[0]);
        particleGenCosts[1] = particleGenBaseCosts[1] * Pow(particleGenCostMult, data.particleGenLevels[1]);
        particleGenCosts[2] = particleGenBaseCosts[0] * Pow(particleGenCostMult, data.particleGenLevels[2]);
        particleGenCosts[3] = particleGenBaseCosts[1] * Pow(particleGenCostMult, data.particleGenLevels[3]);
        particleGenCosts[4] = particleGenBaseCosts[0] * Pow(particleGenCostMult, data.particleGenLevels[4]);
        particleGenCosts[5] = particleGenBaseCosts[1] * Pow(particleGenCostMult, data.particleGenLevels[5]);
        particleGenCosts[6] = particleGenBaseCosts[0] * Pow(particleGenCostMult, data.particleGenLevels[6]);
        particleGenCosts[7] = particleGenBaseCosts[1] * Pow(particleGenCostMult, data.particleGenLevels[7]);
        particleGenCosts[8] = particleGenBaseCosts[0] * Pow(particleGenCostMult, data.particleGenLevels[8]);
        particleGenCosts[9] = particleGenBaseCosts[1] * Pow(particleGenCostMult, data.particleGenLevels[9]);
        particleGenCosts[10] = particleGenBaseCosts[0] * Pow(particleGenCostMult, data.particleGenLevels[10]);
        particleGenCosts[11] = particleGenBaseCosts[1] * Pow(particleGenCostMult, data.particleGenLevels[11]);
    }

    public void NonLoopableTextStorage()
    {
        var data = game.data;
        //Alpha
        particleGenTexts[0].text = $"{tiers[0]} Gen 1 Produces 1 {tiers[0]}/s\nCost:{Methods.NotationMethod(particleGenCosts[0], "F0")} {tiers[0]}\nLevel:{Methods.NotationMethod(data.particleGenLevels[0], "F0")}({Methods.NotationMethod(data.particleGenAmounts[0], "F0")})";
        particleGenTexts[1].text = $"{tiers[0]} Gen 2 Produces 1 {tiers[0]} Gen 1/s\nCost:{Methods.NotationMethod(particleGenCosts[1], "F0")} {tiers[0]}\nLevel:{Methods.NotationMethod(data.particleGenLevels[1], "F0")}({Methods.NotationMethod(data.particleGenAmounts[1], "F0")})";
        
        //Beta
        particleGenTexts[2].text = $"{tiers[1]} Gen 1 Produces 1 {tiers[0]} Gen 2/s\nCost:{Methods.NotationMethod(particleGenCosts[2], "F0")} {tiers[1]}\nLevel:{Methods.NotationMethod(data.particleGenLevels[2], "F0")}({Methods.NotationMethod(data.particleGenAmounts[2], "F0")})";
        particleGenTexts[3].text = $"{tiers[1]} Gen 2 Produces 1 {tiers[1]} Gen 1/s\nCost:{Methods.NotationMethod(particleGenCosts[3], "F0")} {tiers[1]}\nLevel:{Methods.NotationMethod(data.particleGenLevels[3], "F0")}({Methods.NotationMethod(data.particleGenAmounts[3], "F0")})";
        //Gamma
        particleGenTexts[4].text = $"{tiers[2]} Gen 1 Produces 1 {tiers[1]} Gen 2/s\nCost:{Methods.NotationMethod(particleGenCosts[4], "F0")} {tiers[2]}\nLevel:{Methods.NotationMethod(data.particleGenLevels[4], "F0")}({Methods.NotationMethod(data.particleGenAmounts[4], "F0")})";
        particleGenTexts[5].text = $"{tiers[2]} Gen 2 Produces 1 {tiers[2]} Gen 1/s\nCost:{Methods.NotationMethod(particleGenCosts[5], "F0")} {tiers[2]}\nLevel:{Methods.NotationMethod(data.particleGenLevels[5], "F0")}({Methods.NotationMethod(data.particleGenAmounts[5], "F0")})";
        //Delta
        particleGenTexts[6].text = $"{tiers[3]} Gen 1 Produces 1 {tiers[2]} Gen 2/s\nCost:{Methods.NotationMethod(particleGenCosts[6], "F0")} {tiers[3]}\nLevel:{Methods.NotationMethod(data.particleGenLevels[6], "F0")}({Methods.NotationMethod(data.particleGenAmounts[6], "F0")})";
        particleGenTexts[7].text = $"{tiers[3]} Gen 2 Produces 1 {tiers[3]} Gen 1/s\nCost:{Methods.NotationMethod(particleGenCosts[7], "F0")} {tiers[3]}\nLevel:{Methods.NotationMethod(data.particleGenLevels[7], "F0")}({Methods.NotationMethod(data.particleGenAmounts[7], "F0")})";
        //Epsilon
        particleGenTexts[8].text = $"{tiers[4]} Gen 1 Produces 1 {tiers[3]} Gen 2/s\nCost:{Methods.NotationMethod(particleGenCosts[8], "F0")} {tiers[4]}\nLevel:{Methods.NotationMethod(data.particleGenLevels[8], "F0")}({Methods.NotationMethod(data.particleGenAmounts[8], "F0")})";
        particleGenTexts[9].text = $"{tiers[4]} Gen 2 Produces 1 {tiers[4]} Gen 1/s\nCost:{Methods.NotationMethod(particleGenCosts[9], "F0")} {tiers[4]}\nLevel:{Methods.NotationMethod(data.particleGenLevels[9], "F0")}({Methods.NotationMethod(data.particleGenAmounts[9], "F0")})";
        //Zeta
        particleGenTexts[10].text = $"{tiers[5]} Gen 1 Produces 1 {tiers[4]} Gen 2/s\nCost:{Methods.NotationMethod(particleGenCosts[10], "F0")} {tiers[5]}\nLevel:{Methods.NotationMethod(data.particleGenLevels[10], "F0")}({Methods.NotationMethod(data.particleGenAmounts[10], "F0")})";
        particleGenTexts[11].text = $"{tiers[5]} Gen 2 Produces 1 {tiers[5]} Gen 1/s\nCost:{Methods.NotationMethod(particleGenCosts[11], "F0")} {tiers[5]}\nLevel:{Methods.NotationMethod(data.particleGenLevels[11], "F0")}({Methods.NotationMethod(data.particleGenAmounts[11], "F0")})";
    }
}
