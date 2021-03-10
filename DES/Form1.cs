using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace DES
{
    public partial class Form1 : Form
    {
        public enum Mode
        {
            Ecrypt,
            Decrypt
        };
        public Form1()
        {
            InitializeComponent();
        }
        
        //private static string Encrypt(string pword, string key)
        //{
        //    byte[] pwordBytes = ASCIIEncoding.ASCII.GetBytes(pword);
        //    byte[] keyBytes = ASCIIEncoding.ASCII.GetBytes(key);

        //    DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
        //    ICryptoTransform transform = provider.CreateEncryptor(keyBytes, keyBytes);
        //    CryptoStreamMode mode = CryptoStreamMode.Write;

        //    MemoryStream memStream = new MemoryStream();
        //    CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
        //    cryptoStream.Write(pwordBytes, 0, pwordBytes.Length);
        //    cryptoStream.FlushFinalBlock();

        //    byte[] encryptedWordBytes = new byte[memStream.Length];
        //    memStream.Position = 0;
        //    memStream.Read(encryptedWordBytes, 0, encryptedWordBytes.Length);

        //    string encryptedWord = Convert.ToBase64String(encryptedWordBytes);

        //    return encryptedWord;
        //}

        //private static string Decrypt(string encryptedword, string key)
        //{
        //    byte[] encryptedwordBytes = Convert.FromBase64String(encryptedword);
        //    byte[] keyBytes = ASCIIEncoding.ASCII.GetBytes(key);

        //    DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
        //    ICryptoTransform transform = provider.CreateDecryptor(keyBytes, keyBytes);
        //    CryptoStreamMode mode = CryptoStreamMode.Write;

        //    MemoryStream memStream = new MemoryStream();
        //    CryptoStream cryStream = new CryptoStream(memStream, transform, mode);
        //    cryStream.Write(encryptedwordBytes, 0, encryptedwordBytes.Length);
        //    cryStream.FlushFinalBlock();

        //    byte[] decryptedWordBytes = new byte[memStream.Position];
        //    memStream.Position = 0;
        //    memStream.Read(decryptedWordBytes, 0, decryptedWordBytes.Length);

        //    string word = ASCIIEncoding.ASCII.GetString(decryptedWordBytes);

        //    return word;
        //}

        static byte[] DESCrypto(Mode mode, byte[] IV, byte[] key, byte[] word)
        {
            using (var DES = new DESCryptoServiceProvider())
            {
                DES.IV = IV;
                DES.Key = key;
                DES.Mode = CipherMode.CBC;
                DES.Mode = CipherMode.ECB;
                DES.Padding = PaddingMode.PKCS7;

                    using (var memStream = new MemoryStream())
                    {
                        CryptoStream cryptoStream = null;

                        if (mode == Mode.Ecrypt)
                            cryptoStream = new CryptoStream(memStream, DES.CreateEncryptor(), CryptoStreamMode.Write);
                        else if (mode == Mode.Decrypt)
                            cryptoStream = new CryptoStream(memStream, DES.CreateDecryptor(), CryptoStreamMode.Write);
                        if (cryptoStream == null)
                            return null;

                    cryptoStream.Write(word, 0, word.Length);
                    cryptoStream.FlushFinalBlock();
                    return memStream.ToArray();

                    }
                return null;
            }
        }
        static byte[] Decryption(byte[] IV, byte[] key, byte[] word)
        {
            return null;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string word = PrimaryTextBox.Text;
            var random = new Random();
            byte[] IV = new byte[8];
            random.NextBytes(IV);
            byte[] key = new byte[8];
            //random.NextBytes(key);
            string KEY = System.Text.Encoding.UTF8.GetString(key);
            KeyBox1.Text = KEY;
            byte[] encrypted = DESCrypto(Mode.Ecrypt, IV, key, Encoding.UTF8.GetBytes(word));
            EncryptedResultBox.Text = BitConverter.ToString(encrypted).Replace("-", "");
            byte[] decrypted = DESCrypto(Mode.Decrypt, IV, key, encrypted);
            DecryptedTextBox.Text = Encoding.UTF8.GetString(decrypted);
        }
    }
}
