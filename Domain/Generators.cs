namespace Domain;

public static class Generators
{
    private static readonly Random ThisRandom = new();
    
    public static string NewRandomTitle()
    {
        var vi = ThisRandom.Next(Verbs.Length);
        var ni = ThisRandom.Next(Nouns.Length);
        return $"{Verbs[vi]} {Nouns[ni]}";
    }

    private static readonly string[] Verbs = {
        "greet", "admire", "catch", "resolve", "blossom", "imagine", "pursue", "scratch", "cheer",
        "outline", "compile", "distinguish", "wish", "update", "shout", "prevent", "tempt", "waste",
        "project", "bound", "promote", "advertise", "insert", "fulfil", "age", "reckon", "trade",
        "insist", "mention", "borrow", "cancel", "abuse", "threaten", "rush", "warn", "manage", "use",
        "protect", "overcome", "elect", "stress", "frighten", "flee", "opt", "await", "oppose", "hurt",
        "sense", "exclude", "entitle"
    };

    private static readonly string[] Nouns = {
        "rerecognition", "audience", "driver", "elevator", "income", "promotion", "climate", "newspaper",
        "difference", "basket", "editor", "life", "growth", "world", "cheek", "opinion", "establishment",
        "education", "appearance", "dad", "device", "signature", "poet", "assumption", "wedding",
        "introduction", "girl", "engine", "two", "maintenance", "reading", "power", "son", "grocery",
        "failure", "error", "location", "charity", "manufacturer", "software", "classroom", "marriage",
        "week", "relationship", "depression", "setting", "physics", "preference", "paper", "clothes",
    };
}