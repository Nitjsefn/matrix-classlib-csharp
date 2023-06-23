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

		public Matrix(Matrix m)
		{
			Elements = (double[,])m.Elements.Clone();
			Rows = m.Rows;
			Cols = m.Cols;
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

		public void Add(Matrix m)
		{
			if(m.Cols != this.Cols || m.Rows != this.Rows)
				throw new ArgumentException("Matrices not equal");
			for(int r = 0; r < Rows; r++)
				for(int c = 0; c < Cols; c++)
					Elements[r, c] += m.Elements[r, c];
		}
	}
}
