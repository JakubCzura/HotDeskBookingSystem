using HotDeskBookingSystem.Validators;

namespace HotDeskBookingSystemTests
{
    public class DeskValidatorTests
    {
        [Theory]
        [InlineData("Funny desk", "Office", 1)]
        [InlineData("Big desk", "Small office", 2)]
        [InlineData("Small desk", "Big office", 3)]
        public void Validate_SampleDeskNames_IfDataIsCorrect(string name, string locationName, int locationId)
        {
            //arrange
            DeskValidator Validator = new();
            Desk Desk = new() { Name = name, LocationName = locationName, LocationId = locationId };

            //act
            bool result = Validator.Validate(Desk);
            bool expectedResult = true;

            //assert
            Assert.Equal(expectedResult, result);
        }
    }
}