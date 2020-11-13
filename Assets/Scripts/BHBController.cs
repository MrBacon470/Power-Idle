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

public class BHBController : MonoBehaviour
{
    public IdleGame game;

    public Image backGround;
    [Header("Sprites")]
    public Sprite normalBG;
    public Sprite maxedBG;
    public Sprite unstableBG;
    [Header("Texts")]
    public Text superConductorsText;
    public Text BHBPerSecText;
    public Text countdownText;
    public Text[] hcTexts = new Text[3];
    [Header("Game Objects")]
    public GameObject cautionPopUp;
    [Header("Timer")]
    public float destablizationTimer;
    //600secs = 10 minutes so base of 10 minutes plus 10mins * HC2
    public float destablizationTotal => 600 + (600 * game.data.hc2Level);
    public int stableIndex;
    [Header("Level Caps")]
    public int[] maxLevelCaps = new int[]{4, 5, 6};
    private BigDouble Cost1 => 1e74 * Pow(1.5, game.data.hc1Level);
    private BigDouble Cost2 => 1e76 * Pow(1.5, game.data.hc2Level);
    private BigDouble Cost3 => 1e78 * Pow(1.95, game.data.hc3Level);

    public void Run()
    {
        var data = game.data;

        TimeSpan timer = TimeSpan.FromSeconds(destablizationTotal - destablizationTimer);

        superConductorsText.text = $"{Methods.NotationMethod(data.superConductors,"F2")} Super Conductors";
        BHBPerSecText.text = $"Your Black Hole Bomb Produces {Methods.NotationMethod(BHBPowerPerSec(),"F2")} Power/s";
        countdownText.text = $"{timer:hh\\:mm\\:ss}";

        hcTexts[0].text = $"Lower Destablization Chance by 10%\nCost:{Methods.NotationMethod(Cost1,"F2")} Super Conductors\nLevel: {data.hc1Level}/{maxLevelCaps[0]}";
        hcTexts[1].text = $"Increase Destablization Interval By 10 Mins\nCost:{Methods.NotationMethod(Cost2,"F2")} Super Conductors\nLevel: {data.hc2Level}/{maxLevelCaps[1]}";
        hcTexts[2].text = $"Double BHB Power Production\nCost:{Methods.NotationMethod(Cost3,"F2")} Super Conductors\nLevel: {data.hc3Level}/{maxLevelCaps[2]}";

        System.Random rnd = new System.Random();

        if(stableIndex != 1 && data.hc1Level != maxLevelCaps[0] && data.hc2Level != maxLevelCaps[1] && data.hc3Level != maxLevelCaps[2])
            backGround.sprite = normalBG;
        else if(stableIndex == 1)
            backGround.sprite = unstableBG;
        else if(stableIndex != 1 && data.hc1Level == maxLevelCaps[0] && data.hc2Level == maxLevelCaps[1] && data.hc3Level == maxLevelCaps[2])

        
        if(!data.isBHBUnlocked) return;
        if(destablizationTimer < destablizationTotal)
            destablizationTimer += Time.deltaTime;
        if(destablizationTimer >= destablizationTotal)
            destablizationTimer = destablizationTotal;
        if(destablizationTimer == destablizationTotal)
        {
            if(stableIndex == 0 || stableIndex == 2 || stableIndex == 3 || stableIndex == 4 || stableIndex == 5 || stableIndex == 6 || stableIndex == 7 || stableIndex == 8 || stableIndex == 9)
                destablizationTimer = 0;
            else if(stableIndex == 1)
            {
                Destablize();
                destablizationTimer = 0;
            }
            if(data.hc1Level == 0)
                stableIndex = rnd.Next(0,1);
            if(data.hc1Level == 1)
                stableIndex = rnd.Next(0,2);
            if(data.hc1Level == 2)
                stableIndex = rnd.Next(0,3);
            if(data.hc1Level == 3)
                stableIndex = rnd.Next(0,4);
            if(data.hc1Level == 4)
                stableIndex = rnd.Next(0,9);
        }
    }

    public void PopUpToggle()
    {
        cautionPopUp.gameObject.SetActive(!cautionPopUp.gameObject.activeSelf);
    }

    public void Destablize()
    {
        var data = game.data;

        data.power = 10;
        data.transformers = 0;
        data.infusionULevel1 = 0;
        data.infusionULevel2 = 0;
        data.infusionULevel3 = 0;

        stableIndex = 0;
    }

    public void BuyHyperController(int index)
    {
        var data = game.data;
        switch(index)
        {
            case 0:
                if(data.superConductors < Cost1) return;
                if(data.hc1Level >= maxLevelCaps[0]) return;
                data.superConductors -= Cost1;
                data.hc1Level++;
            break;

            case 1:
                if(data.superConductors < Cost2) return;
                if(data.hc2Level >= maxLevelCaps[1]) return;
                data.superConductors -= Cost2;
                data.hc2Level++;
            break;

            case 2:
                if(data.superConductors < Cost3) return;
                if(data.hc3Level >= maxLevelCaps[2]) return;
                data.superConductors -= Cost3;
                data.hc3Level++;
            break;
        }
    }

    public BigDouble BHBPowerPerSec()
    {
        var data = game.data;
        BigDouble temp = 0;
        if(data.isBHBUnlocked && stableIndex != 1)
        {
            temp += 1e100 * Pow(2, data.hc3Level);
        }
        else
        {
            temp = 0;
        }
        return temp;
    }
}
