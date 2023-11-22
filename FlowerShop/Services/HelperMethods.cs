namespace FlowerShop.Services
{
    public class HelperMethods
    {
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
                case "Gift in a box": productTypeId = 9; break;
                case "Gift Basket": productTypeId = 10; break;
                case "Wine": productTypeId = 11; break;

            }

            return productTypeId;
        }
    }
}
