using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Libreria.Models
{
  public class UsuarioSU
  {
    public int IdUsuario { get; set; }

    [Required(ErrorMessage = "Debe escribir un Nombre")]
    [StringLength(120,ErrorMessage = "Los Nombres no deben superar los 120 caracteres")]
    public string Nombres { get; set; }

    [Required(ErrorMessage = "Debe escribir un Apellido")]
    [StringLength(120, ErrorMessage = "Los Apellidos no deben superar los 120 caracteres")]
    public string Apellidos { get; set; }

    [Required(ErrorMessage = "Debe escribir el CI ó NIT")]
    [StringLength(15, ErrorMessage = "El número CI o NIT no deben superar los 15 caracteres")]
    public string CI_NIT { get; set; }

    [StringLength(25, ErrorMessage = "El número de Telefono no debe superar los 25 caracteres")]
    public string Telefonos { get; set; }

    [StringLength(120, ErrorMessage = "La direccion no debe superar los 120 caracteres")]
    public string Direccion { get; set; }

    [StringLength(120, ErrorMessage = "El Email no debe superar los 120 caracteres")]
    [DataType(DataType.EmailAddress,ErrorMessage = "Email Error")]
    [EmailAddress(ErrorMessage = "Correo electronico incorrecto")]
    public string Email { get; set; }

    [StringLength(150, ErrorMessage = "El Email no debe superar los 150 caracteres")]
    public string RazonSocial { get; set; }

    [Required(ErrorMessage = "Debe escribir un login para el usuario")]
    [StringLength(15, ErrorMessage = "El Email no debe superar los 15 caracteres")]
    public string Login { get; set; }

    [Required(ErrorMessage = "Debe escribir una contraseña")]
    [StringLength(15, ErrorMessage = "La contraseña no debe superar los 15 caracteres")]
    public string Pwd { get; set; }

    public short IdRol { get; set; }


  }
}