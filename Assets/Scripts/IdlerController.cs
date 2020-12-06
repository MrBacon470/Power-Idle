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

public class IdlerController : MonoBehaviour
{
    public IdleGame game;

    public Text transformerIdleText;
    public Text superConductorIdleText;

    public void Run()
    {
        var data = game.data;

        transformerIdleText.text = !data.isIdler1Unlocked ? $"Produce Prestige Reward/s\nCost:{Methods.NotationMethod(1e100, "F2")} Transformers" : "Prestige Idler Online";
        superConductorIdleText.text = !data.isIdler2Unlocked ? $"Produce Mastery Reward/s\nCost:{Methods.NotationMethod(1e100, "F2")} Super Conductors" : "Mastery Idler Online";

        if(data.isIdler1Unlocked)
            data.transformers += (Pow(10, (Pow(Log10(game.data.power+1), .95))) + (Pow(10, (Pow(Log10(game.data.power+1), .95))) * (0.25 * game.data.infusionULevel3)) * 0.00001) * Time.deltaTime;

        if(data.isIdler2Unlocked)
            data.superConductors += (150 * Sqrt(data.power / 1e154) + (150 * Sqrt(data.power / 1e154) * (.25 * data.sacraficeULevel2)) * 0.00001) * Time.deltaTime;
    }

    public void BuyIdler(int index)
    {
        var data = game.data;

        switch(index)
        {
            case 0:
            if(data.isIdler1Unlocked) return;
            if(data.transformers < 1e100) return;
            data.transformers -= 1e100;
            data.isIdler1Unlocked = true;
            break;

            case 1:
            if(data.isIdler2Unlocked) return;
            if(data.superConductors < 1e100) return;
            data.superConductors -= 1e100;
            data.isIdler2Unlocked = true;
            break;
        }
    }
}
