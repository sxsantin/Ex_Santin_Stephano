import zeep

# URL del servicio SOAP
url = 'http://localhost:64635/HorariosService.asmx?wsdl'

# Crear un cliente SOAP
client = zeep.Client(url)

# Obtener todos los horarios
def get_all_horarios():
    try:
        horarios = client.service.GetAllHorarios()
        for horario in horarios:
            print(f"ID: {horario.HorarioID}, EmpleadoID: {horario.EmpleadoID}, Día: {horario.DiaSemana}, Entrada: {horario.HoraEntrada}, Salida: {horario.HoraSalida}")
    except Exception as e:
        print(f"Error: {e}")

# Obtener un horario por ID
def get_horario_by_id(horario_id):
    try:
        horario = client.service.GetHorarioById(horario_id)
        print(f"ID: {horario.HorarioID}, EmpleadoID: {horario.EmpleadoID}, Día: {horario.DiaSemana}, Entrada: {horario.HoraEntrada}, Salida: {horario.HoraSalida}")
    except Exception as e:
        print(f"Error: {e}")

# Crear un nuevo horario
def create_horario(empleado_id, dia_semana, hora_entrada, hora_salida):
    try:
        # Crear el objeto horario con todos los campos requeridos
        horario = {
            'HorarioID': 0,  # Puedes poner 0 o un valor predeterminado si el servicio lo maneja automáticamente
            'EmpleadoID': empleado_id,
            'DiaSemana': dia_semana,
            'HoraEntrada': hora_entrada,
            'HoraSalida': hora_salida
        }
        # Llamar al servicio para crear el horario
        client.service.CreateHorario(horario)
        print("Horario creado exitosamente.")
    except Exception as e:
        print(f"Error: {e}")

# Actualizar un horario
def update_horario(horario_id, empleado_id, dia_semana, hora_entrada, hora_salida):
    try:
        horario = {
            'HorarioID': horario_id,
            'EmpleadoID': empleado_id,
            'DiaSemana': dia_semana,
            'HoraEntrada': hora_entrada,
            'HoraSalida': hora_salida
        }
        client.service.UpdateHorario(horario)
        print("Horario actualizado exitosamente.")
    except Exception as e:
        print(f"Error: {e}")

# Eliminar un horario
def delete_horario(horario_id):
    try:
        client.service.DeleteHorario(horario_id)
        print("Horario eliminado exitosamente.")
    except Exception as e:
        print(f"Error: {e}")

# Ejemplo de uso
get_all_horarios()
#get_horario_by_id(10)
#create_horario(1, "Viernes", "08:00:00", "17:00:00")
#update_horario(1, 1, "Martes", "09:00:00", "18:00:00")
#delete_horario(11)
