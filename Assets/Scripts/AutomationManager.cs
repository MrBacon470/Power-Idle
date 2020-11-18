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

public class AutomationManager : MonoBehaviour
{
    public IdleGame game;

    public Text[] autoTexts = new Text[3];

    public BigDouble totalResearchCost => 200 + 1e3 + 2e4 + 3e4 + 2e5 + 2e6 + 2e8;

    public void Run()
    {
        var data = game.data;

        if(!data.isAuto1Unlocked)
            autoTexts[0].text = $"Unlock Auto\nResearch\nCost {Methods.NotationMethod(1e68,"F0")}\nPower";
        else
            autoTexts[0].text = data.isAuto1On ? "Auto Research Active" : "Auto Research Offline";
        /*
        if(!data.isAuto2Unlocked)
            autoTexts[1].text = $"Unlock Auto\nPrestige\nCost {Methods.NotationMethod(1e68,"F0")}\nTransformers";
        else
            autoTexts[1].text = data.isAuto1On ? "Auto Prestige Active" : "Auto Prestige Offline"; Next Update
        */
        autoTexts[1].text = $"Coming\nNext\nUpdate\n3? Part 2";
        if(!data.isAuto3Unlocked)
            autoTexts[2].text = $"Unlock Auto\nRepair\nCost {Methods.NotationMethod(1e68,"F0")}\nPower";
        else
            autoTexts[2].text = data.isAuto1On ? "Auto Prestige Active" : "Auto Prestige Offline";

        if(data.isAuto1On)
        {
            if(data.power >= totalResearchCost)
            {
                data.power -= totalResearchCost;
                data.researchIndex = 7;

                data.isCompleted0 = false;
                data.isCompleted1 = false;
                data.isCompleted2 = false;
                data.isCompleted3 = false;
                data.isCompleted4 = false;
                data.isCompleted5 = false;
                data.isCompleted6 = false;
                data.isCompleted7 = true;
            }
        }
    }

    public void Unlock(int index)
    {
        var data = game.data;

        switch (index)
        {
            case 0:
            if(data.isAuto1Unlocked)
            {
                Toggle(0);
            }
            else
            {
                if(data.power < 1e68) return;
                data.power -= 1e68;
                data.isAuto1Unlocked = true;
            }
            break;
            
            case 1:
            if(data.isAuto2Unlocked)
            {
                Toggle(1);
            }
            else
            {
                if(data.transformers < 1e68) return;
                data.transformers -= 1e68;
                data.isAuto2Unlocked = true;
            }
            break;

            case 2:
            if(data.isAuto3Unlocked)
            {
                Toggle(2);
            }
            else
            {
                if(data.power < 1e68) return;
                data.power -= 1e68;
                data.isAuto3Unlocked = true;
            }
            break;
        }
    }
    
    public void Toggle(int index)
    {
        var data = game.data;
        switch (index)
        {
            case 0:
                data.isAuto1On = !data.isAuto1On;
            break;
            case 1:
                data.isAuto2On = !data.isAuto2On;
            break;
            case 2:
                data.isAuto3On = !data.isAuto3On;
            break;
        }
    }
}


