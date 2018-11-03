using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Decryption_to_memory
{
    public partial class Form1 : Form
    {
        string ff;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ff = "bdlog.txt";

            textBox1.Text = ff;
            textBox4.Text = ff;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 1)
                listBox1.Items.Add("No text in the entry field");

            else if (File.Exists(textBox1.Text))
                listBox1.Items.Add("Confirmed decryption target location");

            else if (!File.Exists(textBox1.Text))
                listBox1.Items.Add("Unable to find decryption target location");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DecryptFile(ff, ff, textBox2.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox4.Text.Length < 1)
                listBox1.Items.Add("No text in the entry field");

            else if (File.Exists(textBox4.Text))
                listBox1.Items.Add("Confirmed decryption target location");

            else if (!File.Exists(textBox4.Text))
                listBox1.Items.Add("Unable to find decryption target location");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to encrypt this file? (Do not double encrypt, it will not be decrypted properly after)", "Confirm Encryption",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question,
    MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                EncryptFile(ff, ff, textBox3.Text);
            }

            
        }


        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public void EncryptFile(string file, string fileEncrypted, string password)
        {
            byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            File.WriteAllBytes(fileEncrypted, bytesEncrypted);

            listBox1.Items.Add("Enrypted the file");
        }


        public void DecryptFile(string fileEncrypted, string file, string password)
        {
            byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);
            listBox1.Items.Add("Decrypted to memory");

            richTextBox1.Text = System.Text.Encoding.Unicode.GetString(bytesDecrypted, 0, bytesDecrypted.Length);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
