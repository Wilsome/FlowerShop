namespace FlowerShop.Services
{
    public class HelperMethods
    {
        
        /// <summary>
        /// Will Capitalize the first letter of each word
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public static string CapitalizeWords(string input)
        {
            // Check if the input is not null or empty
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            // Split the input string into words
            string[] words = input.Split(' ');

            // Capitalize the first letter of each word
            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                }
            }

            // Join the words back into a single string
            string result = string.Join(" ", words);

            return result;
        }

    }
}
