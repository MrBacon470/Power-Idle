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
    
    private BigDouble Cost1 => 1e5 * Pow(1.15, game.data.infusionULevel1);
    private BigDouble Cost2 => 1e6 * Pow(1.25, game.data.infusionULevel2);
    private BigDouble Cost3 => 1e7 * Pow(1.5, game.data.infusionULevel3);

    private BigDouble Cost4 => 1e5 * Pow(1.15, game.data.sacraficeULevel1);
    private BigDouble Cost5 => 1e6 * Pow(1.25, game.data.sacraficeULevel2);
    private BigDouble Cost6 => 1e7 * Pow(1.5, game.data.sacraficeULevel3);
    [Header("UI")]
    public Text[] infusionsText = new Text[3];
    public Text infoText;
    public Text titleText;
    public Text infusionButtonText;
    public Text typeSwitchText;
    public Text boostText;
    public Text[] infusionBuyMaxText = new Text[3];
    public Image[] infusionBG = new Image[3];
    public Image infusionButton;
    public Image typeSwitchButton;
    public GameObject[] buyMaxButtons = new GameObject[3];
    [Header("Numbers")]
    public BigDouble[] infusionUCosts;
    public BigDouble[] infusionULevels;

    public BigDouble[] sacraficeUCosts;
    public BigDouble[] sacraficeULevels;

    public BigDouble[] infusionBaseCosts;
    public BigDouble[] infusionCostMults;
    [Header("Descriptions")]
    public string[] costDesc;
    public string[] sacraficeCostDesc;
    [Header("Sprites")]
    public Sprite sacraficeSprite;
    public Sprite infusionSprite;
    public Sprite infusionButtonSprite;
    public Sprite sacraficeButtonSprite;

    public Color infuse => new Color(.42f, .0039f, 1f);
    public Color mastery => new Color(1f, .0039f, 0f);

    public void StartInfusion()
    {
        var data = game.data;
        infusionUCosts = new BigDouble[3];
        infusionULevels = new BigDouble[3];

        sacraficeUCosts = new BigDouble[3];
        sacraficeULevels = new BigDouble[3];

        infusionBaseCosts = new BigDouble[]{1e5,1e6,1e7};
        infusionCostMults = new BigDouble[]{1.15,1.25,1.5};
        
        sacraficeCostDesc = new string[] { $"{Methods.NotationMethod(75 * data.sacraficeULevel1,"F0")}% More Dyson Sphere Production", $"{Methods.NotationMethod(25 * data.sacraficeULevel2,"F0")}% More Super Conductors On Mastery", $"Earn {Methods.NotationMethod(1 * data.sacraficeULevel3,"F0")}% Of Your Total Transformers Per Second" };
        costDesc = new string[] { $"{Methods.NotationMethod(25 * data.infusionULevel1,"F0")}% More Power Per Second", $"{Methods.NotationMethod(1.25 * data.infusionULevel2,"F0")} More Pollution Capacity", $"{Methods.NotationMethod(25 * data.infusionULevel3,"F0")}% More Transformers On Prestiging" };
    }

    public void Run()
    {
        var data = game.data;

        ArrayManager();
        UI();

        sacraficeCostDesc = new string[] { $"{Methods.NotationMethod(75 * data.sacraficeULevel1,"F0")}% More Dyson Sphere Production", $"{Methods.NotationMethod(25 * data.sacraficeULevel2,"F0")}% More Super Conductors On Mastery", $"Earn {Methods.NotationMethod(1 * data.sacraficeULevel3,"F0")}% Of Your Total Transformers Per Second" };
        costDesc = new string[] { $"{Methods.NotationMethod(25 * data.infusionULevel1,"F0")}% More Power Per Second", $"{Methods.NotationMethod(1.25 * data.infusionULevel2,"F0")} More Pollution Capacity", $"{Methods.NotationMethod(25 * data.infusionULevel3,"F0")}% More Transformers On Prestiging" };
        void UI()
        {
            for (var i = 0; i < infusionsText.Length; i++)
            {
                if(data.infusionIndex == 0)
                    infusionsText[i].text = data.isChallenge2Active || data.isChallenge3Active ? "Infusions\nDisabled": $"{costDesc[i]}\nCost: {Methods.NotationMethod(infusionUCosts[i], "F0")} Transformers\nLevel {infusionULevels[i]}";
                else if(data.infusionIndex == 1)
                    infusionsText[i].text = data.isChallenge3Active ? "Sacrafices\nDisabled" : $"{sacraficeCostDesc[i]}\nCost: {Methods.NotationMethod(sacraficeUCosts[i], "F0")} Super Conductors\nLevel {sacraficeULevels[i]}";
            }
            for (var i = 0; i < 3; i++)
            {
                infusionBuyMaxText[i].text = $"Buy Max ({BuyInfusionMaxCount(i)})";
            }
            if(data.infusionIndex == 0)
            {
                infoText.text = "Infuse Transformers To Upgrade Stats";
                titleText.text = "Infusions";
                infusionButtonText.text = "Infusions";
                typeSwitchText.text = "Switch To Sacrafices";

                for (var i = 0; i < buyMaxButtons.Length; i++)
                {
                    buyMaxButtons[i].gameObject.SetActive(true);
                }

                if (data.transformers >= 0)
                    boostText.text = data.isChallenge2Active || data.isChallenge3Active ? "Boost Disabled" : $"Transformed Boost: {Methods.NotationMethod(game.prestige.TransformerBoost(), "F0")}";

                infoText.color = infuse;
                titleText.color = infuse;
                boostText.color = infuse;
                
                infusionButton.sprite = infusionButtonSprite;
                typeSwitchButton.sprite = infusionButtonSprite;

                for (var i = 0; i < infusionBG.Length; i++)
                    infusionBG[i].sprite = infusionSprite;
            }
            else if(data.infusionIndex == 1)
            {
                infoText.text = "Sacrafice Super Conductors To Upgrade Stats";
                titleText.text = "Sacrafices";
                infusionButtonText.text = "Sacrafices";
                typeSwitchText.text = "Switch To Infusions";

                for (var i = 0; i < buyMaxButtons.Length; i++)
                {
                    buyMaxButtons[i].gameObject.SetActive(false);
                }

                boostText.text = data.isChallenge3Active ? "Boost Disabled" : $"Conductive Boost: {Methods.NotationMethod(game.mastery.ConductorBoost(), "F2")}";

                infoText.color = mastery;
                titleText.color = mastery;
                boostText.color = mastery;

                infusionButton.sprite = sacraficeButtonSprite;
                typeSwitchButton.sprite = sacraficeButtonSprite;

                for (var i = 0; i < infusionBG.Length; i++)
                    infusionBG[i].sprite = sacraficeSprite;
            }

        }
    }

    public void BuyUpgrade(int id)
    {
        var data = game.data;
        switch (id)
        {
            case 0:
                if (data.infusionIndex == 0)
                    Buy(ref data.infusionULevel1);
                else
                    Buy(ref data.sacraficeULevel1);
                break;

            case 1:
                if (data.infusionIndex == 0)
                    Buy(ref data.infusionULevel2);
                else
                    Buy(ref data.sacraficeULevel2);
                break;

            case 2:
                if (data.infusionIndex == 0)
                    Buy(ref data.infusionULevel3);
                else
                    Buy(ref data.sacraficeULevel3);
                break;
        }

        void Buy(ref BigDouble level)
        {
            if(data.infusionIndex == 0)
            {
                if (data.transformers < infusionUCosts[id]) return;
                data.transformers -= infusionUCosts[id];
                level++;
            }
            else
            {
                if (data.superConductors < sacraficeUCosts[id]) return;
                data.superConductors -= sacraficeUCosts[id];
                level++;
            }
            
        }

    }

    public void SwitchType()
    {
        var data = game.data;
        if (data.infusionIndex == 0)
            data.infusionIndex = 1;
        else if (data.infusionIndex == 1)
            data.infusionIndex = 0;
    }

    public void ArrayManager()
    {
        infusionUCosts[0] = Cost1;
        infusionUCosts[1] = Cost2;
        infusionUCosts[2] = Cost3;

        sacraficeUCosts[0] = Cost4;
        sacraficeUCosts[1] = Cost5;
        sacraficeUCosts[2] = Cost6;

        infusionULevels[0] = game.data.infusionULevel1;
        infusionULevels[1] = game.data.infusionULevel2;
        infusionULevels[2] = game.data.infusionULevel3;

        sacraficeULevels[0] = game.data.sacraficeULevel1;
        sacraficeULevels[1] = game.data.sacraficeULevel2;
        sacraficeULevels[2] = game.data.sacraficeULevel3;
    }

    private void NonArrayManager()
    {
        var data = game.data;

        data.infusionULevel1 = infusionULevels[0];
        data.infusionULevel2 = infusionULevels[1];
        data.infusionULevel3 = infusionULevels[2];
    }

    public void BuyInfusionMax(int index)
    {
        var data = game.data;
        var b1 = infusionBaseCosts[index];
        var c1 = data.power;
        var r1 = infusionCostMults[index];
        var k1 = infusionULevels[index];
        var n1 = Floor(Log((c1 * (r1 - 1) / (b1 * Pow(r1, k1))) + 1, r1));

        var cost2 = b1 * (Pow(r1, k1) * (Pow(r1, n1) - 1) / (r1 - 1));

        if (data.transformers >= cost2)
        {
            infusionULevels[index] += n1;
            data.transformers -= cost2;
        }
        NonArrayManager();
    } 

    public BigDouble BuyInfusionMaxCount(int index)
    {
        var data = game.data;
        var b = infusionBaseCosts[index];
        var c = data.transformers;
        var r = infusionCostMults[index];
        var k = infusionULevels[index];
        var n = Floor(Log((c * (r - 1) / (b * Pow(r, k))) + 1, r));
        return n;
    }  
}
