using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Plan
{



[Key]
public int Id { get; set; }

[Required]
public string PlanName { get; set; } = string.Empty;
public string? PlanDescription { get; set; }

[Required]
public decimal CurrentMonthlyPrice { get; set; }
public decimal? CurrentYearlyPrice { get; set; }

public bool IsOfferAvailable { get; set; } = false;

[Required]
public int UserGroupTypeId { get; set; }
public UserGroupType UserGroupType { get; set; }

[Required]
public int SoftwareId { get; set; }
public Software Software { get; set; }

public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
public ICollection<Include> Includes { get; set; } = new List<Include>();
public ICollection<Prerequisite> Prerequisites { get; set; } = new List<Prerequisite>();
public ICollection<OptionIncluded> OptionIncludes { get; set; } = new List<OptionIncluded>();
public ICollection<PlanHistory> PlanHistories { get; set; } = new List<PlanHistory>();



}
