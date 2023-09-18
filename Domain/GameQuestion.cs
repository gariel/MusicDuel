
namespace Domain;

public class GameQuestion
{	
    public GameType GameType { get; set; }

    public string Mp3Base64 { get; set; } = "";
    public GameQuestionItem[] Items { get; set; } = Array.Empty<GameQuestionItem>();

    public class GameQuestionItem
    {
        public string ImageBase64 { get; set; } = "";
        public string Title { get; set; } = "";
        public bool Correct { get; set; }
    }
}