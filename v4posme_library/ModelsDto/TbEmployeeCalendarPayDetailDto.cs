namespace v4posme_library.ModelsDto;

public record TbEmployeeCalendarPayDetailDto()
{
    public int CalendarDetailId { get; set; }
    public int CalendarId { get; set; }
    public int EmployeeId { get; set; }
    public decimal Salary { get; set; }
    public decimal Commission { get; set; }
    public decimal Adelantos { get; set; }
    public decimal Neto { get; set; }
    public ulong IsActive { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmployeNumber { get; set; }
    public decimal HourCost { get; set; }
    public decimal ComissionPorcentage { get; set; }
}