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
public class ConsoleBranch : MonoBehaviour
{
    public TechTreeManager techTree;

    [Header("Object Stuff")]
    public Text[] consoleBranchText;
    public Image[] consoleBranchIcons;
    public string[] consoleBranchDesc;
    [Header("Numbers")]
    public BigDouble[] consoleBranchLevels;
    public BigDouble[] consoleBranchMaxLevels;
    public BigDouble[] consoleBranchBaseCosts;
    public BigDouble[] consoleBranchCosts;
    public BigDouble[] consoleBranchCostMults;
    public bool[] isConsoleBranchModuleLocked;

    public void StartConsole()
    {
        var data = techTree.game.data;
        consoleBranchText = new Text[2];
        consoleBranchIcons = new Image[2];
        consoleBranchCosts = new BigDouble[2];
        consoleBranchCostMults = new BigDouble[] { 5 };
        consoleBranchBaseCosts = new BigDouble[] { 1e9 };
        consoleBranchLevels = new BigDouble[2];
        consoleBranchMaxLevels = new BigDouble[] { 10 };
        consoleBranchDesc = new string[] {$"5x Bytes/s Cost:{Methods.NotationMethod(consoleBranchCosts[0],"F0")} Bytes\nLevel:{Methods.NotationMethod(consoleBranchLevels[0],"F0")}/{Methods.NotationMethod(consoleBranchMaxLevels[0],"F0")}"};
    }

    public void UpdateConsole()
    {
        var data = techTree.game.data;
        ArrayManager();
        BoolManager();

        consoleBranchCosts[0] = consoleBranchBaseCosts[0] * Pow(consoleBranchCostMults[0], data.consoleBranch1Level);

        for (int i = 0; i < 2; i++)
        {
            consoleBranchText[i].text = consoleBranchLevels[i] >= consoleBranchMaxLevels[i] ? "MAX" : consoleBranchDesc[i];
            if (consoleBranchLevels[i] > consoleBranchMaxLevels[i])
                consoleBranchLevels[i] = consoleBranchMaxLevels[i];
        }
    }

    public void BoolManager()
    {
        var data = techTree.game.data;
        if (data.isTechTreeUnlocked)
            data.isPowerBranch1Locked = false;
        else
            data.isPowerBranch1Locked = true;
        if (consoleBranchLevels[0] > 0)
            data.isPowerBranch2Locked = false;
        else
            data.isPowerBranch2Locked = true;
    }

    public void BuyModule(int index)
    {
        if (isConsoleBranchModuleLocked[index]) return;
        if (consoleBranchLevels[index] >= consoleBranchMaxLevels[index]) return;
        var data = techTree.game.data;
        if (data.bytes >= consoleBranchCosts[index])
        {
            consoleBranchLevels[index]++;
            data.bytes -= consoleBranchCosts[index];
        }
        NonArrayManager();
    }

    private void ArrayManager()
    {
        var data = techTree.game.data;

        consoleBranchLevels[0] = data.powerBranch1Level;

        isConsoleBranchModuleLocked[0] = data.isConsoleBranch1Locked;
    }

    private void NonArrayManager()
    {
        var data = techTree.game.data;

        data.consoleBranch1Level = consoleBranchLevels[0];
    }
}
