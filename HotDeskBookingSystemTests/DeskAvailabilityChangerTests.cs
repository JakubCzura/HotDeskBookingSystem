using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDeskBookingSystemTests
{
    public class DeskAvailabilityChangerTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void ChangeDeskAvailability_ForUserDesk_ReturnsUpdatingResult(bool isReserved)
        {
            //arrange
            Desk Desk = new Desk() { IsReserved = isReserved };

            //act
            bool result = DeskAvailabilityChanger.ChangeDeskAvailability(Desk);
            bool expectedResult = false;

            //assert
            Assert.Equal(expectedResult, result);
        }
    }
}
