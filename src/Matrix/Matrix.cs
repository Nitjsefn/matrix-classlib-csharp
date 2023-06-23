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

		public double this[int r, int c]
		{
			get { return Elements[r, c]; }
			set { Elements[r, c] = value; }
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

		public void SwapRows(int r1, int r2)
		{
			double temp;
			for(int i = 0; i < Cols; i++)
			{
				temp = Elements[r1, i];
				Elements[r1, i] = Elements[r2, i];
				Elements[r2, i] = temp;
			}
		}

		public void SwapCols(int c1, int c2)
		{
			double temp;
			for(int i = 0; i < Rows; i++)
			{
				temp = Elements[i, c1];
				Elements[i, c1] = Elements[i, c2];
				Elements[i, c2] = temp;
			}
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
