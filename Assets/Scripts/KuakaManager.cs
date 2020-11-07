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

public class KuakaManager : MonoBehaviour
{
    public IdleGame game;

    public GameObject kuakaButton;

    public GameObject kuakaAchievement1;
    public GameObject kuakaAchievement2;

    public Text[] kuakaTexts = new Text[3];

    public Text kuakaText;
    [Header("Numbers")]
    public bool burnToggle;
    public bool powerToggle;
    public float harvestTimer;

    public void StartKuaka()
    {
        var data = game.data;
        burnToggle = false;
        powerToggle = false;
    }

    public void UpdateKuaka()
    {
        var data = game.data;
        TextManager();

        if (data.kuakaCoin < 1)
            data.kuakaCoin = 1;

        if (!data.isAchievement7Locked && !data.isKuakaCoinUnlocked)
            data.isKuakaCoinUnlocked = true;

        if (harvestTimer > 0)
            harvestTimer -= Time.deltaTime;
        if (data.kuakaCoin < 10 && burnToggle)
            burnToggle = false;
        if (data.kuakaCoin < 10 && powerToggle)
            powerToggle = false;

        if (powerToggle)
            data.power += 1e45 * Time.deltaTime;

        if(!data.isKuakaCoinUnlocked)
        {
            kuakaButton.gameObject.SetActive(false);
            kuakaAchievement1.gameObject.SetActive(false);
            kuakaAchievement2.gameObject.SetActive(false);
        }
        else
        {
            kuakaButton.gameObject.SetActive(true);
            kuakaAchievement1.gameObject.SetActive(true);
            kuakaAchievement2.gameObject.SetActive(true);
        }

    }

    public void TextManager()
    {
        var data = game.data;
        if (harvestTimer <= 0)
            kuakaTexts[0].text = "Kuaka Coin Harvest Ready +1.01x";
        else
            kuakaTexts[0].text = $"Harvest Cool Down:{Methods.NotationMethod(harvestTimer, "F0")}";

        if (powerToggle)
            kuakaTexts[1].text = $"Losing 1 Kuaka Coin/s\nGaining {Methods.NotationMethod(1e45,"F2")} Power/s";
        else
            kuakaTexts[1].text = data.kuakaCoin < 10 ? "Not Enough Kuaka Coin" : $"Convert Kuaka Coin to Power";

        if (burnToggle)
            kuakaTexts[2].text = $"Losing 1 KuakaCoin/s\nBoosting +{Methods.NotationMethod(20, "F2")}x";
        else
            kuakaTexts[2].text = data.kuakaCoin < 10 ? $"Not Enough Kuaka Coin" : "Burn Kuaka Coin For Boost";

        kuakaText.text = $"{Methods.NotationMethod(data.kuakaCoin, "F2")} Kuaka Coin";
    }

    public void Harvest()
    {
        var data = game.data;
        if (harvestTimer > 0) return;
        data.kuakaCoin *= 1.01;
        harvestTimer = 30;
    }

    public void ToggleBurn()
    {
        var data = game.data;

        if(!burnToggle)
        {
            if (data.kuakaCoin < 10) return;
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

        if (!burnToggle)
        {
            if (data.kuakaCoin < 10) return;
            powerToggle = true;
        }
        else
        {
            powerToggle = false;
        }
    }
}
