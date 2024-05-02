using System.ComponentModel.DataAnnotations;

namespace ConcurrencyCheckTest.Entity
{
    public class Wager
    {
        public int Id { get; set; }
        public int Status { get; set; }
        [ConcurrencyCheck]
        public DateTimeOffset UpdateDate { get; set; }
    }
}
