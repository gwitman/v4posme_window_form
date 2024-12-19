using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace v4posme_window.Dto;

public class FormInputCashDetailDto : INotifyPropertyChanged
{
    private string _transactionMasterDenominationName;
    private int _transactionMasterDenominationId;
    private int _transactionMasterDenominationCatalogItemId;
    private int _transactionMasterDenominationCurrencyId;
    private decimal _transactionMasterDenominationExchangeRate;
    private string _transactionMasterDenominationReference;
    private decimal _transactionMasterDenominationQuantity;
    public event PropertyChangedEventHandler? PropertyChanged;

    public FormInputCashDetailDto()
    {
        _transactionMasterDenominationName = string.Empty;
        _transactionMasterDenominationId = 0;
        _transactionMasterDenominationCatalogItemId = 0;
        _transactionMasterDenominationCurrencyId = 0;
        _transactionMasterDenominationExchangeRate = decimal.Zero;
        _transactionMasterDenominationReference = string.Empty;
        _transactionMasterDenominationQuantity = decimal.Zero;
    }
    public FormInputCashDetailDto(string transactionMasterDenominationName, int transactionMasterDenominationId, int transactionMasterDenominationCatalogItemId, int transactionMasterDenominationCurrencyId, decimal transactionMasterDenominationExchangeRate, string transactionMasterDenominationReference, decimal transactionMasterDenominationQuantity)
    {
        _transactionMasterDenominationName = transactionMasterDenominationName;
        _transactionMasterDenominationId = transactionMasterDenominationId;
        _transactionMasterDenominationCatalogItemId = transactionMasterDenominationCatalogItemId;
        _transactionMasterDenominationCurrencyId = transactionMasterDenominationCurrencyId;
        _transactionMasterDenominationExchangeRate = transactionMasterDenominationExchangeRate;
        _transactionMasterDenominationReference = transactionMasterDenominationReference;
        _transactionMasterDenominationQuantity = transactionMasterDenominationQuantity;
    }

    public string TransactionMasterDenominationName
    {
        get => _transactionMasterDenominationName;
        set => SetField(ref _transactionMasterDenominationName, value);
    }

    public int TransactionMasterDenominationId
    {
        get => _transactionMasterDenominationId;
        set => SetField(ref _transactionMasterDenominationId, value);
    }

    public int TransactionMasterDenominationCatalogItemId
    {
        get => _transactionMasterDenominationCatalogItemId;
        set => SetField(ref _transactionMasterDenominationCatalogItemId, value);
    }

    public int TransactionMasterDenominationCurrencyId
    {
        get => _transactionMasterDenominationCurrencyId;
        set => SetField(ref _transactionMasterDenominationCurrencyId, value);
    }

    public decimal TransactionMasterDenominationExchangeRate
    {
        get => _transactionMasterDenominationExchangeRate;
        set => SetField(ref _transactionMasterDenominationExchangeRate, value);
    }

    public string TransactionMasterDenominationReference
    {
        get => _transactionMasterDenominationReference;
        set => SetField(ref _transactionMasterDenominationReference, value);
    }

    public decimal TransactionMasterDenominationQuantity
    {
        get => _transactionMasterDenominationQuantity;
        set => SetField(ref _transactionMasterDenominationQuantity, value);
    }

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