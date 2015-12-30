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
        double _angle = 0;
        int _b = 0;
        double _k = 0;
        int _power = 0;
        int _totalDistance = 0;
        int _x0 = 0;
        int _y0 = 0;
        bool _isGrowingY = true;
        bool _isGrowingX = true;
        public static int Radius = 15;

        public CBall(int _Left, int _Top) : base(Resources.ball)
        {
            Left = _Left;
            Top = _Top;
        }

        public void Move(int _Left, int _Top)
        {
            Left = _Left - 15;
            Top = _Top - 15;
        }

        public double Angle { get { return _angle; } set { _angle = value; } }
        public double K { get { return _k; } set { _k = value; } }
        public int B { get { return _b; } set { _b = value; } }
        public int Power { get { return _power; } set { _power = value; } }
        public bool IsGrowingY { get { return _isGrowingY; } set { _isGrowingY = value; } }
        public bool IsGrowingX { get { return _isGrowingX; } set { _isGrowingX = value; } }
        public int TotalDistance { get { return _totalDistance; } set { _totalDistance = value; } }
        public int X0 { get { return _x0; } set { _x0 = value; } }
        public int Y0 { get { return _y0; } set { _y0 = value; } }

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

        //public int Radius
        //{
        //    get
        //    {
        //        return 15;
        //    }
        //}
    }
}
