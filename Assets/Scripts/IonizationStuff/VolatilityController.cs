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

public class VolatilityController : MonoBehaviour
{
    public IdleGame game;
    public Button volatilityButton;
    [Header("Texts")]
    public Text volatilityText;
    [Header("String Arrays")]
    public string[] tiers;


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
            if(data.volatileIndex >= 23)
            {   
                volatilityText.text = $"Everything is Non Volatile";
                volatilityButton.interactable = false;
            }
            else if(data.volatileIndex < 23)
            {
                volatilityButton.interactable = true;
                volatilityText.text = data.volatileIndex > 11 ? $"<b>Make Next Ray Non Volatile</b>\n\n{tiers[data.volatileIndex]} => {tiers[data.volatileIndex + 1]}\n<b>Cost:{Methods.NotationMethod(1e24, "F2")}</b> {tiers[data.volatileIndex + 1]}" : 
                $"<b>Make Next Particle Non Volatile</b>\n\n{tiers[data.volatileIndex]} => {tiers[data.volatileIndex + 1]}\n<b>Cost:{Methods.NotationMethod(1e15, "F2")}</b> {tiers[data.volatileIndex + 1]}";
            }
    }

    public void Upgrade()
    {
        var data = game.data;

        switch(data.volatileIndex)
        {
            case 0:
                if(data.particleTotals[1] < 1e15) return;
                data.particleTotals[1] -= 1e15;
                data.volatileIndex++;
                break;
            case 1:
                if(data.particleTotals[2] < 1e15) return;
                data.particleTotals[2] -= 1e15;
                data.volatileIndex++;
                break;
            case 2:
                if(data.particleTotals[3] < 1e15) return;
                data.particleTotals[3] -= 1e15;
                data.volatileIndex++;
                break;
            case 3:
                if(data.particleTotals[4] < 1e15) return;
                data.particleTotals[4] -= 1e15;
                data.volatileIndex++;
                break;
            case 4:
                if(data.particleTotals[5] < 1e15) return;
                data.particleTotals[5] -= 1e15;
                data.volatileIndex++;
                break;
            case 5:
                if(data.particleTotals[6] < 1e15) return;
                data.particleTotals[6] -= 1e15;
                data.volatileIndex++;
                break;
            case 6:
                if(data.particleTotals[7] < 1e15) return;
                data.particleTotals[7] -= 1e15;
                data.volatileIndex++;
                break;
            case 7:
                if(data.particleTotals[8] < 1e15) return;
                data.particleTotals[8] -= 1e15;
                data.volatileIndex++;
                break;
            case 8:
                if(data.particleTotals[9] < 1e15) return;
                data.particleTotals[9] -= 1e15;
                data.volatileIndex++;
                break;
            case 9:
                if(data.particleTotals[10] < 1e15) return;
                data.particleTotals[10] -= 1e15;
                data.volatileIndex++;
                break;
            case 10:
                if(data.particleTotals[11] < 1e15) return;
                data.particleTotals[11] -= 1e15;
                data.volatileIndex++;
                break;
            case 11:
                if(data.rayTotals[0] < 1e15) return;
                data.rayTotals[0] -= 1e15;
                data.volatileIndex++;
                break;
            case 12:
                if(data.rayTotals[1] < 1e15) return;
                data.rayTotals[1] -= 1e15;
                data.volatileIndex++;
                break;
            case 13:
                if(data.rayTotals[2] < 1e15) return;
                data.rayTotals[2] -= 1e15;
                data.volatileIndex++;
                break;
            case 14:
                if(data.rayTotals[3] < 1e15) return;
                data.rayTotals[3] -= 1e15;
                data.volatileIndex++;
                break;
            case 15:
                if(data.rayTotals[4] < 1e15) return;
                data.rayTotals[4] -= 1e15;
                data.volatileIndex++;
                break;
            case 16:
                if(data.rayTotals[5] < 1e15) return;
                data.rayTotals[5] -= 1e15;
                data.volatileIndex++;
                break;
            case 17:
                if(data.rayTotals[6] < 1e15) return;
                data.rayTotals[6] -= 1e15;
                data.volatileIndex++;
                break;
            case 18:
                if(data.rayTotals[7] < 1e15) return;
                data.rayTotals[7] -= 1e15;
                data.volatileIndex++;
                break;
            case 19:
                if(data.rayTotals[8] < 1e15) return;
                data.rayTotals[8] -= 1e15;
                data.volatileIndex++;
                break;
            case 20:
                if(data.rayTotals[9] < 1e15) return;
                data.rayTotals[9] -= 1e15;
                data.volatileIndex++;
                break;
            case 21:
                if(data.rayTotals[10] < 1e15) return;
                data.rayTotals[10] -= 1e15;
                data.volatileIndex++;
                break;
            case 22:
                if(data.rayTotals[11] < 1e15) return;
                data.rayTotals[11] -= 1e15;
                data.volatileIndex++;
                break;
        }
    }
}
