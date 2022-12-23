namespace HotDeskBookingSystemTests
{
    public class ReservationCancellationTests
    {
        [Fact]
        public void CancelReservation_SelectedPersonDeskIsNull_ReturnsFalse()
        {
            //arrange
            Desk Desk = null;

            //act
            bool result = ReservationCancellation.CancelReservation(Desk);
            bool expectedResult = false;

            //assert
            Assert.Equal(expectedResult, result);
        }
    }
}