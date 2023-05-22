using System;
using System.Collections.Generic;

namespace sistema_cft.Models;

public partial class EstudianteAsignatura
{
    public int Id { get; set; }

    public int EstudianteId { get; set; }

    public int AsignaturasId { get; set; }

    public DateOnly? FechaRegistro { get; set; }

    public virtual Asignatura Asignaturas { get; set; } = null!;

    public virtual Estudiante Estudiante { get; set; } = null!;
}
