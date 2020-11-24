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
    public BigDouble flameCoin;
    public bool hasPrestiged;
    public bool hasMastered;
    public bool hasIonized;
    public bool isKuakaCoinUnlocked;
    public bool isFlameCoinUnlocked;
    public bool isSoftCapped;
    public bool hasAchievementsBeenReset;
    public bool isHyperUnlocked;
    public bool isScreenClosed;
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

    #region hyperStuff
    public bool isHyperCompleted0;
    public bool isHyperCompleted1;
    public bool isHyperCompleted2;
    public bool isHyperCompleted3;
    public bool isHyperCompleted4;
    public bool isHyperCompleted5;
    public bool isHyperCompleted6;
    public bool isHyperCompleted7;
    public bool isHyperCompleted8;
    public bool isHyperCompleted9;

    public int hyperIndex;
    #endregion

    #region Prestige
    public BigDouble transformers;
    public BigDouble superConductors;
    #endregion

    #region Infusions
    public BigDouble infusionULevel1;
    public BigDouble infusionULevel2;
    public BigDouble infusionULevel3;
    #endregion
    #region Sacrafices
    public BigDouble sacraficeULevel1;
    public BigDouble sacraficeULevel2;
    public BigDouble sacraficeULevel3;

    public bool issacraficesUnlocked;
    public int infusionIndex;
    #endregion
    #region DysonSphere
    public bool array1Complete;
    public bool array2Complete;
    public bool array3Complete;
    #endregion


    #region Challenges
    public BigDouble challengeLevel1;
    public BigDouble challengeLevel2;
    public BigDouble challengeLevel3;
    public BigDouble amps;

    public bool isChallenge1Active;
    public bool isChallenge2Active;
    public bool isChallenge3Active;

    public bool isChallengesUnlocked;
    #endregion

    #region Breaking
    public bool isGen1Broken;
    public bool isGen2Broken;
    public bool isGen3Broken;
    public bool isGen4Broken;
    public bool isGen5Broken;
    public bool isGen6Broken;
    public bool isGen7Broken;
    public bool isGen8Broken;
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

    #region Black Hole Bomb
    public bool isBHBUnlocked;
    public int hc1Level;
    public int hc2Level;
    public int hc3Level;
    #endregion

    #region Automators
    public bool isAuto1Unlocked;
    public bool isAuto2Unlocked;
    public bool isAuto3Unlocked;

    public bool isAuto1On;
    public bool isAuto2On;
    public bool isAuto3On;

    public bool isAutoUnlocked;
    #endregion



    #region Particles
    public bool isBetaUnlocked;
    public bool isGammaUnlocked;
    public bool isDeltaUnlocked;
    public bool isEpsilonUnlocked;
    public bool isZetaUnlocked;
    public bool isEtaUnlocked;
    public bool isThetaUnlocked;
    public bool isIotaUnlocked;
    public bool isKappaUnlocked;
    public bool isLambdaUnlocked;
    public bool isMuUnlocked;

    public BigDouble Alpha;
    public BigDouble Beta;
    public BigDouble Gamma;
    public BigDouble Delta;
    public BigDouble Epsilon;
    public BigDouble Zeta;
    public BigDouble Eta;
    public BigDouble Theta;
    public BigDouble Iota;
    public BigDouble Kappa;
    public BigDouble Lambda;
    public BigDouble Mu;
    #endregion
    
    #region Rays
    public bool isNuUnlocked;
    public bool isXiUnlocked;
    public bool isOmicronUnlocked;
    public bool isPiUnlocked;
    public bool isRhoUnlocked;
    public bool isSigmaUnlocked;
    public bool isTauUnlocked;
    public bool isUpsilonUnlocked;
    public bool isPhiUnlocked;
    public bool isChiUnlocked;
    public bool isPsiUnlocked;
    public bool isOmegaUnlocked;

    public BigDouble Nu;
    public BigDouble Xi;
    public BigDouble Omicron;
    public BigDouble Pi;
    public BigDouble Rho;
    public BigDouble Sigma;
    public BigDouble Tau;
    public BigDouble Upsilon;
    public BigDouble Phi;
    public BigDouble Chi;
    public BigDouble Psi;
    public BigDouble Omega;
    #endregion

    public PlayerData()
    {
        power = 10;
        kuakaCoin = 1;
        flameCoin = 1;
        notationType = 0;
        frameRateType = 0;
        audioType = 0;

        currentDay = 0;
        UTCtime = DateTime.UtcNow;
        dailyRewardReady = false;

        offlineProgressCheck = false;

        hasMastered = false;
        hasPrestiged = false;
        isKuakaCoinUnlocked = false;
        isFlameCoinUnlocked = false;
        isSoftCapped = true;
        hasAchievementsBeenReset = false;
        isHyperUnlocked = false;
        isScreenClosed = false;

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

        #region hyperStuff
        isHyperCompleted0 = true;
        isHyperCompleted1 = false;
        isHyperCompleted2 = false;
        isHyperCompleted3 = false;
        isHyperCompleted4 = false;
        isHyperCompleted5 = false;
        isHyperCompleted6 = false;
        isHyperCompleted7 = false;
        isHyperCompleted8 = false;
        isHyperCompleted9 = false;

        hyperIndex = 0;
        #endregion

        transformers = 0;
        superConductors = 0;

        array1Complete = false;
        array2Complete = false;
        array3Complete = false;

        

        #region Challenges
        challengeLevel1 = 0;
        challengeLevel2 = 0;
        challengeLevel3 = 0;
        amps = 0;

        isChallenge1Active = false;
        isChallenge2Active = false;
        isChallenge3Active = false;
        isChallengesUnlocked = false;
        #endregion

        #region Sacrafices
        sacraficeULevel1 = 0;
        sacraficeULevel2 = 0;
        sacraficeULevel3 = 0;

        issacraficesUnlocked = false;
        infusionIndex = 0;
        #endregion

        #region Breaking
        isGen1Broken = false;
        isGen2Broken = false;
        isGen3Broken = false;
        isGen4Broken = false;
        isGen5Broken = false;
        isGen6Broken = false;
        isGen7Broken = false;
        isGen8Broken = false;
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

        #region Black Hole Bomb
        isBHBUnlocked = false;
        hc1Level = 0;
        hc2Level = 0;
        hc3Level = 0;
        #endregion

        #region Automators
        isAuto1Unlocked = false;
        isAuto2Unlocked = false;
        isAuto3Unlocked = false;

        isAuto1On = false;
        isAuto2On = false;
        isAuto3On = false;

        isAutoUnlocked = false;
        #endregion

        #region Particles
        isBetaUnlocked = false;
        isGammaUnlocked = false;
        isDeltaUnlocked = false;
        isEpsilonUnlocked = false;
        isZetaUnlocked = false;
        isEtaUnlocked = false;
        isThetaUnlocked = false;
        isIotaUnlocked = false;
        isKappaUnlocked = false;
        isLambdaUnlocked = false;
        isMuUnlocked = false;

        Alpha = 10;
        Beta = 0;
        Gamma = 0;
        Delta = 0;
        Epsilon = 0;
        Zeta = 0;
        Eta = 0;
        Theta = 0;
        Iota = 0;
        Kappa = 0;
        Lambda = 0;
        Mu = 0;
        #endregion

        #region Rays
        isNuUnlocked = false;
        isXiUnlocked = false;
        isOmicronUnlocked = false;
        isPiUnlocked = false;
        isRhoUnlocked = false;
        isSigmaUnlocked = false;
        isTauUnlocked = false;
        isUpsilonUnlocked = false;
        isPhiUnlocked = false;
        isChiUnlocked = false;
        isPsiUnlocked = false;
        isOmegaUnlocked = false;

        Nu = 0;
        Xi = 0;
        Omicron = 0;
        Pi = 0;
        Rho = 0;
        Sigma = 0;
        Tau = 0;
        Upsilon = 0;
        Phi = 0;
        Chi = 0;
        Psi = 0;
        Omega = 0;
        #endregion
    }
}
