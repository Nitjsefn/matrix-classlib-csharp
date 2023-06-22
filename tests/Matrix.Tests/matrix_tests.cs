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
	}
}
