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
public class MasteryBranch : MonoBehaviour
{
    public TechTreeManager techTree;

    [Header("Object Stuff")]
    public Text[] masteryBranchText = new Text[2];
    public Image[] masteryBranchIcons = new Image[2];
    [Header("Numbers")]
    public BigDouble[] masteryBranchLevels;
    public BigDouble[] masteryBranchMaxLevels;
    public BigDouble[] masteryBranchBaseCosts;
    public BigDouble[] masteryBranchCosts;
    public BigDouble[] masteryBranchCostMults;
    public bool[] isMasteryBranchModuleLocked;
    [Header("Sprites")]
    public Sprite lockedIcon;
    public Sprite unlockedIcon;
    public Sprite maxedIcon;

    public void StartMastery()
    {
        var data = techTree.game.data;
        masteryBranchCosts = new BigDouble[2];
        masteryBranchCostMults = new BigDouble[] { 20, 1 };
        masteryBranchBaseCosts = new BigDouble[] { 1e6, 1e75 };
        masteryBranchLevels = new BigDouble[2];
        masteryBranchMaxLevels = new BigDouble[] { 5, 1 };
        isMasteryBranchModuleLocked = new bool[2];
    }

    public void UpdateMastery()
    {
        var data = techTree.game.data;
        ArrayManager();
        BoolManager();
        ImageManager();
        TextManager();

        masteryBranchCosts[0] = masteryBranchBaseCosts[0] * Pow(masteryBranchCostMults[0], data.masteryBranch1Level);
        masteryBranchCosts[1] = masteryBranchBaseCosts[1] * Pow(masteryBranchCostMults[1], data.masteryBranch2Level);

        for(int i = 0; i < 2; i++)
        {
            if (masteryBranchLevels[i] > masteryBranchMaxLevels[i])
                masteryBranchLevels[i] = masteryBranchMaxLevels[i];
        }
    }

    public void BoolManager()
    {
        var data = techTree.game.data;
        if (data.isTechTreeUnlocked)
            data.isMasteryBranch1Locked = false;
        else
            data.isMasteryBranch1Locked = true;
        if (masteryBranchLevels[0] > 0)
            data.isMasteryBranch2Locked = false;
        else
            data.isMasteryBranch2Locked = true;
    }

    public void TextManager()
    {
        masteryBranchText[0].text = masteryBranchLevels[0] >= masteryBranchMaxLevels[0] ? "MAX" : $"2x Dyson Sphere Production Cost:{Methods.NotationMethod(masteryBranchCosts[0], "F0")} Super Conductors\nLevel:{Methods.NotationMethod(masteryBranchLevels[0], "F0")}/{Methods.NotationMethod(masteryBranchMaxLevels[0], "F0")}";
        masteryBranchText[1].text = masteryBranchLevels[1] >= masteryBranchMaxLevels[1] ? "MAX" : $"Convert Sun into Neutron Star 50x Dyson Sphere Production Cost:{Methods.NotationMethod(masteryBranchCosts[1], "F0")} Super Conductors\nLevel:{Methods.NotationMethod(masteryBranchLevels[1], "F0")}/{Methods.NotationMethod(masteryBranchMaxLevels[1], "F0")}";
    }

    public void BuyModule(int index)
    {
        if (isMasteryBranchModuleLocked[index]) return;
        if (masteryBranchLevels[index] >= masteryBranchMaxLevels[index]) return;
        var data = techTree.game.data;
        if (data.superConductors >= masteryBranchCosts[index])
        {
            masteryBranchLevels[index]++;
            data.superConductors -= masteryBranchCosts[index];
        }
        NonArrayManager();
    }

    public void ImageManager()
    {
        var data = techTree.game.data;
        if (data.isMasteryBranch1Locked)
            masteryBranchIcons[0].sprite = lockedIcon;
        else
            masteryBranchIcons[0].sprite = masteryBranchLevels[0] >= masteryBranchMaxLevels[0] ? maxedIcon : unlockedIcon;
        if (data.isMasteryBranch2Locked)
            masteryBranchIcons[1].sprite = lockedIcon;
        else
            masteryBranchIcons[1].sprite = masteryBranchLevels[1] >= masteryBranchMaxLevels[1] ? maxedIcon : unlockedIcon;

    }

    public void ArrayManager()
    {
        var data = techTree.game.data;
        masteryBranchLevels[0] = data.masteryBranch1Level;
        masteryBranchLevels[1] = data.masteryBranch2Level;

        isMasteryBranchModuleLocked[0] = data.isMasteryBranch1Locked;
        isMasteryBranchModuleLocked[1] = data.isMasteryBranch2Locked;
    }

    private void NonArrayManager()
    {
        var data = techTree.game.data;

        data.masteryBranch1Level = masteryBranchLevels[0];
        data.masteryBranch2Level = masteryBranchLevels[1];
    }
}
