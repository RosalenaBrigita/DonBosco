using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public static class NPCToDialogueConverter
{
    private static string dialogueUrl = "http://localhost/DonBosco/get_dialogue.php";

    public static IEnumerator GetDialogueFromNPC(int npcID, System.Action<TextAsset> callback)
    {
        string url = $"{dialogueUrl}?npc_id={npcID}";

        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning($"Database load failed for NPC {npcID}: {www.error}");
                callback(null);
            }
            else
            {
                try
                {
                    var result = JsonUtility.FromJson<InkWrapper>(www.downloadHandler.text);
                    callback(new TextAsset(result.ink_json));
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Failed to parse dialogue for NPC {npcID}: {e.Message}");
                    callback(null);
                }
            }
        }
    }

    [System.Serializable]
    private class InkWrapper
    {
        public string ink_json;
    }
}