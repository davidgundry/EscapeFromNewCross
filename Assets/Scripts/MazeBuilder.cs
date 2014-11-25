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
		//private const int S = Directions.S;
		//private const int E = Directions.E;
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
				return (Random.value>0.5f) ? HORIZONTAL : VERTICAL;
			}
		}
	}
	private void divide(int[,] grid, int x,int y,int width,int height,int orientation) {
		if ((width < 2 )||( height < 2)) {
			return;
		}
			

			
				
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
		Directions dir = horizontal ? Directions.S : Directions.E;

		for(int each=0;each<length;each++ ){
			if ((wx != px) || (wy != py))  {
				grid[wy,wx] |= (int)dir;
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
				maze.fillValue (0);
		maze.drawPerimeter ();
		
				
				
				return maze;
		}
			

			
		public Maze Generate ()
		{
				var cells = maze.cells;
				divide (maze.cells,0,0,maze.width,maze.height,choose_orientation(maze.width, maze.height));

				return maze;
		}
			
		
			

			
		
		
}
