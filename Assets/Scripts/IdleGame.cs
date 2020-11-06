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

public class IdleGame : MonoBehaviour
{
    [Header("Scripts")]
    public PlayerData data;
    public OfflineManager offline;
    public SaveSystem nonStaticSaveSystem;
    public UpgradesManager upgrades;
    public ResearchManager research;
    public PollutionManager pollution;
    public PrestigeManager prestige;
    public InfusionManager infuse;
    public MasteryManager mastery;
    public DysonSphereController dysonSphere;
    public ChallengeManager challenge;
    public AchievementManager achievement;
    public KuakaManager kuaka;
    public BreakController broken;
    public HyperResearchManager hyper;
    [Header("Texts")]
    public Text powerText;
    public Text powerPerSecText;

    public Text transformersText;
    public Text superConductorsText;

    public Text quarkText;
    public Text quarkBoostText;

    public Text saveTimerText;
    [Header("Buttons")]
    public GameObject infusionButton;
    public GameObject megaButton;
    public GameObject challengeButton;
    public GameObject repairButton;
    public GameObject hyperButton;
    [Header("Canvases")]
    public Canvas mainMenuGroup;
    public Canvas settingsGroup;
    public Canvas sphereCanvas;
    public Canvas researchCanvas;
    public Canvas prestigeCanvas;
    public Canvas infusionCanvas;
    public Canvas challengeCanvas;
    public Canvas startScreen;
    public Canvas achievementCanvas;
    public Canvas kuakaCanvas;
    public Canvas repairCanvas;
    public Canvas hyperCanvas;

    public void Start()
    {
        Application.targetFrameRate = 60;
        Screen.fullScreen = true;
        Application.runInBackground = true;

        startScreen.gameObject.SetActive(true);
        mainMenuGroup.gameObject.SetActive(false);
        settingsGroup.gameObject.SetActive(false);
        researchCanvas.gameObject.SetActive(false);
        sphereCanvas.gameObject.SetActive(false);
        infusionCanvas.gameObject.SetActive(false);
        prestigeCanvas.gameObject.SetActive(false);
        challengeCanvas.gameObject.SetActive(false);
        achievementCanvas.gameObject.SetActive(false);
        kuakaCanvas.gameObject.SetActive(false);
        repairCanvas.gameObject.SetActive(false);
        hyperCanvas.gameObject.SetActive(false);
        data = SaveSystem.SaveExists("PlayerData") ? SaveSystem.LoadPlayer<PlayerData>("PlayerData") : new PlayerData();
        offline.LoadOfflineProduction();
        infuse.StartInfusion();
        challenge.StartChallenges();
        kuaka.StartKuaka();
        broken.StartBreak();
        if(!data.hasAchievementsBeenReset)
        {
            achievement.ResetAchievements();
            data.hasAchievementsBeenReset = true;
        }
        

        TotalPowerPerSecond();
        Methods.NotationSettings = data.notationType;
        data.audioType = 1;
        data.frameRateType = 0;

        
    }

