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

        Graphics dc;

        public frmMain()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            dc = e.Graphics;

            TextFormatFlags _textFlags = TextFormatFlags.Left | TextFormatFlags.EndEllipsis;
            Font _font = new System.Drawing.Font("Arial", 10, FontStyle.Bold);

#if (DEBUGGING)
            TextRenderer.DrawText(dc, "X=" + _curX.ToString() + "  Y=" + _curY.ToString(), _font,
                new Rectangle(75, 33, 120, 20), SystemColors.ControlText, _textFlags);
#endif
        }

        private void frmMain_MouseMove(object sender, MouseEventArgs e)
        {
#if (DEBUGGING)
            _curX = e.X;
            _curY = e.Y;
            Refresh();
#endif
        }
    }
}
