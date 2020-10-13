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
    
    public Button Singularity;

    public Text plasmaText;
    public Text clickValueText;

    public Text plasmaPerSecText;

    //prestige system


    public BigDouble plasmaTemp;

    public Canvas mainMenuGroup;
    public Canvas upgradesGroup;
    public Canvas settingsGroup;

    public void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(1920, 1080, false);
        Application.runInBackground = true;

        mainMenuGroup.gameObject.SetActive(true);
        upgradesGroup.gameObject.SetActive(false);
        settingsGroup.gameObject.SetActive(false);
        data = SaveSystem.SaveExists("PlayerData") ? SaveSystem.LoadPlayer<PlayerData>("PlayerData") : new PlayerData();
        offline.LoadOfflineProduction();

        TotalPlasmaPerSecond();
        Methods.NotationSettings = data.notationType;
    }

    public void Update()
    {
        


        //progress bars

        plasmaPerSecText.text = Methods.NotationMethod(TotalPlasmaPerSecond(), "F0") + " power/s";
        plasmaText.text = "Plasma: " + Methods.NotationMethod(data.power, y: "F0");

        data.power += TotalPlasmaPerSecond() * Time.deltaTime;
        data.powerCollected += TotalPlasmaPerSecond() * Time.deltaTime;

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


    public BigDouble TotalPlasmaPerSecond()
    {
        BigDouble temp = 0;
        return temp;
    }


    // Buttons



    public void ChangeTabs(string id)
    {
        DisableAll();
        switch (id)
        {
            
            case "upgrades":
                upgradesGroup.gameObject.SetActive(true);
                break;
            case "main":
                mainMenuGroup.gameObject.SetActive(true);
                break;
            case "gsettings":
                settingsGroup.gameObject.SetActive(true);
                break;
            case "bsettings":
                mainMenuGroup.gameObject.SetActive(true);
                break;
            case "realmsback":
                mainMenuGroup.gameObject.SetActive(true);
                break;
            
        }
    }

    void DisableAll()
    {
        mainMenuGroup.gameObject.SetActive(false);
        settingsGroup.gameObject.SetActive(false);
        upgradesGroup.gameObject.SetActive(false);
    }

   public void FullReset()
    {
        data = new PlayerData();
        ChangeTabs("main");
    }

}
