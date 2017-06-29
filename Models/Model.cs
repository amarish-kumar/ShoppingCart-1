namespace ShoppingCart.Models
{
    public class Model
    {
        public int Id { get; set; }
        public string Name { get; set; }

        //This is navigation property
        public Make Make { get; set; }

        public int MakeId { get; set; }
    }
}
