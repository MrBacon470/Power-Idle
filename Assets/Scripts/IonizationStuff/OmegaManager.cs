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

public class OmegaManager : MonoBehaviour
{
    public IdleGame game;

    public Text overclockingText;
    public Text injectorText;
    public string[] tiers;
    private BigDouble overclockingCost => 1e3 * Pow(1.25, game.data.omegaUpgrade1Level);
    private BigDouble injectorCost => 1e6 * Pow(1.25, game.data.omegaUpgrade2Level);

    public void Start()
    {
        tiers = new string[]{"<color=#797979>α</color>", "<color=#825454>β</color>", "<color=#A8AB58>γ</color>"," <color=#77C854>δ</color>", "<color=#63F128>ε</color>", "<color=#28F1D3>ζ</color>", "<color=#28C9F1>η</color>",
        "<color=#2848F1>θ</color>", "<color=#A728F1>ι</color>", "<color=#F128E4>κ</color>", "<color=#F12895>λ</color>", "<color=#F1282F>μ</color>", "<color=#F19028>ν</color>", "<color=#F1F128>ξ</color>", "<color=#81F128>ο</color>",
        "<color=#00C814>π</color>", "<color=#00B9C8>ρ</color>", "<color=#0088C8>σ</color>", "<color=#0500C8>τ</color>", "<color=#8E00CD>υ</color>", "<color=#FF00CD>φ</color>", "<color=#FF007F>χ</color>", "<color=#FF0015>ψ</color>",
        "<color=#FF6500>ω</color>"};
    }

    public void Update()
    {
        var data = game.data;

        overclockingText.text = data.omegaUpgrade1Level <= 0 ? $"Overclock Generator Production by 225%\nCost:{Methods.NotationMethod(overclockingCost, "F2")} {tiers[23]}" 
        : $"Overclocking Generators by {Methods.NotationMethod(225 * data.omegaUpgrade1Level, "F2")}%\nCost:{Methods.NotationMethod(overclockingCost, "F2")} {tiers[23]}";

        injectorText.text = data.omegaUpgrade2Level <= 0 ? $"Inject {tiers[23]} into Accelerator Coils\nBoost by 115%\nCost:{Methods.NotationMethod(injectorCost, "F2")} {tiers[23]}" 
        : $"Injecting {tiers[23]} into Accelerator Coils\nBoosting by {Methods.NotationMethod(115 * data.omegaUpgrade2Level, "F2")}%\nCost:{Methods.NotationMethod(injectorCost, "F2")} {tiers[23]}";
    }

    public void BuyUpgrade(int index)
    {
        var data = game.data;
        switch(index)
        {
            case 0:
                if(data.rayTotals[23] < overclockingCost) return;
                data.rayTotals[23] -= overclockingCost;
                data.omegaUpgrade1Level++;
                break;
            case 1:
                if(data.rayTotals[23] < injectorCost) return;
                data.rayTotals[23] -= injectorCost;
                data.omegaUpgrade2Level++;
                break;
        }
    }
}
