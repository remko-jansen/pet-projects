using System.Drawing;

namespace Stitch
{
    public interface IBorder
    {
        int ThicknessLeft { get; }
        int ThicknessRight { get; }
        int ThicknessTop { get; }
        int ThicknessBottom { get; }
        bool Inside { get; set; }
        void Draw(Graphics graphics, Rectangle target);
    }
}