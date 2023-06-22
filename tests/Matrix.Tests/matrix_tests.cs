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
	}
}
