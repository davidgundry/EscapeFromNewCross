using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System;

// adapted from code at https://gist.github.com/anonymous/828805

public class MazeBuilder
{

		private const int _rowDimension = 0;
		private const int _columnDimension = 1;
		private const int S = 1;
		private const int E = 2;
		private const int HORIZONTAL = 1;
		private const int VERTICAL = 2;
	
		public int MinSize { get; private set; }

		public int MaxSize { get; private set; }

		public Maze maze { get; private set; }

		public MazeBuilder () : this (3,3)
		{

		}
			
		public MazeBuilder (int rows, int columns)
		{
				MinSize = 3;
				MaxSize = 10;
				maze = Initialise (rows, columns);
		}
	public int choose_orientation(int width, int height) {
		if (width < height) {
			return HORIZONTAL;
		} else {
			if (height < width) {
				return VERTICAL;
			} else {
				return (Random.value > 0.5f) ? HORIZONTAL : VERTICAL;
			}
		}
	}
	private void divide(int[,] grid, int x,int y,int width,int height,int orientation) {


		/*if ((width < 2 )||( height < 2)) {
			return;
		}*/
			
				
		bool horizontal = (orientation == HORIZONTAL);
				
				// where will the wall be drawn from?
		int wx = x + (horizontal ? 0 : Random.Range(0,width-2));
		int wy = y + (horizontal ? Random.Range(0,height-2) : 0);
				
				// where will the passage through the wall exist?
		int px = wx + (horizontal ? Random.Range(0,width) : 0);
		int py = wy + (horizontal ? 0 : Random.Range(0,height));
				
				// what direction will the wall be drawn?
		int dx = horizontal ? 1 : 0;
		int dy = horizontal ? 0 : 1;
				
				// how long will the wall be?
		int length = horizontal ? width : height;
				
				// what direction is perpendicular to the wall?
		int dir = horizontal ? S : E;

		for(int each=0;each<length;each++ )
		{
			if ((wx != px) || (wy != py))  {
				grid[wy,wx] |= dir;
			}
			wx += dx;
			wy += dy;
		}
						
		int nx = x;
		int ny = y;
		int w = horizontal ? width : wx - x + 1;
		int h = horizontal ?  wy-y+1 : height;
		divide (grid, nx, ny, w, h, choose_orientation (w, h));
						
		nx = horizontal ? x : wx+1;
		ny = horizontal ? wy+1 : y;
		w = horizontal ? width : x + width - wx - 1;
		h = horizontal ? y + height - wy - 1 : height;
		divide (grid, nx, ny, w, h, choose_orientation (w, h));
						
	}


				
			
		public Maze Initialise (int rows, int columns)
		{
				if (rows < MinSize)
						rows = MinSize;
				
				if (columns < MinSize)
						columns = MinSize;
				
				if (rows > MaxSize)
						rows = MaxSize;
				
				if (columns > MaxSize)
						columns = MaxSize;
				
				var maze = new Maze ();
				maze.create (rows, columns);
				maze.fillValue (15);
				
				
				
				return maze;
		}
			
		private Dictionary<Directions, int> DirectionX = new Dictionary<Directions, int>
			{
				{ Directions.N, 0 },
				{ Directions.S, 0 },
				{ Directions.E, 1 },
				{ Directions.W, -1 }
			};
		private Dictionary<Directions, int> DirectionY = new Dictionary<Directions, int>
			{
				{ Directions.N, -1 },
				{ Directions.S, 1 },
				{ Directions.E, 0 },
				{ Directions.W, 0 }
			};
		private Dictionary<Directions, Directions> Opposite = new Dictionary<Directions, Directions>
			{
				{ Directions.N, Directions.S },
				{ Directions.S, Directions.N },
				{ Directions.E, Directions.W },
				{ Directions.W, Directions.E }
			};
			
		public Maze Generate ()
		{
				var cells = maze.cells;
				;
				//CarvePassagesFrom (0, 0, ref cells);
				return maze;
		}
			
		/*public void CarvePassagesFrom (int currentX, int currentY, ref int[,] grid)
		{
				var directions = new List<Directions>
				{
					Directions.N,
					Directions.S,
					Directions.E,
					Directions.W
				}.OrderBy (x => Guid.NewGuid ());
				
				foreach (var direction in directions) {
						var nextX = currentX + DirectionX [direction];
						var nextY = currentY + DirectionY [direction];
					
						if (IsOutOfBounds (nextX, nextY, grid))
								continue;
					
						if (grid [nextY, nextX] != 15) // has been visited
								continue;
					
						grid [currentY, currentX] &= ~((int)direction);
						grid [nextY, nextX] &= ~((int)Opposite [direction]);
					
						CarvePassagesFrom (nextX, nextY, ref grid);
				}
		}
			
		private bool IsOutOfBounds (int x, int y, int[,] grid)
		{
				if (x < 0 || x > grid.GetLength (_rowDimension) - 1)
						return true;
				
				if (y < 0 || y > grid.GetLength (_columnDimension) - 1)
						return true;
				
				return false;
		}*/
			
		/*	public void Print(int[,] grid)
			{
				var rows = grid.GetLength(_rowDimension);
				var columns = grid.GetLength(_columnDimension);
				
				// Top line
				Console.Write(" ");
				for (int i = 0; i < columns; i++)
					Console.Write(" _");
				Console.WriteLine();
				
				for (int y = 0; y < rows; y++)
				{
					Console.Write(" |");
					
					for (int x = 0; x < columns; x++)
					{
						var directions = (Directions)grid[y, x];
						
						var s = directions.HasFlag(Directions.S) ? " " : "_";
						
						Console.Write(s);
						
						s = directions.HasFlag(Directions.E) ? " " : "|";
						
						Console.Write(s);					
					}
					
					Console.WriteLine();
				}
			}*/
		
}
