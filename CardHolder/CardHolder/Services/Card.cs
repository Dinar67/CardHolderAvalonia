using Avalonia.Controls;
using Avalonia.Media;
using Color = System.Drawing.Color;

namespace CardHolder.Services;

public class Card
{
    public string Id { get; set; }
    public string? Name { get; set; }
    public string Color { get; set; }
    public byte[]? CodeImageBytes { get; set; }
    public byte[]? CardImageBytes { get; set; }
    
    public Card(string id, string name, string color, byte[] codeImageBytes, byte[] cardImageBytes)
    {
        Id = id;
        Name = name;
        Color = color;
        CodeImageBytes = codeImageBytes;
        CardImageBytes = cardImageBytes;
    }
}