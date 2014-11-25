using UnityEngine;
using System.Collections;

//Flags
public enum Directions
{
		N = 1,
		S = 2,
		E = 4,
		W = 8
}

public class Maze
{

		public int[,] cells;
		public int rows;
		public int columns;
		// Use this for initialization
		public Maze ()
		{
		}
		public void create(int newRows,int newColumns) {
			rows = newRows;
			columns = newColumns;
			cells = new int[rows,columns];
		}
		public void fillValue(int newValue) {
			for (int x = 0; x < rows; x++)
			{
				for (int y = 0; y < columns; y++)
				{
					cells[x, y] = newValue;
				}				
			}
		}


		public void fill (int width, int height,int[,] newCells)
		{
				cells = newCells;
		}

		public int contents (int x, int y)
		{
				return cells [x, y];
		}

		public bool hasDirection (int x, int y, Directions flag)
		{
				return ((cells [x, y] & (int)flag) > 0);
		}
}
