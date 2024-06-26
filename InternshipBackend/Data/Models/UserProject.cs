﻿using InternshipBackend.Core;

namespace InternshipBackend.Data.Models;

public class UserProject : UserOwnedEntity
{
    public required User User { get; set; }
    public required string ProjectName { get; set; }
    public required string Description { get; set; }
    public string? ProjectThumbnail { get; set; }
    public string? ProjectLink { get; set; }
}
