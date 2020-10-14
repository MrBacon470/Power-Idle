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
using BreakInfinity;
using static BreakInfinity.BigDouble;
using UnityEngine.UI;

public class PollutionManager : MonoBehaviour
{
    public IdleGame game;

    public Text pollutionText;

    public BigDouble[] pollutionAmount;
    public BigDouble totalPollution = 409.8e6;
    
    public float tickTimer;

    private void Start()
    {
        pollutionAmount = new BigDouble[] { 10, 50, 100, 1e3, 5e3 };
    }

    private void Update()
    {
        var data = game.data;

        pollutionText.text = $"Total Pollution: {Methods.NotationMethod((data.currentPollution / totalPollution) * 100, "F2")}%\n+{Methods.NotationMethod((pollutionAmount[0] * data.productionUpgrade2Level) + (pollutionAmount[1] * data.productionUpgrade3Level) + (pollutionAmount[2] * data.productionUpgrade4Level) + (pollutionAmount[3] * data.productionUpgrade5Level) + (pollutionAmount[4] * data.productionUpgrade7Level), "F2")} Pollution Per Tick";

        if(tickTimer >= 5)
            data.currentPollution += (pollutionAmount[0] * data.productionUpgrade2Level) + (pollutionAmount[1] * data.productionUpgrade3Level) + (pollutionAmount[2] * data.productionUpgrade4Level) + (pollutionAmount[3] * data.productionUpgrade5Level) + (pollutionAmount[4] * data.productionUpgrade7Level);


        if (tickTimer <= 5)
            tickTimer *= Time.deltaTime;
        else
            tickTimer = 0;
    }
}
