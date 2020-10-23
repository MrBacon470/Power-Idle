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
public class PowerBranch : MonoBehaviour
{
    public TechTreeManager techTree;

    [Header("Object Stuff")]
    public Text[] powerBranchText;
    public Image[] powerBranchIcons;
    public string[] powerBranchDesc;
    [Header("Numbers")]
    public BigDouble[] powerBranchLevels;
    public BigDouble[] powerBranchMaxLevels;
    public BigDouble[] powerBranchBaseCosts;
    public BigDouble[] powerBranchCosts;
    public BigDouble[] powerBranchCostMults;
    public bool[] isPowerBranchModuleLocked;

    public void StartPower()
    {
        var data = techTree.game.data;
        powerBranchText = new Text[2];
        powerBranchIcons = new Image[2];
        powerBranchCosts = new BigDouble[2];
        powerBranchCostMults = new BigDouble[] { 10, 5 };
        powerBranchBaseCosts = new BigDouble[] { 1e3, 1e12 };
        powerBranchLevels = new BigDouble[2];
        powerBranchMaxLevels = new BigDouble[] { 20, 10 };
        powerBranchDesc = new string[] {$"3x Power and Bytes Cost:{Methods.NotationMethod(powerBranchCosts[0],"F0")} Power\nLevel:{Methods.NotationMethod(powerBranchLevels[0],"F0")}/{Methods.NotationMethod(powerBranchMaxLevels[0],"F0")}"
            ,$"Decrease R&D Costs by 5% Cost:{Methods.NotationMethod(powerBranchCosts[1],"F0")} Power\nLevel:{Methods.NotationMethod(powerBranchLevels[1],"F0")}/{Methods.NotationMethod(powerBranchMaxLevels[1],"F0")}"};
    }
    
    public void UpdatePower()
    {
        var data = techTree.game.data;
        ArrayManager();
        BoolManager();

        powerBranchCosts[0] = powerBranchBaseCosts[0] * Pow(powerBranchCostMults[0], data.powerBranch1Level);
        powerBranchCosts[1] = powerBranchBaseCosts[1] * Pow(powerBranchCostMults[1], data.powerBranch2Level);

        for(int i = 0; i < 2; i++)
        {
            powerBranchText[i].text = powerBranchLevels[i] >= powerBranchMaxLevels[i] ? "MAX": powerBranchDesc[i];
            if (powerBranchLevels[i] > powerBranchMaxLevels[i])
                powerBranchLevels[i] = powerBranchMaxLevels[i];
        }
    }

    public void BoolManager()
    {
        var data = techTree.game.data;
        if (data.isTechTreeUnlocked)
            data.isPowerBranch1Locked = false;
        else
            data.isPowerBranch1Locked = true;
        if (powerBranchLevels[0] > 0)
            data.isPowerBranch2Locked = false;
        else
            data.isPowerBranch2Locked = true;
    }

    public void ImageManager()
    {
        var data = techTree.game.data;
        if (data.isPowerBranch1Locked)
            powerBranchIcons[0].sprite = techTree.lockedIcon;
        else
            powerBranchIcons[0].sprite = powerBranchLevels[0] >= powerBranchMaxLevels[0] ? techTree.maxedIcon : techTree.unlockedIcon;
        
    }

    public void BuyModule(int index)
    {
        if (isPowerBranchModuleLocked[index]) return;
        if (powerBranchLevels[index] >= powerBranchMaxLevels[index]) return;
        var data = techTree.game.data;
        if (data.power >= powerBranchCosts[index])
        {
                powerBranchLevels[index]++;
                data.power -= powerBranchCosts[index];
        }
        NonArrayManager();
    }

    private void ArrayManager()
    {
        var data = techTree.game.data;

        powerBranchLevels[0] = data.powerBranch1Level;
        powerBranchLevels[1] = data.powerBranch2Level;

        isPowerBranchModuleLocked[0] = data.isPowerBranch1Locked;
        isPowerBranchModuleLocked[1] = data.isPowerBranch2Locked;
    }

    private void NonArrayManager()
    {
        var data = techTree.game.data;

        data.powerBranch1Level = powerBranchLevels[0];
        data.powerBranch2Level = powerBranchLevels[1];
    }
    
}
