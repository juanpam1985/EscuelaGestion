using System.ComponentModel.DataAnnotations;

namespace EscuelaGestion.Models;

public class Clase
{
    public int ClaseId { get; set; }

    [Required(ErrorMessage = "El nombre de la clase es obligatorio")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El horario es obligatorio")]
    [StringLength(60)]
    public string Horario { get; set; } = string.Empty;

    [Display(Name = "Profesor")]
    public int ProfesorId { get; set; }

    public Profesor? Profesor { get; set; }
    public ICollection<AsignacionClase> Asignaciones { get; set; } = new List<AsignacionClase>();
}
