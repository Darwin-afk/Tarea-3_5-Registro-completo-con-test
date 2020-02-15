using System;
using System.Collections.Generic;
using System.Text;
using RegistroCompleto.Entidades;
using RegistroCompleto.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace RegistroCompleto.BLL
{
    public class InscripcionesBLL
    {
        public static bool Guardar(Inscripciones inscripcion)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var persona = PersonasBLL.Buscar(inscripcion.PersonaId);
                persona.Balance += 2600;
                paso = PersonasBLL.Modificar(persona);

                if(paso)
                {
                    inscripcion.Balance = 2600;
                    if (db.Inscripciones.Add(inscripcion) != null)
                        paso = (db.SaveChanges() > 0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }
        public static bool Modificar(Inscripciones inscripcion)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var persona = PersonasBLL.Buscar(inscripcion.PersonaId);
                persona.Balance -= inscripcion.Monto;
                PersonasBLL.Modificar(persona);

                var inscripcionAntigua = InscripcionesBLL.Buscar(inscripcion.InscripcionId);
                inscripcion.Balance -= inscripcion.Monto;
                inscripcion.Monto += inscripcionAntigua.Monto;

                db.Entry(inscripcion).State = EntityState.Modified;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }
        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.Inscripciones.Find(id);
                Personas persona = PersonasBLL.Buscar(eliminar.PersonaId);
                persona.Balance -= eliminar.Balance;
                PersonasBLL.Modificar(persona);
                db.Entry(eliminar).State = EntityState.Deleted;

                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return paso;
        }
        public static Inscripciones Buscar(int InscripcionId)
        {
            Contexto db = new Contexto();
            Inscripciones inscripcion = new Inscripciones();

            try
            {
                inscripcion = db.Inscripciones.Find(InscripcionId);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return inscripcion;
        }
        public static List<Inscripciones> GetList(Expression<Func<Inscripciones, bool>> inscripcion)
        {
            List<Inscripciones> Lista = new List<Inscripciones>();
            Contexto db = new Contexto();

            try
            {
                Lista = db.Inscripciones.Where(inscripcion).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return Lista;
        }
    }
}
