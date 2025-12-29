namespace identity_mvc.Models
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Gerente = "Gerente";
        public const string Vendedor = "Vendedor";
        public const string Cliente = "Cliente";
       
        public const string GerenteVendedor = Gerente + "," + Vendedor;
    }
}
