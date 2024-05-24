using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CostManager
{
    public static int drawedCardCount;
    public static int drawedMajor;
    public static int drawedLib;
    public static int drawedWork;
    public static int drawedPlay;
    public static int dayCount;
    public static float startTime;
    public static int MP;
    public static int chatOrder = 110;
    public static List<Item> passedCards;
    public static bool isMPChatUsed;
    public static bool isTimeChatUsed;
    public static float backgroundVolume = 0.5f;
    public static float popVolume = 0.5f;
}
