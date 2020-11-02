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

public class AchievementManager : MonoBehaviour
{
    public IdleGame game;
    [Header("Sprites")]
    public Sprite lockedIcon;
    public Sprite unlockedIcon;
    [Header("Storage")]
    public Image[] achievementIcons = new Image[9];
    public Text[] achievementTexts = new Text[9];

    public void Run()
    {
        BoolManager();
        ImageManager();
        TextManager();
    }

    public void BoolManager()
    {
        var data = game.data;
        if (data.power > 1e38 && data.isAchievement1Locked)
            data.isAchievement1Locked = false;
        if (data.power > 1.79e308 && data.isAchievement2Locked)
            data.isAchievement2Locked = false;
        if (data.transformers > 1e38 && data.isAchievement3Locked)
            data.isAchievement3Locked = false;
        if (data.transformers > 1.79e308 && data.isAchievement4Locked)
            data.isAchievement4Locked = false;
        if (data.superConductors > 1e38 && data.isAchievement5Locked)
            data.isAchievement5Locked = false;
        if (data.superConductors > 1.79e308 && data.isAchievement6Locked)
            data.isAchievement6Locked = false;
        if (data.challengeLevel1 > 0 && data.challengeLevel2 > 0 && data.challengeLevel3 > 0 && data.isAchievement7Locked)
            data.isAchievement7Locked = false;
        if (data.kuakaCoin >= 1e38 && data.isAchievement8Locked && data.isKuakaCoinUnlocked)
            data.isAchievement8Locked = false;
        if (data.kuakaCoin >= 1.79e308 && data.isAchievement9Locked && data.isKuakaCoinUnlocked)
            data.isAchievement9Locked = false;
    }

    public void TextManager()
    {
        var data = game.data;

        achievementTexts[0].text = $"Power Producer Reach {Methods.NotationMethod(1e38, "F0")} Power";
        achievementTexts[1].text = $"Power Hungry Reach {Methods.NotationMethod(1.79e308, "F0")} Power";
        achievementTexts[2].text = $"Transformered Reach {Methods.NotationMethod(1e38, "F0")} Transformers";
        achievementTexts[3].text = $"Transformer Addict Reach {Methods.NotationMethod(1.79e308, "F0")} Transformers";
        achievementTexts[4].text = $"Super Maniac Reach {Methods.NotationMethod(1e38, "F0")} Super Conductors";
        achievementTexts[5].text = $"Super Insanity Reach {Methods.NotationMethod(1.79e308, "F0")} Super Conductors";
        achievementTexts[6].text = $"Completionist Complete Every Challenge Once";
        achievementTexts[7].text = $"Smol Kuaka Reach {Methods.NotationMethod(1e38, "F0")} Kuaka Coin";
        achievementTexts[8].text = $"Lrg Kuaka Reach {Methods.NotationMethod(1.79e308, "F0")} Kuaka Coin";
    }

    public void ResetAchievements()
    {
        var data = game.data;

        data.isAchievement1Locked = true;
        data.isAchievement2Locked = true;
        data.isAchievement3Locked = true;
        data.isAchievement4Locked = true;
        data.isAchievement5Locked = true;
        data.isAchievement6Locked = true;
        data.isAchievement7Locked = true;
        data.isAchievement8Locked = true;
        data.isAchievement9Locked = true;
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

        if (data.isAchievement8Locked)
            achievementIcons[7].sprite = lockedIcon;
        else
            achievementIcons[7].sprite = unlockedIcon;

        if (data.isAchievement9Locked)
            achievementIcons[8].sprite = lockedIcon;
        else
            achievementIcons[8].sprite = unlockedIcon;

    }
}
