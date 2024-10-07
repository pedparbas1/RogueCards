[System.Serializable]
public class SkillCardDisplayer : CardDisplayer<SkillEffect>, IDisplayer
{
    public void Display()
    {
        UpdateCardDisplayer();
    }
}
