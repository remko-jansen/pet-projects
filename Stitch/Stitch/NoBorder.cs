using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Stitch
{
    public class NoBorder : IBorder
    {
        public virtual int ThicknessLeft
        {
            get
            {
                return 0;
            }
        }

        public virtual int ThicknessRight
        {
            get
            {
                return 0;
            }
        }

        public virtual int ThicknessTop
        {
            get
            {
                return 0;
            }
        }

        public virtual int ThicknessBottom
        {
            get
            {
                return 0;
            }
        }

        public virtual bool Inside
        {
            get
            {
                return true;
            }
            set { throw new NotImplementedException(); }
        }

        public virtual void Draw(Graphics g, Rectangle r)
        {
        }
    }
}
