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
#endif
        int _startVectorX;
        int _startVectorY;

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
            //ball1 = new CBall(156, 342);
            _table = new CTable();
            _table.AddBall(156, 342);
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
                new Rectangle(75, 33, 120, 20), SystemColors.ControlText, _textFlags);
#endif
            //ball1.DrawImage(dc);
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
            Refresh();
        }

        private void frmMain_MouseDown(object sender, MouseEventArgs e)
        {
            CBall _ball;
            _ball = _table.GetBall(e.X, e.Y);

            if(_ball != null)
            {
                _startVectorX = _ball.CenterX;
                _startVectorY = _ball.CenterY;
                showVector = true;
            }
        }

        private void frmMain_MouseUp(object sender, MouseEventArgs e)
        {
            showVector = false;
        }
    }
}
