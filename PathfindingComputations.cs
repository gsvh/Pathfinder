using System;
using System.Collections.Generic;
using System.Text;

namespace Pathfinder

{
	class PathfindingComputations
	{
		public int cellSize { get; set; }
		public int n { get; set; }
		
		static List<int> ReadMap()
        {
			List<int> map = new List<int>(); //draws empty map for the 900 blocks
			for (int i = 0; i<900; i++)
			{
				map.Add(0);
			}
			return map;
			
		}

		public List<int> Map = ReadMap();
		public void DrawOnMap(int type, int pos)
        {
			Map[pos] = type;
        }

		public List<int> SurroundingsX = new List<int>(); //list for x coordinate
		public List<int> SurroundingsY = new List<int>(); //list for y coordinate



		public void CalculateSuroundings(int s) // Calculate surounding boxes' values
        {
            for (int i = (s / n - 1) * n; i <= (s / n + 1)*n; i += n) // calculate the i component to determine the row
            {
                for (int j = s % n - 1; j <= s % n + 1; j++) // calculate the j component to detemine the column
                {
					
					if (0 <= i && i / n < n && 0 <= j && j < n && i + j != s) //only select adresses that are valid and not out of range, as well as not selecting the current block
					{
						int x = cellSize * ((i + j) % n); //converts from address to X coordinate
						int y = cellSize * ((i + j) / n); //converts from address to Y coordinate
						SurroundingsX.Add(x);
                        SurroundingsY.Add(y);
						
					}
					
				}
            }
        }
		
			
        
	
	}
	 /*
	public class Box
    {
		public int gCost;
		public int hCost;
		public int fCost; // g(x) + h(x)
		public int X; // x-coordinate
		public int Y; // y-coordinate									//some code for my class Box
		public int adress;


		

		
    }
	 */
}
