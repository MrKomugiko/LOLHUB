using System.ComponentModel.DataAnnotations;

namespace LOLHUB.Models
{
    public class ZaproszenieDoZnajomych
    {
        [Key]
        public int Id { get; set; }
        public bool? Answer { get; set; }        // True = Akceptacja, False = Odrzucenie

        public int? PlayerId { get; set; }         // FK Gracza ktory chce zostać naszym znajomym
        public virtual Player Player { get; set; }
    }
}