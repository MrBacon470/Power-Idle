using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using TMPro;

public class SimpleAES
{
    // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
    // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
    private const string initVector = "bkmspeed789t16fg";
    // This constant is used to determine the keysize of the encryption algorithm
    private const int keysize = 256;
    //Encrypt
    public static string EncryptString(string plainText, string passPhrase)
    {
        var initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        var password = new PasswordDeriveBytes(passPhrase, null);
        var keyBytes = password.GetBytes(keysize / 8);
        var symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        var memoryStream = new MemoryStream();
        var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        var cipherTextBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        return Convert.ToBase64String(cipherTextBytes);
    }
    //Decrypt
    public static string DecryptString(string cipherText, string passPhrase)
    {
        var initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        var cipherTextBytes = Convert.FromBase64String(cipherText);
        var password = new PasswordDeriveBytes(passPhrase, null);
        var keyBytes = password.GetBytes(keysize / 8);
        var symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        var memoryStream = new MemoryStream(cipherTextBytes);
        var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        var plainTextBytes = new byte[cipherTextBytes.Length];
        var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }
}

public class SaveSystem : MonoBehaviour
{
    public OfflineManager offline;
    private static DateTime currentUTCTime;

    public TMP_InputField importValue;
    public TMP_InputField exportValue;

    protected static string encryptKey = "bls4tyupq6he82jdkeqipoch";
    protected static string savePath => Application.persistentDataPath + "/Saves/";
    protected static string savePathBackUP => Application.persistentDataPath + "/Backups/";

    public static int backUpCount = 0;

    public async Task AwaitGetUTCTIme()
    {
        currentUTCTime = await offline.AwaitGetUTCTIme();
    }

    public static void SavePlayer<T>(T Data, string name)
    {
        Directory.CreateDirectory(savePath);
        Directory.CreateDirectory(savePathBackUP);
        backUpCount++;
        Save(backUpCount % 4 == 0 ? savePathBackUP : savePath);

        PlayerPrefs.SetString("OfflineTime", currentUTCTime.ToBinary().ToString());
        
        void Save(string path)
        {
            using (var writer = new StreamWriter(path + name + ".txt"))
            {
                var formatter = new BinaryFormatter();
                var memoryStream = new MemoryStream();
                formatter.Serialize(memoryStream, Data);
                var dataToWrite = SimpleAES.EncryptString(Convert.ToBase64String(memoryStream.ToArray()), encryptKey);
                writer.WriteLine(dataToWrite);
            }
        }

    }

    public static T LoadPlayer<T>(string name)
    {
        Directory.CreateDirectory(savePath);
        Directory.CreateDirectory(savePathBackUP);
        T returnValue;
        bool backUpNeeded = false;
        Load(savePath);
        if (backUpNeeded)
        {
            Load(savePathBackUP);
        }
        return returnValue;

        void Load(string path)
        {
            using (var reader = new StreamReader(path + name + ".txt"))
            {
                var formatter = new BinaryFormatter();
                var dataToRead = reader.ReadToEnd();
                var memoryStream = new MemoryStream(Convert.FromBase64String(SimpleAES.DecryptString(dataToRead, encryptKey)));
                try
                {
                    returnValue = (T)formatter.Deserialize(memoryStream);
                }
                catch
                {
                    returnValue = default;
                    backUpNeeded = true;
                }
            }
        }
    }

    public static bool SaveExists(string key)
    {
        var path = savePath + key + ".txt";
        return File.Exists(path);
    }

    public void ImportPlayer2(int id)
    {
        var path = "";
        switch(id)
        {
            case 0:
                path = savePath;
                break;
        }

        using (var writer = new StreamWriter(path + name + ".txt"))
        {
            writer.WriteLine(importValue.text);
        }
    }

    public void ExportPlayer2()
    {
        string dataToRead = $"";
        bool backUpNeeded = false;
        Load(savePath);
        if (backUpNeeded)
        {
            Load(savePathBackUP);
        }

        exportValue.text = dataToRead.ToString();

        void Load(string path)
        {
            using (var reader = new StreamReader(path + name + ".txt"))
            {
                try
                {
                    dataToRead = (reader.ReadToEnd()).ToString();
                }
                catch
                {
                    backUpNeeded = true;
                }
            }
        }
    }

    public void ClearFields()
    {
        exportValue.text = "";
        importValue.text = "";
    }
}