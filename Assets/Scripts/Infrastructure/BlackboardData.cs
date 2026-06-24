using System;

public class BlackboardData
{
    public string clave;
    public Type tipo;
    public object valor;

    public BlackboardData(string clave, Type tipo, object valor)
    {
        this.clave = clave;
        this.tipo = tipo;
        this.valor = valor;
    }
}