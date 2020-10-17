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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class MasteryManager : MonoBehaviour
{
    public IdleGame game;
    public UpgradesManager upgrades;
    public ResearchManager research;
    public Text masteryText;
    public GameObject masteryMenu;

    public void Run()
    {
        var data = game.data;

        if (data.power >= 1e306)
            data.superConductorsToGet = 150 * Sqrt((data.power + data.transformers) / 1e306);
        else
            data.transformersToGet = 0;

        if (data.power >= 1e306)
            masteryMenu.gameObject.SetActive(true);
        else
            masteryMenu.gameObject.SetActive(false);

        masteryText.text = $"Mastery +{Methods.NotationMethod(data.transformersToGet, "F2")} Super Conductors";
    }

    public void Mastery()
    {
        var data = game.data;
        if (data.power < 1e306) return;

        data.hasMastered = true;

        data.superConductors += data.superConductorsToGet;

        data.power = 10;
        data.transformersToGet = 0;
        data.transformers = 0;
        data.productionUpgrade1Level = 0;
        data.productionUpgrade2Level = 0;
        data.productionUpgrade3Level = 0;
        data.productionUpgrade4Level = 0;
        data.productionUpgrade5Level = 0;
        data.productionUpgrade6Level = 0;
        data.productionUpgrade7Level = 0;
        data.productionUpgrade8Level = 0;

        data.isCompleted0 = true;
        data.isCompleted1 = false;
        data.isCompleted2 = false;
        data.isCompleted3 = false;
        data.isCompleted4 = false;
        data.isCompleted5 = false;
        data.isCompleted6 = false;
        data.isCompleted7 = false;

        data.researchIndex = 0;

        data.currentPollution = 0;

        upgrades.Deactivate();
        research.Activate();
    }
}
