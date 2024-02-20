namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreFinancial
{
    void GetAmoritizationSimple(float amount,float term,float interes,int share,DateTime dateFirstPay);
}
