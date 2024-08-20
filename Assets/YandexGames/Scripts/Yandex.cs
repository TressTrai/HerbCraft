using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Yandex : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void PlayerData();

    [DllImport("__Internal")]
    private static extern void RateGame();

    [DllImport("__Internal")]
    private static extern void SetToLeaderBord(int value);

    [DllImport("__Internal")]
    private static extern void RewardedApp();

    [DllImport("__Internal")]
    private static extern string GetDevice();



    static public void GetPlayerData()
    {
        PlayerData();
    }

    static public void RateGamePopup()
    {
        RateGame();
    }

    static public void SendToLeaderBord(int money)
    {
        SetToLeaderBord(money);
    }

    static public void PlayRewardedAdd()
    {
        RewardedApp();
    }

    static public string DeviceInfo()
    {
        return GetDevice();
    }
}
