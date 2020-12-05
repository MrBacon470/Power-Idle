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

public class ChallengeManager : MonoBehaviour
{
    public IdleGame game;
    public UpgradesManager upgrades;
    public ResearchManager research;

    private BigDouble reward1 => 1e6 * Pow(10, game.data.challengeLevel1);
    private BigDouble reward2 => 1e9 * Pow(15, game.data.challengeLevel2);
    private BigDouble reward3 => 1e12 * Pow(20, game.data.challengeLevel3);

    public BigDouble challengeGoal1 => 1e12 * Pow(1e20, game.data.challengeLevel1);
    public BigDouble challengeGoal2 => 1e12 * Pow(1e20, game.data.challengeLevel2);
    public BigDouble challengeGoal3 => 1e12 * Pow(1e20, game.data.challengeLevel3);


    public Text[] challengeText = new Text[3];
    public Text challengeGoalText;

    public BigDouble[] challengeReward;
    public BigDouble[] challengeLevels;


    public void StartChallenges()
    {
        challengeReward = new BigDouble[3];
        challengeLevels = new BigDouble[3];
    }

    public void Run()
    {
        var data = game.data;

        UI();
        ArrayManager();
        Conditions();

        if (data.hasPrestiged && data.power > 1e38 && !data.isChallengesUnlocked)
            data.isChallengesUnlocked = true;

        void UI()
        {
            if(game.challengeCanvas.gameObject.activeSelf)
            {
                challengeText[0].text = data.isChallenge3Active || data.isChallenge2Active ? "OTHER CHALLENGE ACTIVE" : $"Challenge: Clean Energy\nUse only Manual Generators, Steam Turbines and Fusion Reactors to get to {Methods.NotationMethod(challengeGoal1, "F2")} Power\nReward: {Methods.NotationMethod(challengeReward[0], "F0")} Amps\nCompletions: {Methods.NotationMethod(challengeLevels[0], "F0")}";
                
                challengeText[1].text = data.isChallenge1Active || data.isChallenge3Active? "OTHER CHALLENGE ACTIVE" : $"Challenge: No Transformers\nGet to {Methods.NotationMethod(challengeGoal2, "F2")} Power with no Transformer Boost and Infusions\nReward: {Methods.NotationMethod(challengeReward[1], "F0")} Amps\nCompletions: {Methods.NotationMethod(challengeLevels[1], "F0")}";

                challengeText[2].text = data.isChallenge1Active || data.isChallenge2Active? "OTHER CHALLENGE ACTIVE" : $"Challenge: Impossible Mode\nGet to {Methods.NotationMethod(challengeGoal3, "F2")} Power with side effects of Clean Energy and No Transformers plus No Mastery Upgrades\nReward: {Methods.NotationMethod(challengeReward[2], "F0")} Amps\nCompletions: {Methods.NotationMethod(challengeLevels[2], "F0")}";
            }

            if(!data.isChallenge1Active && !data.isChallenge2Active && !data.isChallenge3Active)
                challengeGoalText.text = $"Not in a Challenge";
            else if(data.isChallenge1Active)
                challengeGoalText.text = $"C1 Active\nGoal:{Methods.NotationMethod(challengeGoal1, "F2")} Power";
            else if(data.isChallenge2Active)
                challengeGoalText.text = $"C2 Active\nGoal:{Methods.NotationMethod(challengeGoal2, "F2")} Power";
            else if(data.isChallenge3Active)
                challengeGoalText.text = $"C3 Active\nGoal:{Methods.NotationMethod(challengeGoal3, "F2")} Power";
        }
    }

    public void ChooseChallenge(int id)
    {
        var data = game.data;
        if (data.isChallenge1Active) return;
        if (data.isChallenge2Active) return;
        if (data.isChallenge3Active) return;
        switch (id)
        {
            case 0:
                data.isChallenge1Active = true;
                StartChallenge();
                break;

            case 1:
                data.isChallenge2Active = true;
                StartChallenge();
                break;

            case 2:
                data.isChallenge3Active = true;
                StartChallenge();
                break;
        }


    }

    public void ExitChallenge()
    {
        var data = game.data;
        data.isChallenge1Active = false;
        data.isChallenge2Active = false;
        data.isChallenge3Active = false;
    }

    public void StartChallenge()
    {
        var data = game.data;
        game.prestige.Prestige();
    }

    public void CompleteChallenge(int index)
    {
        var data = game.data;
        switch(index)
        {
            case 0:
                data.amps += reward1;
                data.isChallenge1Active = false;
                data.challengeLevel1++;
                break;
            case 1:
                data.amps += reward2;
                data.isChallenge2Active = false;
                data.challengeLevel2++;
                break;
            case 2:
                data.amps += reward3;
                data.isChallenge3Active = false;
                data.challengeLevel3++;
                break;
        }
    }

    public void ArrayManager()
    {
        challengeReward[0] = reward1;
        challengeReward[1] = reward2;
        challengeReward[2] = reward3;

        challengeLevels[0] = game.data.challengeLevel1;
        challengeLevels[1] = game.data.challengeLevel2;
        challengeLevels[2] = game.data.challengeLevel3;
    }
    public void Conditions()
    {
        var data = game.data;
        if(data.isChallenge1Active)
            if(data.power >= challengeGoal1)
            {
                CompleteChallenge(0);
            }
        if (data.isChallenge2Active)
            if (data.power >= challengeGoal2)
            {
                CompleteChallenge(1);
            }
        if (data.isChallenge3Active)
            if (data.power >= challengeGoal3)
            {
                CompleteChallenge(2);
            }
    }

    public BigDouble QuarkBoost()
    {
        var temp = game.data.amps * 0.1;
        return temp + 1;
    }
}

