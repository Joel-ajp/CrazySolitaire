using CrazySolitaire.Properties;

namespace CrazySolitaire;

// the options for uno colors
public enum UnoColor {
    Red, Green, Blue, Yellow
}

// this represents an uno reverse card.
// I heavily referenced Card.cs when writing it
public class UnoReverseCard{
    // this is the color of the card
    public UnoColor CardColor { get; set; }
    // the control in the form that is the card
    public PictureBox PicBox { get; set; }
    // the image to be displayed on the card. Automatically figures out
    // which image to use based on CardColor
    public Bitmap PicImg {
        get => Resources.ResourceManager.GetObject($"uno_reverse_{CardColor.ToString().ToLower()}") as Bitmap;
    }

    // a simple constructor
    public UnoReverseCard() {
        // randomly select a color
        Random rand = new();
        var colors = Enum.GetValues(typeof(UnoColor));
        CardColor = (UnoColor)colors.GetValue(rand.Next(4));
        SetupPicBox();
    }

    private void SetupPicBox() {
        // sets up fhe card's visual settings
        PicBox = new()
        {
            Width = 90,
            Height = 126,
            BackgroundImageLayout = ImageLayout.Stretch,
            BorderStyle = BorderStyle.FixedSingle,
            BackgroundImage = PicImg
        };
    }






}

