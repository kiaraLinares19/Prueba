using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PortalAcademico.Web.Models
{
    public class Matricula
    {
        public int Id { get; set; }

        [Required]
        public int CursoId { get; set; }

        [Required]
        public string UsuarioId { get; set; } = null!; // requerido no nulo

        [Required]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        [Required]
        [EnumDataType(typeof(EstadoMatricula))]
        public EstadoMatricula Estado { get; set; } = EstadoMatricula.Pendiente;

        public Curso? Curso { get; set; } // puede ser nulo si no se carga con Include
        public IdentityUser? Usuario { get; set; } // puede ser nulo si no se carga
    }

    public enum EstadoMatricula
    {
        Pendiente,
        Confirmada,
        Cancelada
    }
}
