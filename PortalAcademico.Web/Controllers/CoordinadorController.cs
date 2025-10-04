using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalAcademico.Web.Models;
using PortalAcademico.Web.Data;

[Authorize(Roles = "Coordinador")]
public class CoordinadorController : Controller
{
    private readonly ApplicationDbContext _context;

    public CoordinadorController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Panel principal
    public IActionResult Index()
    {
        var cursos = _context.Cursos.ToList();
        return View(cursos);
    }
    public IActionResult CrearCurso() => View();

    [HttpPost]
    public IActionResult CrearCurso(Curso curso)
    {
        if (ModelState.IsValid)
        {
            _context.Cursos.Add(curso);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(curso);
    }

    public IActionResult EditarCurso(int id)
    {
        var curso = _context.Cursos.Find(id);
        if (curso == null) return NotFound();
        return View(curso);
    }

    [HttpPost]
    public IActionResult EditarCurso(Curso curso)
    {
        if (ModelState.IsValid)
        {
            _context.Cursos.Update(curso);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(curso);
    }

    public IActionResult DesactivarCurso(int id)
    {
        var curso = _context.Cursos.Find(id);
        if (curso == null) return NotFound();

        curso.Activo = false;
        _context.SaveChanges();
        return RedirectToAction(nameof(Index));
    }


    public IActionResult MatriculasPorCurso(int cursoId)
    {
        var matriculas = _context.Matriculas
            .Where(m => m.CursoId == cursoId)
            .ToList();
        ViewBag.CursoId = cursoId;
        return View(matriculas);
    }

    [HttpPost]
    public IActionResult ConfirmarMatricula(int id)
    {
        var matricula = _context.Matriculas.Find(id);
        if (matricula != null)
        {
            matricula.Estado=EstadoMatricula.Confirmada;
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(MatriculasPorCurso), new { cursoId = matricula.CursoId });
    }

    [HttpPost]
    public IActionResult CancelarMatricula(int id)
    {
        var matricula = _context.Matriculas.Find(id);
        if (matricula != null)
        {
            matricula.Estado =EstadoMatricula.Cancelada;
            _context.SaveChanges();
        }
        return RedirectToAction(nameof(MatriculasPorCurso), new { cursoId = matricula.CursoId });
    }
}
