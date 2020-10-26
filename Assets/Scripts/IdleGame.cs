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

public class IdleGame : MonoBehaviour
{
    public PlayerData data;
    public OfflineManager offline;
    public SaveSystem nonStaticSaveSystem;
    public DailyRewardManager daily;
    public UpgradesManager upgrades;
    public ResearchManager research;
    public PollutionManager pollution;
    public PrestigeManager prestige;
    public InfusionManager infuse;
    public MasteryManager mastery;
    public DysonSphereController dysonSphere;
    public ConsoleController console;
    public ScriptLibrary scriptLibrary;
    public ByteInfusionManager bytes;
    public ChallengeManager challenge;
    public TechTreeManager techTree;
    [Header("Branch Controllers")]
    public ConsoleBranch consoleB;
    public PowerBranch power;
    public PrestigeBranch prestigeB;
    public MasteryBranch masteryB;
    public ChallengeBranch challengeB;

    public Text powerText;
    public Text powerPerSecText;

    public Text transformersText;
    public Text superConductorsText;

    public Text quarkText;
    public Text quarkBoostText;

    public Text saveTimerText;

    public GameObject infusionButton;
    public GameObject megaButton;
    public GameObject consoleButton;
    public GameObject challengeButton;
    public GameObject techTreeButton;
    
    public BigDouble plasmaTemp;

    public Canvas mainMenuGroup;
    public Canvas settingsGroup;
    public Canvas sphereCanvas;
    public Canvas researchCanvas;
    public Canvas prestigeCanvas;
    public Canvas infusionCanvas;
    public Canvas consoleCanvas;
    public Canvas scriptLibraryCanvas;
    public Canvas techTreeCanvas;
    public Canvas byteInfusionCanvas;
    public Canvas challengeCanvas;
    public Canvas startScreen;

    public void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, false);
        Application.runInBackground = true;

        startScreen.gameObject.SetActive(true);
        mainMenuGroup.gameObject.SetActive(false);
        settingsGroup.gameObject.SetActive(false);
        researchCanvas.gameObject.SetActive(false);
        sphereCanvas.gameObject.SetActive(false);
        infusionCanvas.gameObject.SetActive(false);
        prestigeCanvas.gameObject.SetActive(false);
        consoleCanvas.gameObject.SetActive(false);
        scriptLibraryCanvas.gameObject.SetActive(false);
        techTreeCanvas.gameObject.SetActive(false);
        byteInfusionCanvas.gameObject.SetActive(false);
        challengeCanvas.gameObject.SetActive(false);
        data = SaveSystem.SaveExists("PlayerData") ? SaveSystem.LoadPlayer<PlayerData>("PlayerData") : new PlayerData();
        offline.LoadOfflineProduction();
        infuse.StartInfusion();
        console.StartConsole();
        scriptLibrary.StartLibrary();
        bytes.StartInfusion();
        challenge.StartChallenges();
        power.StartPower();
        consoleB.StartConsole();
        prestigeB.StartPrestige();
        masteryB.StartMastery();
        challengeB.StartChallenge();

        if (data.quarks > 0)
        {
            data.amps += data.quarks;
            data.quarks = 0;
        }
        TotalPowerPerSecond();
        Methods.NotationSettings = data.notationType;
    }

    public void Update()
    {
        if (data.power < 0)
            data.power = 0;
        if (data.powerCollected < data.power)
            data.powerCollected = data.power;
        if (data.isSoftCapped && data.power > 1.79e308)
            data.power = 1.79e308;

        prestige.Run();
        upgrades.RunUpgradesUI();
        upgrades.RunUpgrades();
        research.Run();
        pollution.Run();
        infuse.Run();
        mastery.Run();
        dysonSphere.Run();
        console.Run();
        scriptLibrary.Run();
        bytes.Run();
        challenge.Run();
        power.UpdatePower();
        consoleB.UpdateConsole();
        prestigeB.UpdatePrestige();
        masteryB.UpdateMastery();
        challengeB.UpdateChallenge();

        if (data.frameRateType == 0)
            Application.targetFrameRate = 60;
        else if (data.frameRateType == 1)
            Application.targetFrameRate = 30;
        else if (data.frameRateType == 2)
            Application.targetFrameRate = 15;


        transformersText.text = data.hasPrestiged ? $"{Methods.NotationMethod(data.transformers, "F2")} Transformers" : "Not Discovered Yet";
        superConductorsText.text = data.hasMastered ? $"{Methods.NotationMethod(data.superConductors, "F2")} Super Conductors" : "Not Discovered Yet";
        powerPerSecText.text = Methods.NotationMethod(TotalPowerPerSecond(), "F0") + " Power/s";
        powerText.text = data.power >= 1.79e308 && data.isSoftCapped ? $"{Methods.NotationMethod(data.power, "F2")} Power(Softcapped)" : "Power: " + Methods.NotationMethod(data.power, y: "F0");
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

        if (data.isConsoleUnlocked)
            consoleButton.gameObject.SetActive(true);
        else
            consoleButton.gameObject.SetActive(false);

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

        if (data.isTechTreeUnlocked)
            techTreeButton.gameObject.SetActive(true);
        else
            techTreeButton.gameObject.SetActive(false);

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
        temp += (1 - (1 * pollution.pollutionBoost)) * data.productionUpgrade1Level;
        temp += (5 - (5 * pollution.pollutionBoost)) * data.productionUpgrade2Level;
        temp += (10 - (10 * pollution.pollutionBoost)) * data.productionUpgrade3Level;
        temp += (100 - (100 * pollution.pollutionBoost)) * data.productionUpgrade4Level;
        temp += (1e3 - (1e3 * pollution.pollutionBoost)) * data.productionUpgrade5Level;
        temp += (1e4 - (1e4 * pollution.pollutionBoost)) * data.productionUpgrade6Level;
        temp += (1e7 - (1e7 * pollution.pollutionBoost)) * data.productionUpgrade7Level;
        temp += (1e10- (1e10 * pollution.pollutionBoost)) * data.productionUpgrade8Level;
        if (data.infusionULevel1 > 0)
            temp += temp * (0.05 * data.infusionULevel1);
        if(!data.isChallenge3Active && !data.isChallenge2Active)
            if (data.byteInfusionULevel1 > 0)
                temp += temp * (0.05 * data.byteInfusionULevel1);
        if (temp <= 0)
            temp = 0;
        if (data.hasMastered)
            temp += dysonSphere.SpherePowerPerSec();
        if (data.isConsoleUnlocked)
            temp *= console.BytesBoost();
        if (data.amps > 0)
            temp *= challenge.QuarkBoost();
        if(data.powerBranch1Level > 0)
            temp *= 30 * data.powerBranch1Level;
        if (data.isConsoleOn)
            temp -= 10;
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
            case "console":
                consoleCanvas.gameObject.SetActive(true);
                break;
            case "library":
                scriptLibraryCanvas.gameObject.SetActive(true);
                break;
            case "techtree":
                techTreeCanvas.gameObject.SetActive(true);
                break;
            case "challenge":
                challengeCanvas.gameObject.SetActive(true);
                break;
            case "bytes":
                byteInfusionCanvas.gameObject.SetActive(true);
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
        consoleCanvas.gameObject.SetActive(false);
        scriptLibraryCanvas.gameObject.SetActive(false);
        techTreeCanvas.gameObject.SetActive(false);
        challengeCanvas.gameObject.SetActive(false);
        byteInfusionCanvas.gameObject.SetActive(false);
    }

   public void FullReset()
    {
        data = new PlayerData();
        ChangeTabs("main");
        upgrades.Deactivate();
        research.Activate();
    }

}
