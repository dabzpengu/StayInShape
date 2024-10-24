using UnityEngine;


[CreateAssetMenu(fileName = "matchingCard", menuName = "SOs/matchingCard")]
public class MatchingCardSO : ScriptableObject
{
    public new string name;
    public string description;
    public int id;
    public Color col;
    public Sprite img;
}
