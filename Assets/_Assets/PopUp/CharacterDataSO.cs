using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character Data")]
public class CharacterDataSO : ScriptableObject
{
    public string characterID; // ID unik untuk setiap NPC
    public string characterName;
    public Sprite characterImage;
    [TextArea(3, 5)]
    public string description;
}
