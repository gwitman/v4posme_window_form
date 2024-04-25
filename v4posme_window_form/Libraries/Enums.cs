namespace v4posme_window.Libraries;

public enum TypeError
{
    Informacion,
    Error,
    Warning
}


public enum TypeOpenForm
{
    Init,
    NotInit
}
public enum TypeRender
{
    New,
    Edit
}

public enum TypePrice
{
    Publico = 154,
    PorMayor = 155,
    Credito = 156,
    CreditoPorMayor = 477,
    Especial = 478
}

public enum WorkflowStatus
{    
    FacturaStatusAplicado = 67,
    FacturaStatusAnulado = 68
}