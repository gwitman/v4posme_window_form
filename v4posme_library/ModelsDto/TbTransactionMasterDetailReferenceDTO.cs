using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace v4posme_library.ModelsDto;

public class TbTransactionMasterDetailReferenceDTO : INotifyPropertyChanged
{
    private int _transactionMasterDetailReferenceId;
    private int _transactionMasterDetailId;
    private int _componentId;
    private int _componentItemId;
    private short _isActive;
    private string _quantity;
    private DateTime? _createdOn;
    private string? _ItemName;
    private decimal _unitaryPrice;
    private decimal _amount;
    private decimal _tax1;

    public int TransactionMasterDetailReferenceId
    {
        get => _transactionMasterDetailReferenceId;
        set => SetField(ref _transactionMasterDetailReferenceId, value);
    }

    public int TransactionMasterDetailId
    {
        get => _transactionMasterDetailId;
        set => SetField(ref _transactionMasterDetailId, value);
    }

    public int ComponentId
    {
        get => _componentId;
        set => SetField(ref _componentId, value);
    }

    public int ComponentItemId
    {
        get => _componentItemId;
        set => SetField(ref _componentItemId, value);
    }

    public short IsActive
    {
        get => _isActive;
        set => SetField(ref _isActive, value);
    }

    public string Quantity
    {
        get => _quantity;
        set => SetField(ref _quantity, value);
    }

    public DateTime? CreatedOn
    {
        get => _createdOn;
        set => SetField(ref _createdOn, value);
    }

    public string? ItemName
    {
        get => _ItemName;
        set => SetField(ref _ItemName, value);
    }

    public decimal UnitaryPrice
    {
        get => _unitaryPrice;
        set => SetField(ref _unitaryPrice, value);
    }

    public decimal Amount
    {
        get => _amount;
        set => SetField(ref _amount, value);
    }

    public decimal Tax1
    {
        get => _tax1;
        set => SetField(ref _tax1, value);
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