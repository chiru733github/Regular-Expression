using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using static System.Net.Mime.MediaTypeNames;

namespace Regex_Practice
{
    class Program
    {
        static void Main(string[] args)
        {
            Author author = new Author();
            author.FirstName = "Joy";
            author.LastName = "Dileep";
            author.PhoneNumber = "1234567890";
            author.Email = "chiru@gmail.com";
            ValidationContext context = new ValidationContext(author, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(author, context, validationResults, true);
            if (!valid)
            {
                foreach (ValidationResult validationResult in
                validationResults)
                {
                    Console.WriteLine("{0}",
                    validationResult.ErrorMessage);
                }
            }
            string[] inputs = ["a", "b", "ab", "aab", "aaab"];
            string pattern1 = @"a*b";
            foreach (string input in inputs)
            {
                if (Regex.IsMatch(input, pattern1)) Console.WriteLine($"{input} is matched by this pattern {pattern1}");
                else Console.WriteLine($"{input} is not matched by this pattern {pattern1}");
            }
            string pattern2 = @"a+b";
            Console.WriteLine("---------------------");
            foreach (string input in inputs)
            {
                if (Regex.IsMatch(input, pattern2)) Console.WriteLine($"{input} is matched by this pattern {pattern2}");
                else Console.WriteLine($"{input} is not matched by this pattern {pattern2}");
            }
            Console.WriteLine("---------------------");
            string pattern3 = @"a?b";
            foreach (string input in inputs)
            {
                if (Regex.IsMatch(input, pattern3)) Console.WriteLine($"{input} is matched by this pattern {pattern3}");
                else Console.WriteLine($"{input} is not matched by this pattern {pattern3}");
            }
            string input1 = "C# is a Programming language";
            string pattern4 = @"^C#";
            string pattern5 = @"language$";
            if (Regex.IsMatch(input1, pattern4))
            {
                Console.WriteLine("{0} is matched by pattern {1}", input1, pattern4);
            }
            if (Regex.IsMatch(input1, pattern5))
            {
                Console.WriteLine("{0} is matched by pattern {1}", input1, pattern5);
            }

            //Write a C# Sharp program to check whether a given string is a valid Hex code or not.
            //Return true if this string is a valid code otherwise false
            string input2 = "#CAD123";
            bool flag = ValidHexCode(input2);
            if (flag) Console.WriteLine($"{input2} is a Hex Code.");

            //Write a C# Sharp program to calculate the average word length in a given string.
            //Round the average length up to two decimal places.
            string input3 = "C# is an elegant and type-safe object-oriented language";
            float avg = FindAverage(input3);
            Console.WriteLine($"the average of word length in given string is {avg}.");

            //Write a C# Sharp program to check whether a given string of characters can be
            //transformed into a palindrome. Return true otherwise false.
            string input4 = "madam";
            string orderText = string.Concat(input4.OrderBy(x => x));
            MatchCollection match = Regex.Matches(orderText, @"([a-z])\1{1}");
            foreach (Match match2 in match) Console.WriteLine(match2);
            if (Palindrome(input4)) Console.WriteLine("{0} is a palindrome",input4);
            
            //Write a C# Sharp program to validate a password of length 7 to 16 characters with the following guidelines:
            //• Length between 7 and 16 characters.
            //• At least one lowercase letter(a - z).
            //• At least one uppercase letter(A - Z).
            //• At least one digit(0 - 9).
            //• Supported special characters: ! @ # $ % ^ & * ( ) + = _ - { } [ ] : ; " ' ? < > , .
            string input5 = "Chiru@@10";
            bool PasswordFlag=ValidPassword(input5);
            if (PasswordFlag) Console.WriteLine("{0} is a valid password pattern",input5);
            //Date format
            string PatternForDate = @"(3[01]|[12][0-9]|0?[1-9])/(0?[1-9]|1[0-2])/\d{4}";
            string date = @"21/02/2001";
            string date2 = @"31/12/2000";
            Console.WriteLine(Regex.IsMatch(date,PatternForDate));
            Console.WriteLine(Regex.IsMatch(date2, PatternForDate));
            Console.ReadKey();
        }

        static bool ValidPassword(string input5)
        {
            bool flag = input5.Length >= 7 && input5.Length <= 16 &&
                        Regex.IsMatch(input5,"[A-Z]") &&
                        Regex.IsMatch(input5,"[a-z]") &&
                        Regex.IsMatch(input5,@"\d") &&
                        Regex.IsMatch(input5, @"[!-/:-@\[-_{-~]") &&
                        !Regex.IsMatch(input5, @"[^\dA-Za-z!-/:-@\[-_{-~]");
            return flag;
        }

        static bool ValidHexCode(string input)
        {
            string? Pattern = @"^#[0-9A-Fa-f]{6}";
            Regex regex = new Regex(Pattern);
            return regex.IsMatch(input);
        }
        static float FindAverage(string input)
        {
            string new_text = Regex.Replace(input,@"[^A-Za-z ]","");
            double average_len = new_text.Split(" ").Select(x => x.Length).Average();
            return (float)Math.Round(average_len, 2);
        }
        static bool Palindrome(string input4)
        {
            string orderText = string.Concat(input4.OrderBy(x => x));
            Regex regex = new(@"([a-z]){2}");
            string output = regex.Replace(orderText,"");
            return output.Length <= 1;
        }
    }
    public class Author
    {
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "First Name should be minimum 3 characters and a maximum of 50 characters")]
        [DataType(DataType.Text)]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, MinimumLength = 3,
        ErrorMessage = "Last Name should be minimum 3 characters and a maximum of 50 characters")]
        [DataType(DataType.Text)]
        public string? LastName { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Phone]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0} must contains 10 digits")]
        public string? PhoneNumber { get; set; }
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Required(ErrorMessage = "{0} is required")]
        public string? Email { get; set; }
    }
}
