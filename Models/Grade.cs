using System;
using System.Collections.Generic;

namespace EduTrackerNew.Models;

public partial class Grade
{
    public int GradeId { get; set; }

    public int? StudentId { get; set; }

    public int? CourseId { get; set; }

    public int? TeacherId { get; set; }

    public string? GradeValue { get; set; }

    public DateOnly? GradeDate { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Student? Student { get; set; }

    public virtual Staff? Teacher { get; set; }
}
