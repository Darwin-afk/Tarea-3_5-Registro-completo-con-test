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

            Assert.AreEqual(persona.Balance, 2600);
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void EliminarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void BuscarTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetListTest()
        {
            Assert.Fail();
        }
    }
}