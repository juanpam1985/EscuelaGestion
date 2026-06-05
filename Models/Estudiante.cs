using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EscuelaGestion.Models;

public class Estudiante
{
    public int EstudianteId { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(80)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El apellido es obligatorio")]
    [StringLength(80)]
    public string Apellido { get; set; } = string.Empty;

    [Display(Name = "Fecha de nacimiento")]
    [DataType(DataType.Date)]
    public DateTime FechaNacimiento { get; set; }

    [Required(ErrorMessage = "El grado es obligatorio")]
    [StringLength(30)]
    public string Grado { get; set; } = string.Empty;

    [NotMapped]
    public string NombreCompleto => $"{Nombre} {Apellido}";

    public ICollection<AsignacionClase> Asignaciones { get; set; } = new List<AsignacionClase>();
}
