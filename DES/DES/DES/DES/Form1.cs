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

        private static string Encrypt(string pword, string key)
        {
            byte[] pwordBytes = ASCIIEncoding.ASCII.GetBytes(pword);
            byte[] keyBytes = ASCIIEncoding.ASCII.GetBytes(key);

            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            ICryptoTransform transform = provider.CreateEncryptor(pwordBytes, keyBytes);
            CryptoStreamMode mode = CryptoStreamMode.Write;

            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memStream, transform, mode);
            cryptoStream.Write(pwordBytes, 0, pwordBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] encryptedWordBytes = new byte[memStream.Length];
            memStream.Position = 0;
            memStream.Read(encryptedWordBytes, 0, encryptedWordBytes.Length);

            string encryptedWord = Convert.ToBase64String(encryptedWordBytes);

            return encryptedWord;
        }
    }
}
