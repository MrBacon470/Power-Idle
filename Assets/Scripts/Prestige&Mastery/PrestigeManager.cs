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

public class PrestigeManager : MonoBehaviour
{
    public IdleGame game;
    public ResearchManager research;
    public UpgradesManager upgrades;
    public Text prestigeText;

    public GameObject prestigeMenu;
    public BigDouble transformersToGet => game.data.isChallenge2Active || game.data.isChallenge2Active ? 300 * Sqrt(game.data.power * 2.5) : 300 * Sqrt(game.data.power * 2.5) + (150 * Sqrt(game.data.power * 2) * (0.25 * game.data.infusionULevel3));

    public void Run()
    {
        var data = game.data;
        if (data.currentPollution >= game.pollution.totalPollution)
            prestigeMenu.gameObject.SetActive(true);
        else
            prestigeMenu.gameObject.SetActive(false);

        if (prestigeMenu.gameObject.activeSelf)
            prestigeText.text = data.isChallenge2Active ? $"!Warning! Less Transformers on Prestige +{Methods.NotationMethod(transformersToGet,"F0")} Transformers" : $"Prestige +{Methods.NotationMethod(transformersToGet, "F0")} Transformers";

        
    }

    public void Prestige()
    {
        var data = game.data;
        if (data.currentPollution < game.pollution.totalPollution) return;

        data.hasPrestiged = true;

        data.transformers += transformersToGet;

        data.power = 10;
        data.powerCollected = 10;

        data.productionUpgrade1Level = data.productionUpgrade2Level = data.productionUpgrade3Level = data.productionUpgrade4Level = data.productionUpgrade5Level = data.productionUpgrade6Level
            = data.productionUpgrade7Level = data.productionUpgrade8Level = 0;

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

        game.broken.breakIndex = 8;
        game.broken.breakTimer = 0;

        data.isGen1Broken = false;
        data.isGen2Broken = false;
        data.isGen3Broken = false;
        data.isGen4Broken = false;
        data.isGen5Broken = false;
        data.isGen6Broken = false;
        data.isGen7Broken = false;
        data.isGen8Broken = false;

        upgrades.Deactivate();
        research.Activate();
    }

    public BigDouble TransformerBoost()
    {
        var data = game.data;
        BigDouble temp = data.transformers * 0.0001;

        return temp + 1;
    }
}
