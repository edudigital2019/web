using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calificaciones.Models
{
    [Table("Calificaciones")]
    [Index(nameof(EstudianteId), nameof(CursoId), nameof(PeriodoId), IsUnique = true)]
    public class CalificacionesModels
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(EstudianteId))]
        [Display(Name = "EstudiantesModels")]
        public int EstudianteId { get; set; }

        [ForeignKey(nameof(CursoId))]
        [Display(Name = "CursosModels")]
        public int CursoId { get; set; }

        [ForeignKey(nameof(PeriodoId))]
        [Display(Name = "PeriodosModels")]
        public int PeriodoId { get; set; }

        [Range(0, 10)]
        [Display(Name = "Nota")]
        [Precision(5, 2)]
        public decimal Nota { get; set; }

        public EstudiantesModels? Estudiante { get; set; }
        public CursosModels? Curso { get; set; }
        public PeriodosModels? Periodo { get; set; }

        //public ProfesoresModels Profesor { get; set; }
    }
}
