using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

// thank you .NET documentation :D

class AESEncoder {

    public string Decrypt(byte[] soup, string key) {

        string outputString;

        using (Aes myAes = Aes.Create()) // iv must be 128 bits (16 bytes), key must be 32 bytes
        {
            byte[] iv = Encoding.ASCII.GetBytes("bonjourbitchfuck");
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            myAes.IV = iv;
            myAes.Key = keyBytes;

            //// Decrypt the bytes to a string.
            outputString = DecryptStringFromBytes_Aes(soup, myAes.Key, myAes.IV);
        }

        return outputString;
    }

    public byte[] Encrypt(string original, string key) {
        byte[] encrypted;

        using (Aes myAes = Aes.Create()) // iv must be 128 bits (16 bytes), key must be 32 bytes
        {
            byte[] iv = Encoding.ASCII.GetBytes("bonjourbitchfuck");
            byte[] keyBytes = Encoding.ASCII.GetBytes(key);
            myAes.IV = iv;
            myAes.Key = keyBytes;

            encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);
        }

        return encrypted;
    }

    static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (plainText == null || plainText.Length <= 0)
            throw new ArgumentNullException("plainText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");
        byte[] encrypted;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create an encryptor to perform the stream transform.
            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
        }

        // Return the encrypted bytes from the memory stream.
        return encrypted;
    }

    static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
    {
        // Check arguments.
        if (cipherText == null || cipherText.Length <= 0)
            throw new ArgumentNullException("cipherText");
        if (Key == null || Key.Length <= 0)
            throw new ArgumentNullException("Key");
        if (IV == null || IV.Length <= 0)
            throw new ArgumentNullException("IV");

        // Declare the string used to hold
        // the decrypted text.
        string plaintext = null;

        // Create an Aes object
        // with the specified key and IV.
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            // Create a decryptor to perform the stream transform.
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            // Create the streams used for decryption.
            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {

                        // Read the decrypted bytes from the decrypting stream
                        // and place them in a string.
                        plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
        }

        return plaintext;
    }
}
