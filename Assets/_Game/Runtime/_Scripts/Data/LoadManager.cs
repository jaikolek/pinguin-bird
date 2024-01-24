using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.IO;

namespace PinguinBird.Storage
{
    [CreateAssetMenu(fileName = "LoadManager", menuName = "LoadManager")]
    public class LoadManager : ScriptableObjectSingleton<LoadManager>
    {
        private const string filename = "storage";
        private const string encryptionKey = "b14ca5898a4e4133bbce2ea2315a1916";
        private string dataPath => Application.persistentDataPath + "/" + filename;

        [Header("Data")]
        [SerializeField] private Data loadData;

        public int HighScore
        {
            set
            {
                loadData.highScore = value;
                Save();
            }

            get
            {
                return loadData.highScore;
            }
        }

        protected override void Awake()
        {
            base.Awake();

            Reset();
            Load();
        }

        private void SetData(Data tmpLoadData)
        {
            loadData = tmpLoadData;
        }

        public void Reset()
        {
            DoReset();
        }

        private void DoReset()
        {
            loadData = new Data()
            {
                highScore = 0,
            };
        }

        private string Encrypt(string input)
        {
            byte[] iv = new byte[16];
            byte[] array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = System.Text.Encoding.UTF8.GetBytes(encryptionKey);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(input);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }
            return System.Convert.ToBase64String(array);
        }

        private string Decrypt(string input)
        {
            byte[] iv = new byte[16];
            try
            {
                byte[] buffer = System.Convert.FromBase64String(input);
                using (Aes aes = Aes.Create())
                {
                    aes.Key = System.Text.Encoding.UTF8.GetBytes(encryptionKey);
                    aes.IV = iv;
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
                    using (MemoryStream memoryStream = new MemoryStream(buffer))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                return "";
            }
        }

        public void Load()
        {
            DoLoad();
        }

        private void DoLoad()
        {
            bool saveExists = File.Exists(dataPath);

            if (saveExists)
            {
                string rawDecrypted = File.ReadAllText(dataPath);

                string decrypted = Decrypt(rawDecrypted);
                Debug.Log($"[{typeof(LoadManager)}] {decrypted}");

                Data tmpStorageData = JsonUtility.FromJson<Data>(decrypted);

                SetData(tmpStorageData);

                Debug.Log($"[{typeof(LoadManager)}] Data loaded!");
            }
            else
            {
                DoReset();
                Debug.Log($"[{typeof(LoadManager)}] Data using default!");
            }
        }

        public void Save()
        {
            DoSave();
        }

        private void DoSave()
        {
            string rawDataStructure = JsonUtility.ToJson(loadData);
            string encrypted = Encrypt(rawDataStructure);
            File.WriteAllText(dataPath, encrypted);
            Debug.Log("Data saved!");
        }
    }
}
