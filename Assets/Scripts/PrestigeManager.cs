using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BreakInfinity;
using static BreakInfinity.BigDouble;

public class PrestigeManager : MonoBehaviour
{
    public IdleGame game;
    public UpgradesManager upgrades;
    public ResearchManager research;
    public Text prestigeText;
    public GameObject prestigeMenu;

    public void Run()
    {
        var data = game.data;

        if (data.currentPollution >= 409.8e6)
            data.transformersToGet = 150 * Sqrt(data.power / 1e7);
        else
            data.transformersToGet = 0;

        if (data.currentPollution >= 409.8e6)
            prestigeMenu.gameObject.SetActive(true);
        else
            prestigeMenu.gameObject.SetActive(false);

        prestigeText.text = $"Prestige +{Methods.NotationMethod(data.transformersToGet, "F2")} Transformers";
    }

    public void Prestige()
    {
        var data = game.data;
        if (data.currentPollution < 409.8e6) return;
        
        data.hasPrestiged = true;
        if (data.infusionULevel2 <= 0)
            data.transformers += data.transformersToGet;
        else
            data.transformers += data.transformersToGet + (data.transformersToGet * (0.05 * data.infusionULevel3));

        data.power = 10;
        data.transformersToGet = 0;
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
