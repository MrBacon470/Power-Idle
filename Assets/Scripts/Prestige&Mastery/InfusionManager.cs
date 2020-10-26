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

public class InfusionManager : MonoBehaviour
{
    public IdleGame game;
    [Header("Image Stuff")]
    public Sprite sacraficeIcon;
    public Sprite infusionIcon;
    public Sprite sacraficeBG;
    public Sprite infusionBG;
    public Image buttonIcon;
    public Text buttonText;
    public Image[] infusionImages = new Image[3];
    public Text titleText;
    public Text infoText;

    private BigDouble Cost1 => 1e5 * Pow(1.15, game.data.infusionULevel1);
    private BigDouble Cost2 => 1e6 * Pow(1.25, game.data.infusionULevel2);
    private BigDouble Cost3 => 1e7 * Pow(1.5, game.data.infusionULevel3);

    public Text[] infusionsText = new Text[3];

    public BigDouble[] infusionUCosts;
    public BigDouble[] infusionULevels;

    public string[] costDesc;

    public void StartInfusion()
    {
        infusionUCosts = new BigDouble[3];
        infusionULevels = new BigDouble[3];
        costDesc = new string[] { "5% More Power Per Second", "1.5x Pollution Capacity", "5% More Transformers On Prestiging" };
    }

    public void Run()
    {
        var data = game.data;

        ArrayManager();
        UI();

        if(data.hasSacraficesBeenUnlocked)
        {
            buttonIcon.sprite = sacraficeIcon;
            buttonText.text = "Sacrafices";
            titleText.text = "Sacrafices";
            infoText.text = "Sacrafice Transformers\nTo Upgrade Stats";
        }
        else
        {
            buttonIcon.sprite = infusionIcon;
            buttonText.text = "Infusions";
            titleText.text = "Infusions";
            infoText.text = "Infuse Transformers\nTo Upgrade Stats";
        }

        void UI()
        {
            for (var i = 0; i < infusionsText.Length; i++)
            {
                infusionsText[i].text = data.hasSacraficesBeenUnlocked ? $"{costDesc[i]}\nCost: {Methods.NotationMethod(infusionUCosts[i] + data.transformers / 2, "F0")} Transformers\nLevel {infusionULevels[i]}" : $"{costDesc[i]}\nCost: {Methods.NotationMethod(infusionUCosts[i], "F0")} Transformers\nLevel {infusionULevels[i]}";
            }
            if(data.hasSacraficesBeenUnlocked)
            {
                for(int i = 0; i < 3; i++)
                {
                    infusionImages[i].sprite = sacraficeBG;
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    infusionImages[i].sprite = infusionBG;
                }
            }
        }
    }

    public void BuyUpgrade(int id)
    {
        switch (id)
        {
            case 0:
                Buy(ref game.data.infusionULevel1);
                break;

            case 1:
                Buy(ref game.data.infusionULevel2);
                break;

            case 2:
                Buy(ref game.data.infusionULevel3);
                break;
        }

        void Buy(ref BigDouble level)
        {
            var data = game.data;
            if(!data.hasSacraficesBeenUnlocked)
            {
                if (data.transformers < infusionUCosts[id]) return;
                data.transformers -= infusionUCosts[id];
                level++;
            }
            else
            {
                if (data.transformers < (infusionUCosts[id] + data.transformers / 2)) return;
                data.transformers -= infusionUCosts[id] + data.transformers / 2;
                level += 10;
            }
        }

    }

    public void ArrayManager()
    {
        infusionUCosts[0] = Cost1;
        infusionUCosts[1] = Cost2;
        infusionUCosts[2] = Cost3;

        infusionULevels[0] = game.data.infusionULevel1;
        infusionULevels[1] = game.data.infusionULevel2;
        infusionULevels[2] = game.data.infusionULevel3;
    }
}
