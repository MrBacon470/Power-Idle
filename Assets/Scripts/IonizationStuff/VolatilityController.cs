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

    public static VolatilityController Instance;

    public void StartVolatility()
    {
        tiers = new string[]{"<color=#797979>α</color>", "<color=#825454>β</color>", "<color=#A8AB58>γ</color>"," <color=#77C854>δ</color>", "<color=#63F128>ε</color>", "<color=#28F1D3>ζ</color>", "<color=#28C9F1>η</color>",
        "<color=#2848F1>θ</color>", "<color=#A728F1>ι</color>", "<color=#F128E4>κ</color>", "<color=#F12895>λ</color>", "<color=#F1282F>μ</color>", "<color=#F19028>ν</color>", "<color=#F1F128>ξ</color>", "<color=#81F128>ο</color>",
        "<color=#00C814>π</color>", "<color=#00B9C8>ρ</color>", "<color=#0088C8>σ</color>", "<color=#0500C8>τ</color>", "<color=#8E00CD>υ</color>", "<color=#FF00CD>φ</color>", "<color=#FF007F>χ</color>", "<color=#FF0015>ψ</color>",
        "<color=#FFFFFF>ω</color>"};
    }

    public void Run()
    {
        var data = game.data;
        for(int i = 0; i < tiers.Length; i++)
        {
            if(data.volatileIndex >= 23)
            {   
                volatilityText.text = $"Everything is Non Volatile";
                volatilityButton.interactable = false;
            }
            else if(data.volatileIndex < 23)
            {
                volatilityButton.interactable = true;
                volatilityText.text = data.volatileIndex > 11 ? $"<b>Make Next Ray Non Volatile</b>\n\n{tiers[i]} => {tiers[i + 1]}\n<b>Cost:{Methods.NotationMethod(1e24, "F2")}</b> {tiers[i + 1]}" : $"Make Next Particle Non Volatile\n\n{tiers[i]} => {tiers[i + 1]}\nCost:{Methods.NotationMethod(1e24, "F2")} {tiers[i + 1]}";
            }
        }
    }

    public void Upgrade()
    {
        var data = game.data;

        switch(data.volatileIndex)
        {
            case 0:
                if(data.Beta < 1e24) return;
                data.Beta -= 1e24;
                data.volatileIndex++;
                break;
            case 1:
                if(data.Gamma < 1e24) return;
                data.Gamma -= 1e24;
                data.volatileIndex++;
                break;
            case 2:
                if(data.Delta < 1e24) return;
                data.Delta -= 1e24;
                data.volatileIndex++;
                break;
            case 3:
                if(data.Epsilon < 1e24) return;
                data.Epsilon -= 1e24;
                data.volatileIndex++;
                break;
            case 4:
                if(data.Zeta < 1e24) return;
                data.Zeta -= 1e24;
                data.volatileIndex++;
                break;
            case 5:
                if(data.Eta < 1e24) return;
                data.Eta -= 1e24;
                data.volatileIndex++;
                break;
            case 6:
                if(data.Theta < 1e24) return;
                data.Theta -= 1e24;
                data.volatileIndex++;
                break;
            case 7:
                if(data.Iota < 1e24) return;
                data.Iota -= 1e24;
                data.volatileIndex++;
                break;
            case 8:
                if(data.Kappa < 1e24) return;
                data.Kappa -= 1e24;
                data.volatileIndex++;
                break;
            case 9:
                if(data.Lambda < 1e24) return;
                data.Lambda -= 1e24;
                data.volatileIndex++;
                break;
            case 10:
                if(data.Mu < 1e24) return;
                data.Mu -= 1e24;
                data.volatileIndex++;
                break;
            case 11:
                if(data.Nu < 1e24) return;
                data.Nu -= 1e24;
                data.volatileIndex++;
                break;
            case 12:
                if(data.Xi < 1e24) return;
                data.Xi -= 1e24;
                data.volatileIndex++;
                break;
            case 13:
                if(data.Omicron < 1e24) return;
                data.Omicron -= 1e24;
                data.volatileIndex++;
                break;
            case 14:
                if(data.Pi < 1e24) return;
                data.Pi -= 1e24;
                data.volatileIndex++;
                break;
            case 15:
                if(data.Rho < 1e24) return;
                data.Rho -= 1e24;
                data.volatileIndex++;
                break;
            case 16:
                if(data.Sigma < 1e24) return;
                data.Sigma -= 1e24;
                data.volatileIndex++;
                break;
            case 17:
                if(data.Tau < 1e24) return;
                data.Tau -= 1e24;
                data.volatileIndex++;
                break;
            case 18:
                if(data.Upsilon < 1e24) return;
                data.Upsilon -= 1e24;
                data.volatileIndex++;
                break;
            case 19:
                if(data.Phi < 1e24) return;
                data.Phi -= 1e24;
                data.volatileIndex++;
                break;
            case 20:
                if(data.Chi < 1e24) return;
                data.Chi -= 1e24;
                data.volatileIndex++;
                break;
            case 21:
                if(data.Psi < 1e24) return;
                data.Psi -= 1e24;
                data.volatileIndex++;
                break;
            case 22:
                if(data.Omega < 1e24) return;
                data.Omega -= 1e24;
                data.volatileIndex++;
                break;
        }
    }
}
