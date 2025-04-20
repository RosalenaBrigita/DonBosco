using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public static class NPCToDialogueConverter
{
    private static string dialogueUrl = "http://localhost/DonBosco/get_dialogue.php";

    public static IEnumerator GetDialogueFromNPC(int npcID, System.Action<TextAsset> callback)
    {
        string url = $"{dialogueUrl}?npc_id={npcID}";
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error load dialogue: " + www.error);
            callback(null);
        }
        else
        {
            var result = JsonUtility.FromJson<InkWrapper>(www.downloadHandler.text);
            callback(new TextAsset(result.ink_json));
        }
    }

    [System.Serializable]
    private class InkWrapper
    {
        public string ink_json;
    }
}