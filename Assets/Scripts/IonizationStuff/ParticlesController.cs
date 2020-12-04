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
    public string[] tiers;
    [Header("Objects")]
    public GameObject[] particleObjects = new GameObject[11];
    [Header("Other Array Stuff")]
    public bool[] unlockBools = new bool[11];
    public bool[] isIrridiatorActive = new bool[6];
    public BigDouble[] ionizeRewards;
    public BigDouble[] totalCurrency;
    public BigDouble[] particleGenBaseCosts = new BigDouble[] {10, 1e3};
    public BigDouble particleGenCostMult = 1.15;
    public BigDouble[] particleGenCosts;

    public static ParticlesController Instance;

    public void StartParticles()
    {
        var data = game.data;
        ionizeRewards = new BigDouble[12];
        totalCurrency = new BigDouble[13];
        particleGenCosts = new BigDouble[12];
        tiers = new string[]{"<color=#797979>α</color>", "<color=#825454>β</color>", "<color=#A8AB58>γ</color>"," <color=#77C854>δ</color>", "<color=#63F128>ε</color>", "<color=#28F1D3>ζ</color>", "<color=#28C9F1>η</color>",
        "<color=#2848F1>θ</color>", "<color=#A728F1>ι</color>", "<color=#F128E4>κ</color>", "<color=#F12895>λ</color>", "<color=#F1282F>μ</color>", "<color=#F19028>ν</color>", "<color=#F1F128>ξ</color>", "<color=#81F128>ο</color>",
        "<color=#00C814>π</color>", "<color=#00B9C8>ρ</color>", "<color=#0088C8>σ</color>", "<color=#0500C8>τ</color>", "<color=#8E00CD>υ</color>", "<color=#FF00CD>φ</color>", "<color=#FF007F>χ</color>", "<color=#FF0015>ψ</color>",
        "<color=#FFFFFF>ω</color>"};
    }

    public void Run()
    {
        ArrayManager();
        PerSecCalculations();
        NonLoopableTextStorage();
        var data = game.data;

        for(int i = 0; i < 12; i++)
        {
            ionizeRewards[i] = 300 * Sqrt(totalCurrency[i] / 1e24);
            ionizingTexts[i].text = i <= data.volatileIndex ? $"Ionize +{Methods.NotationMethod(ionizeRewards[i], "F2")} {tiers[i + 1]}" : $"Ionizing +{Methods.NotationMethod(ionizeRewards[i], "F2")} {tiers[i + 1]}/s"; 
            particleAmountTexts[i].text = $"{Methods.NotationMethod(totalCurrency[i], "F2")} {tiers[i]}";
            if(totalCurrency[i + 1] > 0 && !unlockBools[i])
            {
                unlockBools[i] = true;
                NonArrayManager();
            }
            if(unlockBools[i])
                particleObjects[i].gameObject.SetActive(true);
        }

        for(int i = 0; i < particleObjects.Length; i++)
        {
            if(unlockBools[i])
                particleObjects[i].gameObject.SetActive(true);
            else
                particleObjects[i].gameObject.SetActive(false);
        }

        for(int i = 0; i < 6; i++)
        {
            if(isIrridiatorActive[i] && totalCurrency[i + 6] < 1e24)
                isIrridiatorActive[i] = false;
            
            if(isIrridiatorActive[i])
                irradiatorsText[i].text = $"{tiers[i + 6]} Irridiator Online\nProducing {Methods.NotationMethod((totalCurrency[i + 6] * .25) * 2, "F2")} {tiers[i + 5]}\nLosing {Methods.NotationMethod(totalCurrency[i + 6] * .25, "F2")} {tiers[i + 6]}";
            else
                irradiatorsText[i].text = totalCurrency[i + 6] > 1e24 ? "Irridiator Offline" : $"Not Enough {tiers[i + 6]}\n {Methods.NotationMethod(totalCurrency[i + 6], "F2")}/{Methods.NotationMethod(1e24, "F2")}";
        }

        
    }

    public void PerSecCalculations()
    {
        var data = game.data;
        //Alpha Actually produces its own currency
        data.Alpha += ((data.particleGenLevels[0] + data.particleGenAmounts[0] + data.particleGenLevels[1] + data.particleGenAmounts[1]) * particleBoost(1) ) * Time.deltaTime;
        data.Beta += data.volatileIndex <= 0 ? 0 : ionizeRewards[0] * particleBoost(2);
        data.Gamma += data.volatileIndex <= 1 ? 0 : ionizeRewards[1] * particleBoost(3);
        data.Delta += data.volatileIndex <= 2 ? 0 : ionizeRewards[2] * particleBoost(4);
        data.Epsilon += data.volatileIndex <= 3 ? 0 : ionizeRewards[3] * particleBoost(5);
        data.Zeta += data.volatileIndex <= 4 ? 0 : ionizeRewards[4] * particleBoost(6);
        data.Eta += data.volatileIndex <= 5 ? 0 : ionizeRewards[5] * (particleBoost(7) + particleBoost(0));
        data.Theta += data.volatileIndex <= 6 ? 0 : ionizeRewards[6] * (particleBoost(8) + particleBoost(0));
        data.Iota += data.volatileIndex <= 7 ? 0 : ionizeRewards[7] * (particleBoost(9) + particleBoost(0));
        data.Kappa += data.volatileIndex <= 8 ? 0 : ionizeRewards[8] * particleBoost(10);
        data.Lambda += data.volatileIndex <= 9 ? 0 : ionizeRewards[9] * particleBoost(11);
        data.Mu += data.volatileIndex <= 10 ? 0 : ionizeRewards[10] * Sqrt(data.Nu) + 1;
    }
    
    public void Ionize(int index)
    {
        var data = game.data;

        if(totalCurrency[index] < 1e24) return;
        if(data.volatileIndex > index) return;
        totalCurrency[index + 1] += ionizeRewards[index];
    }

    public void BuyProductionUpgrade(int index)
    {
        var data = game.data;
        if (data.power >= particleGenCosts[index])
        {
            data.particleGenLevels[index]++;
            data.power -= particleGenCosts[index];
        }
        NonArrayManager();
    }

    public void ArrayManager()
    {
        var data = game.data;

        unlockBools[0] = data.isBetaUnlocked;
        unlockBools[1] = data.isGammaUnlocked;
        unlockBools[2] = data.isDeltaUnlocked;
        unlockBools[3] = data.isEpsilonUnlocked;
        unlockBools[4] = data.isZetaUnlocked;
        unlockBools[5] = data.isEtaUnlocked;
        unlockBools[6] = data.isThetaUnlocked;
        unlockBools[7] = data.isIotaUnlocked;
        unlockBools[8] = data.isKappaUnlocked;
        unlockBools[9] = data.isLambdaUnlocked;
        unlockBools[10] = data.isMuUnlocked;

        totalCurrency[0] = data.Alpha;
        totalCurrency[1] = data.Beta;
        totalCurrency[2] = data.Gamma;
        totalCurrency[3] = data.Delta;
        totalCurrency[4] = data.Epsilon;
        totalCurrency[5] = data.Zeta;
        totalCurrency[6] = data.Eta;
        totalCurrency[7] = data.Theta;
        totalCurrency[8] = data.Iota;
        totalCurrency[9] = data.Kappa;
        totalCurrency[10] = data.Lambda;
        totalCurrency[11] = data.Mu;
        totalCurrency[12] = data.Nu;
    }

    public BigDouble particleBoost(int index)
    {
        BigDouble temp = 1;
        temp += Sqrt(totalCurrency[index]);
        return temp;
    }

    public void Activate(int index)
    {
        var data = game.data;

        if(!isIrridiatorActive[index] && totalCurrency[index + 6] > 1e24)
            isIrridiatorActive[index] = true;
        else if(isIrridiatorActive[index])
            isIrridiatorActive[index] = false;

    }

    public void NonArrayManager()
    {
        var data = game.data;

        data.Alpha = totalCurrency[0];
        data.Beta = totalCurrency[1];
        data.Gamma = totalCurrency[2];
        data.Delta = totalCurrency[3];
        data.Epsilon = totalCurrency[4];
        data.Zeta = totalCurrency[5];
        data.Eta = totalCurrency[6];
        data.Theta = totalCurrency[7];
        data.Iota = totalCurrency[8];
        data.Kappa = totalCurrency[9];
        data.Lambda = totalCurrency[10];
        data.Mu = totalCurrency[11];
        data.Nu = totalCurrency[12];

        data.isBetaUnlocked = unlockBools[0];
        data.isGammaUnlocked = unlockBools[1];
        data.isDeltaUnlocked = unlockBools[2];
        data.isEpsilonUnlocked = unlockBools[3];
        data.isZetaUnlocked = unlockBools[4];
        data.isEtaUnlocked = unlockBools[5];
        data.isThetaUnlocked = unlockBools[6];
        data.isIotaUnlocked = unlockBools[7];
        data.isKappaUnlocked = unlockBools[8];
        data.isLambdaUnlocked = unlockBools[9];
        data.isMuUnlocked = unlockBools[10];
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
        particleGenCosts[10] = particleGenBaseCosts[1] * Pow(particleGenCostMult, data.particleGenLevels[11]);
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
