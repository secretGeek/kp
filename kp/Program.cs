namespace kp
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using StashyLib;

    class Program
    {
        private const char DEFAULT_MASK_CHARACTER = '*';
        private const char SPACE = ' ';
        private const string HELPTEXT = @"
kp hotmail user@hotmail.com
    save the username, 'user@hotmail.com' under the key, 'hotmail'
    and you will be prompt for the password to be stored with the key and username. 

kp hotmail
    show the username 'user@hotmail.com'
    and copy the password to the clipboard

kp
    list all keys

kp ho* 
    list all keys that match the pattern 'ho*'

kp list
    list all keys and usernames

kp list ho*
    list all keys and usernames where key or username match the pattern 'ho*'

kp -r hotmail
    remove the key 'hotmail' (and its username/password) from your store";

        [STAThread]
        static int Main(string[] args)
        {
            var result = 0;
            var f = new FileStashy();

            string valueFromPipe = ReadPipeLine();

            if (args.Length == 0)
            {
                Console.Out.WriteLine("No Arguments Provided.");
            } else
            {
                int ai = 0;
                foreach(var arg in args)
                {
                    Console.Out.WriteLine($"{ai:00}: '{arg}'");
                    ai++;
                }
            }

            if (args.Length == 0)
            {
                ListAllKeys(f);
                return result;
            }

            if (args.Length == 1 && valueFromPipe == null)
            {
                if (args[0].In("h", "?", "help", "/?", "-?", "/h", "-h", "--help"))
                {
                    WriteHelpText(Console.Out);
                    result = 0;
                    return result;
                }

                if (args[0].In("list", "/list", "-list", "--list"))
                {
                    var foundOne = ListAllKeysAndNames(f);
                    result = 0;
                    if (!foundOne)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("No keys found");
                        return 5;
                    }
                    return result;
                }

                return RetrieveValueByKey(args, f);
            }
            if (args.Length > 1 || (args.Length == 1 && valueFromPipe != null))
            {
                if (args[0].In("r", "-r", "/r", "--remove"))
                {
                    //To delete a key use -r
                    //, e.g. kp -r a
                    var foundOne = f.Delete<Property>(args[1]);
                    if (!foundOne)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("No such key");
                        Console.ResetColor();
                        return 5;

                    }
                    return 0;
                }

                if (args[0].In("list", "/list", "-list", "--list"))
                {
                    var foundOne = ListAllKeysAndNames(f, args[1]);
                    result = 0;

                    if (!foundOne)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.Write("No matching keys");
                        Console.WriteLine(args[1].Has("?", "*"));
                        if (!args[1].Has("?", "*"))
                        {
                            Console.Error.Write(" Consider using wildcards: *, ?.");
                        }
                        Console.Error.WriteLine();
                        Console.ResetColor();
                        return 5;
                    }

                    return result;
                }

                return AddAKey(args, f, valueFromPipe);

            }
            return 0;
        }

        private static void WriteHelpText(TextWriter output)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            output.Write("kp");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            output.Write(" -- ");
            Console.ForegroundColor = ConsoleColor.White;
            output.WriteLine(" a key-password store integrated with the clipboard.");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            output.Write("inspired by: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            output.WriteLine("https://secretgeek.net/kv");
            output.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            output.WriteLine("usage:");
            output.WriteLine();
            Console.ResetColor();

            foreach (var line in HELPTEXT.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.StartsWith(" "))
                {
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                }
                output.WriteLine(line);
                Console.ResetColor();
            }
        }

        private static int AddAKey(string[] args, FileStashy f, string valueFromPipe)
        {
            var key = args[0];
            string data = valueFromPipe;

            if (args.Length > 1)
            {
                // all of the arguments after the first one are joined up and used as the value.
                string[] value = new string[args.Length - 1];
                for (int i = 1; i < args.Length; i++)
                {
                    value[i - 1] = args[i];
                }

                data = string.Join(" ", value);
            }

            try
            {
                Console.Write("password:");
                string password = ReadLineMasked();
                if (string.IsNullOrWhiteSpace(password))
                {
                    Console.WriteLine("**cancelled**");
                    return -1;
                }
                Console.WriteLine();
                var property = new Property() { Key = key, Username = data };
                property.SetPassword(password);
                f.Save<Property>(property, property.Key);
                Console.Write("Saved");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetType().ToString());
                return 2;
            }
        }

        private static int RetrieveValueByKey(string[] args, FileStashy f)
        {
            var key = args[0];
            try
            {
                //if key is an exact match, return it.
                var property = f.Load<Property>(key);
                string user = property.Username;
                Console.Write("username:" + user);
                Clipboard.SetDataObject(property.GetPassword(), true);
                return 0;
            }
            catch (FileNotFoundException)
            {
                bool foundOne = false;
                if (key.Contains("*") || key.Contains("?"))
                {
                    // List all keys.
                    foundOne = ListAllKeys(f, key);
                }

                if (!foundOne)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("No such key");
                    Console.ResetColor();
                    return 5;
                }
                return 0;
            }
        }

        private static bool ListAllKeysAndNames(FileStashy f)
        {
            var foundOne = false;
            foreach (var keyName in f.ListKeysAndNames<Property>())
            {
                if (foundOne) Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(keyName.Item1);
                Console.Write("\t");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(keyName.Item2);
                foundOne = true;
            }
            Console.ResetColor();
            return foundOne;
        }

        private static bool ListAllKeysAndNames(FileStashy f, string pattern)
        {
            var foundOne = false;
            foreach (var keyName in f.ListKeysAndNames<Property>(pattern))
            {
                if (foundOne) Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(keyName.Item1);
                Console.Write("\t");
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(keyName.Item2);
                foundOne = true;
            }

            Console.ResetColor();
            return foundOne;
        }

        private static bool ListAllKeys(FileStashy f, string key)
        {
            var foundOne = false;
            foreach (var s in f.ListKeys<Property>(key))
            {
                if (foundOne) Console.WriteLine();
                Console.Write(s);
                foundOne = true;
            }
            return foundOne;
        }

        private static void ListAllKeys(FileStashy f)
        {
            bool foundOne = false;
            foreach (var s in f.ListKeys<Property>())
            {
                if (foundOne) Console.WriteLine();
                Console.Write(s);
                foundOne = true;
            }
        }

        /// <summary>
        /// Reads a line from the console, masking the characters.
        /// </summary>
        /// <returns></returns>
        public static string ReadLineMasked()
        => ReadLineMasked(DEFAULT_MASK_CHARACTER);

        /// <summary>
        /// Reads a line from the console, masking the characters.
        /// </summary>
        /// <param name="maskCharacter">The mask character.</param>
        /// <returns></returns>
        public static string ReadLineMasked(char maskCharacter)
        {
            var line = new StringBuilder();
            var info = Console.ReadKey(true);

            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    line.Append(info.KeyChar);
                    Console.Write(maskCharacter);
                }
                else if (line.Length != 0)
                {
                    line.Remove(line.Length - 1, 1);

                    // Remove the extra mask character
                    if (Console.CursorLeft == 0)
                    {
                        Console.CursorTop--;
                        Console.CursorLeft = Console.BufferWidth - 1;
                        Console.Write(SPACE);
                        Console.CursorLeft = Console.BufferWidth - 1;
                        Console.CursorTop--;
                    }
                    else
                    {
                        Console.CursorLeft--;
                        Console.Write(SPACE);
                        Console.CursorLeft--;
                    }
                }

                info = Console.ReadKey(true);
            }

            Console.WriteLine();
            return line.ToString();
        }

        // read the whole pipeline -- or return null if there is nothing in the pipe.
        // hat tip: http://stackoverflow.com/questions/199528/c-console-receive-input-with-pipe/4074212#4074212
        private static string ReadPipeLine()
        {
            string valueFromPipe = null;
            try
            {
                bool isKeyAvailable = Console.KeyAvailable;
            }
            catch (InvalidOperationException)
            {
                valueFromPipe = Console.In.ReadToEnd();
            }

            return valueFromPipe;
        }
    }

    public class Property
    {
        /// <summary>
        /// Website, machine, or service name. e.g 'TESTServer', 'LocalCMS'
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Username for which the password applies.
        /// </summary>
        public string Username { get; set; }
        public string EncryptedPassword { get; set; }
        public string Entropy { get; set; }
        public string GetPassword()
        {
            if (this.Entropy == null)
            {
                throw new InvalidOperationException("Password not set.");
            }
            return this.EncryptedPassword.UnprotectString(this.Entropy);
        }
        public void SetPassword(string value)
        {
            if (this.Entropy == null)
            {
                this.Entropy = Guid.NewGuid().ToString();
            }
            this.EncryptedPassword = value.ProtectString(this.Entropy);
        }
    }

    //hat tip to http://sysi.codeplex.com
    public static class Extensions
    {
        /// <summary>
        /// Does a string contain at least 1 substring from a set of substrings.
        /// "Freddy".Has("x","y") --- true, it has y.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static bool Has(this string self, params string[] strings)
        {
            foreach (var s in strings)
            {
                if (self.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) >= 0)
                {
                    return true;
                }
            }

            return false;
        }
        public static bool HasNoneOf(this string self, params string[] strings)
