/*using System.Collections.Generic;
using UnityEngine;

public static class DialogueCacheManager
{
    private static Dictionary<int, TextAsset> dialogueCache = new Dictionary<int, TextAsset>();

    public static bool TryGetDialogue(int npcID, out TextAsset dialogue)
    {
        return dialogueCache.TryGetValue(npcID, out dialogue);
    }

    public static void CacheDialogue(int npcID, TextAsset dialogue)
    {
        if (!dialogueCache.ContainsKey(npcID))
        {
            dialogueCache[npcID] = dialogue;
        }
    }
}
*/