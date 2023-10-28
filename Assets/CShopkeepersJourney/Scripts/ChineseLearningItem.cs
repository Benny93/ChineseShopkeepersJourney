using UnityEngine;

[CreateAssetMenu(fileName = "New Chinese Learning Item", menuName = "Chinese Learning Item")]
public class ChineseLearningItem : ScriptableObject
{
    public string englishName;
    public string chineseTranslationInPinyin;
    public string writtenChinese;
    public AudioClip audioFile;
}