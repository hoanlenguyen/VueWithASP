using System.ComponentModel.DataAnnotations;

namespace webapi.Model.NamedModels;
public class NamedModelRequest
{
    public string TypeName { get; set; } = default!;
    public string? SearchKey { get; set; }
    [Range(1, 500)]
    public int? Take { get; set; } = 500;
    public int? Skip { get; set; }
}
