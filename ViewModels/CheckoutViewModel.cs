using RestaurantApp.Models;
using System.ComponentModel.DataAnnotations;

namespace RestaurantApp.ViewModels
{
    public class CheckoutViewModel
    {
        public Cart Cart { get; set; } = new();

        [Required(ErrorMessage = "Le nom est obligatoire")]
        [Display(Name = "Nom complet")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le téléphone est obligatoire")]
        [Phone(ErrorMessage = "Numéro invalide")]
        [Display(Name = "Téléphone")]
        public string CustomerPhone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email invalide")]
        [Display(Name = "Email")]
        public string CustomerEmail { get; set; } = string.Empty;

        [Display(Name = "Adresse de livraison")]
        public string CustomerAddress { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Mode de réception")]
        public DeliveryType DeliveryType { get; set; } = DeliveryType.Livraison;

        [Display(Name = "Notes / instructions")]
        public string? Notes { get; set; }
    }
}