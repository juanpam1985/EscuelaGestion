using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscuelaGestion.Models;

public class Profesor
{
    public int ProfesorId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(80)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(80)]
    public string Apellido { get; set; } = string.Empty;

    [Required(ErrorMessage = "La especialidad es obligatoria")]
    [StringLength(80)]
    public string Especialidad { get; set; } = string.Empty;

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Ingrese un email válido")]
    [StringLength(120)]
    public string Email { get; set; } = string.Empty;

    [NotMapped]
    public string NombreCompleto => $"{Nombre} {Apellido}";

    public ICollection<Clase> Clases { get; set; } = new List<Clase>();
}
