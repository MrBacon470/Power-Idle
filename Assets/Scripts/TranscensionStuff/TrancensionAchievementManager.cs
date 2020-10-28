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

public class TrancensionAchievementManager : MonoBehaviour
{
    public IdleGame game;
    [Header("Sprites")]
    public Sprite lockedIcon;
    public Sprite unlockedIcon;
    [Header("Storage")]
    public Image[] achievementIcons = new Image[7];
    public Text[] achievementTexts = new Text[7];

    public void Run()
    {
        BoolManager();
        ImageManager();
        TextManager();
    }

    public void BoolManager()
    {
        var data = game.data;
        if (data.realityShards > 1e38 && data.isTransAchievement1Locked)
            data.isTransAchievement1Locked = false;
        if (data.realityShards > 1.79e308 && data.isTransAchievement2Locked)
            data.isTransAchievement2Locked = false;
        if (data.corruption > 1e38 && data.isTransAchievement3Locked)
            data.isTransAchievement3Locked = false; 
        if (data.corruption > 1.79e308 && data.isTransAchievement4Locked)
            data.isTransAchievement4Locked = false;
        if (data.tomes1Level >= 1 && data.tomes2Level >= 1 && data.tomes3Level >= 1 && data.tomes4Level >= 1 && data.tomes5Level >= 1 && data.isTransAchievement5Locked)
            data.isTransAchievement5Locked = false;
        if (data.tomes1Level >= 10 && data.tomes2Level >= 10 && data.tomes3Level >= 10 && data.tomes4Level >= 10 && data.tomes5Level >= 10 && data.isTransAchievement6Locked)
            data.isTransAchievement6Locked = false;
        if (data.tomes1Level >= 100 && data.tomes2Level >= 100 && data.tomes3Level >= 100 && data.tomes4Level >= 100 && data.tomes5Level >= 100 && data.isTransAchievement7Locked)
            data.isTransAchievement7Locked = false;
    }

    public void TextManager()
    {
        var data = game.data;

        achievementTexts[0].text = $"Reality Bearer Reach {Methods.NotationMethod(1e38, "F0")} Reality Shards";
        achievementTexts[1].text = $"Reality Writer Reach {Methods.NotationMethod(1.79e308, "F0")} Reality Shards";
        achievementTexts[2].text = $"Small Problem Reach {Methods.NotationMethod(1e38, "F0")} Corruption";
        achievementTexts[3].text = $"Large Problem Reach {Methods.NotationMethod(1.79e308, "F0")} Corruption";
        achievementTexts[4].text = $"Wizard Upgrade All Tomes Once";
        achievementTexts[5].text = $"Alchemist Upgrade All Tomes 10 times";
        achievementTexts[6].text = $"Mystic Upgrade Max Out All Tomes";
    }

    public void ImageManager()
    {
        var data = game.data;
        if (data.isAchievement1Locked)
            achievementIcons[0].sprite = lockedIcon;
        else
            achievementIcons[0].sprite = unlockedIcon;

        if (data.isAchievement2Locked)
            achievementIcons[1].sprite = lockedIcon;
        else
            achievementIcons[1].sprite = unlockedIcon;

        if (data.isAchievement3Locked)
            achievementIcons[2].sprite = lockedIcon;
        else
            achievementIcons[2].sprite = unlockedIcon;

        if (data.isAchievement4Locked)
            achievementIcons[3].sprite = lockedIcon;
        else
            achievementIcons[3].sprite = unlockedIcon;

        if (data.isAchievement5Locked)
            achievementIcons[4].sprite = lockedIcon;
        else
            achievementIcons[4].sprite = unlockedIcon;

        if (data.isAchievement6Locked)
            achievementIcons[5].sprite = lockedIcon;
        else
            achievementIcons[5].sprite = unlockedIcon;

        if (data.isAchievement7Locked)
            achievementIcons[6].sprite = lockedIcon;
        else
            achievementIcons[6].sprite = unlockedIcon;

    }
}
