using System.ComponentModel.DataAnnotations;

namespace EscuelaGestion.Models;

public class AsignacionClase
{
    public int AsignacionId { get; set; }

    [Display(Name = "Estudiante")]
    public int EstudianteId { get; set; }

    [Display(Name = "Clase")]
    public int ClaseId { get; set; }

    [Display(Name = "Fecha de asignación")]
    [DataType(DataType.Date)]
    public DateTime FechaAsignacion { get; set; } = DateTime.Today;

    public Estudiante? Estudiante { get; set; }
    public Clase? Clase { get; set; }
}
