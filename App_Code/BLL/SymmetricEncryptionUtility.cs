using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;

/// <summary>
/// Сводное описание для SymmetricEncryptionUtility
/// </summary>
public static class SymmetricEncryptionUtility
{
    public static string getMd5Hash(string input)
    {
        return getMd5Hash(input, Encoding.Default);
    }
    public static byte[] getMd5Hash(byte[] input)
    {
        MD5 md5Hasher = MD5.Create();
        byte[] data = md5Hasher.ComputeHash(input);
        return data;
    }
    public static string getMd5Hash(string input, Encoding enc)
    {
        byte[] dataIn = enc.GetBytes(input);
        byte[] data = getMd5Hash(dataIn);
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        return sBuilder.ToString();
    }
    public static string getSHA1Hash(string input)
    {
        return getSHA1Hash(input, Encoding.Default);
    }
    public static string getSHA1Hash(string input, Encoding enc)
    {
        SHA1 sha1hasher = SHA1.Create();
        byte[] dataIn = enc.GetBytes(input);
        byte[] data = sha1hasher.ComputeHash(dataIn);
        StringBuilder sBuilder = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
            sBuilder.Append(data[i].ToString("x2"));
        }
        return sBuilder.ToString();
    }
}