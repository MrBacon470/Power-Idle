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
using UnityEngine.Analytics;
using BreakInfinity;
using static BreakInfinity.BigDouble;
using System;

public class ParticlesController : MonoBehaviour
{
    public IdleGame game;

    [Header("Buttons")]
    public GameObject raysButton;
    public GameObject DeltaButton;
    public GameObject EtaButton;
    public GameObject KappaButton;
    [Header("Texts")]
    public Text[] particleAmountTexts = new Text[12];
    public String[] particleAmountDesc;
    [Header("Objects")]
    public GameObject[] particleObjects = new GameObject[11];


    public void StartParticles()
    {
        var data = game.data;
        particleAmountDesc = new String[]{$"<color=#666666>α</color> {Methods.NotationMethod(data.Alpha,"F2")}", $"<color=#825454>β</color> {Methods.NotationMethod(data.Beta,"F2")}", $"<color=#A8AB58>γ</color> {Methods.NotationMethod(data.Gamma,"F2")}",
        $"<color=#77C854>δ</color> {Methods.NotationMethod(data.Delta,"F2")}", $"<color=#63F128>ε</color> {Methods.NotationMethod(data.Epsilon,"F2")}", $"<color=#28F1D3>ζ</color> {Methods.NotationMethod(data.Zeta,"F2")}", 
        $"<color=#28C9F1>η</color> {Methods.NotationMethod(data.Eta,"F2")}", $"<color=#2848F1>θ</color> {Methods.NotationMethod(data.Theta,"F2")}", $"<color=#A728F1>ι</color> {Methods.NotationMethod(data.Iota, "F2")}",
        $"<color=#F128E4>κ</color> {Methods.NotationMethod(data.Kappa, "F2")}", $"<color=#F12895>λ</color> {Methods.NotationMethod(data.Lambda,"F2")}", $"<color=#F1282F>μ</color> {Methods.NotationMethod(data.Mu,"F2")}"};
    }

    public void Run()
    {
        var data = game.data;

        particleAmountDesc = new String[]{$"<color=#666666>α</color> {Methods.NotationMethod(data.Alpha,"F2")}", $"<color=#825454>β</color> {Methods.NotationMethod(data.Beta,"F2")}", $"<color=#A8AB58>γ</color> {Methods.NotationMethod(data.Gamma,"F2")}",
        $"<color=#77C854>δ</color> {Methods.NotationMethod(data.Delta,"F2")}", $"<color=#63F128>ε</color> {Methods.NotationMethod(data.Epsilon,"F2")}", $"<color=#28F1D3>ζ</color> {Methods.NotationMethod(data.Zeta,"F2")}", 
        $"<color=#28C9F1>η</color> {Methods.NotationMethod(data.Eta,"F2")}", $"<color=#2848F1>θ</color> {Methods.NotationMethod(data.Theta,"F2")}", $"<color=#A728F1>ι</color> {Methods.NotationMethod(data.Iota, "F2")}",
        $"<color=#F128E4>κ</color> {Methods.NotationMethod(data.Kappa, "F2")}", $"<color=#F12895>λ</color> {Methods.NotationMethod(data.Lambda,"F2")}", $"<color=#F1282F>μ</color> {Methods.NotationMethod(data.Mu,"F2")}"};

        for(int i = 0; i < particleAmountDesc.Length; i++)
        {
            particleAmountTexts[i].text = particleAmountDesc[i];
        }

    }
}
