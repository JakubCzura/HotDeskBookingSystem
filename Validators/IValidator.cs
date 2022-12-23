namespace HotDeskBookingSystem.Validators
{
    public interface IValidator<T>
    {
        /// <summary>
        /// Validate object's data
        /// </summary>
        /// <param name="value">Object to validate</param>
        /// <returns>True if data is correct, otherwise false</returns>
        bool Validate(T value);
    }
}