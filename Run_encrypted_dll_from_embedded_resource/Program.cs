using System;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
using System.IO;

namespace Run_encrypted_dll_from_embedded_resource
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes2BeDecrptd = null;
            getencryptdembdeddl();
            byte[] passwordBytes = Encoding.UTF8.GetBytes("0321");
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);
            byte[] bytesDecrypted = AES_Decrypt(bytes2BeDecrptd, passwordBytes);
            var hahadecrptddd = Assembly.Load(bytesDecrypted);

            var typ = hahadecrptddd.GetType("simple_ClassLibrary1.Calculate");//namespace.class
            var obj = Activator.CreateInstance(typ);
            var methd = typ.GetMethod("add");//method
            string result = System.Convert.ToString(methd.Invoke(obj, new object[] { 1, 2 }));
            Console.WriteLine(result);
            Console.ReadLine();



            void getencryptdembdeddl()
            {
                Assembly bcbsd = Assembly.GetExecutingAssembly();
                const string NAME = "Run_encrypted_dll_from_embedded_resource.Resources.123456";

                using (Stream resFilestream = bcbsd.GetManifestResourceStream(NAME))
                {
                    if (resFilestream == null)
                        Console.WriteLine("nothing fuond from resource");
                    byte[] ba = new byte[resFilestream.Length];
                    resFilestream.Read(ba, 0, ba.Length);
                    bytes2BeDecrptd = ba;
                }
            }


            byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] dsgsdgdsg)
            {
                byte[] decryptedBytes = null;

                // Set your salt here, change it to meet your flavor:
                // The salt bytes must be at least 8 bytes.
                byte[] saltBytes = new byte[] { 2, 1, 7, 3, 6, 4, 8, 5 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(dsgsdgdsg, saltBytes, 1000);
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
        }
    }
}
