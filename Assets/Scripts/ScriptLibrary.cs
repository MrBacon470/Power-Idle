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

public class ScriptLibrary : MonoBehaviour
{
    public IdleGame game;

    public string[] scriptDesc;
    public string[] activationCostDesc;
    public Text[] scriptDescText = new Text[6];                                                    
    public Text[] activationCostText = new Text[6];
    public BigDouble[] activationCost;

    public void StartLibrary()
    {
        scriptDesc = new string[] { "Console.js", "Console.c++", "Console.html", "Console.css", "Console.py", "Console.cs" };
        activationCostDesc = new string[] { $"Activation Cost:Free", $"Activation Cost:100 Bytes", $"Activation Cost:{Methods.NotationMethod(1e3, "F2")} Bytes", $"Activation Cost:{Methods.NotationMethod(1e4, "F2")} Bytes", $"Activation Cost:{Methods.NotationMethod(1e5, "F2")} Bytes", $"Activation Cost:{Methods.NotationMethod(1e6, "F2")} Bytes" };
        activationCost = new BigDouble[] { 0, 100, 1e3, 1e4, 1e5, 1e6};
    }

    public void Run()
    {
        var data = game.data;
        #region ifs
        if (data.isScript1Selected)
        {
            data.scriptIndex = 0;
            data.isScript2Selected = false;
            data.isScript3Selected = false;
            data.isScript4Selected = false;
            data.isScript5Selected = false;
            data.isScript6Selected = false;
        }

        if (data.isScript2Selected)
        {
            data.scriptIndex = 1;
            data.isScript1Selected = false;
            data.isScript3Selected = false;
            data.isScript4Selected = false;
            data.isScript5Selected = false;
            data.isScript6Selected = false;
        }

        if (data.isScript3Selected)
        {
            data.scriptIndex = 2;
            data.isScript1Selected = false;
            data.isScript2Selected = false;
            data.isScript4Selected = false;
            data.isScript5Selected = false;
            data.isScript6Selected = false;
        }

        if (data.isScript4Selected)
        {
            data.scriptIndex = 3;
            data.isScript1Selected = false;
            data.isScript2Selected = false;
            data.isScript3Selected = false;
            data.isScript5Selected = false;
            data.isScript6Selected = false;
        }

        if (data.isScript5Selected)
        {
            data.scriptIndex = 4;
            data.isScript1Selected = false;
            data.isScript2Selected = false;
            data.isScript3Selected = false;
            data.isScript4Selected = false;
            data.isScript6Selected = false;
        }

        if (data.isScript6Selected)
        {
            data.scriptIndex = 5;
            data.isScript1Selected = false;
            data.isScript2Selected = false;
            data.isScript3Selected = false;
            data.isScript4Selected = false;
            data.isScript5Selected = false;
        }
        #endregion



        if (game.scriptLibraryCanvas.gameObject.activeSelf)
        {
            for (var i = 0; i < 6; i++)
            {
                scriptDescText[i].text = scriptDesc[i];
                activationCostText[i].text = activationCostDesc[i];
            }
        }
    }

    public void ActivateScript(int index)
    {
        var data = game.data;
        if (data.bytes >= activationCost[index])
        {
            data.bytes -= activationCost[index];

            if (index == 0)
                data.isScript1Selected = true;
            if (index == 1)
                data.isScript2Selected = true;
            if (index == 2)
                data.isScript3Selected = true;
            if (index == 3)
                data.isScript4Selected = true;
            if (index == 4)
                data.isScript5Selected = true;
            if (index == 5)
                data.isScript6Selected = true;
        }
    }
}
