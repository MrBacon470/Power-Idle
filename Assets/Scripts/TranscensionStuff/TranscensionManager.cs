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
    [Header("Scripts")]
    public IdleGame game;
    public TomeManager tome;
    public CorruptionController corruption;
    public TrancensionAchievementManager achievement;
    [Header("UI Stuff")]
    public GameObject transcendMenu;

    public Text shardsText;
    public Text transcendText;

    public Canvas mainCanvas;
    public Canvas corruptionCanvas;
    public Canvas tomeCanvas;

    public GameObject transcensionButton;

    public BigDouble realityShardsToGet => Sqrt(game.data.power + game.data.transformers + game.data.bytes + game.data.superConductors / Log10(1e308));

    [Header("Achievements")]
    public GameObject[] achievements = new GameObject[7];


    public void Start()
    {
        var data = game.data;
        tome.StartTomes();
        corruption.StartCorruption();
    }

    public void Update()
    {
        var data = game.data;
        tome.Run();
        corruption.Run();
        achievement.Run();

        

        if (data.power >= 1.79e308 && data.bytes >= 1.79e308 && data.transformers >= 1.79e308 && data.superConductors >= 1.79e308)
            transcendMenu.gameObject.SetActive(true);
        else
            transcendMenu.gameObject.SetActive(false);

        if (!data.hasTranscended)
            transcensionButton.gameObject.SetActive(false);
        else
            transcensionButton.gameObject.SetActive(true);

        if (game.transcensionCanvas.gameObject.activeSelf)
        {
            shardsText.text = $"{Methods.NotationMethod(data.realityShards, "F0")} Reality Shards";
        }

        if(transcendMenu.gameObject.activeSelf)
        {
            transcendText.text = $"Transcend +{Methods.NotationMethod(realityShardsToGet, "F0")} Reality Shards";
        }

        if(game.achievementCanvas.gameObject.activeSelf)
        {
            if(data.hasTranscended)
            {
                achievements[0].gameObject.SetActive(true);
                achievements[1].gameObject.SetActive(true);
                achievements[2].gameObject.SetActive(true);
                achievements[3].gameObject.SetActive(true);
                achievements[4].gameObject.SetActive(true);
                achievements[5].gameObject.SetActive(true);
                achievements[6].gameObject.SetActive(true);
            }
            else
            {
                achievements[0].gameObject.SetActive(false);
                achievements[1].gameObject.SetActive(false);
                achievements[2].gameObject.SetActive(false);
                achievements[3].gameObject.SetActive(false);
                achievements[4].gameObject.SetActive(false);
                achievements[5].gameObject.SetActive(false);
                achievements[6].gameObject.SetActive(false);
            }
        }
    }

    public void Transcend()
    {
        var data = game.data;
        if (data.power < 1.79e308 || data.bytes < 1.79e308 || data.transformers < 1.79e308 || data.superConductors < 1.79e308) return;

        data.realityShards += realityShardsToGet;

        data.power = 10;
        data.bytes = 0;
        data.transformers = 0;
        data.superConductors = 0;
        data.kuakaCoin = 0;
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
