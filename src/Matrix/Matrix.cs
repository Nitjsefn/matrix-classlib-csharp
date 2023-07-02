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

		public static Matrix IdentityMatrixPreset(int n)
		{
			var m = new Matrix(n, n);
			for(int i = 0; i < n; i++)
			{
				m[i, i] = 1;
			}
			return m;
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

		public Matrix Add(Matrix m)
		{
			var sum = new Matrix(this);
			if(m.Cols != sum.Cols || m.Rows != sum.Rows)
				throw new ArgumentException("Matrices size not equal");
			for(int r = 0; r < sum.Rows; r++)
				for(int c = 0; c < sum.Cols; c++)
					sum[r, c] += m[r, c];
			return sum;
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

		public double Determinant()
		{
			if(Rows != Cols) throw new Exception("Matrix is not a square");
			if(Rows == 0 || Rows == 1) throw new Exception("Matrix is to small to perform upper triangular transformation");

			double det = 1;
			bool transformed = true;
			for(int r = 1; r < Rows && transformed; r++)
				for(int c = 0; c < r && transformed; c++)
					if(Elements[r, c] != 0) transformed = false;

			if(transformed)
			{
				for(int i = 0; i < Rows; i++)
					det *= Elements[i, i];
				return det;
			}

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
					det *= -1;
				}
				for(int crntRow = refRow + 1; crntRow < m.Rows; crntRow++)
				{
					if(m[crntRow, colToReset] == 0) continue;
					double subScale = m[crntRow, colToReset] / m[refRow, colToReset];
					for(int c = colToReset; c < m.Cols; c++)
						m[crntRow, c] -= subScale * m[refRow, c];
				}
			}

			for(int i = 0; i < Rows; i++)
				det *= m[i, i];
			return det;
		}

		public Matrix Invert()
		{
			if(Rows != Cols) throw new Exception("Matrix is not a square");
			if(this.Determinant() == 0) throw new Exception("Matrix does not have inverted form");

			var invertedMatrix = Matrix.IdentityMatrixPreset(Rows);
			var m = new Matrix(this);
			for(int diagonalIndx = 0; diagonalIndx < m.Cols; diagonalIndx++)
			{
				if(m[diagonalIndx, diagonalIndx] == 0)
				{
					int not0Indx = diagonalIndx + 1;
					while(not0Indx < m.Rows && m[not0Indx, diagonalIndx] == 0) not0Indx++;
					if(not0Indx == m.Rows) throw new Exception("Matrix does not have inverted form");
					m.SwapRows(not0Indx, diagonalIndx);
					invertedMatrix.SwapRows(not0Indx, diagonalIndx);
				}
				for(int r = 0; r < m.Rows; r++)
				{
					if(r == diagonalIndx) continue;
					double subScale = m[r, diagonalIndx] / m[diagonalIndx, diagonalIndx];
					for(int c = 0; c < m.Cols; c++)
					{
						m[r, c] -= subScale * m[diagonalIndx, c];
						invertedMatrix[r, c] -= subScale * invertedMatrix[diagonalIndx, c];
					}
				}
			}
			for(int diagonalIndx = 0; diagonalIndx < m.Cols; diagonalIndx++)
			{
				double diagScale = 1 / m[diagonalIndx, diagonalIndx];
				for(int c = 0; c < m.Cols; c++)
				{
					m[diagonalIndx, c] *= diagScale;
					invertedMatrix[diagonalIndx, c] *= diagScale;
				}
			}
			return invertedMatrix;
		}

		public Matrix Multiply(Matrix m2)
		{
			if(this.Cols != m2.Rows) throw new ArgumentException("Matrices have incompatible size to multiply");
			var product = new Matrix(this.Rows, m2.Cols);
			for(int r = 0; r < product.Rows; r++)
			{
				for(int c = 0; c < product.Cols; c++)
				{
					double sum = 0;
					for(int i = 0; i < this.Cols; i++)
						sum += Elements[r, i] * m2[i, c];
					product[r, c] = sum;
				}
			}
			return product;
		}

		public Matrix Multiply(double k)
		{
			var m = new Matrix(this);
			for(int r = 0; r < Rows; r++)
				for(int c = 0; c < Cols; c++)
					m[r, c] *= k;
			return m;
		}
	}
}
