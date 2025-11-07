namespace CardHolder.Services;

public class Card
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public byte[]? CodeImageBytes { get; set; }
    public byte[]? CardImageBytes { get; set; }
    
    public Card(string id, string name, byte[] codeImageBytes, byte[] cardImageBytes)
    {
        Id = id;
        Name = name;
        CodeImageBytes = codeImageBytes;
        CardImageBytes = cardImageBytes;
    }
}