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

public class FlameManager : MonoBehaviour
{
    public IdleGame game;

    public Text flameButtonText;

    public GameObject flameAchievement1;
    public GameObject flameAchievement2;

    public Text[] flameTexts = new Text[3];

    public Text flameText;
    [Header("Numbers")]
    public bool burnToggle;
    public bool powerToggle;
    public float harvestTimer;

    public void StartFlame()
    {
        var data = game.data;
        burnToggle = false;
        powerToggle = false;
    }

    public void UpdateFlame()
    {
        var data = game.data;
        TextManager();

        if (data.flameCoin < 1)
            data.flameCoin = 1;

        if (data.isFlameCoinUnlocked)
            flameButtonText.text = "Go To Flame Coin";
        else
            flameButtonText.text = $"Unlock Flame Coin\nCost: {Methods.NotationMethod(1e3,"F0")} Kuaka Coin";

        if (harvestTimer > 0)
            harvestTimer -= Time.deltaTime;
        if (data.flameCoin < 10 && burnToggle)
            burnToggle = false;
        if (data.flameCoin < 10 && powerToggle)
            powerToggle = false;

        if (powerToggle)
        {
            data.power += 1e45 * Time.deltaTime;
            data.kuakaCoin -= 1 * Time.deltaTime;
        }
        if (burnToggle)
        {
            data.kuakaCoin -= 1 * Time.deltaTime;
        }

        if (!data.isFlameCoinUnlocked)
        {
            flameAchievement1.gameObject.SetActive(false);
            flameAchievement2.gameObject.SetActive(false);
        }
        else
        {
            flameAchievement1.gameObject.SetActive(true);
            flameAchievement2.gameObject.SetActive(true);
        }

    }

    public void TextManager()
    {
        var data = game.data;
        if (harvestTimer <= 0)
            flameTexts[0].text = "FlameCoin Harvest Ready +1.01x";
        else
            flameTexts[0].text = $"Harvest Cool Down:{Methods.NotationMethod(harvestTimer, "F0")}";

        if (powerToggle)
            flameTexts[1].text = $"Losing 1 FlameCoin/s\nGaining {Methods.NotationMethod(1, "F2")} Kuaka/s";
        else
            flameTexts[1].text = data.flameCoin < 10 ? "Not Enough Flame Coin" : $"Convert Flame Coin to Kuaka";

        if (burnToggle)
            flameTexts[2].text = $"Losing 1 FlameCoin/s\nBoosting Kuaka Harvesting +{Methods.NotationMethod(20, "F2")}";
        else
            flameTexts[2].text = data.flameCoin < 10 ? $"Not Enough Flame Coin" : "Burn Flame Coin For Boost";

        flameText.text = $"{Methods.NotationMethod(data.flameCoin, "F2")} Flame Coin";
    }

    public void Harvest()
    {
        var data = game.data;
        if (harvestTimer > 0) return;
        data.flameCoin *= 1.01;
        harvestTimer = 60;
    }

    public void Unlock()
    {
        var data = game.data;
        if(data.isFlameCoinUnlocked)
        {
            game.ChangeTabs("flame");
        }
        else
        {
            if (data.kuakaCoin < 1e3) return;

            data.isFlameCoinUnlocked = true;
            data.kuakaCoin -= 1e3;
            if (data.flameCoin < 1)
                data.flameCoin = 1;
        }
    }

    public void ToggleBurn()
    {
        var data = game.data;

        if (!burnToggle)
        {
            if (data.flameCoin < 10) return;
            burnToggle = true;
        }
        else
        {
            burnToggle = false;
        }
    }

    public void TogglePower()
    {
        var data = game.data;

        if (!powerToggle)
        {
            if (data.flameCoin < 10) return;
            powerToggle = true;
        }
        else
        {
            powerToggle = false;
        }
    }
}
