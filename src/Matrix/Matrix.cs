using System;

namespace Matrix
{
	public class Matrix
	{
		public double[,] Elements { get; }
		public int Rows { get; }
		public int Cols { get; }

		public Matrix(int rows, int cols)
		{
			Elements = new double[rows, cols];
			Rows = rows;
			Cols = cols;
		}

		public override string ToString()
		{
			string toRet = "";
			for(int r = 0; r < Rows; r++)
			{
				for(int c = 0; c < Cols; c++)
					toRet += Elements[r, c] + " ";
				toRet = toRet.TrimEnd(' ');
				toRet += '\n';
			}
			return toRet;
		}

		public void Set(int r, int c, double e)
		{
			if(r >= Rows || r < 0 || c >= Cols || c < 0)
				throw new ArgumentOutOfRangeException("Out of matrix bounds exception");
			Elements[r, c] = e;
		}
	}
}
