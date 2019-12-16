using System.Web;
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
        [StringLength(120, ErrorMessage = "El Email no debe superar los 120 caracteres")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Error")]
        [EmailAddress(ErrorMessage = "Correo electronico incorrecto")]
        public string Email { get; set; }
        [Required]
        public string Telefono { get; set; }

    }
}