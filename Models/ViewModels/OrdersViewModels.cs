using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.ViewModels
{
    public class OrdersViewModels
    {


        [Required]
        [Display(Name = "Código del Cliente")]
        public int CustomerId { get; set; }

        [Display(Name = "Fecha")]
        public DateTime? Date { get; set; }


        [Required]
        public string? Estado { get; set; }

        [Required]
        [Display(Name = "Código del Usuario")]
        public int UsuarioId { get; set; }


        public uint? IdOrders { get; set; }

    }
}
