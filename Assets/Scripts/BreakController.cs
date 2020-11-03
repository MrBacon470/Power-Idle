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
using System;
using static BreakInfinity.BigDouble;

public class BreakController : MonoBehaviour
{
    public IdleGame game;

    public Sprite[] generatorIcons = new Sprite[9];

    public Image breakIcon;

    public Text breakInfoText;

    public int breakIndex = 8;

    public float breakTimer;

    public void StartBreak()
    {
        var data = game.data;
        if (data.isGen1Broken)
            breakIndex = 0;
        if (data.isGen2Broken)
            breakIndex = 1;
        if (data.isGen3Broken)
            breakIndex = 2;
        if (data.isGen4Broken)
            breakIndex = 3;
        if (data.isGen5Broken)
            breakIndex = 4;
        if (data.isGen6Broken)
            breakIndex = 5;
        if (data.isGen7Broken)
            breakIndex = 6;
        if (data.isGen8Broken)
            breakIndex = 7;

        if (!data.isGen1Broken && !data.isGen2Broken && !data.isGen3Broken && !data.isGen4Broken && !data.isGen5Broken && !data.isGen6Broken && !data.isGen7Broken && !data.isGen8Broken)
            breakIndex = 8;

        breakTimer = 0;
    }

    public void Run()
    {
        Break();
    }

    public void Break()
    {
        var data = game.data;

        System.Random rnd = new System.Random();

        if(breakIndex == 8)
            UpdateUI(breakIndex);

        if (breakIndex == 8)
            breakTimer += Time.deltaTime;
        if (breakTimer >= 120 && breakIndex == 8)
        {
            breakTimer = 0;
            breakIndex = rnd.Next(0, 7);
        }

        if(breakIndex < 8)
            switch(breakIndex)
            {
                case 0:
                    data.isGen1Broken = true;
                    UpdateUI(breakIndex);
                    break;
                case 1:
                    data.isGen2Broken = true;
                    UpdateUI(breakIndex);
                    break;
                case 2:
                    data.isGen3Broken = true;
                    UpdateUI(breakIndex);
                    break;
                case 3:
                    data.isGen4Broken = true;
                    UpdateUI(breakIndex);
                    break;
                case 4:
                    data.isGen5Broken = true;
                    UpdateUI(breakIndex);
                    break;
                case 5:
                    data.isGen6Broken = true;
                    UpdateUI(breakIndex);
                    break;
                case 6:
                    data.isGen7Broken = true;
                    UpdateUI(breakIndex);
                    break;
                case 7:
                    data.isGen8Broken = true;
                    UpdateUI(breakIndex);
                    break;
            }

    }

    public void Repair()
    {
        var data = game.data;
        if(breakIndex < 8)
            switch(breakIndex)
            {
                case 0:
                    if (data.power < 50) return;
                    data.power -= 50;
                    data.isGen1Broken = false;
                    breakIndex = 8;
                    breakTimer = 0;
                    break;
                case 1:
                    if (data.power < 500) return;
                    data.power -= 500;
                    data.isGen2Broken = false;
                    breakIndex = 8;
                    breakTimer = 0;
                    break;
                case 2:
                    if (data.power < 5e3) return;
                    data.power -= 5e3;
                    data.isGen3Broken = false;
                    breakIndex = 8;
                    breakTimer = 0;
                    break;
                case 3:
                    if (data.power < 5e4) return;
                    data.power -= 5e4;
                    data.isGen4Broken = false;
                    breakIndex = 8;
                    breakTimer = 0;
                    break;
                case 4:
                    if (data.power < 5.5e4) return;
                    data.power -= 5.5e4;
                    data.isGen5Broken = false;
                    breakIndex = 8;
                    breakTimer = 0;
                    break;
                case 5:
                    if (data.power < 5e5) return;
                    data.power -= 5e5;
                    data.isGen6Broken = false;
                    breakIndex = 8;
                    breakTimer = 0;
                    break;
                case 6:
                    if (data.power < 5e7) return;
                    data.power -= 5e7;
                    data.isGen7Broken = false;
                    breakIndex = 8;
                    breakTimer = 0;
                    break;
                case 7:
                    if (data.power < 5e10) return;
                    data.power -= 5e10;
                    data.isGen8Broken = false;
                    breakIndex = 8;
                    breakTimer = 0;
                    break;
            }
    }

    public void UpdateUI(int index)
    {
        switch(index)
        {
            case 0:
                breakIcon.sprite = generatorIcons[index];
                breakInfoText.text = $"Manual Generator Broken\nCost:{Methods.NotationMethod(50, "F0")} Power";
                break;
            case 1:
                breakIcon.sprite = generatorIcons[index];
                breakInfoText.text = $"Wood Burner Broken\nCost:{Methods.NotationMethod(500, "F0")} Power";
                break;
            case 2:
                breakIcon.sprite = generatorIcons[index];
                breakInfoText.text = $"Coal Generator Broken\nCost:{Methods.NotationMethod(5e3, "F0")} Power";
                break;
            case 3:
                breakIcon.sprite = generatorIcons[index];
                breakInfoText.text = $"Oil Generator Broken\nCost:{Methods.NotationMethod(5e4, "F0")} Power";
                break;
            case 4:
                breakIcon.sprite = generatorIcons[index];
                breakInfoText.text = $"Natural Gas Generator Broken\nCost:{Methods.NotationMethod(5.5e4, "F0")} Power";
                break;
            case 5:
                breakIcon.sprite = generatorIcons[index];
                breakInfoText.text = $"Steam Turbine Broken\nCost:{Methods.NotationMethod(5e5, "F0")} Power";
                break;
            case 6:
                breakIcon.sprite = generatorIcons[index];
                breakInfoText.text = $"Nuclear Reactor Broken\nCost:{Methods.NotationMethod(5e7, "F0")} Power";
                break;
            case 7:
                breakIcon.sprite = generatorIcons[index];
                breakInfoText.text = $"Fusion Reactor Broken\nCost:{Methods.NotationMethod(5e10, "F0")} Power";
                break;
            case 8:
                breakIcon.sprite = generatorIcons[index];
                breakInfoText.text = $"Nothing Is Broken Check Back Later";
                break;
        }
            
    }
}
