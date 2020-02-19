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
            Personas persona = PersonasBLL.Buscar(1);

            int balanceAnterior = persona.Balance;

            Inscripciones inscripcion = new Inscripciones();
            inscripcion.InscripcionId = 0;
            inscripcion.Fecha = DateTime.Now;
            inscripcion.PersonaId = 1;
            inscripcion.Comentarios = String.Empty;
            inscripcion.Balance = 0;
            inscripcion.Monto = 0;
            InscripcionesBLL.Guardar(inscripcion);

            persona = PersonasBLL.Buscar(1);

            Assert.IsTrue(persona.Balance==(balanceAnterior + 2600));
        }

        [TestMethod()]
        public void ModificarTest()
        {
            Personas persona = PersonasBLL.Buscar(1);

            int balanceAnterior = persona.Balance;
            int pago = 400;

            Inscripciones inscripcion = InscripcionesBLL.Buscar(1);
            inscripcion.Monto += pago;


            InscripcionesBLL.Modificar(inscripcion);

            persona = PersonasBLL.Buscar(1);

            Assert.IsTrue(persona.Balance == (balanceAnterior - pago));
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