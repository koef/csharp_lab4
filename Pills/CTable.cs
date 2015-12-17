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
        int leftBorder = 50;
        int rightBorder = 913;
        int topBorder = 50;
        int bottomBorder = 480;

        public List<CBall> Balls
        {
            get
            {
                return _balls;
            }
        }

        public CTable()
        {
            _balls = new List<CBall>();
        }

        public CBall GetBall(int x, int y)
        {
            foreach(CBall ball in _balls)
            {
                if (Math.Pow((x - ball.CenterX), 2) + Math.Pow((y - ball.CenterY), 2) < Math.Pow(ball.Radius, 2))
                {
                    return ball;
                }
            }
            return null;
        }

        public void AddBall(int x, int y)
        {
            if(x > leftBorder && x < rightBorder && y > topBorder && y < bottomBorder)
            {
                _balls.Add(new CBall(x, y));
            }
        }
    }
}
