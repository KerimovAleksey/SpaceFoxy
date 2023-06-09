using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private string _dataDirPath = "";
    private string _dataFIleName = "";
    private bool _useEncryption = false;
    private readonly string encryptionCodeWord = "kaisa";
    public FileDataHandler(string dataDirParh, string dataFileName, bool useEncryption)
    {
        _dataDirPath = dataDirParh;
        _dataFIleName = dataFileName;
        _useEncryption = useEncryption;
    }

    public GameData Load()
    {
		string fullPath = Path.Combine(_dataDirPath, _dataFIleName);
		GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }
                // ���������
                if (_useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
                }

                // ��������� json � �# ������
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
				Debug.LogError("Error occured when trying to load data from file: " + fullPath + "\n" + e);
			}
        }
        return loadedData;
	}

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(_dataDirPath, _dataFIleName);
        try
        {
            // ���� �� ���������� ����� �/��� �����
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // ��������� �# ������ � json
            string dataToStore = JsonUtility.ToJson(data, true);

            // ����������
            if (_useEncryption)
            {
                dataToStore = EncryptDecrypt(dataToStore);
            }

            // ��������� � ����
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }

    private string EncryptDecrypt (string data)
    {
        string modifiedData = "";

        for (int i = 0; i < data.Length; i++)
        {
            modifiedData += (char)(data[i] ^ encryptionCodeWord[i % encryptionCodeWord.Length]);
        }
        return modifiedData;
    }
}
