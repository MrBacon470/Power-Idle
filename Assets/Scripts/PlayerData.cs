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

using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;

[Serializable]
public class PlayerData
{
    #region main stuff
    public BigDouble power;
    public BigDouble powerCollected;
    public BigDouble kuakaCoin;
    public bool hasPrestiged;
    public bool hasMastered;
    public bool hasTranscended;
    public bool isKuakaCoinUnlocked;
    #endregion
    #region settings/daily
    public bool dailyRewardReady;
    public int currentDay;
    public DateTime UTCtime;
    public bool offlineProgressCheck;

    public short notationType;
    public short frameRateType;
    public short audioType;
    #endregion
    #region Upgrades
    public BigDouble productionUpgrade1Level;
    public BigDouble productionUpgrade2Level;
    public BigDouble productionUpgrade3Level;
    public BigDouble productionUpgrade4Level;
    public BigDouble productionUpgrade5Level;
    public BigDouble productionUpgrade6Level;
    public BigDouble productionUpgrade7Level;
    public BigDouble productionUpgrade8Level;

    public BigDouble productionUpgrade1Power;
    public BigDouble productionUpgrade2Power;
    public BigDouble productionUpgrade3Power;
    public BigDouble productionUpgrade4Power;
    public BigDouble productionUpgrade5Power;
    public BigDouble productionUpgrade6Power;
    public BigDouble productionUpgrade7Power;
    public BigDouble productionUpgrade8Power;
    #endregion
    public BigDouble currentPollution;

    #region researchStuff
    public bool isCompleted0;
    public bool isCompleted1;
    public bool isCompleted2;
    public bool isCompleted3;
    public bool isCompleted4;
    public bool isCompleted5;
    public bool isCompleted6;
    public bool isCompleted7;

    public int researchIndex;
    #endregion
    #region Prestige
    public BigDouble transformers;
    public BigDouble transformersToGet;
    public BigDouble superConductors;
    public BigDouble superConductorsToGet;
    #endregion

    #region Infusions
    public BigDouble infusionULevel1;
    public BigDouble infusionULevel2;
    public BigDouble infusionULevel3;
    #endregion
    #region DysonSphere
    public bool array1Complete;
    public bool array2Complete;
    public bool array3Complete;
    #endregion
    #region ConsoleStuff
    public BigDouble bytes;
    public bool isConsoleUnlocked;
    public bool isConsoleOn;
    public bool consolePopupClosed;
    public int scriptIndex;
    public bool isSoftCapped;

    public bool isScript1Selected;
    public bool isScript2Selected;
    public bool isScript3Selected;
    public bool isScript4Selected;
    public bool isScript5Selected;
    public bool isScript6Selected;

    public BigDouble byteInfusionULevel1;
    public BigDouble byteInfusionULevel2;
    public BigDouble byteInfusionULevel3;
    #endregion

    #region Challenges
    public BigDouble challengeLevel1;
    public BigDouble challengeLevel2;
    public BigDouble challengeLevel3;
    public BigDouble challengeLevel4;
    public BigDouble challengeLevel5;
    public BigDouble amps;
    public BigDouble quarks;

    public bool isChallenge1Active;
    public bool isChallenge2Active;
    public bool isChallenge3Active;
    public bool isChallenge4Active;
    public bool isChallenge5Active;

    public bool isChallengesUnlocked;
    #endregion

    #region techTreeStuff
    public bool isTechTreeUnlocked;
    #region Power Branch
    public BigDouble powerBranch1Level;
    public BigDouble powerBranch2Level;

    public bool isPowerBranch1Locked;
    public bool isPowerBranch2Locked;
    #endregion
    #region Console Branch
    public BigDouble consoleBranch1Level;

    public bool isConsoleBranch1Locked;
    #endregion
    #region Mastery Branch
    public BigDouble masteryBranch1Level;
    public BigDouble masteryBranch2Level;

    public bool isMasteryBranch1Locked;
    public bool isMasteryBranch2Locked;
    #endregion
    #region Prestige Branch
    public BigDouble prestigeBranch1Level;
    public BigDouble prestigeBranch2Level;

    public bool isPrestigeBranch1Locked;
    public bool isPrestigeBranch2Locked;
    public bool hasSacraficesBeenUnlocked;
    #endregion
    #region Challenge Branch
    public BigDouble challengeBranch1Level;
    public BigDouble challengeBranch2Level;

    public bool isChallengeBranch1Locked;
    public bool isChallengeBranch2Locked;
    #endregion
    #endregion