    public void Update()
    {
        if (data.power < 0)
            data.power = 0;
        if (data.powerCollected < data.power)
            data.powerCollected = data.power;
        if (data.isSoftCapped && data.power > 1.79e308)
            data.power = 1.79e308;

        if (!data.isKuakaCoinUnlocked)
            data.isAchievement10Locked = true;
        if (!data.isKuakaCoinUnlocked)
            data.isAchievement11Locked = true;

        if (data.power > 1e21 && data.hasPrestiged)
            data.isChallengesUnlocked = true;

        prestige.Run();
        upgrades.RunUpgradesUI();
        upgrades.RunUpgrades();
        research.Run();
        pollution.Run();
        infuse.Run();
        mastery.Run();
        dysonSphere.Run();
        challenge.Run();
        achievement.Run();
        kuaka.UpdateKuaka();
        hyper.Run();
        if(data.hasPrestiged)
            broken.Run();
        if (data.frameRateType == 0)
            Application.targetFrameRate = 60;
        else if (data.frameRateType == 1)
            Application.targetFrameRate = 30;
        else if (data.frameRateType == 2)
            Application.targetFrameRate = 15;


        transformersText.text = data.hasPrestiged ? $"{Methods.NotationMethod(data.transformers, "F2")} Transformers" : "Not Discovered Yet";
        superConductorsText.text = data.hasMastered ? $"{Methods.NotationMethod(data.superConductors, "F2")} Super Conductors" : "Not Discovered Yet";
        powerText.text = data.power >= 1.79e308 && data.isSoftCapped ? $"{Methods.NotationMethod(data.power, "F2")} Power(Softcapped)" : "Power: " + Methods.NotationMethod(data.power, y: "F0");
        powerPerSecText.text = $"{Methods.NotationMethod(TotalPowerPerSecond(), "F0")} Power/s";
        quarkText.text = data.amps <= 0 ? "Not Discovered Yet" : $"{Methods.NotationMethod(data.amps, "F2")} Amps";
        quarkBoostText.text = data.amps <= 0 ? "?????" : $"{Methods.NotationMethod(challenge.QuarkBoost(), "F2")}";

        if (data.hasPrestiged)
            infusionButton.gameObject.SetActive(true);
        else
            infusionButton.gameObject.SetActive(false);

        if (data.hasMastered)
            megaButton.gameObject.SetActive(true);
        else
            megaButton.gameObject.SetActive(false);


        if (data.power > 1.79e308 && data.isSoftCapped)
            data.power = 1.79e308;

        if (data.hasMastered)
            data.isSoftCapped = false;
        else
            data.isSoftCapped = true;

        if (data.isChallengesUnlocked)
            challengeButton.gameObject.SetActive(true);
        else
            challengeButton.gameObject.SetActive(false);

        if (data.hasPrestiged)
            repairButton.gameObject.SetActive(true);
        else
            repairButton.gameObject.SetActive(false);

        if (data.power > 1e38 && data.isChallengesUnlocked && data.isKuakaCoinUnlocked && !data.isHyperUnlocked)
            data.isHyperUnlocked = true;

        if (data.isHyperUnlocked)
            hyperButton.gameObject.SetActive(true);
        else
            hyperButton.gameObject.SetActive(false);

        data.power += TotalPowerPerSecond() * Time.deltaTime;
        data.powerCollected += TotalPowerPerSecond() * Time.deltaTime;

            saveTimerText.text = saveTimer < 12 ? $"{Methods.NotationMethod(15 - saveTimer, "F2")} Safe To Quit" : $"{Methods.NotationMethod(15 - saveTimer, "F2")} Not Safe To Quit";
            saveTimerText.color = saveTimer < 12 ? Color.green : Color.red; 

        saveTimer += Time.deltaTime;

        if (!(saveTimer >= 15)) return;
        Save();
        data.offlineProgressCheck = true;
        saveTimer = 0;
    }

    public void Save()
    {
        SaveSystem.SavePlayer(data, "PlayerData");
    }

    public float saveTimer;

    public void SmoothNumber(ref BigDouble smooth, BigDouble actual)
    {
        if (smooth > actual & actual == 0)
            smooth -= (smooth - actual) / 10 + 0.1 * Time.deltaTime;
        else if (Floor(smooth) < actual)
            smooth += (actual - smooth) / 10 + 0.1 * Time.deltaTime;
        else if (Floor(smooth) > actual)
            smooth -= (smooth - actual) / 10 + 0.1 * Time.deltaTime;
        else
        {
            smooth = actual;
        }
    }


