using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace v4posme_window.Dto;

public class FormInventoryItemEditWarehouseDTO : INotifyPropertyChanged
{
    public FormInventoryItemEditWarehouseDTO()
    {
        _warehouseName = "";
    }

    public FormInventoryItemEditWarehouseDTO(int warehouseId, string warehouseName, decimal quantity, decimal quantityMin, decimal quantityMax)
    {
        _warehouseId = warehouseId;
        _warehouseName = warehouseName;
        _quantity = quantity;
        _quantityMin = quantityMin;
        _quantityMax = quantityMax;
    }

    private int _warehouseId;

    public int WarehouseId
    {
        get => _warehouseId;
        set => SetField(ref _warehouseId, value);
    }

    private string _warehouseName;

    public string WarehouseName
    {
        get=>_warehouseName; 
        set=>SetField(ref _warehouseName, value);
    }

    private decimal _quantity;

    public decimal Quantity
    {
        get=>_quantity; 
        set=>SetField(ref _quantity, value);
    }

    private decimal _quantityMin;

    public decimal QuantityMin
    {
        get=>_quantityMin; 
        set=>SetField(ref _quantityMin, value);
    }

    private decimal _quantityMax;

    public decimal QuantityMax
    {
        get=>_quantityMax; 
        set=>SetField(ref _quantityMax, value);
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