    #region achievements
    public bool isAchievement1Locked;
    public bool isAchievement2Locked;
    public bool isAchievement3Locked;
    public bool isAchievement4Locked;
    public bool isAchievement5Locked;
    public bool isAchievement6Locked;
    public bool isAchievement7Locked;
    public bool isAchievement8Locked;
    public bool isAchievement9Locked;
    public bool isAchievement10Locked;
    public bool isAchievement11Locked;
    #endregion

    public PlayerData()
    {
        power = 10;
        kuakaCoin = 1;
        notationType = 0;
        frameRateType = 0;
        audioType = 0;

        currentDay = 0;
        UTCtime = DateTime.UtcNow;
        dailyRewardReady = false;

        offlineProgressCheck = false;

        hasMastered = false;
        hasPrestiged = false;
        hasTranscended = false;
        isKuakaCoinUnlocked = false;

        #region Upgrades
        productionUpgrade1Level = 0;
        productionUpgrade2Level = 0;
        productionUpgrade3Level = 0;
        productionUpgrade4Level = 0;
        productionUpgrade5Level = 0;
        productionUpgrade6Level = 0;
        productionUpgrade7Level = 0;
        productionUpgrade8Level = 0;

        productionUpgrade1Power = 10;
        productionUpgrade2Power = 50;
        productionUpgrade3Power = 100;
        productionUpgrade4Power = 1e3;
        productionUpgrade5Power = 1e4;
        productionUpgrade6Power = 1e5;
        productionUpgrade7Power = 1e7;
        productionUpgrade8Power = 1e10;
        #endregion

        currentPollution = 0;

        isCompleted0 = true;
        isCompleted1 = false;
        isCompleted2 = false;
        isCompleted3 = false;
        isCompleted4 = false;
        isCompleted5 = false;
        isCompleted6 = false;
        isCompleted7 = false;

        researchIndex = 0;

        transformers = 0;
        transformersToGet = 0;
        superConductors = 0;
        superConductorsToGet = 0;

        array1Complete = false;
        array2Complete = false;
        array3Complete = false;

        #region ConsoleStuff
        bytes = 0;
        isConsoleUnlocked = false;
        isConsoleOn = false;
        consolePopupClosed = false;
        isSoftCapped = true;
        scriptIndex = 0;
        isScript1Selected = true;
        isScript2Selected = false;
        isScript3Selected = false;
        isScript4Selected = false;
        isScript5Selected = false;
        isScript6Selected = false;

        byteInfusionULevel1 = 0;
        byteInfusionULevel2 = 0;
        byteInfusionULevel3 = 0;
        #endregion

        #region Challenges
        challengeLevel1 = 0;
        challengeLevel2 = 0;
        challengeLevel3 = 0;
        challengeLevel4 = 0;
        challengeLevel5 = 0;
        amps = 0;
        quarks = 0;

        isChallenge1Active = false;
        isChallenge2Active = false;
        isChallenge3Active = false;
        isChallenge4Active = false;
        isChallenge5Active = false;
        isChallengesUnlocked = false;
        #endregion

        #region TechTreeStuff
        isTechTreeUnlocked = false;
        #region Power Branch
        powerBranch1Level = 0;
        powerBranch2Level = 0;

        isPowerBranch1Locked = true;
        isPowerBranch2Locked = true;
        #endregion
        #region Console Branch
        consoleBranch1Level = 0;

        isConsoleBranch1Locked = true;
        #endregion
        #region Mastery Branch
        masteryBranch1Level = 0;
        masteryBranch2Level = 0;

        isMasteryBranch1Locked = true;
        isMasteryBranch2Locked = true;
        #endregion
        #region Prestige Branch
        prestigeBranch1Level = 0;
        prestigeBranch2Level = 0;

        isPrestigeBranch1Locked = true;
        isPrestigeBranch2Locked = true;
        #endregion
        #region Challenge Branch
        challengeBranch1Level = 0;
        challengeBranch2Level = 0;

        isChallengeBranch1Locked = true;
        isChallengeBranch2Locked = true;
        hasSacraficesBeenUnlocked = false;
        #endregion
        #endregion

        #region achievements
        isAchievement1Locked = true;
        isAchievement2Locked = true;
        isAchievement3Locked = true;
        isAchievement4Locked = true;
        isAchievement5Locked = true;
        isAchievement6Locked = true;
        isAchievement7Locked = true;
        isAchievement8Locked = true;
        isAchievement9Locked = true;
        isAchievement10Locked = true;
        isAchievement11Locked = true;
        #endregion
    }
}
