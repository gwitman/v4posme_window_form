using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DevExpress.XtraEditors.DXErrorProvider;

namespace v4posme_window.Dto;

public class FormShareEditDetailDTO : INotifyPropertyChanged, IDXDataErrorInfo
{
    //txtDetailCustomerCreditDocumentID
    private int _detailCustomerCreditDocumentId;

    //txtDetailTransactionDetailID
    private int _detailTransactionDetailId;

    //txtDetailTransactionDetailDocument
    private string _detailTransactionDetailDocument;

    //txtDetailTransactionDetailFecha
    private DateTime? _detailTransactionDetailFecha;

    //txtDetailAmortizationID
    private int _detailAmortizationId;

    //txtDetailBalanceStart
    private decimal _detailBalanceStart;

    //txtDetailBalanceFinish
    private decimal _detailBalanceFinish;

    //txtDetailShare
    private decimal _detailShare;

    //txtBalanceStartShare
    private decimal _detailBalanceStartShare;

    //txtBalanceFinishShare
    private decimal _detailBalanceFinishShare;

    public FormShareEditDetailDTO(int detailCustomerCreditDocumentId = default, int detailTransactionDetailId = default, string detailTransactionDetailDocument = "", DateTime detailTransactionDetailFecha = default, int detailAmortizationId = default, decimal detailBalanceStart = default, decimal detailBalanceFinish = default, decimal detailShare = default, decimal detailBalanceStartShare = default, decimal detailBalanceFinishShare = default)
    {
        _detailCustomerCreditDocumentId = detailCustomerCreditDocumentId;
        _detailTransactionDetailId = detailTransactionDetailId;
        _detailTransactionDetailDocument = detailTransactionDetailDocument;
        _detailTransactionDetailFecha = detailTransactionDetailFecha;
        _detailAmortizationId = detailAmortizationId;
        _detailBalanceStart = detailBalanceStart;
        _detailBalanceFinish = detailBalanceFinish;
        _detailShare = detailShare;
        _detailBalanceStartShare = detailBalanceStartShare;
        _detailBalanceFinishShare = detailBalanceFinishShare;
    }

    public FormShareEditDetailDTO()
    {
        _detailCustomerCreditDocumentId = 0;
        _detailTransactionDetailId = 0;
        _detailTransactionDetailDocument = string.Empty;
        _detailTransactionDetailFecha = DateTime.Now;
        _detailAmortizationId = 0;
        _detailBalanceStart = decimal.Zero;
        _detailBalanceFinish = decimal.Zero;
        _detailShare = decimal.Zero;
        _detailBalanceStartShare = decimal.Zero;
        _detailBalanceFinishShare = decimal.Zero;
    }

    public int DetailCustomerCreditDocumentId
    {
        get => _detailCustomerCreditDocumentId;
        set => SetField(ref _detailCustomerCreditDocumentId, value);
    }

    public int DetailTransactionDetailId
    {
        get => _detailTransactionDetailId;
        set => SetField(ref _detailTransactionDetailId, value);
    }

    public string DetailTransactionDetailDocument
    {
        get => _detailTransactionDetailDocument;
        set => SetField(ref _detailTransactionDetailDocument, value);
    }

    public DateTime? DetailTransactionDetailFecha
    {
        get => _detailTransactionDetailFecha;
        set => SetField(ref _detailTransactionDetailFecha, value);
    }

    public int DetailAmortizationId
    {
        get => _detailAmortizationId;
        set => SetField(ref _detailAmortizationId, value);
    }

    public decimal DetailBalanceStart
    {
        get => _detailBalanceStart;
        set => SetField(ref _detailBalanceStart, value);
    }

    public decimal DetailBalanceFinish
    {
        get => _detailBalanceFinish;
        set => SetField(ref _detailBalanceFinish, value);
    }

    public decimal DetailShare
    {
        get => _detailShare;
        set => SetField(ref _detailShare, value);
    }

    public decimal BalanceStartShare
    {
        get => _detailBalanceStartShare;
        set => SetField(ref _detailBalanceStartShare, value);
    }

    public decimal BalanceFinishShare
    {
        get => _detailBalanceFinishShare;
        set => SetField(ref _detailBalanceFinishShare, value);
    }

    public void GetPropertyError(string propertyName, ErrorInfo info)
    {
        if (propertyName == "DetailTransactionDetailDocument" && string.IsNullOrWhiteSpace(DetailTransactionDetailDocument))
        {
            info.ErrorText = $"El cmapo {propertyName} no puede estar vacío";
        }
    }

    public void GetError(ErrorInfo info)
    {
        Debug.WriteLine(info);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}