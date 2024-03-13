using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestowanieOprogramowania
{
    public static class PolaczenieBazyDanych
    {

        public static string DataSource { get; set; } = "LAPTOP-72SPAJ8D";
        public static string InitialCatalog { get; set; } = "MagazynTestowanieOprogramowania";
        public static bool IntegratedSecurity { get; set; } = true;
        public static bool TrustServerCertificate { get; set; } = true;

        public static string StringPolaczeniowy()
        {
            
            return $"Data Source={DataSource};" +
                   $"Initial Catalog={InitialCatalog};" +
                   $"Integrated Security={IntegratedSecurity};" +
                   $"TrustServerCertificate={TrustServerCertificate};";
        }

    }
}
