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
        
        public Form1()
        {
            InitializeComponent();
        }
        
       
        
        //static byte[] DESCrypto(Mode mode, byte[] IV, byte[] key, byte[] word)
        //{
        //    using (var DES = new DESCryptoServiceProvider())
        //    {
        //        DES.IV = IV;
        //        DES.Mode = CipherMode.ECB;
        //        DES.Padding = PaddingMode.PKCS7;

        //            using (var memStream = new MemoryStream())
        //            {
        //                CryptoStream cryptoStream = null;

        //                if (mode == Mode.Ecrypt)
        //                    cryptoStream = new CryptoStream(memStream, DES.CreateEncryptor(), CryptoStreamMode.Write);
        //                else if (mode == Mode.Decrypt)
        //                    cryptoStream = new CryptoStream(memStream, DES.CreateDecryptor(), CryptoStreamMode.Write);
        //                if (cryptoStream == null)
        //                    return null;

        //            cryptoStream.Write(word, 0, word.Length);
        //            cryptoStream.FlushFinalBlock();
        //            return memStream.ToArray();

        //            }
        //        return null;
        //    }
        //}
        //static byte[] Decryption(byte[] IV, byte[] key, byte[] word)
        //{
        //    return null;
        //}

        public string Encryptd(string strWord, string strKey, bool ECB)
        {
            byte[] key = new byte[8];
            byte[] byteArray;
            byte[] hash;
            byte[] setIV = { 10, 20, 30, 40, 50, 60, 70, 80};

            try
            {
                MD5CryptoServiceProvider hsh = new MD5CryptoServiceProvider();
                hash = hsh.ComputeHash(Encoding.ASCII.GetBytes(strKey));
                for (int i = 0; i < 8; i++)
                    key[i] = hash[i];
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                if (ECB == true)
                {
                    DES.Mode = CipherMode.ECB;
                    DES.Key = key;
                }
                else
                {
                    DES.Mode = CipherMode.CBC;
                    DES.Key = key;
                    DES.IV = setIV;
                }
                byteArray = Encoding.ASCII.GetBytes(strWord);
                MemoryStream memStream = new MemoryStream();
                CryptoStream cryStream = new CryptoStream(memStream, DES.CreateEncryptor(), CryptoStreamMode.Write);
                cryStream.Write(byteArray, 0, byteArray.Length);
                cryStream.FlushFinalBlock();

                return Convert.ToBase64String(memStream.ToArray());

            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if(ECBButton.Checked == true)
            {
                string Encrypted = Encryptd(PrimaryTextBox.Text, KeyBox1.Text, true);
                if (ToFileCheckBox.Checked)
                    File.WriteAllText(@"C:\Users\njusp\OneDrive - Vilniaus kolegija\2 kursas\Informacijos Saugumas\DEStexts\ForEncrypt.txt", Encrypted);
                EncryptedResultBox.Text = Encrypted;
            }
           else if (CBCButton.Checked == true)
            {
                string Encrypted = Encryptd(PrimaryTextBox.Text, KeyBox1.Text, false);
                if (ToFileCheckBox.Checked)
                    File.WriteAllText(@"C:\Users\njusp\OneDrive - Vilniaus kolegija\2 kursas\Informacijos Saugumas\DEStexts\ForEncrypt.txt", Encrypted);
                EncryptedResultBox.Text = Encrypted;
            }
        }
    }
}
