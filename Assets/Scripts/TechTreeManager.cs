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

public class TechTreeManager : MonoBehaviour
{
    public IdleGame game;
    [Header("Sprites")]
    public Sprite lockedSprite;
    public Sprite unlockedSprite;
    public Sprite maxedSprite;
    [Header("Objects")]
    public Image[] techTreeIcons;
    public Text[] techTreeText;
    public string[] techTreeDesc;
    [Header("Numbers")]
    public BigDouble[] techTreeLevels;
    public BigDouble[] techTreeMaxLevels;
    public BigDouble[] techTreeCosts;
    public BigDouble[] techTreeCostMults;

    public void StartTechTree()
    {
        techTreeIcons = new Image[6];
        techTreeDesc = new string[] {$"3x Power/s and Bytes/s Cost:{Methods.NotationMethod(techTreeCosts[0],"F0")} Power\nLevel {Methods.NotationMethod(techTreeLevels[0],"F0")}/{Methods.NotationMethod(techTreeMaxLevels[0],"F0")}"
            ,$"Boost Infusions by 1.5x Cost:{Methods.NotationMethod(techTreeCosts[1], "F0")} Transformers\nLevel {Methods.NotationMethod(techTreeLevels[1],"F0")}/{Methods.NotationMethod(techTreeMaxLevels[1],"F0")}"
            ,$"" };
        techTreeText = new Text[6];

        techTreeLevels = new BigDouble[6];
        techTreeMaxLevels = new BigDouble[] { 20, 10, 5, 1 };
        techTreeCosts = new BigDouble[6];
        techTreeCostMults = new BigDouble[] { 10, 5, 2.5, 1.5 };
    }
}