    public BigDouble TotalPowerPerSecond()
    {
        BigDouble temp = 0;
        if(!data.isGen1Broken)
            temp += data.hyperIndex > 0 ? (1e3 - (1e3 * pollution.pollutionBoost)) * data.productionUpgrade1Level : (1 - (1 * pollution.pollutionBoost)) * data.productionUpgrade1Level;
        if(!data.isGen2Broken)
            temp += data.hyperIndex > 1 ? (5e3 - (5e3 * pollution.pollutionBoost)) * data.productionUpgrade2Level : (5 - (5 * pollution.pollutionBoost)) * data.productionUpgrade2Level;
        if (!data.isGen3Broken)
            temp += data.hyperIndex > 2 ? (1e4 - (1e4 * pollution.pollutionBoost)) * data.productionUpgrade3Level : (10 - (10 * pollution.pollutionBoost)) * data.productionUpgrade3Level;
        if (!data.isGen4Broken)
            temp += data.hyperIndex > 3 ? (1e5 - (1e5 * pollution.pollutionBoost)) * data.productionUpgrade4Level : (100 - (100 * pollution.pollutionBoost)) * data.productionUpgrade4Level;
        if (!data.isGen5Broken)
            temp += data.hyperIndex > 4 ? (1e6 - (1e6 * pollution.pollutionBoost)) * data.productionUpgrade5Level : (1e3 - (1e3 * pollution.pollutionBoost)) * data.productionUpgrade5Level;
        if (!data.isGen6Broken)
            temp += data.hyperIndex > 5 ? (1e7 - (1e7 * pollution.pollutionBoost)) * data.productionUpgrade6Level : (1e4 - (1e4 * pollution.pollutionBoost)) * data.productionUpgrade6Level;
        if (!data.isGen7Broken)
            temp += data.hyperIndex > 6 ? (1e10 - (1e10 * pollution.pollutionBoost)) * data.productionUpgrade7Level : (1e7 - (1e7 * pollution.pollutionBoost)) * data.productionUpgrade7Level;
        if (!data.isGen8Broken)
            temp += data.hyperIndex > 7 ? (1e20 - (1e20 * pollution.pollutionBoost)) * data.productionUpgrade8Level : (1e10- (1e10 * pollution.pollutionBoost)) * data.productionUpgrade8Level;
        if (data.infusionULevel1 > 0 && !data.isChallenge2Active)
            temp += temp * (0.25 * data.infusionULevel1);
        if (temp <= 0)
            temp = 0;
        if (data.transformers > 0 && !data.isChallenge2Active)
            temp *= prestige.TransformerBoost();
        if (data.hasMastered)
            temp += dysonSphere.SpherePowerPerSec();
        if (data.amps > 0)
            temp *= challenge.QuarkBoost();
        if (kuaka.burnToggle)
            temp *= 20;
        
        return temp;
    }


    // Buttons



    public void ChangeTabs(string id)
    {
        DisableAll();
        switch (id)
        {
            
            case "research":
                researchCanvas.gameObject.SetActive(true);
                break;
            case "main":
                mainMenuGroup.gameObject.SetActive(true);
                break;
            case "settings":
                settingsGroup.gameObject.SetActive(true);
                break;
            case "infuse":
                infusionCanvas.gameObject.SetActive(true);
                break;
            case "prestige":
                prestigeCanvas.gameObject.SetActive(true);
                break;
            case "sphere":
                sphereCanvas.gameObject.SetActive(true);
                break;
            case "challenge":
                challengeCanvas.gameObject.SetActive(true);
                break;
            case "achievement":
                achievementCanvas.gameObject.SetActive(true);
                break;
            case "kuaka":
                kuakaCanvas.gameObject.SetActive(true);
                break;
            case "repair":
                repairCanvas.gameObject.SetActive(true);
                break;
            case "hyper":
                hyperCanvas.gameObject.SetActive(true);
                break;
        }
    }

    void DisableAll()
    {
        startScreen.gameObject.SetActive(false);
        mainMenuGroup.gameObject.SetActive(false);
        settingsGroup.gameObject.SetActive(false);
        researchCanvas.gameObject.SetActive(false);
        sphereCanvas.gameObject.SetActive(false);
        infusionCanvas.gameObject.SetActive(false);
        prestigeCanvas.gameObject.SetActive(false);
        challengeCanvas.gameObject.SetActive(false);
        achievementCanvas.gameObject.SetActive(false);
        kuakaCanvas.gameObject.SetActive(false);
        repairCanvas.gameObject.SetActive(false);
        hyperCanvas.gameObject.SetActive(false);
    }

   public void FullReset()
    {
        data = new PlayerData();
        ChangeTabs("main");
        upgrades.Deactivate();
        research.Activate();
    }

}
