using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegistroCompleto.BLL;
using System;
using System.Collections.Generic;
using System.Text;
using RegistroCompleto.Entidades;

namespace RegistroCompleto.BLL.Tests
{
    [TestClass()]
    public class InscripcionesBLLTests
    {
        [TestMethod()]
        public void GuardarTest()
        {
            Personas persona = new Personas();
            persona.PersonaId = 0;
            persona.Nombre = "Jose";
            persona.Telefono = "123";
            persona.Cedula = "321";
            persona.Direccion = "qwe";
            persona.FechaNacimiento = DateTime.Now;
            persona.Balance = 0;
            PersonasBLL.Guardar(persona);

            Inscripciones inscripcion = new Inscripciones();
            inscripcion.InscripcionId = 0;
            inscripcion.Fecha = DateTime.Now;
            inscripcion.PersonaId = 1;
            inscripcion.Comentarios = String.Empty;
            inscripcion.Balance = 0;
            inscripcion.Monto = 0;
            InscripcionesBLL.Guardar(inscripcion);

            persona = PersonasBLL.Buscar(1);

            Assert.IsTrue(persona.Balance==2600);
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Inscripciones inscripcion = new Inscripciones();
            inscripcion.InscripcionId = 11;
            inscripcion.Fecha = DateTime.Now;
            inscripcion.PersonaId = 20;
            inscripcion.Comentarios = "Primer pago";
            inscripcion.Balance = 2600;
            inscripcion.Monto = 400;

            InscripcionesBLL.Modificar(inscripcion);

            Personas persona = PersonasBLL.Buscar(20);

            Assert.IsTrue(persona.Balance == (2600 - inscripcion.Monto));
        }

        [TestMethod()]
        public void EliminarTest()
        {
            int InscripcionId = 1;

            Inscripciones inscripcion = InscripcionesBLL.Buscar(InscripcionId);

            int PersonaId = inscripcion.PersonaId;

            Personas persona = PersonasBLL.Buscar(PersonaId);

            int balance = persona.Balance;

            InscripcionesBLL.Eliminar(InscripcionId);

            persona = PersonasBLL.Buscar(PersonaId);

            Assert.IsTrue(persona.Balance == (balance-inscripcion.Balance));
        }
    }
}