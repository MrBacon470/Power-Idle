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

public class ConsoleController : MonoBehaviour
{
    public IdleGame game;

    public GameObject consolePopup;
    public Text consoleInfoText;
    public Text powerLossText;
    public Text powerButtonText;
    public Image powerButtonImage;

    public Text bytesText;
    public Text bytesBoostText;

    public string[] scriptDesc;
    public string[] bytesPerScript;

    public void StartConsole()
    {
        scriptDesc = new string[] { "Console.js", "Console.c++", "Console.html", "Console.css", "Console.py", "Console.cs" };
        bytesPerScript = new string[] { "1 Byte/s", "10 Bytes/s", "100 Bytes/s", $"{Methods.NotationMethod(1e3, "F2")} Bytes/s", $"{Methods.NotationMethod(1e4, "F2")} Bytes/s", $"{Methods.NotationMethod(1e5, "F2")} Bytes/s" };
    }

    public void Run()
    {
        var data = game.data;

        if (data.isChallenge2Active)
            data.isConsoleOn = false;
            

        if (data.power > 1e12 && data.hasPrestiged)
            data.isConsoleUnlocked = true;

        if (data.isConsoleUnlocked && !data.consolePopupClosed)
            consolePopup.gameObject.SetActive(true);
        else
            consolePopup.gameObject.SetActive(false);

        if (data.isConsoleOn)
            data.bytes += totalBytesPerSecond() * Time.deltaTime;

        if (data.currentPollution >= game.pollution.totalPollution)
            data.isConsoleOn = false;

        bytesText.text = data.isConsoleUnlocked ? $"{Methods.NotationMethod(data.bytes, "F2")} Bytes" : "Not Discovered Yet";
        bytesBoostText.text = data.isConsoleUnlocked ? $"Boost: +{Methods.NotationMethod(BytesBoost(), "F2")}x" : "Not Discovered Yet";

        if (game.consoleCanvas.gameObject.activeSelf)
        {
            if(data.isConsoleOn)
            {
                powerButtonImage.color = Color.green;
                powerButtonText.text = "ON";
                consoleInfoText.text = $"Script Loaded:" + scriptDesc[data.scriptIndex] + "\n" + Methods.NotationMethod(totalBytesPerSecond(),"F0");
                powerLossText.text = "-10 Power/s";
            }
            else
            {
                powerButtonImage.color = Color.red;
                powerButtonText.text = "OFF";
                consoleInfoText.text = "Console Offline";
                powerLossText.text = "Offline";
            }
        }
    }

    public void ToggleConsole()
    {
        var data = game.data;

        if (!data.isConsoleUnlocked) return;

        data.isConsoleOn = !data.isConsoleOn;

    }

    public void ClosePopup()
    {
        var data = game.data;
        if (data.consolePopupClosed) return;
        data.consolePopupClosed = true;
        consolePopup.gameObject.SetActive(false);
    }

    public BigDouble totalBytesPerSecond()
    {
        var data = game.data;
        BigDouble temp = 0;
        if (data.scriptIndex == 0)
            temp = 1;
        if (data.scriptIndex == 1)
            temp = 10;
        if (data.scriptIndex == 2)
            temp = 100;
        if (data.scriptIndex == 3)
            temp = 1e3;
        if (data.scriptIndex == 4)
            temp = 1e4;
        if (data.scriptIndex == 5)
            temp = 1e5;
        if (data.byteInfusionULevel2 > 0)
            temp += temp * (0.05 * data.byteInfusionULevel2);
        if (data.powerBranch1Level > 0)
            temp *= 3 * data.powerBranch1Level;
        if (data.consoleBranch1Level > 0)
            temp *= 5 * data.consoleBranch1Level;
        return temp;
    }

    public BigDouble BytesBoost()
    {
        var temp = game.data.bytes * 0.01;
        return temp + 1;
    }
}
