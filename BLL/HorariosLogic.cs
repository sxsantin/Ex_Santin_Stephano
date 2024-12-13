using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DAL;

namespace BLL
{
    public class HorariosLogic
    {
        public Horarios Create(Horarios horario)
        {
            if (horario == null)
            {
                throw new ArgumentNullException("El objeto horario no puede ser nulo.");
            }

            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Validar si ya existe un horario para el mismo empleado y día
                Horarios _result = repository.Retrieve<Horarios>(h => h.EmpleadoID == horario.EmpleadoID && h.DiaSemana == horario.DiaSemana);

                if (_result != null)
                {
                    throw new Exception($"El horario ya existe para el empleado con ID {horario.EmpleadoID} en el día {horario.DiaSemana}.");
                }

                return repository.Create(horario);
            }
        }

        public Horarios RetrieveById(int id)
        {
            Horarios _horario = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                _horario = repository.Retrieve<Horarios>(h => h.HorarioID == id);
            }
            return _horario;
        }

        public bool Update(Horarios horario)
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                var existingHorario = repository.Retrieve<Horarios>(h => h.HorarioID == horario.HorarioID);

                if (existingHorario == null)
                {
                    throw new Exception("El horario no existe.");
                }

                existingHorario.EmpleadoID = horario.EmpleadoID;
                existingHorario.DiaSemana = horario.DiaSemana;
                existingHorario.HoraEntrada = horario.HoraEntrada;
                existingHorario.HoraSalida = horario.HoraSalida;

                return repository.Update(existingHorario);
            }
        }

        public bool Delete(int id)
        {
            bool _delete = false;
            var _horario = RetrieveById(id);
            if (_horario != null)
            {
                using (var repository = RepositoryFactory.CreateRepository())
                {
                    _delete = repository.Delete(_horario);
                }
            }
            return _delete;
        }

        public List<Horarios> RetrieveAll()
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                var horarios = repository.Filter<Horarios>(h => h.HorarioID > 0).ToList();
                return horarios;
            }
        }
    }
}
