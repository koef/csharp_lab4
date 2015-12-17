using Pills.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pills
{
    class CBall : CImageBase
    {
        public CBall(int _Left, int _Top) : base(Resources.ball)
        {
            Left = _Left;
            Top = _Top;
        }

        public void Move(int _Left, int _Top)
        {
            Left = _Left;
            Top = _Top;
        }

        public int CenterX
        {
            get
            {
                return Left + 15;
            }
        }

        public int CenterY
        {
            get
            {
                return Top + 15;
            }
        }

        public int Radius
        {
            get
            {
                return 15;
            }
        }
    }
}
