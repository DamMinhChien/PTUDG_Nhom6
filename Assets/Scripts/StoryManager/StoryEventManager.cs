using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class StoryEventManager : MonoBehaviour
{
    public TextAsset storyEventsJson;
    public List<StoryEvent> events;

    void Awake()
    {
        events = JsonUtilityWrapper.FromJsonList<StoryEvent>(storyEventsJson.text);
        foreach (var e in events)
        {
            Debug.Log($"Đã load sự kiện: {e.id}, lines count = {e.lines?.Count ?? 0}");
        }
    }

    IEnumerator Start()
    {
        yield return null; // chờ 1 frame để đảm bảo mọi Awake đã chạy

        Debug.Log("StoryEventManager Start running");

        foreach (var e in events)
        {
            if (e.triggerType == "OnStart")
            {
                Debug.Log("Đã kích hoạt sự kiện OnStart");
                TriggerEvent(e);
            }
        }
    }


    void TriggerEvent(StoryEvent e)
    {
        Debug.Log("Kích hoạt sự kiện: " + e.name);

        if (e.lines == null || e.lines.Count == 0)
        {
            Debug.LogWarning("Không có đoạn thoại trong sự kiện: " + e.id);
            return;
        }

        DialogueManager.Instance.ShowDialogue(e.lines);
    }


    public static class JsonUtilityWrapper
    {
        [System.Serializable]
        private class Wrapper<T>
        {
            public List<T> items;
        }

        public static List<T> FromJsonList<T>(string json)
        {
            string wrappedJson = "{\"items\":" + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(wrappedJson);
            return wrapper.items;
        }
    }
}
