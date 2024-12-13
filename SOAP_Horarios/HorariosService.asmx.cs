using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entities;
using BLL;
using System.Xml.Serialization;

namespace SOAP_Horarios
{
    /// <summary>
    /// Descripción breve de HorariosService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class HorariosService : System.Web.Services.WebService
    {
        private HorariosLogic _logic = new HorariosLogic();

        [WebMethod]
        public List<SerializableHorarios> GetAllHorarios()
        {
            var horarios = _logic.RetrieveAll();
            return horarios.ConvertAll(h => new SerializableHorarios(h));
        }

        [WebMethod]
        public SerializableHorarios GetHorarioById(int id)
        {
            return new SerializableHorarios(_logic.RetrieveById(id));
        }

        [WebMethod]
        public void CreateHorario(SerializableHorarios horario)
        {
            if (horario == null)
            {
                throw new ArgumentNullException("El objeto horario no puede ser nulo.");
            }

            if (string.IsNullOrEmpty(horario.DiaSemana) || string.IsNullOrEmpty(horario.HoraEntrada) || string.IsNullOrEmpty(horario.HoraSalida))
            {
                throw new ArgumentException("Los campos DiaSemana, HoraEntrada y HoraSalida son obligatorios.");
            }

            // Validar formato de HoraEntrada y HoraSalida
            if (!TimeSpan.TryParse(horario.HoraEntrada, out _) || !TimeSpan.TryParse(horario.HoraSalida, out _))
            {
                throw new ArgumentException("Los campos HoraEntrada y HoraSalida deben tener un formato válido (hh:mm:ss).");
            }

            _logic.Create(horario.ToHorarios());
        }

        [WebMethod]
        public void UpdateHorario(SerializableHorarios horario)
        {
            if (horario == null)
            {
                throw new ArgumentNullException("El objeto horario no puede ser nulo.");
            }

            if (horario.HorarioID <= 0)
            {
                throw new ArgumentException("El ID del horario no es válido.");
            }

            if (string.IsNullOrEmpty(horario.DiaSemana) || string.IsNullOrEmpty(horario.HoraEntrada) || string.IsNullOrEmpty(horario.HoraSalida))
            {
                throw new ArgumentException("Los campos DiaSemana, HoraEntrada y HoraSalida son obligatorios.");
            }

            if (!TimeSpan.TryParse(horario.HoraEntrada, out _) || !TimeSpan.TryParse(horario.HoraSalida, out _))
            {
                throw new ArgumentException("Los campos HoraEntrada y HoraSalida deben tener un formato válido (hh:mm:ss).");
            }

            _logic.Update(horario.ToHorarios());
        }

        [WebMethod]
        public void DeleteHorario(int id)
        {
            _logic.Delete(id);
        }
    }

    [XmlRoot("Horarios")]
    public class SerializableHorarios
    {
        public int HorarioID { get; set; }
        public int EmpleadoID { get; set; }
        public string DiaSemana { get; set; }

        // Serializamos HoraEntrada y HoraSalida como cadenas
        public string HoraEntrada { get; set; }
        public string HoraSalida { get; set; }

        public SerializableHorarios() { }

        public SerializableHorarios(Horarios horario)
        {
            HorarioID = horario.HorarioID;
            EmpleadoID = horario.EmpleadoID;
            DiaSemana = horario.DiaSemana;
            HoraEntrada = horario.HoraEntrada.ToString(); // Convertimos TimeSpan a string
            HoraSalida = horario.HoraSalida.ToString();   // Convertimos TimeSpan a string
        }

        public Horarios ToHorarios()
        {
            return new Horarios
            {
                HorarioID = this.HorarioID,
                EmpleadoID = this.EmpleadoID,
                DiaSemana = this.DiaSemana,
                HoraEntrada = TimeSpan.Parse(this.HoraEntrada), // Convertimos de string a TimeSpan
                HoraSalida = TimeSpan.Parse(this.HoraSalida)    // Convertimos de string a TimeSpan
            };
        }
    }
}
