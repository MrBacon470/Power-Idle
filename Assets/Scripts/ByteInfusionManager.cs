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

public class ByteInfusionManager : MonoBehaviour
{
    public IdleGame game;

    private BigDouble Cost1 => 1e5 * Pow(1.5, game.data.byteInfusionULevel1);
    private BigDouble Cost2 => 1e6 * Pow(1.75, game.data.byteInfusionULevel2);
    private BigDouble Cost3 => 1e7 * Pow(2.5, game.data.byteInfusionULevel3);

    public Text[] infusionsText = new Text[3];

    public BigDouble[] byteInfusionUCosts;
    public BigDouble[] byteInfusionULevels;

    public string[] costDesc;

    public void StartInfusion()
    {
        byteInfusionUCosts = new BigDouble[3];
        byteInfusionULevels = new BigDouble[3];
        costDesc = new string[] { "5% More Power Per Second", "5% More Bytes Per Second", "5% More Transformers On Prestiging" };
    }

    public void Run()
    {
        var data = game.data;

        ArrayManager();
        UI();

        void UI()
        {
            for (var i = 0; i < infusionsText.Length; i++)
            {
                infusionsText[i].text = $"Level {byteInfusionULevels[i]}\n{costDesc[i]}\nCost: {Methods.NotationMethod(byteInfusionUCosts[i], "F0")} Bytes";
            }

        }
    }

    public void BuyUpgrade(int id)
    {
        switch (id)
        {
            case 0:
                Buy(ref game.data.byteInfusionULevel1);
                break;

            case 1:
                Buy(ref game.data.byteInfusionULevel2);
                break;

            case 2:
                Buy(ref game.data.byteInfusionULevel3);
                break;
        }

        void Buy(ref BigDouble level)
        {

            if (game.data.bytes < byteInfusionUCosts[id]) return;
            game.data.bytes -= byteInfusionUCosts[id];
            level++;
        }

    }

    public void ArrayManager()
    {
        byteInfusionUCosts[0] = Cost1;
        byteInfusionUCosts[1] = Cost2;
        byteInfusionUCosts[2] = Cost3;

        byteInfusionULevels[0] = game.data.byteInfusionULevel1;
        byteInfusionULevels[1] = game.data.byteInfusionULevel2;
        byteInfusionULevels[2] = game.data.byteInfusionULevel3;
    }
}
