namespace Api_Mediconnet.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, object key)
            : base($"La entidad '{entityName}' con clave '{key}' no fue encontrada.") {}
    }
}