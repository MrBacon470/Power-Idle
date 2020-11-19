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

public class HyperResearchManager : MonoBehaviour
{
    public IdleGame game;
    [Header("Sprites")]
    public Sprite[] hyperIcons = new Sprite[9];
    public Sprite[] nextHyperIcons = new Sprite[8];
    public Image researchIcon;
    public Image nextResearchIcon;
    [Header("Texts")]
    public Text hyperInfoText;
    public Text hyperNextInfoText;
    [Header("Misc")]
    public BigDouble[] hyperCosts;
    public GameObject hyperButton;

    public void Start()
    {
        hyperCosts = new BigDouble[] { 1e24, 1e30, 1e36, 1e42, 1e48, 1e54, 1e60, 1e66, 1e72 };
        ActivateHyper();
    }

    public void Run()
    {
        var data = game.data;
        #region ifs
        if (data.isHyperCompleted0)
        {
            researchIcon.sprite = hyperIcons[0];
            nextResearchIcon.sprite = nextHyperIcons[0];
            hyperInfoText.text = $"Anti-Grav Treadmill\nResearch Cost:{Methods.NotationMethod(hyperCosts[0], "F2")} Transformers";
            hyperNextInfoText.text = $"Wood Plasmifier\nResearch Cost:{Methods.NotationMethod(hyperCosts[1], "F2")} Transformers";
        }
        if (data.isHyperCompleted1)
        {
            researchIcon.sprite = hyperIcons[1];
            nextResearchIcon.sprite = nextHyperIcons[1];
            hyperInfoText.text = $"Wood Plasmifier\nResearch Cost:{Methods.NotationMethod(hyperCosts[1], "F2")} Transformers";
            hyperNextInfoText.text = $"Coal Incinerator\nResearch Cost:{Methods.NotationMethod(hyperCosts[2], "F2")} Transformers";
        }
        if (data.isHyperCompleted2)
        {
            researchIcon.sprite = hyperIcons[2];
            nextResearchIcon.sprite = nextHyperIcons[2];
            hyperInfoText.text = $"Coal Incinerator\nResearch Cost:{Methods.NotationMethod(hyperCosts[2], "F2")} Transformers";
            hyperNextInfoText.text = $"Petroleum Burner\nResearch Cost:{Methods.NotationMethod(hyperCosts[3], "F2")} Transformers";
        }
        if (data.isHyperCompleted3)
        {
            researchIcon.sprite = hyperIcons[3];
            nextResearchIcon.sprite = nextHyperIcons[3];
            hyperInfoText.text = $"Petroleum Burner\nResearch Cost:{Methods.NotationMethod(hyperCosts[3], "F2")} Transformers";
            hyperNextInfoText.text = $"Hot Gas Reactor\nResearch Cost:{Methods.NotationMethod(hyperCosts[4], "F2")} Transformers";
        }
        if (data.isHyperCompleted4)
        {
            researchIcon.sprite = hyperIcons[4];
            nextResearchIcon.sprite = nextHyperIcons[4];
            hyperInfoText.text = $"Hot Gas Reactor\nResearch Cost:{Methods.NotationMethod(hyperCosts[4], "F2")} Transformers";
            hyperNextInfoText.text = $"High Pressure Turbine\nResearch Cost:{Methods.NotationMethod(hyperCosts[5], "F2")} Transformers";
        }
        if (data.isHyperCompleted5)
        {
            researchIcon.sprite = hyperIcons[5];
            nextResearchIcon.sprite = nextHyperIcons[5];
            hyperInfoText.text = $"High Pressure Turbine\nResearch Cost:{Methods.NotationMethod(hyperCosts[5], "F2")} Transformers";
            hyperNextInfoText.text = $"Molten Salt Reactor\nResearch Cost:{Methods.NotationMethod(hyperCosts[6], "F2")} Transformers";
        }
        if (data.isHyperCompleted6)
        {
            researchIcon.sprite = hyperIcons[6];
            nextResearchIcon.sprite = nextHyperIcons[6];
            hyperInfoText.text = $"Molten Salt Reactor\nResearch Cost:{Methods.NotationMethod(hyperCosts[6], "F2")} Transformers";
            hyperNextInfoText.text = $"Zero Point Reactor\nResearch Cost:{Methods.NotationMethod(hyperCosts[7], "F2")} Transformers";
        }
        if (data.isHyperCompleted7)
        {
            researchIcon.sprite = hyperIcons[7];
            nextResearchIcon.sprite = nextHyperIcons[7];
            nextResearchIcon.gameObject.SetActive(false);
            hyperInfoText.text = $"Zero Point Reactor\nResearch Cost:{Methods.NotationMethod(hyperCosts[7], "F2")} Transformers";
            hyperNextInfoText.text = $"Black Hole Bomb\nResearch Cost:{Methods.NotationMethod(hyperCosts[8],"F2")} Super Conductors";
        }
        if (data.isHyperCompleted8)
        {
            researchIcon.sprite = hyperIcons[8];
            nextResearchIcon.gameObject.SetActive(false);
            hyperInfoText.text = $"Black Hole Bomb\nResearch Cost:{Methods.NotationMethod(hyperCosts[8],"F2")} Super Conductors";
            hyperNextInfoText.text = $"No More Research";
        }
        if (data.isHyperCompleted9)
        {
            researchIcon.gameObject.SetActive(false);
            nextResearchIcon.gameObject.SetActive(false);
            hyperInfoText.text = $"No More Research";
            hyperNextInfoText.text = $"No More Research";
        }

        #endregion

        if (data.hyperIndex >= 9)
            hyperButton.gameObject.SetActive(false);
        else
            hyperButton.gameObject.SetActive(true);
    }

    public void Research()
    {
        var data = game.data;
        if (data.transformers < hyperCosts[data.hyperIndex] && data.hyperIndex <= 7) return;
        else if(data.superConductors < hyperCosts[data.hyperIndex] && data.hyperIndex == 8) return;
        if (data.hyperIndex <= 7)
            data.transformers -= hyperCosts[data.hyperIndex];
        else if(data.hyperIndex == 8)
            data.superConductors -= hyperCosts[data.hyperIndex];

        switch (data.hyperIndex)
        {
            case 0:
                data.isHyperCompleted0 = false;
                data.isHyperCompleted1 = true;
                break;
            case 1:
                data.isHyperCompleted1 = false;
                data.isHyperCompleted2 = true;
                break;
            case 2:
                data.isHyperCompleted2 = false;
                data.isHyperCompleted3 = true;
                break;
            case 3:
                data.isHyperCompleted3 = false;
                data.isHyperCompleted4 = true;
                break;
            case 4:
                data.isHyperCompleted4 = false;
                data.isHyperCompleted5 = true;
                break;
            case 5:
                data.isHyperCompleted5 = false;
                data.isHyperCompleted6 = true;
                break;
            case 6:
                data.isHyperCompleted6 = false;
                data.isHyperCompleted7 = true;
                break;
            case 7:
                data.isHyperCompleted7 = false;
                data.isHyperCompleted8 = true;
                break;
            case 8:
                data.isHyperCompleted8 = false;
                data.isHyperCompleted9 = true;
                break;
        }
        if (data.hyperIndex <= 8)
            data.hyperIndex++;
        else return;
    }

    public void ActivateHyper()
    {
        researchIcon.gameObject.SetActive(true);
        nextResearchIcon.gameObject.SetActive(true);
    }
}
