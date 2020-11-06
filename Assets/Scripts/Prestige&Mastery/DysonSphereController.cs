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

public class DysonSphereController : MonoBehaviour
{
    public IdleGame game;

    public Text powerPerSecText;
    public Text arrayStatus1Text;
    public Text arrayStatus2Text;
    public Text arrayStatus3Text;

    public Text array1Text;
    public Text array2Text;
    public Text array3Text;

    public GameObject array2Button;
    public GameObject array3Button;

    public void Run()
    {
        var data = game.data;

        if (!game.sphereCanvas) return;

        powerPerSecText.text = $"{Methods.NotationMethod(SpherePowerPerSec(), "F2")} Power Per Second";

        arrayStatus1Text.text = data.array1Complete ? $"Array 1: <color=#00FF02>Online</color>" : "Array 1: <color=#FF0100>Offline</color>";
        arrayStatus2Text.text = data.array2Complete ? $"Array 2: <color=#00FF02>Online</color>" : "Array 2: <color=#FF0100>Offline</color>";
        arrayStatus3Text.text = data.array3Complete ? $"Array 3: <color=#00FF02>Online</color>" : "Array 3: <color=#FF0100>Offline</color>";

        if (data.array1Complete)
            array2Button.gameObject.SetActive(true);
        else
            array2Button.gameObject.SetActive(false);

        if (data.array2Complete)
            array3Button.gameObject.SetActive(true);
        else
            array3Button.gameObject.SetActive(false);

        array1Text.text = data.array1Complete ? "Constructed" : $"Array 1 Cost: {Methods.NotationMethod(1e100, "F2")} Power";
        array2Text.text = data.array2Complete ? "Constructed" : $"Array 1 Cost: {Methods.NotationMethod(1e75, "F2")} Transformers";
        array3Text.text = data.array3Complete ? "Constructed" : $"Array 1 Cost: {Methods.NotationMethod(1e50, "F2")} Super Conductors";
    }

    public void BuildArray1()
    {
        var data = game.data;

        if (data.array1Complete) return;
        if (data.power < 1e10) return;

        data.power -= 1e10;
        data.array1Complete = true;
    }

    public void BuildArray2()
    {
        var data = game.data;

        if (data.array2Complete) return;
        if (data.transformers < 1e10) return;

        data.transformers -= 1e10;
        data.array2Complete = true;
    }

    public void BuildArray3()
    {
        var data = game.data;

        if (data.array3Complete) return;
        if (data.superConductors < 1e10) return;

        data.superConductors -= 1e10;
        data.array3Complete = true;
    }

    public BigDouble SpherePowerPerSec()
    {
        var data = game.data;
        BigDouble temp = 0;
        if (data.array1Complete)
            temp += 1e30;
        if (data.array2Complete)
            temp += 1e50;
        if (data.array3Complete)
            temp += 1e70;
        return temp;
    }
}
