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
	}
}
