namespace HotDeskBookingSystem.DataBase
{
    public class Hasher
    {
        /// <summary>
        /// Verify user's password
        /// </summary>
        /// <param name="password">Password as string</param>
        /// <param name="hash">Hashed password</param>
        /// <returns>True if password matches hashed password, otherwise false</returns>
        public static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        /// <summary>
        /// Hash password
        /// </summary>
        /// <param name="password">Password as string</param>
        /// <returns>Hashed password</returns>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}