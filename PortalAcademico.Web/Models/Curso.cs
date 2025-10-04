using System;
using System.ComponentModel.DataAnnotations;

namespace PortalAcademico.Web.Models
{
    public class Curso
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Codigo { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Los créditos deben ser mayores a 0.")]
        public int Creditos { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El cupo máximo debe ser mayor que 0.")]
        public int CupoMaximo { get; set; }

        [Required]
        public TimeSpan HorarioInicio { get; set; }

        [Required]
        [CustomValidation(typeof(Curso), nameof(ValidarHorarioFin))]
        public TimeSpan HorarioFin { get; set; }

        public bool Activo { get; set; }

        public static ValidationResult ValidarHorarioFin(TimeSpan horarioFin, ValidationContext context)
        {
            var curso = context.ObjectInstance as Curso;
            if (curso != null && horarioFin <= curso.HorarioInicio)
            {
                return new ValidationResult("El horario de fin debe ser posterior al inicio.");
            }
            return ValidationResult.Success;
        }
    }
}
