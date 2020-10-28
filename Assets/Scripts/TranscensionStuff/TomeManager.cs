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

using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;

public class TomeManager : MonoBehaviour
{
    public IdleGame game;

    [Header("UI Stuff")]
    public Text[] tomeTexts = new Text[5];

    private BigDouble Cost1 => 1e12 * Pow(1.15, game.data.tomes1Level);
    private BigDouble Cost2 => 1e12 * Pow(1.25, game.data.tomes2Level);
    private BigDouble Cost3 => 1e12 * Pow(1.35, game.data.tomes3Level);
    private BigDouble Cost4 => 1e12 * Pow(1.45, game.data.tomes4Level);
    private BigDouble Cost5 => 1e12 * Pow(1.55, game.data.tomes5Level);
    [Header("Numbers")]
    public BigDouble[] tomesCosts;
    public BigDouble[] tomesLevels;

    public void StartTomes()
    {
        var data = game.data;
        tomesCosts = new BigDouble[5];
        tomesLevels = new BigDouble[5];
    }

    public void Run()
    {
        var data = game.data;
        ArrayManager();
        TextManager();
    }

    public void TextManager()
    {
        var data = game.data;
        tomeTexts[0].text = data.tomes1Level >= 1e2 ? "MAXED" : $"Cost:{Methods.NotationMethod(Cost1, "F0")} Power\nLevel:{data.tomes1Level}/100";
        tomeTexts[1].text = data.tomes2Level >= 1e2 ? "MAXED" : $"Cost:{Methods.NotationMethod(Cost2, "F0")} Bytes\nLevel:{data.tomes2Level}/100";
        tomeTexts[2].text = data.tomes3Level >= 1e2 ? "MAXED" : $"Cost:{Methods.NotationMethod(Cost3, "F0")} Transformers\nLevel:{data.tomes3Level}/100";
        tomeTexts[3].text = data.tomes4Level >= 1e2 ? "MAXED" : $"Cost:{Methods.NotationMethod(Cost4, "F0")} Super Conductors\nLevel:{data.tomes4Level}/100";
        tomeTexts[4].text = data.tomes5Level >= 1e2 ? "MAXED" : $"Cost:{Methods.NotationMethod(Cost5, "F0")} Reality Shards\nLevel:{data.tomes5Level}/100";
    }

    public void BuyTome(int id)
    {
        var data = game.data;
        switch (id)
        {
            case 0:
                if (data.tomes1Level >= 1e2) return;
                if(data.power >= Cost1)
                {
                    data.power -= Cost1;
                    data.tomes1Level++;
                }
                break;
            case 1:
                if (data.tomes2Level >= 1e2) return;
                if (data.bytes >= Cost2)
                {
                    data.bytes -= Cost2;
                    data.tomes1Level++;
                }
                break;
            case 2:
                if (data.tomes3Level >= 1e2) return;
                if (data.transformers >= Cost3)
                {
                    data.transformers -= Cost3;
                    data.tomes1Level++;
                }
                break;
            case 3:
                if (data.tomes4Level >= 1e2) return;
                if (data.superConductors >= Cost4)
                {
                    data.superConductors -= Cost4;
                    data.tomes1Level++;
                }
                break;
            case 4:
                if (data.tomes5Level >= 1e2) return;
                if (data.realityShards >= Cost5)
                {
                    data.realityShards -= Cost5;
                    data.tomes1Level++;
                }
                break;
        }
    }

    public void ArrayManager()
    {
        tomesCosts[0] = Cost1;
        tomesCosts[1] = Cost2;
        tomesCosts[2] = Cost3;
        tomesCosts[3] = Cost4;
        tomesCosts[4] = Cost5;

        tomesLevels[0] = game.data.tomes1Level;
        tomesLevels[1] = game.data.tomes2Level;
        tomesLevels[2] = game.data.tomes3Level;
        tomesLevels[3] = game.data.tomes3Level;
        tomesLevels[4] = game.data.tomes3Level;
    }
}
