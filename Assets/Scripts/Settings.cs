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

public class Settings : MonoBehaviour
{
    public IdleGame game;
    public Text notationTypeText;
    /*
     * Notation Key:
     * 0 = Sci
     * 1 = Eng
     * 2 = Letter
     * 3 = Word
     */

    private void Start()
    {
        UpdateNotationText();
    }

    private void UpdateNotationText()
    {
        var note = game.data.notationType;
        switch (note)
        {
            case 0:
                notationTypeText.text = "Notation:Scientific";
                break;
            case 1:
                notationTypeText.text = "Notation:Engineering";
                break;
            case 2:
                notationTypeText.text = "Notation:Word";
                break;
        }
    }

    public void ChangeNotation()
    {
        var note = game.data.notationType;
        if (note == 2) 
            note = -1;
        note++;
        /* switch (note)
        {
            case 0:
                note = 1;
                break;
            case 1:
                note = 2;
                break;
            case 2:
                note = 0;
                break;
        } */
        game.data.notationType = note;
        Methods.NotationSettings = note;
        UpdateNotationText();
    }
}