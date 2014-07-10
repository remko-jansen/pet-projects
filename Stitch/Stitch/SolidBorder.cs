using System.Drawing;
using System.Drawing.Drawing2D;

namespace Stitch
{
    public class SolidBorder : IBorder
    {
        private readonly int _thickness;
        private readonly Color _color;
        private bool _inside;

        public virtual bool Inside
        {
            get { return _inside; }
            set { _inside = value; }
        }

        public virtual int ThicknessLeft
        {
            get
            {
                return _thickness;
            }
        }

        public virtual int ThicknessRight
        {
            get
            {
                return _thickness;
            }
        }

        public virtual int ThicknessTop
        {
            get
            {
                return _thickness;
            }
        }

        public virtual int ThicknessBottom
        {
            get
            {
                return _thickness;
            }
        }

        public SolidBorder(int thickness, Color color)
        {
            _thickness = thickness;
            _color = color;
            _inside = true;
        }

        public virtual void Draw(Graphics graphics, Rectangle target)
        {
            var pen = new Pen(_color, _thickness)
                          {
                              Alignment = PenAlignment.Inset,
                          };

            if (!Inside)
            {
                target.Inflate(_thickness, _thickness);
            }

            graphics.DrawRectangle(pen, target);
        }
    }
}