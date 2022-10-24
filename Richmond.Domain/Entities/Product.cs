using System;

namespace Richmond.Domain.Entities
{
    public class Product : Entity
    {
        public decimal SetupFee { get; private set; }
        public Product(decimal setupFee)
        {
            SetupFee = setupFee;
            DateCreated = DateTime.UtcNow;
        }
    }
}
