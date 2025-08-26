using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Password_Manager
{
    internal class Program
    {

        public static readonly Dictionary<string, string> passwords = new Dictionary<string, string>();
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\t\t\t\t==========================================");
                Console.WriteLine("\t\t\t\t===== Welcome in my Password Manager =====");
                Console.WriteLine("\t\t\t\t==========================================");
                
                Console.WriteLine("1. List all passwords");
                Console.WriteLine("2. Add a password");
                Console.WriteLine("3. Change a password");
                Console.WriteLine("4. Delete a password");
                Console.WriteLine("5. Update data and password");
                Console.WriteLine("6. Search for a password");
                Console.WriteLine("7. Generate Password ");
                Console.WriteLine("8. Exit");

                Console.Write("Select an option: ");
                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Listing all passwords...");
                        ListAllPassword();
                        break;
                    case "2":
                        Console.WriteLine("Adding a password...");
                        AddPassword();
                    
                        break;
                    case "3":
                        Console.WriteLine("Changing a password...");
                        ChangePassword();
                        break;
                    case "4":
                        Console.WriteLine("Deleting a password...");
                        DeletePassword();

                        break;
                    case "5":
                        Console.WriteLine("Updating a password...");
                        UpdatePassword();
                        break;
                    case "6":
                        Console.WriteLine("Searching for a password...");
                        SearchPassword();
                        break;
                    case "7":
                      
                        Console.WriteLine("Genarate Password ...");
                        GeneratePassword();
                        break;
                        case "8":
                            Console.WriteLine("Exiting...");
                            Environment.Exit(0);
                            break;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        Environment.Exit(0);
                        break;
                }
                Console.WriteLine(); 
            }
        }

        private static void GeneratePassword(int length = 12)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()";
            Random random = new Random();
            char[] password = new char[length];

            for (int i = 0; i < length; i++)
            {
                password[i] = chars[random.Next(chars.Length)];
            }

            Console.WriteLine("Here is a randomly generated password: " + new string(password));
        }

        private static void ListAllPassword()
        {

            Console.Write("enter your identity -> ");
           string typeuser = Console.ReadLine();
             if (passwords.Count == 0)
            {
                Console.WriteLine("No passwords stored.");
                return;
            }
            if (typeuser == "ceo")
            {
                Console.WriteLine("Stored passwords:");
                foreach (var pass in passwords)
                {
                    Console.WriteLine($"Service: {pass.Key}, Password: {pass.Value}");
                }
                return;
            }
            else if (typeuser == "employee")
            {
                Console.WriteLine("Stored passwords (hidden):");
                foreach (var pass in passwords)
                {
                    string hiddenPassword = new string('*', pass.Value.Length);
                    Console.WriteLine($"Service: {pass.Key}, Password: {hiddenPassword}");
                }
                return;
            }

      

        }

        private static void AddPassword()
        {


            Console.Write("Do you want a suggestion for a strong password? (generate/no): ");
            string suggestion = Console.ReadLine();
            if (suggestion == "generate")
            {
                Console.Write("Enter service name: ");
                string service = Console.ReadLine();
                if (passwords.ContainsKey(service))
                {
                    Console.WriteLine("Service already exists. Use update option to change the password.");
                    return;
                }

                GeneratePassword();
                Console.Write("Enter the generated password: ");
                string password = Console.ReadLine();
                passwords[service] = password;
                Console.WriteLine("Password added successfully.");
            }
            else if (suggestion == "no")
            {
                Console.Write("Enter service name: ");
                string service = Console.ReadLine();
                Console.Write("Enter password: ");
                string password = Console.ReadLine();
                if (passwords.ContainsKey(service))
                {
                    Console.WriteLine("Service already exists. Use update option to change the password.");
                    return;
                }
                passwords[service] = password;
                Console.WriteLine("Password added successfully.");
            }
        }
        private static void ChangePassword()
        {
            Console.Write("Enter service name to change password: ");
            string service = Console.ReadLine();
            if (!passwords.ContainsKey(service))
            {
                Console.WriteLine("Service not found.");
                return;
            }
            Console.WriteLine($"Current password for {service}: {passwords[service]}"); 
            Console.Write("Enter new password: ");
            string newPassword = Console.ReadLine();
            passwords[service] = newPassword;
            Console.WriteLine("Password changed successfully.");
        }

        private static void DeletePassword()
        {
            Console.Write("Enter service name to delete: ");
            string service = Console.ReadLine();
            if (!passwords.ContainsKey(service))
            {
                Console.WriteLine("Service not found.");
                return;
            }
            Console.Write("Enter the current password for verification: ");
            string currentPassword = Console.ReadLine();
            if (passwords[service] != currentPassword)
            {
                Console.WriteLine("Incorrect password. Deletion aborted.");
                return;
            }
            passwords.Remove(service);
            Console.WriteLine("Service and password deleted successfully.");
        }

        private static void UpdatePassword()
        {
            Console.Write("Enter service name to update: ");
            string service = Console.ReadLine();
            if (!passwords.ContainsKey(service))
            {
                Console.WriteLine("Service not found.");
                return;
            }
            Console.Write("Enter new service name (or press Enter to keep the same): ");
            string newService = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(newService))
            {
                newService = service; 
            }
            Console.Write("Enter new password: ");
            string newPassword = Console.ReadLine();
            passwords.Remove(service);
            passwords[newService] = newPassword;
            Console.WriteLine("Service and password updated successfully.");

        }

        private static void SearchPassword()
        {
       
            Console.Write("Enter service name to search: ");
            string service = Console.ReadLine();
            if (passwords.TryGetValue(service, out string password))
            {
                Console.WriteLine($"Password for {service}: {password}");
            }
            else
            {
                Console.WriteLine("Service not found.");
            }
        }
    }
}
