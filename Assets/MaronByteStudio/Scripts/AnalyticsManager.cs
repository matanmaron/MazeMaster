using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace MaronByteStudio.AnalyticsManager
{
    public class AnalyticsManager : MonoBehaviour
    {
        private static bool IsEditor = false;

        private void Awake()
        {
#if UNITY_EDITOR
            IsEditor = true;
#endif
            if (Debug.isDebugBuild)
            {
                IsEditor = true;
            }
            if (IsEditor)
            {
                Debug.LogWarning("THIS IS DEBUG BUILD, DO NOT USE IN PRODUCTION BUILD!");
            }
        }

        public void SendAnalyticsEvent(string eName, string pKey, object pValue)
        {
            if (IsEditor)
            {
                Debug.Log($"[SendAnalyticsEvent] - ({eName})");
                return;
            }
            AnalyticsResult res = Analytics.CustomEvent(eName,
                new Dictionary<string, object>
                {
                    { pKey, pValue }
                });
            Debug.Log($"[SendAnalyticsEvent] - {res} ({eName})");
        }

        public void SendAnalyticsEvent(string eName)
        {
            AnalyticsResult res = Analytics.CustomEvent(eName);
            Debug.Log($"[SendAnalyticsEvent] - {res} ({eName})");
        }
    }
}
