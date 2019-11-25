using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Libreria.Models
{
    public class CorreoModel
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Asunto { get; set; }

        public string Mensaje { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Telefono { get; set; }

    }
}