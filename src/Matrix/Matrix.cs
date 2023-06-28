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

		public Matrix(double[,] arr)
		{
			Elements = (double[,])arr.Clone();
			Rows = Elements.GetLength(0);
			Cols = Elements.GetLength(1);
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

		public Matrix Transpose()
		{
			var m = new Matrix(Cols, Rows);
			for(int r = 0; r < Rows; r++)
				for(int c = 0; c < Cols; c++)
					m[c, r] = Elements[r, c];
			return m;
		}

		public Matrix UpperTriangularTransform()
		{
			if(Rows != Cols) throw new Exception("Matrix is not a square");
			if(Rows == 0 || Rows == 1) throw new Exception("Matrix is to small to perform upper triangular transformation");

			bool transformed = true;
			for(int r = 1; r < Rows && transformed; r++)
				for(int c = 0; c < r && transformed; c++)
					if(Elements[r, c] != 0) transformed = false;
			if(transformed) return new Matrix(this);

			var m = new Matrix(this);
			for(int refRow = 0; refRow < m.Rows - 1; refRow++)
			{
				int colToReset = refRow;
				if(m[refRow, colToReset] == 0)
				{
					int i = refRow + 1;
					while(i < m.Rows && m[i, refRow] == 0) i++;
					if(i == m.Rows) continue;
					m.SwapRows(refRow, i);
				}
				for(int crntRow = refRow + 1; crntRow < m.Rows; crntRow++)
				{
					if(m[crntRow, colToReset] == 0) continue;
					double subScale = m[crntRow, colToReset] / m[refRow, colToReset];
					for(int c = colToReset; c < m.Cols; c++)
						m[crntRow, c] -= subScale * m[refRow, c];
				}
			}
			return m;
		}

		public Matrix LowerTriangularTransform()
		{
			if(Rows != Cols) throw new Exception("Matrix is not a square");
			if(Rows == 0 || Rows == 1) throw new Exception("Matrix is to small to perform lower triangular transformation");

			bool transformed = true;
			for(int r = 0; r < Rows - 1 && transformed; r++)
				for(int c = r+1; c < Cols && transformed; c++)
					if(Elements[r, c] != 0) transformed = false;
			if(transformed) return new Matrix(this);

			var m = new Matrix(this);
			for(int refRow = m.Rows - 1; refRow >= 0; refRow--)
			{
				int colToReset = refRow;
				if(m[refRow, colToReset] == 0)
				{
					int i = refRow - 1;
					while(i >= 0  && m[i, refRow] == 0) i--;
					if(i == -1) continue;
					m.SwapRows(refRow, i);
				}
				for(int crntRow = refRow - 1; crntRow >= 0; crntRow--)
				{
					if(m[crntRow, colToReset] == 0) continue;
					double subScale = m[crntRow, colToReset] / m[refRow, colToReset];
					for(int c = colToReset; c >= 0; c--)
						m[crntRow, c] -= subScale * m[refRow, c];
				}
			}
			return m;
		}
	}
}
