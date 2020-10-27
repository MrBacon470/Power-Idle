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

using UnityEngine;
using System;
using UnityEngine.UI;
using BreakInfinity;
using System.Threading.Tasks;

public class OfflineManager : MonoBehaviour
{
    
    public IdleGame game;
    public DailyRewardManager daily;
    public GameObject offlinePopUp;
    public Text timeAwayText;
    public Text GainText;
    public Text bytesGainedText;

    public DateTime currentTime;

    public async void LoadOfflineProduction()
    {
        var data = game.data;
        if(data.offlineProgressCheck)
        {
            var tempOfflineTime = Convert.ToInt64(PlayerPrefs.GetString("OfflineTime"));
            var oldTime = DateTime.FromBinary(tempOfflineTime);
            var currentTime = await AwaitGetUTCTIme();
            var difference = currentTime.Subtract(oldTime);
            var rawTime = (float)difference.TotalSeconds;
            var offlineTime = rawTime / 10;

            offlinePopUp.gameObject.SetActive(true);
            TimeSpan timer = TimeSpan.FromSeconds(rawTime);
            timeAwayText.text = $"You were away for\n<color=#FF0000>{timer:dd\\:hh\\:mm\\:ss}</color>";
            BigDouble powerGains = game.TotalPowerPerSecond() <= 0 ? 0 : game.TotalPowerPerSecond() * offlineTime;
            BigDouble bytesGains = game.console.totalBytesPerSecond() <= 0 ? 0 : game.console.totalBytesPerSecond() * offlineTime;
            if(powerGains < 0 || bytesGains < 0)
            {
                GainText.text = $"You Earned:\n<color=#E7D600>+0.00 (Error) Power</color>";
                bytesGainedText.text = data.isConsoleOn ? $"You Earned:\n<color=#01A6B0>+0.00 (Error) Bytes</Color>" : "Console: Offline";
            }
            data.power += powerGains;
            data.powerCollected += powerGains;
            data.bytes += data.isConsoleOn ? bytesGains : 0;
            GainText.text = $"You Earned:\n<color=#E7D600>+{Methods.NotationMethod(powerGains, "F2")} Power</color>";
            bytesGainedText.text = data.isConsoleOn ? $"You Earned:\n<color=#01A6B0>+{Methods.NotationMethod(bytesGains, "F2")} Bytes</Color>" : "Console: Offline";
        }
    }

    public DateTime currentUTCDateTime;
    public async Task<DateTime> AwaitGetUTCTIme()
    {
        StartCoroutine(daily.GetUTCTime());
        await Task.Delay(1000);
        return daily.tempDateTime;
    }

    public void CloseOffline()
    {
        offlinePopUp.gameObject.SetActive(false);
    }

}
