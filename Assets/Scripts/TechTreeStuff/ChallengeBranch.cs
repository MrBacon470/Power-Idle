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

public class ChallengeBranch : MonoBehaviour
{
    public TechTreeManager techTree;

    [Header("Object Stuff")]
    public Text[] challengeBranchText;
    public Image[] challengeBranchIcons = new Image[2];
    public string[] challengeBranchDesc;
    [Header("Numbers")]
    public BigDouble[] challengeBranchLevels;
    public BigDouble[] challengeBranchMaxLevels;
    public BigDouble[] challengeBranchBaseCosts;
    public BigDouble[] challengeBranchCosts;
    public BigDouble[] challengeBranchCostMults;
    public bool[] isChallengeBranchModuleLocked;
    [Header("Sprites")]
    public Sprite lockedIcon;
    public Sprite unlockedIcon;
    public Sprite maxedIcon;

    public void StartChallenge()
    {
        var data = techTree.game.data;
        challengeBranchText = new Text[2];
        challengeBranchCosts = new BigDouble[2];
        challengeBranchCostMults = new BigDouble[] { 1, 1 };
        challengeBranchBaseCosts = new BigDouble[] { 1e6, 1e12 };
        challengeBranchLevels = new BigDouble[2];
        challengeBranchMaxLevels = new BigDouble[] { 1, 1 };
        isChallengeBranchModuleLocked = new bool[2];
        challengeBranchDesc = new string[] { $"Unlock Sterlitzia Challenge Cost:{Methods.NotationMethod(challengeBranchCosts[0], "F0")} Transformers\nLevel:{Methods.NotationMethod(challengeBranchLevels[0], "F0")}/{Methods.NotationMethod(challengeBranchMaxLevels[0], "F0")}"
            ,$"Unlock Red Dwarf Challenge Cost:{Methods.NotationMethod(challengeBranchCosts[1], "F0")} Transformers\nLevel:{Methods.NotationMethod(challengeBranchLevels[1], "F0")}/{Methods.NotationMethod(challengeBranchMaxLevels[1], "F0")}" };
    }

    public void UpdateChallenge()
    {
        var data = techTree.game.data;
        ArrayManager();
        BoolManager();
        ImageManager();

        challengeBranchCosts[0] = challengeBranchBaseCosts[0] * Pow(challengeBranchCostMults[0], data.prestigeBranch1Level);
        challengeBranchCosts[1] = challengeBranchBaseCosts[1] * Pow(challengeBranchCostMults[1], data.prestigeBranch2Level);

        for (int i = 0; i < 2; i++)
        {
            challengeBranchText[i].text = challengeBranchLevels[i] >= challengeBranchMaxLevels[i] ? "MAX" : challengeBranchDesc[i];
            if (challengeBranchLevels[i] > challengeBranchMaxLevels[i])
                challengeBranchLevels[i] = challengeBranchMaxLevels[i];
        }
    }

    public void BoolManager()
    {
        var data = techTree.game.data;
        if (data.isTechTreeUnlocked)
            data.isChallengeBranch1Locked = false;
        else
            data.isChallengeBranch1Locked = true;
        if (challengeBranchLevels[0] > 0)
            data.isChallengeBranch2Locked = false;
        else
            data.isChallengeBranch2Locked = true;
    }

    public void BuyModule(int index)
    {
        if (isChallengeBranchModuleLocked[index]) return;
        if (challengeBranchLevels[index] >= challengeBranchMaxLevels[index]) return;
        var data = techTree.game.data;
        if (data.amps >= challengeBranchCosts[index])
        {
            challengeBranchLevels[index]++;
            data.amps -= challengeBranchCosts[index];
        }
        NonArrayManager();
    }

    public void ImageManager()
    {
        var data = techTree.game.data;
        if (data.isMasteryBranch1Locked)
            challengeBranchIcons[0].sprite = lockedIcon;
        else
            challengeBranchIcons[0].sprite = challengeBranchLevels[0] >= challengeBranchMaxLevels[0] ? maxedIcon : unlockedIcon;
        if (data.isMasteryBranch2Locked)
            challengeBranchIcons[1].sprite = lockedIcon;
        else
            challengeBranchIcons[1].sprite = challengeBranchLevels[1] >= challengeBranchMaxLevels[1] ? maxedIcon : unlockedIcon;

    }

    public void ArrayManager()
    {
        var data = techTree.game.data;
        challengeBranchLevels[0] = data.challengeBranch1Level;
        challengeBranchLevels[1] = data.challengeBranch2Level;

        isChallengeBranchModuleLocked[0] = data.isChallengeBranch1Locked;
        isChallengeBranchModuleLocked[1] = data.isChallengeBranch2Locked;
    }

    private void NonArrayManager()
    {
        var data = techTree.game.data;

        data.challengeBranch1Level = challengeBranchLevels[0];
        data.challengeBranch2Level = challengeBranchLevels[1];
    }
}
