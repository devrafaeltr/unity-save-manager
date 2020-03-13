using System;
using System.Security.Cryptography;
using System.Text;

public class SimpleEncryption
{
    private const int encriptionOffset = 24;

    public static string Encrypt(string textToEncrypt)
    {
        return EncryptDecrypt(textToEncrypt);
    }

    public static string Decrypt(string textToDecrypt)
    {
        return EncryptDecrypt(textToDecrypt);
    }

    private static string EncryptDecrypt(string text)
    {
        StringBuilder inSb = new StringBuilder(text);
        StringBuilder outSb = new StringBuilder(text.Length);

        char tempChar;

        for (int i = 0; i < text.Length; i++)
        {
            tempChar = inSb[i];
            tempChar = (char)(tempChar ^ encriptionOffset);
            outSb.Append(tempChar);
        }

        return outSb.ToString();
    }
}