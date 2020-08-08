using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace MineSweeper
{
    class Cell : Button
    {
       
        public int ri;
        public int ci;
        public bool isOpen;
        public int Value;
        public Cell (int r,int c,int W,int H,int Deminsion)
        {
            ri = r;
            ci = c;
            isOpen = false;
            Value = 0;
            this.Width = W / Deminsion-10;
            this.Height = H/ Deminsion-10;
        }
    
    }
}
