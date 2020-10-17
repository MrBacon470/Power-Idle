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
    
    public Text powerText;
    public Text powerPerSecText;

    public Text transformersText;
    public Text superConductorsText;

    public GameObject infusionButton;
    public GameObject megaButton;
    public GameObject consoleButton;
    
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

    public void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, false);
        Application.runInBackground = true;

        mainMenuGroup.gameObject.SetActive(true);
        settingsGroup.gameObject.SetActive(false);
        researchCanvas.gameObject.SetActive(false);
        sphereCanvas.gameObject.SetActive(false);
        infusionCanvas.gameObject.SetActive(false);
        prestigeCanvas.gameObject.SetActive(false);
        consoleCanvas.gameObject.SetActive(false);
        scriptLibraryCanvas.gameObject.SetActive(false);
        techTreeCanvas.gameObject.SetActive(false);
        data = SaveSystem.SaveExists("PlayerData") ? SaveSystem.LoadPlayer<PlayerData>("PlayerData") : new PlayerData();
        offline.LoadOfflineProduction();
        infuse.StartInfusion();
        console.StartConsole();
        scriptLibrary.StartLibrary();
        bytes.StartInfusion();

        TotalPowerPerSecond();
        Methods.NotationSettings = data.notationType;
    }

    public void Update()
    {
        if (data.power < 0)
            data.power = 0;

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


        transformersText.text = data.hasPrestiged ? $"{Methods.NotationMethod(data.transformers, "F2")} Transformers" : "Not Discovered Yet";
        superConductorsText.text = data.hasMastered ? $"{Methods.NotationMethod(data.superConductors, "F2")} Super Conductors" : "Not Discovered Yet";
        powerPerSecText.text = Methods.NotationMethod(TotalPowerPerSecond(), "F0") + " Power/s";
        powerText.text = data.power > 1e306 && data.isSoftCapped ? $"{Methods.NotationMethod(data.power, "F2")} Power(Softcapped)" : "Power: " + Methods.NotationMethod(data.power, y: "F0"); 

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

        if (data.power > 1e306 && data.isSoftCapped)
            data.power = 1e306;

        if (data.hasMastered)
            data.isSoftCapped = false;
        else
            data.isSoftCapped = true;

        data.power += TotalPowerPerSecond() * Time.deltaTime;
        data.powerCollected += TotalPowerPerSecond() * Time.deltaTime;

    saveTimer += Time.deltaTime;

        if (!(saveTimer >= 15)) return;
        Save();
        data.offlineProgressCheck = true;
        saveTimer = 0;
    }

    public async void Save()
    {
        await nonStaticSaveSystem.AwaitGetUTCTIme();
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
        if (data.byteInfusionULevel1 > 0)
            temp += temp * (0.05 * data.byteInfusionULevel1);
        if (temp <= 0)
            temp = 0;
        if (data.hasMastered)
            temp += dysonSphere.SpherePowerPerSec();
        if (data.isConsoleUnlocked)
            temp *= console.BytesBoost();
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
        }
    }

    void DisableAll()
    {
        mainMenuGroup.gameObject.SetActive(false);
        settingsGroup.gameObject.SetActive(false);
        researchCanvas.gameObject.SetActive(false);
        sphereCanvas.gameObject.SetActive(false);
        infusionCanvas.gameObject.SetActive(false);
        prestigeCanvas.gameObject.SetActive(false);
        consoleCanvas.gameObject.SetActive(false);
        scriptLibraryCanvas.gameObject.SetActive(false);
        techTreeCanvas.gameObject.SetActive(false);
    }

   public void FullReset()
    {
        data = new PlayerData();
        ChangeTabs("main");
        upgrades.Deactivate();
        research.Activate();
    }

}
