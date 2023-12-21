using System.Collections.Generic;
using UnityEngine;

namespace WhatsThisButton
{
    internal class GameTips
    {
        private static List<string> tipHeaders = new List<string>();
        private static List<string> tipBodys = new List<string>();

        private static string currentHeader = "";
        private static string currentBody = "";

        private static float lastMessageTime;

        public static void ShowTip(string header, string body)
        {
            int hasHeader = tipHeaders.FindIndex(t => t == header);
            int hasBody = tipBodys.FindIndex(t => t == body);
            Plugin.StaticLogger.LogInfo($"hasHeader {hasHeader}");
            Plugin.StaticLogger.LogInfo($"hasBody {hasBody}");
            if (hasHeader < 0 || hasBody < 0 || hasHeader != hasBody)
            {
                if (currentHeader != header || currentBody != body)
                {
                    tipHeaders.Add(header);
                    tipBodys.Add(body);
                }
            }
        }

        public static void UpdateInternal()
        {
            lastMessageTime -= Time.deltaTime;
            if (lastMessageTime < 0f)
            {
                currentHeader = "";
                currentBody = "";
                if (tipHeaders.Count > 0)
                {
                    lastMessageTime = 2.5f;
                    currentHeader = tipHeaders[0];
                    currentBody = tipBodys[0];
                    if (HUDManager.Instance != null)
                    {
                        HUDManager.Instance.DisplayTip(currentHeader, currentBody, false, false, "WTFISB_Tip");
                    }
                    tipHeaders.RemoveAt(0);
                    tipBodys.RemoveAt(0);
                }
            }
        }
    }
}
