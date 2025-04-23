using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character Data")]
public class CharacterDataSO : ScriptableObject
{
    public string characterID;
    public string characterName;
    public Sprite characterImage;
    [TextArea(3, 5)]
    public string description;

    [Header("Optional Real Photo")]
    public Sprite realImage; 
    public string sourceCredit; 
}