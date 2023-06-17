using BattleshipGame.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipGame.Tests.Services
{
    public class CoordinateTranslatorTests
    {
        [Theory]
        [InlineData("A1", 0, 0)]
        [InlineData("D5", 3, 4)]
        [InlineData("Z100", 25, 99)]
        void TranslateCoordinates_WhenInputIsValid_ShouldReturnCoordinates(string input, int x, int y)
        {
            var coordinateTranslator = new CoordinateTranslator();

            var result = coordinateTranslator.TranslateCoordinates(input);

            result.X.Should().Be(x);
            result.Y.Should().Be(y);
        }

        [Theory]
        [InlineData(".1")]
        [InlineData("d5")]
        [InlineData("b")]
        [InlineData("6")]
        [InlineData("")]
        [InlineData("test")]
        void TranslateCoordinates_WhenInputIsInvalid_ShouldReturnCoordinates(string input)
        {
            var coordinateTranslator = new CoordinateTranslator();

            var translation = () => coordinateTranslator.TranslateCoordinates(input);

            translation.Should().Throw<ArgumentException>("Invalid coordinates");
        }
    }
}
