using Microsoft.AspNetCore.Identity;

namespace LmsApi.Helpers
{
    public static class PasswordHelper
    {
        private static PasswordHasher<string> _hasher = new();

        public static string Hash(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return "Password cannot be null or empty";
            }
            return _hasher.HashPassword(null, password);
        }

        public static bool VerifyPassword(string hashedPassword, string inputPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword) || string.IsNullOrEmpty(inputPassword))
            {
                return false;
            }
            var result = _hasher.VerifyHashedPassword(null, hashedPassword, inputPassword);//returns a passwordVerificationResult which can be Success, Failed, or SuccessRehashNeeded
            return result == PasswordVerificationResult.Success;
        }

        //so what is the idea used in this hashing?
        //the idea is to use a secure hashing algorithm to hash the password and store it in the database
        //when the user logs in, we hash the input password and compare it with the hashed password stored in the database
        //if they match, the user is authenticated
        //if they don't match, the user is not authenticated
        //this way, we don't store the password in plain text in the database, which is a security risk
        //the PasswordHasher class uses a secure hashing algorithm (PBKDF2) to hash the password
        //it also adds a salt to the password before hashing it, which makes it more secure
        //the salt is a random value that is added to the password before hashing it
        //the salt is stored with the hashed password in the database, so that it can be used to hash the input password when the user logs in
        //the PasswordHasher class also uses a work factor to determine how many iterations of the hashing algorithm to use
        //the work factor is a value that determines how long it takes to hash the password
        //the higher the work factor, the longer it takes to hash the password, which makes it more secure
        //the PasswordHasher class uses a default work factor of 10, which is a good balance between security and performance
        //you can change the work factor by passing a value to the PasswordHasher constructor
        //the PasswordHasher class also uses a default salt size of 16 bytes, which is a good size for the salt
        //you can change the salt size by passing a value to the PasswordHasher constructor
        //the PasswordHasher class also uses a default hash size of 32 bytes, which is a good size for the hash
        //you can change the hash size by passing a value to the PasswordHasher constructor
        //the PasswordHasher class also uses a default hash algorithm of SHA256, which is a good algorithm for hashing passwords
        //you can change the hash algorithm by passing a value to the PasswordHasher constructor
        //the PasswordHasher class also uses a default hash format of Base64, which is a good format for storing hashed passwords
        //you can change the hash format by passing a value to the PasswordHasher constructor
       

    }
}
