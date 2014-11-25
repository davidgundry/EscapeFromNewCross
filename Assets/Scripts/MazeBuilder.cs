using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

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
			return HORIZONTAL
		} else {
			if (height < width) {
				return VERTICAL
			} else {
				return Random.Range(0,1) ? HORIZONTAL : VERTICAL;
			}
		}
	}
	private divide(grid, x, y, width, height, orientation) {
		return if width < 2 || height < 2
			
			display_maze(grid)
				sleep 0.02
				
				horizontal = orientation == HORIZONTAL
				
				# where will the wall be drawn from?
				wx = x + (horizontal ? 0 : rand(width-2))
				wy = y + (horizontal ? rand(height-2) : 0)
				
				# where will the passage through the wall exist?
				px = wx + (horizontal ? rand(width) : 0)
				py = wy + (horizontal ? 0 : rand(height))
				
				# what direction will the wall be drawn?
				dx = horizontal ? 1 : 0
				dy = horizontal ? 0 : 1
				
				# how long will the wall be?
				length = horizontal ? width : height
				
				# what direction is perpendicular to the wall?
				dir = horizontal ? S : E
				
				length.times do
				grid[wy][wx] |= dir if wx != px || wy != py
					wx += dx
						wy += dy
						end
						
						nx, ny = x, y
						w, h = horizontal ? [width, wy-y+1] : [wx-x+1, height]
						divide(grid, nx, ny, w, h, choose_orientation(w, h))
						
						nx, ny = horizontal ? [x, wy+1] : [wx+1, y]
						w, h = horizontal ? [width, y+height-wy-1] : [x+width-wx-1, height]
						divide(grid, nx, ny, w, h, choose_orientation(w, h))
						end
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
				CarvePassagesFrom (0, 0, ref cells);
				return maze;
		}
			
		public void CarvePassagesFrom (int currentX, int currentY, ref int[,] grid)
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
		}
			
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
