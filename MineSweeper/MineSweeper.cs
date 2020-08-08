using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class MineSweeper : Form
    {
        int Deminsion;
        Cell[][] Cell_Map;
        int MineCount;
        Cell[] TouchedOrNot;

        private void OpenTouchingAnyUnOpenZero(int r, int c)
        {
            for (int row = r - 1; row <= r + 1; row++)
            {
                if (row < 0 || row >=Deminsion)
                {
                    continue;
                }
                for (int col = c - 1; col <= c + 1; col++)
                {
                    if (col < 0 || col >=Deminsion)
                    {
                        continue;
                    }
                    if (Cell_Map[row][col].Value == 0 && Cell_Map[row][col].isOpen == false)
                    {
                        Cell_Map[row][col].isOpen = true;
                        Cell_Map[row][col].BackColor = Color.DarkGray;
                        Cell_Map[row][col].Text = "";
                    }
                    else if(Cell_Map[row][col].isOpen == false)
                    {
                        Cell_Map[row][col].isOpen = true;
                        Cell_Map[row][col].BackColor = Color.DarkGray;
                        Cell_Map[row][col].Text = Cell_Map[row][col].Value.ToString();
                    }
                    
                }
            }

        }
        void RegrowMyCellDataarrya(int Size)
        {
            Cell[] temp = new Cell[Size+1];
            for(int i=0;i<Size;i++)
            {
                temp[i] = TouchedOrNot[i];
            }
            TouchedOrNot = temp;
          
        }
        bool ShouldIConsiderThis(int size,Cell A)
        {
            for(int i=0;i<size;i++)
            {
                if(TouchedOrNot[i]==A)
                {
                    return false;
                }
            }
            return true;
        }
        private void OpenAllConnectedZeros(object sender, EventArgs e)
        {
            
            int size = 0;
            bool repeat = true;
            while (repeat == true)
            {
                repeat = false;
                for (int r = 0; r < Deminsion; r++)
                {
                    for (int c = 0; c < Deminsion; c++)
                    {
                        if (Cell_Map[r][c].Value == 0 && Cell_Map[r][c].isOpen == true && ShouldIConsiderThis(size,Cell_Map[r][c]))
                        {
                            repeat = true;
                            OpenTouchingAnyUnOpenZero(r, c);
                            RegrowMyCellDataarrya(size);
                            size++;
                            TouchedOrNot[size - 1] = Cell_Map[r][c];
                        }
                    }
                }
            }
        }
        bool WinOrNot(  )
        {
            
            for(int r=0;r<Deminsion;r++)
            {
                for(int c=0;c<Deminsion;c++)
                {
                    if(Cell_Map[r][c].Value!=-1 && Cell_Map[r][c].isOpen!=true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private void WhenClicked(object sender, EventArgs e)
        {
                Cell tmp = (Cell)sender;
            
            
            tmp.BackColor = Color.DarkGray;
            tmp.isOpen = true;
            if(WinOrNot()==true)
            {
                for (int r = 0; r < Deminsion; r++)
                {
                    for (int c = 0; c < Deminsion; c++)
                    {
                        Cell_Map[r][c].Text = Cell_Map[r][c].Value.ToString();
                    }
                }
                MessageBox.Show("    You Win   ");
                Close();
            }
            else if (tmp.Value == -1)
            {

                for (int r = 0; r < Deminsion; r++)
                {
                    for (int c = 0; c < Deminsion; c++)
                    {
                        //Cell_Map[r][c].Text = Cell_Map[r][c].Value.ToString();
                        if (Cell_Map[r][c].Value != -1 && Cell_Map[r][c].Value != 0)
                        {
                            Cell_Map[r][c].Text = Cell_Map[r][c].Value.ToString();
                        }
                        else if ( Cell_Map[r][c].Value == 0)
                        {
                            Cell_Map[r][c].Text = "";
                        }
                        else
                        {
                            Cell_Map[r][c].BackgroundImage = Image.FromFile(@"C:\Users\asad\Desktop\SEM-2\PFX\PRE PFX MID\MineSweeper\MineSweeper\mine_048x048_32_lma_icon.ico");
                            Cell_Map[r][c].BackgroundImageLayout = ImageLayout.Center;
                        }

                   
                    }
                }
                MessageBox.Show("    You Lose   ");

                Close();

                
            }
            else if (tmp.Value == 0)
            {
             
              
                    OpenAllConnectedZeros(sender, e);
                
            }
            else
            {
                tmp.Text = tmp.Value.ToString();
            }
        }
        
     
        public MineSweeper()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MineSweeper_Load(object sender, EventArgs e)
        {
           
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }
        void GenerateRandomMines()
        {
            for (int i = 0; i < MineCount; i++)
            {   int r,c;
                do
                {
                    Random R = new Random();
                    r = R.Next(Deminsion);
                    c = R.Next(Deminsion);
                   // Cell_Map[r][c].Value = -1;
                } while (Cell_Map[r][c].Value == -1);
                Cell_Map[r][c].Value = -1;
                for(int row=r-1;row<=r+1 ;row++)
                {
                    if (row<0 || row>=Deminsion)
                    {
                        continue;
                    }
                    for(int col=c-1;col<=c+1;col++)
                    {
                        if (col < 0 || col >= Deminsion)
                        {
                            continue;
                        }
                        if( Cell_Map[row][col].Value !=-1)
                                Cell_Map[row][col].Value ++;
                         
                        
                    }
                }
            }
        }
        private void Start_Click(object sender, EventArgs e)
        {
            if(easymode.Checked==true)
            {
                Deminsion = 5;
                MineCount = 5;
               
            }

            else if (MedMode.Checked == true)
            {
                MineCount = 10;
                Deminsion = 7;
            }
            else if (HardMode.Checked == true)
            {
                MineCount = 15;
                Deminsion = 9;
            }
            else
            {
                MessageBox.Show("PLEASE !!! SELECT A LEVEL ");
                return;
            }
            Cell_Map=new  Cell [Deminsion][];
            for(int i=0;i<Deminsion;i++)
            {
                Cell_Map[i]=new Cell[Deminsion];
            }
            CellScreen.Controls.Clear();
            for(int r=0;r<Deminsion;r++)
            {
                for(int c=0;c<Deminsion;c++)
                {
                    Cell_Map[r][c] = new Cell(r, c, CellScreen.Width, CellScreen.Height, Deminsion);
                    Cell_Map[r][c].Click += new System.EventHandler(this.WhenClicked);
                    CellScreen.Controls.Add(Cell_Map[r][c]);
                }
            }
            GenerateRandomMines();
          

        }


        private void easymode_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void HardMode_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            easymode.Checked = true;
            easyToolStripMenuItem.Text = "✔  Easy";
            mediumToolStripMenuItem.Text = " Medium ";
            hardToolStripMenuItem.Text = " Hard";
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MedMode.Checked = true;
            mediumToolStripMenuItem.Text = "✔  Medium";
            easyToolStripMenuItem.Text = " Easy";
            hardToolStripMenuItem.Text = " Hard";
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {

            HardMode.Checked = true;
            hardToolStripMenuItem.Text = "✔  Hard";
            mediumToolStripMenuItem.Text = " Medium ";
            easyToolStripMenuItem.Text = " Easy";
           
        }

        private void levelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("     ----------   M I N E S W E E P E R  ----------  \n\n                      Made by ALI SEHRAN ");
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void toolStripContainer1_TopToolStripPanel_Click(object sender, EventArgs e)
        {

        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Start_Click(sender, e);
        }

        private void CellScreen_MouseDown(object sender, MouseEventArgs e)
        {
            switch (MouseButtons)
            {
                case MouseButtons.Left:
                    WhenClicked(sender, (EventArgs)e);
                    break;
                case MouseButtons.Right:
                   
                        Cell tmp = (Cell)sender;
                        tmp.Image = Image.FromFile(@"C:\Users\asad\Desktop\SEM-2\PFX\PRE PFX MID\MineSweeper\MineSweeper\3079_32x32x32_jDk_icon.ico");
          
           
                break;


            }
        }
    }
}
