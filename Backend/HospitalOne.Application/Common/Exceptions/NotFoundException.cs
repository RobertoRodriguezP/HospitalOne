namespace HospitalOne.Application.Common.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"Entidad \"{name}\" ({key}) no fue encontrada.")
    {
    }
}