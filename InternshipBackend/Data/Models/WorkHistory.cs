﻿using System.ComponentModel.DataAnnotations;
using InternshipBackend.Core;
using InternshipBackend.Data.Models.Enums;

namespace InternshipBackend.Data.Models;

public class WorkHistory : UserOwnedEntity
{
    public required string Position { get; set; }
    public required string CompanyName { get; set; }
    [DataType(DataType.Date)]
    public required DateTime StartDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set;}
    public bool IsWorkingNow { get; set; }
    public string? Description { get; set; }
    public string? Duties { get; set; }
    public WorkType WorkType { get; set; }
    public string? ReasonForLeave { get; set; }
}
