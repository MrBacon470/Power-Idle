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

public class CorruptionController : MonoBehaviour
{
    public IdleGame game;

    public Text corruptionText;
    public Text corruptionBoostText;

    public Text[] corruptUpgradesText = new Text[3];

    public BigDouble[] corruptUpgradeCosts;
    public BigDouble[] corruptUpgradeLevels;

    private BigDouble Cost1 => 1e3 * Pow(1.15, game.data.corruptUpgrade1Level);
    private BigDouble Cost2 => 1e9 * Pow(1.25, game.data.corruptUpgrade2Level);
    private BigDouble Cost3 => 1e15 * Pow(1.5, game.data.corruptUpgrade3Level);

    public void StartCorruption()
    {
        corruptUpgradeCosts = new BigDouble[3];
        corruptUpgradeLevels = new BigDouble[3];
    }

    public void Run()
    {
        var data = game.data;
        ArrayManager();
        TextManager();


        data.corruptionCapacity = 1e6 + data.realityShards * 4;

        if (data.corruption / data.corruptionCapacity > 1)
            data.corruption = data.corruptionCapacity;

        if(data.corruption / data.corruptionCapacity < 1)
        {
            data.corruption = (data.corruptUpgrade1Level + data.corruptUpgrade1Produced) * Time.deltaTime;
            data.corruptUpgrade1Produced = (data.corruptUpgrade2Level + data.corruptUpgrade2Produced) * Time.deltaTime;
            data.corruptUpgrade2Produced = data.corruptUpgrade3Level * Time.deltaTime;
        }
    }

    public void TextManager()
    {
        var data = game.data;
        corruptUpgradesText[0].text = $"Harvester 1 Corruption/s\nCost:{Methods.NotationMethod(Cost1, "F0")}\nLevel:{Methods.NotationMethod(data.corruptUpgrade1Level + data.corruptUpgrade1Produced, "F0")}";
        corruptUpgradesText[1].text = $"Fabricator 1 Harvester/s\nCost:{Methods.NotationMethod(Cost2, "F0")}\nLevel:{Methods.NotationMethod(data.corruptUpgrade2Level + data.corruptUpgrade2Produced, "F0")}";
        corruptUpgradesText[2].text = $"Synthesiser 1 Fabricator/s\nCost:{Methods.NotationMethod(Cost3, "F0")}\nLevel:{Methods.NotationMethod(data.corruptUpgrade3Level, "F0")}";
        corruptionText.text = $"C̶o̷r̴r̴u̸p̴t̸i̵o̴n̷:{Methods.NotationMethod(data.corruption, "F0")}\nCapacity Filled:{Methods.NotationMethod((data.corruption / data.corruptionCapacity) * 100,"F2")}%";
        corruptionBoostText.text = $"Corrupt Boost:+{Methods.NotationMethod(corruptBoost(), "F0")}x";
    }

    public void BuyUpgrade(int id)
    {
        switch (id)
        {
            case 0:
                Buy(ref game.data.corruptUpgrade1Level);
                break;

            case 1:
                Buy(ref game.data.corruptUpgrade2Level);
                break;

            case 2:
                Buy(ref game.data.corruptUpgrade3Level);
                break;
        }

        void Buy(ref BigDouble level)
        {
            var data = game.data;

            if (data.realityShards < corruptUpgradeCosts[id]) return;
            data.realityShards -= corruptUpgradeCosts[id];
            level++;
            
        }

    }

    public void ArrayManager()
    {
        corruptUpgradeCosts[0] = Cost1;
        corruptUpgradeCosts[1] = Cost2;
        corruptUpgradeCosts[2] = Cost3;

        corruptUpgradeLevels[0] = game.data.infusionULevel1;
        corruptUpgradeLevels[1] = game.data.infusionULevel2;
        corruptUpgradeLevels[2] = game.data.infusionULevel3;
    }

    public BigDouble corruptBoost()
    {
        BigDouble temp = 0;
        if(game.data.corruption / game.data.corruptionCapacity < .75)
            temp = game.data.corruption / 2;
        if (game.data.corruption / game.data.corruptionCapacity >= .75)
            temp = game.data.corruption - game.data.corruption * ( game.data.corruption / game.data.corruptionCapacity);
        if (game.data.tomes5Level > 0)
            temp *= 2 * game.data.tomes5Level;
        return temp;
    }
}
