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

public class ResearchManager : MonoBehaviour
{
    public IdleGame game;

    public Sprite[] researchIcons = new Sprite[7];
    public Sprite[] nextResearchIcons = new Sprite[6];
    public Text currentInfoText;
    public Text nextInfoText;

    public Image researchIcon;
    public Image nextResearchIcon;

    public BigDouble[] researchCosts;

    public void Start()
    {
        researchCosts = new BigDouble[] { 200, 1e3, 2e4, 3e4, 2e5, 2e6, 2e8 };
        Activate();
    }

    public void Run()
    {
        var data = game.data;
        #region ifs

        if (game.researchCanvas.gameObject.activeSelf)
        {
            if (data.isCompleted0)
            {
                researchIcon.sprite = researchIcons[0];
                nextResearchIcon.sprite = nextResearchIcons[0];
                currentInfoText.text = $"Wood Burner\nResearch Cost:{Methods.NotationMethod(researchCosts[0], "F2")} Power";
                nextInfoText.text = $"Coal Generator\nResearch Cost:{Methods.NotationMethod(researchCosts[1], "F2")} Power";
            }
            if (data.isCompleted1)
            {
                researchIcon.sprite = researchIcons[1];
                nextResearchIcon.sprite = nextResearchIcons[1];
                currentInfoText.text = $"Coal Generator\nResearch Cost:{Methods.NotationMethod(researchCosts[1], "F2")} Power";
                nextInfoText.text = $"Oil Generator\nResearch Cost:{Methods.NotationMethod(researchCosts[2], "F2")} Power";
            }
            if (data.isCompleted2)
            {
                researchIcon.sprite = researchIcons[2];
                nextResearchIcon.sprite = nextResearchIcons[2];
                currentInfoText.text = $"Oil Generator\nResearch Cost:{Methods.NotationMethod(researchCosts[2], "F2")} Power";
                nextInfoText.text = $"Natural Gas Generator\nResearch Cost:{Methods.NotationMethod(researchCosts[3], "F2")} Power";
            }
            if (data.isCompleted3)
            {
                researchIcon.sprite = researchIcons[3];
                nextResearchIcon.sprite = nextResearchIcons[3];
                currentInfoText.text = $"Natural Gas Generator\nResearch Cost:{Methods.NotationMethod(researchCosts[3], "F2")} Power";
                nextInfoText.text = $"Steam Turbine\nResearch Cost:{Methods.NotationMethod(researchCosts[4], "F2")} Power";
            }
            if (data.isCompleted4)
            {
                researchIcon.sprite = researchIcons[4];
                nextResearchIcon.sprite = nextResearchIcons[4];
                currentInfoText.text = $"Steam Turbine\nResearch Cost:{Methods.NotationMethod(researchCosts[4], "F2")} Power";
                nextInfoText.text = $"Nuclear Reactor\nResearch Cost:{Methods.NotationMethod(researchCosts[5], "F2")} Power";
            }
            if (data.isCompleted5)
            {
                researchIcon.sprite = researchIcons[5];
                nextResearchIcon.sprite = nextResearchIcons[5];
                currentInfoText.text = $"Nuclear Reactor\nResearch Cost:{Methods.NotationMethod(researchCosts[5], "F2")} Power";
                nextInfoText.text = $"Fusion Reactor\nResearch Cost:{Methods.NotationMethod(researchCosts[6], "F2")} Power";
            }
            if (data.isCompleted6)
            {
                researchIcon.sprite = researchIcons[6];
                nextResearchIcon.gameObject.SetActive(false);
                currentInfoText.text = $"Fusion Reactor\nResearch Cost:{Methods.NotationMethod(researchCosts[6], "F2")} Power";
                nextInfoText.text = $"No More Research";
            }
            if (data.isCompleted7)
            {
                researchIcon.gameObject.SetActive(false);
                nextResearchIcon.gameObject.SetActive(false);
                currentInfoText.text = $"No More Research GG";
                nextInfoText.text = $"No More Research";
            }
        }
        #endregion


    }

    public void Research()
    {
        var data = game.data;
        if (data.power <= researchCosts[data.researchIndex]) return;
        if(data.researchIndex <= 6)
            data.power -= researchCosts[data.researchIndex];

        switch(data.researchIndex)
        {
            case 0:
                data.isCompleted0 = false;
                data.isCompleted1 = true;
                break;
            case 1:
                data.isCompleted1 = false;
                data.isCompleted2 = true;
                break;
            case 2:
                data.isCompleted2 = false;
                data.isCompleted3 = true;
                break;
            case 3:
                data.isCompleted3 = false;
                data.isCompleted4 = true;
                break;
            case 4:
                data.isCompleted4 = false;
                data.isCompleted5 = true;
                break;
            case 5:
                data.isCompleted5 = false;
                data.isCompleted6 = true;
                break;
            case 6:
                data.isCompleted6 = false;
                data.isCompleted7 = true;
                break;
        }
        if (data.researchIndex <= 6)
            data.researchIndex++;
        else return;
    }

    public void Activate()
    {
        researchIcon.gameObject.SetActive(true);
        nextResearchIcon.gameObject.SetActive(true);
    }
}
