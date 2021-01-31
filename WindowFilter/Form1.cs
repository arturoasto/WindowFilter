using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowFilter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();            
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        const int GWL_EXSTYLE = -20;
        const int WS_EX_LAYERED = 0x80000;
        const int WS_EX_TRANSPARENT = 0x20;

        public double MyOpacity { get; set; } = 0.3;

        protected void Form1_Load(object sender, EventArgs e)
        {
            this.Opacity = MyOpacity;
            this.BackColor = Color.Black;
            this.TopMost = true;

            var style = GetWindowLong(this.Handle, GWL_EXSTYLE);

            SetWindowLong(this.Handle, GWL_EXSTYLE, style | WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)            
                Application.Exit();            
            else
            {
                if (e.KeyCode == Keys.Add && MyOpacity < 1)                
                    MyOpacity += 0.05;
                
                if (e.KeyCode == Keys.Subtract && MyOpacity > 0)                      
                    MyOpacity -= 0.05;               

                this.Opacity = MyOpacity;
            }            
        }
    }
}
