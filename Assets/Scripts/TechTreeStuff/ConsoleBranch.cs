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
    public Text[] consoleBranchText = new Text[1];
    public Image[] consoleBranchIcons = new Image[1];
    [Header("Numbers")]
    public BigDouble[] consoleBranchLevels;
    public BigDouble[] consoleBranchMaxLevels;
    public BigDouble[] consoleBranchBaseCosts;
    public BigDouble[] consoleBranchCosts;
    public BigDouble[] consoleBranchCostMults;
    public bool[] isConsoleBranchModuleLocked;
    [Header("Sprites")]
    public Sprite lockedIcon;
    public Sprite unlockedIcon;
    public Sprite maxedIcon;

    public void StartConsole()
    {
        var data = techTree.game.data;
        consoleBranchCosts = new BigDouble[1];
        consoleBranchCostMults = new BigDouble[] { 5 };
        consoleBranchBaseCosts = new BigDouble[] { 1e9 };
        consoleBranchLevels = new BigDouble[1];
        consoleBranchMaxLevels = new BigDouble[] { 10 };
        isConsoleBranchModuleLocked = new bool[1];
    }

    public void UpdateConsole()
    {
        var data = techTree.game.data;
        ArrayManager();
        BoolManager();
        ImageManager();
        TextManager();

        consoleBranchCosts[0] = consoleBranchBaseCosts[0] * Pow(consoleBranchCostMults[0], data.consoleBranch1Level);

        for (int i = 0; i < 1; i++)
        {
            if (consoleBranchLevels[i] > consoleBranchMaxLevels[i])
                consoleBranchLevels[i] = consoleBranchMaxLevels[i];
        }
    }

    public void BoolManager()
    {
        var data = techTree.game.data;
        if (techTree.power.powerBranchLevels[0] > 0)
            data.isConsoleBranch1Locked = false;
        else
            data.isConsoleBranch1Locked = true;
    }

    public void TextManager()
    {
        consoleBranchText[0].text = consoleBranchLevels[0] >= consoleBranchMaxLevels[0] ? "MAX" : $"5x Bytes/s Cost:{Methods.NotationMethod(consoleBranchCosts[0], "F0")} Bytes\nLevel:{Methods.NotationMethod(consoleBranchLevels[0], "F0")}/{Methods.NotationMethod(consoleBranchMaxLevels[0], "F0")}";
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

    public void ImageManager()
    {
        var data = techTree.game.data;
        if (data.isConsoleBranch1Locked)
            consoleBranchIcons[0].sprite = lockedIcon;
        else
            consoleBranchIcons[0].sprite = consoleBranchLevels[0] >= consoleBranchMaxLevels[0] ? maxedIcon : unlockedIcon;

    }

    private void ArrayManager()
    {
        var data = techTree.game.data;

        consoleBranchLevels[0] = data.consoleBranch1Level;

        isConsoleBranchModuleLocked[0] = data.isConsoleBranch1Locked;
    }

    private void NonArrayManager()
    {
        var data = techTree.game.data;

        data.consoleBranch1Level = consoleBranchLevels[0];
    }
}
