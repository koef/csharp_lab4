using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pills
{
    class CTable
    {
        List<CBall> _balls;
        int _leftBorder = 50;
        int _rightBorder = 913;
        int _topBorder = 50;
        int _bottomBorder = 480;

        public List<CBall> Balls
        {
            get
            {
                return _balls;
            }
        }

        public int LeftBorder { get { return _leftBorder; } }
        public int RightBorder { get { return _rightBorder; } }
        public int TopBorder { get { return _topBorder; } }
        public int BottomBorder { get { return _bottomBorder; } }

        public CTable()
        {
            _balls = new List<CBall>();
        }

        public CBall GetBall(int x, int y)
        {
            foreach(CBall ball in _balls)
            {
                if (Math.Pow((x - ball.CenterX), 2) + Math.Pow((y - ball.CenterY), 2) < Math.Pow(CBall.Radius, 2))
                {
                    return ball;
                }
            }
            return null;
        }

        public void AddBall(int x, int y)
        {
            if(x > _leftBorder && x < _rightBorder && y > _topBorder && y < _bottomBorder)
            {
                _balls.Add(new CBall(x, y));
            }
        }
    }
}
