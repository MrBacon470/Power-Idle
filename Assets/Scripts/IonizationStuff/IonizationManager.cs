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

public class IonizationManager : MonoBehaviour
{
    public IdleGame game;

    public GameObject ionizationMenu;
    public Text ionizationText;
    public Text ionizationTitle;
    public GameObject ionizationButton;
    

    public void Update()
    {
        var data = game.data;

        if(data.hasMastered)
            ionizationMenu.gameObject.SetActive(true);
        else
            ionizationMenu.gameObject.SetActive(false);
        
        if(ionizationMenu.gameObject.activeSelf)
        {
            if(data.power < new BigDouble(1, 1000000) || data.transformers < new BigDouble(1, 100000) || data.superConductors < new BigDouble(1, 10000))
            {
                ionizationButton.gameObject.SetActive(false);
                ionizationText.text = $"More Material Required\n{Methods.NotationMethod(data.power, "F2")}/{Methods.NotationMethod(new BigDouble(1, 1000000),"F2")} Power\n{Methods.NotationMethod(data.transformers, "F2")}/{Methods.NotationMethod(new BigDouble(1, 100000),"F2")} Transformers\n{Methods.NotationMethod(data.superConductors, "F2")}/{Methods.NotationMethod(new BigDouble(1, 10000),"F2")} Super Conductors";
                ionizationTitle.text = "3?";
            }
            else if(data.power >= new BigDouble(1, 1000000) || data.transformers >= new BigDouble(1, 100000) || data.superConductors >= new BigDouble(1, 10000))
            {
                ionizationButton.gameObject.SetActive(true);
                ionizationText.text = $"The Peak of your production has been hit...\nThe universe can't contain you anymore...\nStrip every last electron to take whatever you can to the other side...";
                ionizationTitle.text = "Ionize";
            }
        }
    }

    public void Ionize()
    {
        var data = game.data;
        game.ChangeTabs("ABG");
        data.hasIonized = true;

        data.power = 10;
        data.transformers = 0;
        data.superConductors = 0;
        data.productionUpgrade1Level = data.productionUpgrade2Level = data.productionUpgrade3Level = data.productionUpgrade4Level = data.productionUpgrade5Level = data.productionUpgrade6Level
            = data.productionUpgrade7Level = data.productionUpgrade8Level = 0;

        data.isCompleted0 = true;
        data.isCompleted1 = false;
        data.isCompleted2 = false;
        data.isCompleted3 = false;
        data.isCompleted4 = false;
        data.isCompleted5 = false;
        data.isCompleted6 = false;
        data.isCompleted7 = false;

        data.isHyperCompleted0 = true;
        data.isHyperCompleted1 = false;
        data.isHyperCompleted2 = false;
        data.isHyperCompleted3 = false;
        data.isHyperCompleted4 = false;
        data.isHyperCompleted5 = false;
        data.isHyperCompleted6 = false;
        data.isHyperCompleted7 = false;
        data.isHyperCompleted8 = false;
        data.isHyperCompleted9 = false;

        data.infusionULevel1 = 0;
        data.infusionULevel2 = 0;
        data.infusionULevel3 = 0;

        data.sacraficeULevel1 = data.sacraficeULevel2 = data.sacraficeULevel3 = 0;

        data.researchIndex = 0;
        data.hyperIndex = 0;

        data.currentPollution = 0;

        game.broken.breakIndex = 8;
        game.broken.breakTimer = 0;

        data.isGen1Broken = false;
        data.isGen2Broken = false;
        data.isGen3Broken = false;
        data.isGen4Broken = false;
        data.isGen5Broken = false;
        data.isGen6Broken = false;
        data.isGen7Broken = false;
        data.isGen8Broken = false;

        data.isBHBUnlocked = false;
        data.hc1Level = 0;
        data.hc2Level = 0;
        data.hc3Level = 0;

        data.isAuto1On = false;
        data.isAuto2On = false;
        data.isAuto3On = false;

        data.array1Complete = false;
        data.array2Complete = false;
        data.array3Complete = false;

        
    }
}
