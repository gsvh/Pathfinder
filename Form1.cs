using ImTools;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pathfinder
{
    public partial class lblCoordinates : Form
    {
        public lblCoordinates()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
        }

        Graphics g;
        public int cellSize = 20; //pixel size = 20x20
        int numOfCells = 30; //30x30 grid
        Pen p = new Pen(Color.Black, 1f);
        public int startMaze = 0; //default start pos
        public int endMaze = 899; // default end pos
        public int mazeWall; // temp var for drawing a solid square
        bool canFill = false;
        bool setPointA = false;
        bool setPointB = false;
        bool lockA = false;
        bool lockB = false;

        PathfindingComputations FindPath = new Pathfinder.PathfindingComputations(); 
        

        private void button1_Click(object sender, EventArgs e)
        {
            //Draws grid
            for (int i = 0; i <= numOfCells; i++)
            {
                g.DrawLine(p, i * cellSize, 0, i * cellSize, numOfCells * cellSize);
                g.DrawLine(p, 0, i * cellSize, numOfCells * cellSize, i * cellSize);
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (setPointA)
            {
                //Draw the Red starting square
                SolidBrush fillSquare = new SolidBrush(Color.Red);
                g.FillRectangle(fillSquare, (e.X / 20 * 20), (e.Y / 20 * 20), 20, 20); // divide by 20 and multiply by 20 to get topleft corner pos
                setPointA = false;
                lockA = true; //prevent selecting again

                //Save number into array
                startMaze = (e.X / 20 * 20)/20 + (e.Y / 20 * 20)/20*30;
                FindPath.DrawOnMap(2, startMaze);
                
            }
            else if (setPointB)
            {
                //Draw the Blue ending square
                SolidBrush fillSquare = new SolidBrush(Color.Blue);
                g.FillRectangle(fillSquare, (e.X / 20 * 20), (e.Y / 20 * 20), 20, 20);
                setPointB = false;
                lockB = true;

                //Save number into array
                endMaze = (e.X / 20 * 20) / 20 + (e.Y / 20 * 20) / 20 * 30;
                label2.Text = "Ending position: " + endMaze;
                FindPath.DrawOnMap(3, endMaze);
            }
            else
            {
                //fills square with black, creating 'wall'
                SolidBrush fillSquare = new SolidBrush(Color.Black);
                g.FillRectangle(fillSquare, (e.X / 20 * 20), (e.Y / 20 * 20), 20, 20);
                mazeWall = (e.X / 20 * 20) / 20 + ((e.Y / 20 * 20) / 20 * 30);
                FindPath.DrawOnMap(1, mazeWall); // changes the pos in map from zero to one to indicate it is a closed pos (to be optimized later)
            }
            
        }
        
        
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            canFill = true; //enable drawing 
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            canFill = false; //disable drawing 
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)   //drawing while draging
        {
            if (canFill && !setPointA && !setPointB)
            {
                SolidBrush fillSquare = new SolidBrush(Color.Black);
                g.FillRectangle(fillSquare, (e.X / 20 * 20), (e.Y / 20 * 20), 20, 20);
                mazeWall = (e.X / 20 * 20) / 20 + ((e.Y / 20 * 20) / 20 * 30);
                FindPath.DrawOnMap(1, mazeWall); // changes the pos in map from zero to one to indicate it is a closed pos (to be optimized later)
            }
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!lockA)
            {
                setPointA = true; //enables setting of start pos
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!lockB)
            {
                setPointB = true; // enables setting of end pos
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            foreach (int pos in FindPath.Map)
                {
                    listBox1.Items.Add(pos);                    //some code used for debugging
                
                }
             
            label2.Text = "" + FindPath.Map.Count;

             foreach (int pos in FindPath.SurroundingsX)
            {
                listBox1.Items.Add(pos);                    

            }
            foreach (int pos in FindPath.SurroundingsY)
            {
                listBox2.Items.Add(pos);                    

            }
            label1.Text = FindPath.SurroundingsX.Count + "  " + FindPath.SurroundingsY.Count;
            */


            FindPath.n = numOfCells; //shouldn't be here but it is for now
            FindPath.cellSize = cellSize; //  "


            
            FindPath.CalculateSuroundings(startMaze); // draws squares surrounding the start pos (used for debugging)
            for (int i = 0; i < FindPath.SurroundingsX.Count; i++)
            {
                SolidBrush fillSquare = new SolidBrush(Color.DarkGreen);
                g.FillRectangle(fillSquare, FindPath.SurroundingsX[i], FindPath.SurroundingsY[i], cellSize, cellSize);
                
            }
            
            


        }
    }



}
