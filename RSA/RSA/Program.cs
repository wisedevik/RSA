using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        Console.Title = "RSA";

        // Генерация ключей
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048))
        {
            string publicKey = rsa.ToXmlString(false); // Получение открытого ключа
            string privateKey = rsa.ToXmlString(true); // Получение закрытого ключа

            Console.Write("Enter the type Encrypt (E) / Decrypt (D): ");
            string type = Console.ReadLine().ToLower();

            switch (type)
            {
                case "e":
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine($"\nPublic Key: {publicKey}\n");
                        Console.WriteLine($"Private Key: {privateKey}\n");
                        Console.ResetColor();

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[WARNING] Remember the Private Key to decrypt the text!\n");
                        Console.ResetColor();

                        Console.Write("Enter the text: ");
                        string originalText = Console.ReadLine();

                        Console.WriteLine($"Original Text: {originalText}");
                        byte[] encryptedData = Encrypt(Encoding.UTF8.GetBytes(originalText), publicKey);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"Encrypted Text: {Convert.ToBase64String(encryptedData)}");
                        Console.ResetColor();
                        break;
                    }
                case "d":
                    {
                        Console.Write("Enter the encrypted text: ");
                        string originalText = Console.ReadLine();

                        Console.Write("Enter the Private Key: ");
                        string privateKeyXml = Console.ReadLine();

                        byte[] encryptedData = Convert.FromBase64String(originalText);
                        string decryptedText = Decrypt(encryptedData, privateKeyXml);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nDecrypted Text: {decryptedText}");
                        Console.ResetColor();
                        break;
                    }
            }
        }

        Console.Write("\nPress any key to exit...");
        Console.ReadLine();
    }

    // Шифрование данных
    static byte[] Encrypt(byte[] data, string publicKey)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(publicKey);
            return rsa.Encrypt(data, false);
        }
    }

    // Дешифрование данных
    static string Decrypt(byte[] encryptedData, string privateKey)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(privateKey);
            byte[] decryptedData = rsa.Decrypt(encryptedData, false);
            return Encoding.UTF8.GetString(decryptedData);
        }
    }
}
