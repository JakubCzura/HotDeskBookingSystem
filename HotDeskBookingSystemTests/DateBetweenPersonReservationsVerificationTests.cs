
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotDeskBookingSystemTests
{
    public class DateBetweenPersonReservationsVerificationTests
    {
        [Fact]
        public void HaveDeskReservedInThisTimeSpan_DeskDateNotBetweenTwoDates_ReturnFalse()
        {
            //arrange

            Desk UserDesk = new() { ReservationStartDate = new DateTime(2022, 12, 5), ReservationEndDate = new DateTime(2022, 12, 7) };

            List<Desk> UserDesks = new()
            { new Desk() { ReservationStartDate = new DateTime(2022, 12, 10), ReservationEndDate =new DateTime(2022, 12, 12) },
             new Desk() { ReservationStartDate = new DateTime(2022, 12, 14), ReservationEndDate =new DateTime(2022, 12, 16) },
             new Desk() { ReservationStartDate = new DateTime(2022, 12, 18), ReservationEndDate =new DateTime(2022, 12, 19) } };

            //act

            bool result = DateBetweenPersonReservationsVerification.HaveDeskReservedInThisTimeSpan(UserDesk, UserDesks);
            bool expectedResult = false;

            //assert
            Assert.Equal(expectedResult, result);
        }
    }
}