=> !self.Has(strings);

        public static bool HasAllOf(this string self, params string[] strings)
        {
            foreach (var s in strings)
            {
                if (self.IndexOf(s, StringComparison.CurrentCultureIgnoreCase) < 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool In(this string self, params string[] strings)
        {
            foreach (var s in strings)
            {
                if (s.Equals(self, StringComparison.CurrentCultureIgnoreCase)) return true;
            }

            return false;
        }

        // Hat-tip... though it needed rework! 
        // https://secretgeek.net/sql_style_csharp
        public static bool Like(this string self, string pattern)
        {
            var regexPattern = GlobToRegex(pattern);
            var regex = new Regex(regexPattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(self);
        }

        // Turn a Wilcard-glob-like-pattern into regex, by turning '*' into '.*'
        // and '?' into '.{1,1}'
        // (First though, it escapes any existing regex characters, to avoid reginjextion.
        public static string GlobToRegex(string globby)
        => "^" + Regex.Escape(globby)
                .Replace("\\*", ".*")
                .Replace("\\?", ".{1,1}")
                + "$";

        ////Protect and unprotect string: hat tip: http://stackoverflow.com/questions/269101/c-reading-back-encrypted-passwords/269218#269218
        public static string ProtectString(this string value, string entropy)
        {
            byte[] valueBytes = Encoding.Unicode.GetBytes(value);
            byte[] entropicBytes = Encoding.Unicode.GetBytes(entropy);
            byte[] protectedValue = ProtectedData.Protect(valueBytes, entropicBytes, DataProtectionScope.CurrentUser);

            return Convert.ToBase64String(protectedValue);
        }

        public static string UnprotectString(this string protectedValue, string entropy)
        {
            byte[] bytes = Convert.FromBase64String(protectedValue);
            byte[] entropicBytes = Encoding.Unicode.GetBytes(entropy);
            byte[] value = ProtectedData.Unprotect(bytes, entropicBytes, DataProtectionScope.CurrentUser);

            return Encoding.Unicode.GetString(value);
        }
    }
}
