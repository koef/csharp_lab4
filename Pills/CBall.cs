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

    }
}
