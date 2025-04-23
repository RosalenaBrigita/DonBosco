using UnityEngine;
using UnityEngine.Playables;

namespace DonBosco.Dialogue
{
    public class StartDialogueAsset : PlayableAsset
    {
        public TextAsset localDialogue; // Renamed from 'dialogue' for clarity
        public int npcID;
        public string knotPath;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<StartDialogueBehaviour>.Create(graph);
            var behaviour = playable.GetBehaviour();
            behaviour.localDialogue = localDialogue;
            behaviour.npcID = npcID;
            behaviour.knotPath = knotPath;
            return playable;
        }
    }
}