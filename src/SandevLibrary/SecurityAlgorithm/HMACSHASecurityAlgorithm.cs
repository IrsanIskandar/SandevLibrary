using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SandevLibrary.SecurityAlgorithm
{
    public class HMACSHASecurityAlgorithm
    {
        private static string password = "BCA631F7F578AE36F7DA2B6EC8B499CD024EAE4860145C1DD69B653EB4B0A438";
        private static string hashKey = "AD6D02790B58903BE64B2B68E6E2AB2A9D91B274B1CD5A4AF825389C6DFA87B4";

        private static String seed = "PKBL_20210526";

        /// <summary>
        /// The default hash algorithm
        /// </summary>
        private const string DefaultHashAlgorithm = "SHA1";

        /// <summary>
        /// The default key size
        /// </summary>
        private const int DefaultKeySize = 256;

        /// <summary>
        /// The maximum allowed salt length
        /// </summary>
        private const int MaxAllowedSaltLen = 255;

        /// <summary>
        /// The minimum allowed salt length
        /// </summary>
        private const int MinAllowedSaltLen = 8;

        /// <summary>
        /// The default minimum salt length
        /// </summary>
        private const int DefaultMinSaltLen = MinAllowedSaltLen;

        /// <summary>
        /// The default maximum salt length
        /// </summary>
        private const int DefaultMaxSaltLen = 12;

        /// <summary>
        /// The _min salt length
        /// </summary>
        private readonly int _minSaltLen = -1;

        /// <summary>
        /// The _max salt length
        /// </summary>
        private readonly int _maxSaltLen = -1;

        /// <summary>
        /// The _encryptor
        /// </summary>
        private readonly ICryptoTransform _encryptor = null;

        /// <summary>
        /// The _decryptor
        /// </summary>
        private readonly ICryptoTransform _decryptor = null;

        public byte[] Salt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Encryption"/> class.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        public HMACSHASecurityAlgorithm(string passPhrase) :
            this(passPhrase, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Encryption"/> class.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="initVector">The initialize vector.</param>
        public HMACSHASecurityAlgorithm(string passPhrase, string initVector) :
            this(passPhrase, initVector, -1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Encryption"/> class.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="initVector">The initialize vector.</param>
        /// <param name="minSaltLen">Minimum length of the salt.</param>
        public HMACSHASecurityAlgorithm(string passPhrase, string initVector, int minSaltLen) :
            this(passPhrase, initVector, minSaltLen, -1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Encryption"/> class.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="initVector">The initialize vector.</param>
        /// <param name="minSaltLen">Minimum length of the salt.</param>
        /// <param name="maxSaltLen">Maximum length of the salt.</param>
        public HMACSHASecurityAlgorithm(string passPhrase, string initVector, int minSaltLen, int maxSaltLen) :
            this(passPhrase, initVector, minSaltLen, maxSaltLen, -1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Encryption"/> class.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="initVector">The initialize vector.</param>
        /// <param name="minSaltLen">Minimum length of the salt.</param>
        /// <param name="maxSaltLen">Maximum length of the salt.</param>
        /// <param name="keySize">Size of the key.</param>
        public HMACSHASecurityAlgorithm(string passPhrase, string initVector, int minSaltLen, int maxSaltLen, int keySize) :
            this(passPhrase, initVector, minSaltLen, maxSaltLen, keySize, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Encryption"/> class.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="initVector">The initialize vector.</param>
        /// <param name="minSaltLen">Minimum length of the salt.</param>
        /// <param name="maxSaltLen">Maximum length of the salt.</param>
        /// <param name="keySize">Size of the key.</param>
        /// <param name="hashAlgorithm">The hash algorithm.</param>
        public HMACSHASecurityAlgorithm(string passPhrase, string initVector, int minSaltLen, int maxSaltLen, int keySize, string hashAlgorithm) :
            this(passPhrase, initVector, minSaltLen, maxSaltLen, keySize, hashAlgorithm, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Encryption"/> class.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="initVector">The initialize vector.</param>
        /// <param name="minSaltLen">Minimum length of the salt.</param>
        /// <param name="maxSaltLen">Maximum length of the salt.</param>
        /// <param name="keySize">Size of the key.</param>
        /// <param name="hashAlgorithm">The hash algorithm.</param>
        /// <param name="saltValue">The salt value.</param>
        public HMACSHASecurityAlgorithm(string passPhrase, string initVector, int minSaltLen, int maxSaltLen, int keySize, string hashAlgorithm, string saltValue) :
            this(passPhrase, initVector, minSaltLen, maxSaltLen, keySize, hashAlgorithm, saltValue, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Encryption"/> class.
        /// </summary>
        /// <param name="passPhrase">The pass phrase.</param>
        /// <param name="initVector">The initialize vector.</param>
        /// <param name="minSaltLen">Minimum length of the salt.</param>
        /// <param name="maxSaltLen">Maximum length of the salt.</param>
        /// <param name="keySize">Size of the key.</param>
        /// <param name="hashAlgorithm">The hash algorithm.</param>
        /// <param name="saltValue">The salt value.</param>
        /// <param name="passwordIterations">The password iterations.</param>
        public HMACSHASecurityAlgorithm(string passPhrase, string initVector, int minSaltLen, int maxSaltLen, int keySize, string hashAlgorithm, string saltValue, int passwordIterations)
        {
            // Save min salt length; set it to default if invalid value is passed.
            if (minSaltLen < MinAllowedSaltLen)
                this._minSaltLen = DefaultMinSaltLen;
            else
                this._minSaltLen = minSaltLen;

            // Save max salt length; set it to default if invalid value is passed.
            if (maxSaltLen < 0 || maxSaltLen > MaxAllowedSaltLen)
                this._maxSaltLen = DefaultMaxSaltLen;
            else
                this._maxSaltLen = maxSaltLen;

            // Set the size of cryptographic key.
            if (keySize <= 0)
                keySize = DefaultKeySize;

            // Set the name of algorithm. Make sure it is in UPPER CASE and does
            // not use dashes, e.g. change "sha-1" to "SHA1".
            if (hashAlgorithm == null)
                hashAlgorithm = DefaultHashAlgorithm;
            else
                hashAlgorithm = hashAlgorithm.ToUpper().Replace("-", "");

            // Initialization vector converted to a byte array.
            byte[] initVectorBytes = null;

            // Salt used for password hashing (to generate the key, not during
            // encryption) converted to a byte array.
            byte[] saltValueBytes = null;

            // Get bytes of initialization vector.
            if (initVector == null)
                initVectorBytes = new byte[0];
            else
                initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            // Get bytes of salt (used in hashing).
            if (saltValue == null)
            {
                saltValueBytes = new byte[8];
                using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                {
                    // Fill the array with a random value.
                    rngCsp.GetBytes(saltValueBytes);

                    var rawSaltValueBytes = saltValueBytes;
                    SHA256 saltSHA256 = SHA256Managed.Create();
                    byte[] saltHash = saltSHA256.ComputeHash(saltValueBytes);
                    Console.WriteLine(String.Format("saltHash {0}", ByteToString(saltHash)));


                    saltValueBytes = Encoding.ASCII.GetBytes(ByteToString(saltHash));

                }
            }
            else
                saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Generate password, which will be used to derive the key.
            //PasswordDeriveBytes password = new PasswordDeriveBytes(
            //                                           passPhrase,
            //                                           saltValueBytes,
            //                                           hashAlgorithm,
            //                                           passwordIterations);
            this.Salt = saltValueBytes;
            Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, saltValueBytes);

            // Convert key to a byte array adjusting the size from bits to bytes.
            byte[] keyBytes = password.GetBytes(keySize / 8);

            // Initialize Rijndael key object.
            RijndaelManaged symmetricKey = new RijndaelManaged();

            // If we do not have initialization vector, we cannot use the CBC mode.
            // The only alternative is the ECB mode (which is not as good).
            if (initVectorBytes.Length == 0)
                symmetricKey.Mode = CipherMode.ECB;
            else
                symmetricKey.Mode = CipherMode.CBC;

            // Create encryptor and decryptor, which we will use for cryptographic
            // operations.
            _encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            _decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        }

        /// <summary>
        /// Encrypts a string value generating a base64-encoded string.
        /// </summary>
        /// <param name="plainText">
        /// Plain text string to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a base64-encoded string.
        /// </returns>
        public string Encrypt(string plainText)
        {
            return Encrypt(Encoding.UTF8.GetBytes(plainText));
        }

        /// <summary>
        /// Encrypts a byte array generating a base64-encoded string.
        /// </summary>
        /// <param name="plainTextBytes">
        /// Plain text bytes to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a base64-encoded string.
        /// </returns>
        public string Encrypt(byte[] plainTextBytes)
        {
            return Convert.ToBase64String(EncryptToBytes(plainTextBytes));
        }

        /// <summary>
        /// Encrypts a string value generating a byte array of cipher text.
        /// </summary>
        /// <param name="plainText">
        /// Plain text string to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a byte array.
        /// </returns>
        public byte[] EncryptToBytes(string plainText)
        {
            return EncryptToBytes(Encoding.UTF8.GetBytes(plainText));
        }

        /// <summary>
        /// Encrypts a byte array generating a byte array of cipher text.
        /// </summary>
        /// <param name="plainTextBytes">
        /// Plain text bytes to be encrypted.
        /// </param>
        /// <returns>
        /// Cipher text formatted as a byte array.
        /// </returns>
        public byte[] EncryptToBytes(byte[] plainTextBytes)
        {
            // Add salt at the beginning of the plain text bytes (if needed).
            byte[] plainTextBytesWithSalt = AddSalt(plainTextBytes);

            // Encryption will be performed using memory stream.
            MemoryStream memoryStream = new MemoryStream();

            // Let's make cryptographic operations thread-safe.
            lock (this)
            {
                // To perform encryption, we must use the Write mode.
                CryptoStream cryptoStream = new CryptoStream(memoryStream, _encryptor, CryptoStreamMode.Write);

                // Start encrypting data.
                cryptoStream.Write(plainTextBytesWithSalt,
                                    0,
                                   plainTextBytesWithSalt.Length);

                // Finish the encryption operation.
                cryptoStream.FlushFinalBlock();

                // Move encrypted data from memory into a byte array.
                byte[] cipherTextBytes = memoryStream.ToArray();

                // Close memory streams.
                memoryStream.Close();
                cryptoStream.Close();

                // Return encrypted data.
                return cipherTextBytes;
            }
        }



        /// <summary>
        /// Decrypts a base64-encoded cipher text value generating a string result.
        /// </summary>
        /// <param name="cipherText">
        /// Base64-encoded cipher text string to be decrypted.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        public string Decrypt(string cipherText)
        {
            return Decrypt(Convert.FromBase64String(cipherText));
        }

        /// <summary>
        /// Decrypts a byte array containing cipher text value and generates a
        /// string result.
        /// </summary>
        /// <param name="cipherTextBytes">
        /// Byte array containing encrypted data.
        /// </param>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        public string Decrypt(byte[] cipherTextBytes)
        {
            return Encoding.UTF8.GetString(DecryptToBytes(cipherTextBytes));
        }

        /// <summary>
        /// Decrypts a base64-encoded cipher text value and generates a byte array
        /// of plain text data.
        /// </summary>
        /// <param name="cipherText">
        /// Base64-encoded cipher text string to be decrypted.
        /// </param>
        /// <returns>
        /// Byte array containing decrypted value.
        /// </returns>
        public byte[] DecryptToBytes(string cipherText)
        {
            return DecryptToBytes(Convert.FromBase64String(cipherText));
        }

        /// <summary>
        /// Decrypts a base64-encoded cipher text value and generates a byte array
        /// of plain text data.
        /// </summary>
        /// <param name="cipherTextBytes">
        /// Byte array containing encrypted data.
        /// </param>
        /// <returns>
        /// Byte array containing decrypted value.
        /// </returns>
        public byte[] DecryptToBytes(byte[] cipherTextBytes)
        {
            byte[] decryptedBytes = null;
            byte[] plainTextBytes = null;
            int decryptedByteCount = 0;
            int saltLen = 0;

            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Since we do not know how big decrypted value will be, use the same
            // size as cipher text. Cipher text is always longer than plain text
            // (in block cipher encryption), so we will just use the number of
            // decrypted data byte after we know how big it is.
            decryptedBytes = new byte[cipherTextBytes.Length];

            // Let's make cryptographic operations thread-safe.
            lock (this)
            {
                // To perform decryption, we must use the Read mode.
                CryptoStream cryptoStream = new CryptoStream(memoryStream, _decryptor, CryptoStreamMode.Read);

                // Decrypting data and get the count of plain text bytes.
                decryptedByteCount = cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                // Release memory.
                memoryStream.Close();
                cryptoStream.Close();
            }

            // If we are using salt, get its length from the first 4 bytes of plain
            // text data.
            if (_maxSaltLen > 0 && _maxSaltLen >= _minSaltLen)
            {
                saltLen = (decryptedBytes[0] & 0x03) | (decryptedBytes[1] & 0x0c) | (decryptedBytes[2] & 0x30) | (decryptedBytes[3] & 0xc0);
            }

            // Allocate the byte array to hold the original plain text (without salt).
            plainTextBytes = new byte[decryptedByteCount - saltLen];

            // Copy original plain text discarding the salt value if needed.
            Array.Copy(decryptedBytes, saltLen, plainTextBytes,
                        0, decryptedByteCount - saltLen);

            // Return original plain text value.
            return plainTextBytes;
        }

        /// <summary>
        /// Adds the salt.
        /// </summary>
        /// <param name="plainTextBytes">The plain text bytes.</param>
        /// <returns></returns>
        private byte[] AddSalt(byte[] plainTextBytes)
        {
            // The max salt value of 0 (zero) indicates that we should not use 
            // salt. Also do not use salt if the max salt value is smaller than
            // the min value.
            if (_maxSaltLen == 0 || _maxSaltLen < _minSaltLen)
                return plainTextBytes;

            // Generate the salt.
            byte[] saltBytes = GenerateSalt();

            // Allocate array which will hold salt and plain text bytes.
            byte[] plainTextBytesWithSalt = new byte[plainTextBytes.Length +
                                                     saltBytes.Length];
            // First, copy salt bytes.
            Array.Copy(saltBytes, plainTextBytesWithSalt, saltBytes.Length);

            // Append plain text bytes to the salt value.
            Array.Copy(plainTextBytes, 0,
                        plainTextBytesWithSalt, saltBytes.Length,
                        plainTextBytes.Length);

            return plainTextBytesWithSalt;
        }

        /// <summary>
        /// Generates the salt.
        /// </summary>
        /// <returns></returns>
        private byte[] GenerateSalt()
        {
            // We don't have the length, yet.
            int saltLen = 0;

            // If min and max salt values are the same, it should not be random.
            if (_minSaltLen == _maxSaltLen)
                saltLen = _minSaltLen;
            // Use random number generator to calculate salt length.
            else
                saltLen = GenerateRandomNumber(_minSaltLen, _maxSaltLen);

            // Allocate byte array to hold our salt.
            byte[] salt = new byte[saltLen];

            // Populate salt with cryptographically strong bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

            rng.GetNonZeroBytes(salt);

            // Split salt length (always one byte) into four two-bit pieces and
            // store these pieces in the first four bytes of the salt array.
            salt[0] = (byte)((salt[0] & 0xfc) | (saltLen & 0x03));
            salt[1] = (byte)((salt[1] & 0xf3) | (saltLen & 0x0c));
            salt[2] = (byte)((salt[2] & 0xcf) | (saltLen & 0x30));
            salt[3] = (byte)((salt[3] & 0x3f) | (saltLen & 0xc0));

            return salt;
        }


        /// <summary>
        /// Generates the random number.
        /// </summary>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <returns></returns>
        private int GenerateRandomNumber(int minValue, int maxValue)
        {
            // We will make up an integer seed from 4 bytes of this array.
            byte[] randomBytes = new byte[4];

            // Generate 4 random bytes.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            // Convert four random bytes into a positive integer value.
            int seed = ((randomBytes[0] & 0x7f) << 24) | (randomBytes[1] << 16) | (randomBytes[2] << 8) | (randomBytes[3]);

            // Now, this looks more like real randomization.
            Random random = new Random(seed);

            // Calculate a random number.
            return random.Next(minValue, maxValue + 1);
        }

        public static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }

            return (sbinary);
        }

        public static string EncryptHMACAlgorithm(string plainText)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(hashKey);

            byte[] messageBytes = encoding.GetBytes(seed);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            String cryptoInitilizer = ByteToString(hashmessage);

            String IV = cryptoInitilizer.Substring(0, 16);
            String salt = cryptoInitilizer.Substring(cryptoInitilizer.Length - 32, 32);

            string hashAlgo = string.Concat(SHAType.SHAKE256.ToString());
            HMACSHASecurityAlgorithm rijndaelKey = new HMACSHASecurityAlgorithm(password, IV, 64, 192, 256, hashAlgo, salt, 1000);
            var ciphertext = rijndaelKey.Encrypt(plainText);

            return ciphertext;
        }

        public static string DecryptHMACAlgorithm(string cipherText)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(hashKey);

            byte[] messageBytes = encoding.GetBytes(seed);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            String cryptoInitilizer = ByteToString(hashmessage);

            String IV = cryptoInitilizer.Substring(0, 16);
            String salt = cryptoInitilizer.Substring(cryptoInitilizer.Length - 32, 32);

            string hashAlgo = string.Concat(SHAType.SHAKE256.ToString());
            HMACSHASecurityAlgorithm rijndaelKey2 = new HMACSHASecurityAlgorithm(password, IV, 64, 192, 256, hashAlgo, salt, 1000);
            var cleartext = rijndaelKey2.Decrypt(cipherText);

            return cleartext;
        }

        public static void UseHMACAlgorithm(string plainText)
        {
            System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(hashKey);

            byte[] messageBytes = encoding.GetBytes(seed);

            HMACSHA256 hmacsha256 = new HMACSHA256(keyByte);
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            String cryptoInitilizer = ByteToString(hashmessage);

            Console.WriteLine(String.Format("cryptoInitilizer {0} length {1} ", cryptoInitilizer, cryptoInitilizer.Length));

            String IV = cryptoInitilizer.Substring(0, 16);

            Console.WriteLine(String.Format("  IV: {0} (Length: {1})", IV, IV.Length));


            String salt = cryptoInitilizer.Substring(cryptoInitilizer.Length - 32, 32);

            Console.WriteLine(String.Format("salt: {0} (Length: {1})", salt, salt.Length));

            //HMACSHASecurityAlgorithm rijndaelKey = new HMACSHASecurityAlgorithm(password, IV, 64, 192, 256, string.Concat(SHAType.SHA.ToString(), "-", NumberOfSHA.TwoHundredFiftySix.ToString()), salt, 1000);
            //HMACSHASecurityAlgorithm rijndaelKey = new HMACSHASecurityAlgorithm(password, IV, 64, 192, 256, string.Concat(SHAType.SHA.ToString(), "-", NumberOfSHA.TwoHundredFiftySix.ToString()), salt);
            HMACSHASecurityAlgorithm rijndaelKey = new HMACSHASecurityAlgorithm(password, IV, 64, 192, 256, string.Concat(SHAType.SHA.ToString(), "-", NumberOfSHA.TwoHundredFiftySix.ToString()));

            var ciphertext = rijndaelKey.Encrypt(plainText);
            Console.WriteLine("Encrypted Text");
            Console.WriteLine(ciphertext);
            Console.WriteLine();

            //HMACSHASecurityAlgorithm rijndaelKey2 = new HMACSHASecurityAlgorithm(password, IV, 64, 192, 256, string.Concat(SHAType.SHA.ToString(), "-", NumberOfSHA.TwoHundredFiftySix.ToString()), salt, 1000);
            //HMACSHASecurityAlgorithm rijndaelKey2 = new HMACSHASecurityAlgorithm(password, IV, 64, 192, 256, string.Concat(SHAType.SHA.ToString(), "-", NumberOfSHA.TwoHundredFiftySix.ToString()), salt);
            HMACSHASecurityAlgorithm rijndaelKey2 = new HMACSHASecurityAlgorithm(password, IV, 64, 192, 256, string.Concat(SHAType.SHA.ToString(), "-", NumberOfSHA.TwoHundredFiftySix.ToString()));
            var cleartext = rijndaelKey2.Decrypt(ciphertext);
            Console.WriteLine("Decrypted Text");
            Console.WriteLine(cleartext);
        }

        public static string SignatureHash(string yourSecretKey, string yourMessage)
        {
            // change according to your needs, an UTF8Encoding
            // could be more suitable in certain situations
            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] secretBytes = encoding.GetBytes(yourSecretKey);
            byte[] MessageBytes = encoding.GetBytes(yourMessage);

            byte[] hashBytes;

            using (HMACSHA256 hash = new HMACSHA256(secretBytes))
                hashBytes = hash.ComputeHash(MessageBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
    }

    public enum SHAType
    {
        SHA,
        SHA3,
        SHAKE128,
        SHAKE256
    }

    public enum NumberOfSHA
    {
        zero = 0,
        One = 1,
        Two = 2,
        Three = 3,
        OneHundredTwentyEight = 128,
        OneHundredSixty = 160,
        TwoHundredTwentyFour = 224,
        TwoHundredFiftySix = 256,
        ThreeHundredEightyFour = 384,
        FiveHundredTwelve = 512
    }
}
