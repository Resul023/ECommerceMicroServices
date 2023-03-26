﻿using System.Text.Json.Serialization;

namespace Discounts.API.Model;
[Dapper.Contrib.Extensions.Table("discount")]
public class Discount
{
    public int Id { get; set; }
    [JsonIgnore]
    public string? UserId { get; set; }
    public int Rate { get; set; }
    public string Code { get; set; }
    public DateTime CreatedTime { get; set; }
}