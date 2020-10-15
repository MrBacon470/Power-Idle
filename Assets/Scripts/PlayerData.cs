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
    public bool hasPrestiged;
    public bool hasMastered;
    #endregion
    #region settings/daily
    public bool dailyRewardReady;
    public int currentDay;
    public DateTime UTCtime;
    public bool offlineProgressCheck;

    public short notationType;
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
    public BigDouble superConductors;
    public BigDouble transformersToGet;
    #endregion

    #region Infusions
    public BigDouble infusionULevel1;
    public BigDouble infusionULevel2;
    public BigDouble infusionULevel3;
    #endregion
    public PlayerData()
    {
        power = 10;

        currentDay = 0;
        UTCtime = DateTime.UtcNow;
        dailyRewardReady = false;

        offlineProgressCheck = false;

        hasMastered = false;
        hasPrestiged = false;

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
        superConductors = 0;
        transformersToGet = 0;
    }
}
