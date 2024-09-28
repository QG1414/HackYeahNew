
public interface IMinigame 
{
    public MinigameType MinigameType { get; set; }
    public void ExitMinigameBool(bool value);
}

public enum MinigameType
{
    generator
}
