#define DEBUGGING
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pills
{
    public partial class frmMain : Form
    {
#if (DEBUGGING)
        int _curX = 0;
        int _curY = 0;
        int b = 0;
        double k = 0;
#endif
        int _startVectorX;
        int _startVectorY;
        CBall _selectedBall;

        int vectorLength;
        double alphaAngle;

        bool showVector;

        Graphics dc;

        //CBall ball1; 
        CTable _table;
        public frmMain()
        {
            InitializeComponent();
            DoubleBuffered = true;
            GameInit();
        }

        private void GameInit()
        {
            showVector = false;
            vectorLength = 0;
            alphaAngle = -300;
            //ball1 = new CBall(156, 342);
            _table = new CTable();
            _table.AddBall(156, 342);
            _table.AddBall(257, 230);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            dc = e.Graphics;
            dc.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            TextFormatFlags _textFlags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            Font _font = new System.Drawing.Font("Arial", 10, FontStyle.Bold);

#if (DEBUGGING)
            TextRenderer.DrawText(dc, "X=" + _curX.ToString() + "  Y=" + _curY.ToString(), _font,
                new Rectangle(75, 33, 100, 20), SystemColors.ControlText, _textFlags);
            TextRenderer.DrawText(dc, "Vector len= " + vectorLength.ToString(), _font,
                new Rectangle(175, 33, 120, 20), SystemColors.ControlText, _textFlags);
            TextRenderer.DrawText(dc, "α= " + alphaAngle.ToString(), _font,
                new Rectangle(295, 33, 95, 20), SystemColors.ControlText, _textFlags);
            TextRenderer.DrawText(dc, "b= " + b.ToString() + " k= " + k.ToString(), _font,
                new Rectangle(520, 33, 140, 20), SystemColors.ControlText, _textFlags);
#endif
            _table.Balls.ForEach(delegate(CBall ball) 
            {
                ball.DrawImage(dc);
            });

            if (showVector)
            {
                Pen vectorPen = new Pen(Color.White, 2);
                vectorPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                dc.DrawLine(vectorPen, _startVectorX, _startVectorY, _curX, _curY);
            }


        }

        private void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
            _curX = e.X;
            _curY = e.Y;
            //if (showVector && _selectedBall != null)
            //{
            //    SetBallParms(e.X, e.Y, _selectedBall);
            //}
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            CBall _ball;
            _ball = _table.GetBall(e.X, e.Y);

            if(_ball != null)
            {
                _startVectorX = _ball.CenterX;
                _startVectorY = _ball.CenterY;
                _selectedBall = _ball;
                showVector = true;
            }
        }

        private void frmMain_MouseUp(object sender, MouseEventArgs e)
        {
            //высчитываем длину вектора, которая будет определять силу удара
            vectorLength = (int)Math.Sqrt(Math.Pow(e.Y - _startVectorY, 2) + Math.Pow(e.X - _startVectorX, 2));
            if (_selectedBall != null && vectorLength != 0)
            {
                SetBallParms(e.X, e.Y, _selectedBall);
            }
            vectorLength = 0;
            showVector = false;
            _selectedBall = null;
            tmrHearbeat.Enabled = true;
        }

        private void tmrHearbeat_Tick(object sender, EventArgs e)
        {
            int new_x = 0;
            int new_y = 0;
            int step = 0;

            _table.Balls.ForEach(delegate (CBall ball)
            {
                if(ball.Power > 0)
                {
                    step = Convert.ToInt32(ball.Power / 16);
                    int dist = step + ball.TotalDistance;
                    if (ball.Angle != 0)
                    {
                        if(ball.IsGrowingY == false && ball.IsGrowingX == false && ball.Angle < 90)
                        {
                            //шар летит в левый верхний угол
                            new_x = Convert.ToInt32(ball.X0 - dist * Math.Cos(ball.Angle * Math.PI / 180.0));
                            new_y = Convert.ToInt32(ball.Y0 - dist * Math.Sin(ball.Angle * Math.PI / 180.0));
                        } else if(ball.IsGrowingY == true && ball.IsGrowingX == false && ball.Angle < 90)
                        {
                            //шар летит в левый нижний угол
                            new_x = Convert.ToInt32(ball.X0 - dist * Math.Cos(ball.Angle * Math.PI / 180.0));
                            new_y = Convert.ToInt32(ball.Y0 + dist * Math.Sin(ball.Angle * Math.PI / 180.0));
                        } else if(ball.IsGrowingY == true && ball.IsGrowingX == true && ball.Angle < 90)
                        {
                            //шар летит в правый нижний угол
                            new_x = Convert.ToInt32(ball.X0 + dist * Math.Cos(ball.Angle * Math.PI / 180.0));
                            new_y = Convert.ToInt32(ball.Y0 + dist * Math.Sin(ball.Angle * Math.PI / 180.0));
                        } else if(ball.IsGrowingY == false && ball.IsGrowingX == true && ball.Angle < 90)
                        {
                            //шар летит в правый верхний угол
                            new_x = Convert.ToInt32(ball.X0 + dist * Math.Cos(ball.Angle * Math.PI / 180.0));
                            new_y = Convert.ToInt32(ball.Y0 - dist * Math.Sin(ball.Angle * Math.PI / 180.0));
                        } else if(ball.IsGrowingY == true && ball.Angle == 90)
                        {
                            //шар летит строго вверх
                            new_x = ball.X0;
                            new_y = ball.Y0 + dist;
                        } else if(ball.IsGrowingY == false && ball.Angle == 90)
                        {
                            //шар летит строго вниз
                            new_x = ball.X0;
                            new_y = ball.Y0 - dist;
                        }
                    }
                    else
                    {
                        if (ball.IsGrowingX)
                        {
                            //движение вправо
                            new_x = ball.X0 + dist;
                            new_y = ball.Y0;
                        }
                        else
                        {
                            //движение влево
                            new_x = ball.X0 - dist;
                            new_y = ball.Y0;
                        }
                    }
                    ball.Move(new_x, new_y);
                    ball.TotalDistance = dist;
                    ball.Power -= 1;

                    if(ball.Left <= _table.LeftBorder || ball.Left + CBall.Radius * 2 >= _table.RightBorder)
                    {
                        if (ball.Left < _table.LeftBorder) ball.Left = _table.LeftBorder;
                        if (ball.Left + CBall.Radius * 2 >= _table.RightBorder) ball.Left = _table.RightBorder - CBall.Radius * 2;

                        ball.IsGrowingX = !ball.IsGrowingX;
                        ball.X0 = ball.CenterX;
                        ball.Y0 = ball.CenterY;
                        ball.TotalDistance = 0;
                    }
                    if(ball.Top <= _table.TopBorder || ball.Top + CBall.Radius * 2 >= _table.BottomBorder)
                    {
                        if (ball.Top <= _table.TopBorder) ball.Top = _table.TopBorder;
                        if (ball.Top + CBall.Radius * 2 >= _table.BottomBorder) ball.Top = _table.BottomBorder - CBall.Radius * 2;

                        ball.IsGrowingY = !ball.IsGrowingY;
                        ball.X0 = ball.CenterX;
                        ball.Y0 = ball.CenterY;
                        ball.TotalDistance = 0;
                    }

                    //столкновение двух шаров
                    foreach (CBall _ball in _table.Balls)
                    {
                        if (ball.CenterY == _ball.CenterY && ball.CenterX == _ball.CenterX) continue;
                        int distanceBetweenBalls = (int)Math.Sqrt(Math.Pow(ball.CenterY - _ball.CenterY, 2) + Math.Pow(ball.CenterX - _ball.CenterX, 2));
                        if (distanceBetweenBalls < CBall.Radius * 2)
                        {
                            //так как шары чаще всего взаимопроникают друг в друга, раздвигаем их на расстояние 2R - 1
                            int Xa2 = ball.CenterX;
                            int Ya2 = ball.CenterY;
                            if (ball.CenterX < _ball.CenterX)
                            {
                                while ((int)Math.Sqrt(Math.Pow(Ya2 - _ball.CenterY, 2) + Math.Pow(Xa2 - _ball.CenterX, 2)) < CBall.Radius * 2)
                                {
                                    Ya2 = (int)(ball.K * Xa2 + ball.B);
                                    Xa2 -= 1;
                                }
                            }
                            else
                            {
                                while ((int)Math.Sqrt(Math.Pow(Ya2 - _ball.CenterY, 2) + Math.Pow(Xa2 - _ball.CenterX, 2)) < CBall.Radius * 2)
                                {
                                    Ya2 = (int)(ball.K * Xa2 + ball.B);
                                    Xa2 += 1;
                                }
                            }
                            //Ya2 = (int)(ball.K * Xa2 + ball.B);
                            ball.Move(Xa2, Ya2);

                            //находим координаты точки соприкосновения
                            int touchpointX = 0;
                            int touchpointY = 0;
                            touchpointX = _ball.CenterX + (ball.CenterX - _ball.CenterX) / 2;
                            touchpointY = _ball.CenterY + (ball.CenterY - _ball.CenterY) / 2;

                            SetBallParms(touchpointX, touchpointY, _ball);

                            //высчитываем угол движения шара А после столкновения
                            ball.Angle = 180 - (_ball.Angle + (90 - (ball.Angle + (90 - _ball.Angle))));

                            ball.Power = 0;
                            _ball.Power = 0;
                        }
                    }
                }
            });
            Refresh();
        }

        private void SetBallParms(int e_x, int e_y, CBall _ball)
        {
            if (vectorLength > 0)
            {
                //считаем угол по разным катетам для повышения точности
                if (Math.Abs(_startVectorX - e_x) > 25)
                {
                    alphaAngle = Math.Asin(Math.Abs(_startVectorY - e_y) / (double)vectorLength) * 180 / Math.PI;
                }
                else
                {
                    alphaAngle = Math.Acos(Math.Abs(_startVectorX - e_x) / (double)vectorLength) * 180 / Math.PI;
                }

                if (alphaAngle != 0)
                {
                    //движение вверх
                    if (_startVectorY < e_y) _ball.IsGrowingY = false;
                    //движение вниз
                    else _ball.IsGrowingY = true;
                    //движение влево
                    if (_startVectorX < e_x) _ball.IsGrowingX = false;
                    //движение вправо
                    else _ball.IsGrowingX = true;
                }
                else
                {
                    _ball.IsGrowingY = true;
                    //движение строго влево
                    if (_startVectorX < e_x) _ball.IsGrowingX = false;
                    //движение строго вправо
                    else _ball.IsGrowingX = true;
                }

                if (_startVectorX != e_x)
                {
                    k = (_startVectorY - e_y) * 1.0 / (_startVectorX - e_x) * 1.0;
                    b = (int)Math.Round(e_y - e_x * k);
                }

                _ball.Angle = alphaAngle;
                _ball.B = b;
                _ball.K = k;
                if (vectorLength > 400) vectorLength = 400;
                _ball.Power = vectorLength;
                _ball.X0 = _ball.CenterX;
                _ball.Y0 = _ball.CenterY;
                _ball.TotalDistance = 0;
            }
        }
    }
}
