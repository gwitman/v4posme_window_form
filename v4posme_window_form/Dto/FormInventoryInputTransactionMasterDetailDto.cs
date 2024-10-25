using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using DevExpress.Utils.MVVM;

namespace v4posme_window.Dto;

public class FormInventoryInputTransactionMasterDetailDto : INotifyPropertyChanged
{
    private int _itemId;
    private int _transactionMasterDetailId;
    private string? _codigo;
    private string? _nombre;
    private string? _unidadMedida;
    private decimal _quantity;
    private decimal _costo;
    private decimal _precio;
    private decimal _precio2;
    private decimal _precio3;
    private string? _lote;
    private string? _reference4;
    private DateTime? _vencimiento;
    private string? _barCodeExtende;

    public event PropertyChangedEventHandler? PropertyChanged;

    public FormInventoryInputTransactionMasterDetailDto()
    {
        
    }

    public FormInventoryInputTransactionMasterDetailDto(int itemId, int transactionMasterDetailId, string? codigo, string? nombre, string? unidadMedida, decimal quantity, decimal costo, decimal precio, decimal precio2, decimal precio3, string? lote, string? reference4, DateTime vencimiento, string? barCodeExtende)
    {
        _itemId = itemId;
        _transactionMasterDetailId = transactionMasterDetailId;
        _codigo = codigo;
        _nombre = nombre;
        _unidadMedida = unidadMedida;
        _quantity = quantity;
        _costo = costo;
        _precio = precio;
        _precio2 = precio2;
        _precio3 = precio3;
        _lote = lote;
        _reference4 = reference4;
        _vencimiento = vencimiento;
        _barCodeExtende = barCodeExtende;
    }

    public int ItemId
    {
        get => _itemId;
        set => SetField(ref _itemId, value);
    }

    public int TransactionMasterDetailId
    {
        get => _transactionMasterDetailId;
        set => SetField(ref _transactionMasterDetailId, value);
    }

    public string? Codigo
    {
        get => _codigo;
        set => SetField(ref _codigo, value);
    }

    public string? Nombre
    {
        get => _nombre;
        set => SetField(ref _nombre, value);
    }

    public string? UnidadMedida
    {
        get => _unidadMedida;
        set => SetField(ref _unidadMedida, value);
    }

    public decimal Quantity
    {
        get => _quantity;
        set => SetField(ref _quantity, value);
    }

    public decimal Costo
    {
        get => _costo;
        set => SetField(ref _costo, value);
    }

    public decimal Precio
    {
        get => _precio;
        set => SetField(ref _precio, value);
    }

    public decimal Precio2
    {
        get => _precio2;
        set => SetField(ref _precio2, value);
    }

    public decimal Precio3
    {
        get => _precio3;
        set => SetField(ref _precio3, value);
    }

    public string? Lote
    {
        get => _lote;
        set => SetField(ref _lote, value);
    }

    public string? Reference4
    {
        get => _reference4;
        set => SetField(ref _reference4, value);
    }

    public DateTime? Vencimiento
    {
        get => _vencimiento;
        set => SetField(ref _vencimiento, value);
    }

    public string? BarCodeExtende
    {
        get => _barCodeExtende;
        set => SetField(ref _barCodeExtende, value);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}