using SonglistGenerator;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace SonglistGeneratorTests
{
    public class CaretsWrapperTests
    {
        [Theory]
        [ClassData(typeof(CaretsWrapperTestData))]
        [SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters", Justification = "This is description of input time, useful to easily find failing case.")]
        public void CaretsWrapper_ShouldWrapAllCaretsInSongs(string _, string input, string expectedOutput)
        {
            // act
            var result = CaretsWrapper.WrapCarets(input);

            // assert
            Assert.Equal(expectedOutput, result);
        }
    }
}
