using Xunit;
using Matrix;

namespace Matrix.Tests
{
	public class matrix_tests
	{
		[Fact]
		public void converts_to_string()
		{
			const string outputStr = "0 0 0\n0 0 0\n0 0 0\n";
			int rows = 3;
			int cols = 3;
			var m = new Matrix(rows, cols);
			Assert.Equal(outputStr, m.ToString());
		}

		[Fact]
		public void set_values_into_matrix()
		{
			const string outputStr = "1 0 0\n0 0 0\n0 9 0\n";
			int rows = 3;
			int cols = 3;
			var m = new Matrix(rows, cols);
			m.Set(0, 0, 1);
			m.Set(2, 1, 9);
			Assert.Equal(outputStr, m.ToString());
		}

		[Fact]
		public void set_out_of_range_values()
		{
			int rows = 3;
			int cols = 3;
			var m = new Matrix(rows, cols);
			try
			{
				m.Set(0, 3, 1);
				m.Set(2, 1, 9);
				Assert.Fail("Out of range Set does not throw exception");
			}
			catch
			{}
		}

		[Fact]
		public void adds_another_matrix()
		{
			int rows = 3;
			int cols = 3;
			var m = new Matrix(rows, cols);
			m.Set(0, 0, 1);
			m.Set(2, 1, 1);
			var m2 = new Matrix(rows, cols);
			m2.Set(0, 0, 1);
			m2.Set(2, 1, 8);
			var mCorrect = new Matrix(rows, cols);
			mCorrect.Set(0, 0, 2);
			mCorrect.Set(2, 1, 9);
			m.Add(m2);
			Assert.Equal(mCorrect.Elements, m.Elements);
		}

		[Fact]
		public void throws_error_on_wrong_added_matrix_size()
		{
			int rows = 3;
			int cols = 3;
			var m = new Matrix(rows, cols);
			var m2 = new Matrix(rows+1, cols);
			try
			{
				m.Add(m2);
				Assert.Fail("Out of range Add does not throw exception");
			}
			catch
			{}
		}

		[Fact]
		public void copy_constructor()
		{
			var m1 = new Matrix(2, 2);
			var m2 = new Matrix(m1);
			m2.Elements[0, 0] = 12;
			Assert.NotEqual(m1.Elements, m2.Elements);
		}

		[Fact]
		public void indexer_set_and_get()
		{
			double a = 12.3;
			Matrix m = new Matrix(2, 2);
			m[0, 0] = a;
			Assert.Equal(a, m[0, 0]);
			Assert.Equal(0D, m[0, 1]);
		}

		[Fact]
		public void swaps_rows()
		{
			int rows = 3;
			int cols = 2;
			var m = new Matrix(rows, cols);
			for(int i = 0; i < rows; i++)
				for(int j = 0; j < cols; j++)
					m[i, j] = i+1;
			m.SwapRows(0, 2);
			Assert.Equal(3, m[0, 0]);
			Assert.Equal(3, m[0, 1]);
			Assert.Equal(1, m[2, 0]);
			Assert.Equal(1, m[2, 1]);
		}
		
		[Fact]
		public void swaps_cols()
		{
			int rows = 2;
			int cols = 3;
			var m = new Matrix(rows, cols);
			for(int i = 0; i < rows; i++)
				for(int j = 0; j < cols; j++)
					m[i, j] = j+1;
			m.SwapCols(0, 2);
			Assert.Equal(3, m[0, 0]);
			Assert.Equal(3, m[1, 0]);
			Assert.Equal(1, m[0, 2]);
			Assert.Equal(1, m[1, 2]);
		}

		[Fact]
		public void constructs_from_2d_array()
		{
			double[,] arr = {{1, 2}, {3, 4}, {5, 6}};
			var m = new Matrix(arr);
			Assert.Equal(arr, m.Elements);
			Assert.Equal(arr[1, 1], m[1, 1]);
			m[1, 1] = 36;
			Assert.NotEqual(arr[1, 1], m[1, 1]);
			Assert.Equal(3, m.Rows);
			Assert.Equal(2, m.Cols);
		}
		
		[Fact]
		public void transforms_matrix_into_upper_triangular()
		{
			int rows = 3;
			int cols = 3;
			var m = new Matrix(rows, cols);
			m.Set(0, 0, 1);
			m.Set(0, 1, 1);
			m.Set(0, 2, 1);
			m.Set(1, 0, 1);
			m.Set(1, 1, 2);
			m.Set(1, 2, 1);
			m.Set(2, 0, 3);
			m.Set(2, 1, 3);
			m.Set(2, 2, 1);
			m = m.UpperTriangularTransform();
			Assert.Equal(0, m.Elements[1, 0]);
			Assert.Equal(0, m.Elements[2, 0]);
			Assert.Equal(0, m.Elements[2, 1]);
			double product = m.Elements[0, 0] * m.Elements[1, 1] * m.Elements[2, 2];
			Assert.Equal(-2, product);
		}

		[Fact]
		public void throws_error_on_wrong_size_to_upper_triangular_transform()
		{
			int rows = 3;
			int cols = 4;
			var m = new Matrix(rows, cols);
			try
			{
				m.UpperTriangularTransform();
				Assert.Fail("Out of range Add does not throw exception");
			}
			catch
			{}
		}

		[Fact]
		public void transposes_3x3_itself()
		{
			double[,] arr = {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}};
			double[,] correctArr = {{1, 4, 7}, {2, 5, 8}, {3, 6, 9}};
			var m = new Matrix(arr);
			Assert.Equal(correctArr, m.Transpose().Elements);
		}

		[Fact]
		public void transposes_3x2_itself()
		{
			double[,] arr = {{1, 2}, {3, 4}, {5, 6}};
			double[,] correctArr = {{1, 3, 5}, {2, 4, 6}};
			var m = new Matrix(arr);
			Assert.Equal(correctArr, m.Transpose().Elements);
		}

		[Fact]
		public void transforms_matrix_into_lower_triangular()
		{
			double[,] arr = {{1, 1, 1}, {1, 2, 1}, {3 ,3, 1}};
			var m = new Matrix(arr);
			m = m.LowerTriangularTransform();
			Assert.Equal(0, m.Elements[0, 1]);
			Assert.Equal(0, m.Elements[0, 2]);
			Assert.Equal(0, m.Elements[1, 2]);
			double product = m.Elements[0, 0] * m.Elements[1, 1] * m.Elements[2, 2];
			Assert.Equal(-2, product);
		}
	}
}
