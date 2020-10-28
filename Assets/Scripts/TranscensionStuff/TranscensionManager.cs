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

public class TranscensionManager : MonoBehaviour
{
    public IdleGame game;

    public GameObject transcendMenu;

    public Text shardsText;
    public Text shardsBoostText;

    public Canvas mainCanvas;
    public Canvas corruptionCanvas;
    public Canvas tomeCanvas;

    public void Start()
    {
        var data = game.data;
    }

    public void Update()
    {
        var data = game.data;

        if (data.power >= 1.79e308 && data.bytes >= 1.79e308 && data.transformers >= 1.79e308 && data.superConductors >= 1.79e308)
            transcendMenu.gameObject.SetActive(true);
        else
            transcendMenu.gameObject.SetActive(false);
    }

    public void Transcend()
    {
        var data = game.data;
        if (data.power < 1.79e308 || data.bytes < 1.79e308 || data.transformers < 1.79e308 || data.superConductors < 1.79e308) return;

        
    }

    public void ChangeTabs(string id)
    {
        DisableAll();
        switch (id)
        {
            case "main":
                mainCanvas.gameObject.SetActive(true);
                break;
            case "corruption":
                corruptionCanvas.gameObject.SetActive(true);
                break;
            case "tomes":
                tomeCanvas.gameObject.SetActive(true);
                break;
        }
    }

    void DisableAll()
    {
        mainCanvas.gameObject.SetActive(false);
        corruptionCanvas.gameObject.SetActive(false);
        tomeCanvas.gameObject.SetActive(false);
    }
}
