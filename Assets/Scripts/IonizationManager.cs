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

public class IonizationManager : MonoBehaviour
{
    public IdleGame game;

    public GameObject ionizationMenu;
    [Header("Buttons")]
    public GameObject raysButton;
    public GameObject DeltaButton;
    public GameObject EtaButton;
    public GameObject KappaButton;

    public void Run()
    {
        var data = game.data;

        if(data.power >= 1.7e308 && !data.hasIonized)
            ionizationMenu.gameObject.SetActive(true);
        else
            ionizationMenu.gameObject.SetActive(false);
    }
}
