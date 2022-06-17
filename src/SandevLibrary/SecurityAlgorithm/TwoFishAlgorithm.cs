using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Text;

namespace SandevLibrary.SecurityAlgorithm
{
    public class TwoFishAlgorithm
    {
        private readonly Encoding _encoding;
        private readonly IBlockCipher _blockCipher;
        private PaddedBufferedBlockCipher _cipher;
        private IBlockCipherPadding _padding;

        public TwoFishAlgorithm() { }

        TwoFishAlgorithm(IBlockCipher blockCipher, Encoding encoding)
        {
            _blockCipher = blockCipher;
            _encoding = encoding;
        }

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TextPlain"></param>
        /// <param name="Password"></param>
        /// <param name="Salt"></param>
        /// <returns></returns>
        public static string TwoFishEncryption(string TextPlain, string Password, byte[] Salt)
        {
            Sha3Digest Sha3Digest = new Sha3Digest();
            Pkcs5S2ParametersGenerator gen = new Pkcs5S2ParametersGenerator(Sha3Digest);
            gen.Init(Encoding.UTF8.GetBytes(Password), Salt, 1000);
            KeyParameter param = (KeyParameter)gen.GenerateDerivedParameters(new TwofishEngine().AlgorithmName, 256);

            TwoFishAlgorithm bcEngine = new TwoFishAlgorithm(new TwofishEngine(), Encoding.UTF8);
            bcEngine.SetPadding(new Pkcs7Padding());
            return bcEngine.Encrypt(TextPlain, param);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TextEncripted"></param>
        /// <param name="Password"></param>
        /// <param name="Salt"></param>
        /// <returns></returns>
        public static string TwoFishDecryption(string TextEncripted, string Password, byte[] Salt)
        {
            Sha3Digest Sha3Digest = new Sha3Digest();
            Pkcs5S2ParametersGenerator gen = new Pkcs5S2ParametersGenerator(Sha3Digest);
            gen.Init(Encoding.UTF8.GetBytes(Password), Salt, 1000);
            KeyParameter param = (KeyParameter)gen.GenerateDerivedParameters(new TwofishEngine().AlgorithmName, 256);

            TwoFishAlgorithm bcEngine = new TwoFishAlgorithm(new TwofishEngine(), Encoding.UTF8);
            bcEngine.SetPadding(new Pkcs7Padding());
            return bcEngine.Decrypt(TextEncripted, param);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="padding"></param>
        void SetPadding(IBlockCipherPadding padding)
        {
            if (padding != null)
                _padding = padding;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plain"></param>
        /// <param name="SetKeyParameter"></param>
        /// <returns></returns>
        string Encrypt(string plain, ICipherParameters SetKeyParameter)
        {
            byte[] result = BouncyCastleCrypto(true, _encoding.GetBytes(plain), SetKeyParameter);
            return Convert.ToBase64String(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cipher"></param>
        /// <param name="SetKeyParameter"></param>
        /// <returns></returns>
        string Decrypt(string cipher, ICipherParameters SetKeyParameter)
        {
            byte[] result = BouncyCastleCrypto(false, Convert.FromBase64String(cipher), SetKeyParameter);
            return _encoding.GetString(result, 0, result.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forEncrypt"></param>
        /// <param name="input"></param>
        /// <param name="SetKeyParameter"></param>
        /// <returns></returns>
        byte[] BouncyCastleCrypto(bool forEncrypt, byte[] input, ICipherParameters SetKeyParameter)
        {
            try
            {
                _cipher = _padding == null ?
                new PaddedBufferedBlockCipher(_blockCipher) : new PaddedBufferedBlockCipher(_blockCipher, _padding);

                _cipher.Init(forEncrypt, SetKeyParameter);

                byte[] ret = _cipher.DoFinal(input);
                return ret;

            }
            catch (CryptoException ex)
            {
                // throw new CryptoException(ex);
            }
            return null;
        }

        #endregion
    }
}
