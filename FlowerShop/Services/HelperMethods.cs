namespace FlowerShop.Services
{
    public class HelperMethods
    {
        //this should be updated it needs to change when a product type is added 

        /// <summary>
        /// Gets the productTypeId from Product Name
        /// </summary>
        /// <param name="productName"></param>
        /// <returns></returns>
        public static int GetProductTypeId(string productName) 
        {
            int productTypeId = -1;

            switch (productName) 
            {
                case "Balloon" : productTypeId = 1; break;
                case "Roses": productTypeId = 2; break;
                case "Daysies": productTypeId = 3; break;
                case "Tulips": productTypeId = 4; break;
                case "Carnations": productTypeId = 5; break;
                case "Toy": productTypeId = 6; break;
                case "Greeting Cards": productTypeId = 7; break;
                case "Gift Card": productTypeId = 8; break;
                case "Gift In A Box": productTypeId = 9; break;
                case "Gift Basket": productTypeId = 10; break;
                case "Wine": productTypeId = 11; break;
                case "Floral": productTypeId = 12; break;

            }

            return productTypeId;
        }

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
