namespace api.Enums;

public enum PlanChangeReason
{
    Upgrade = 1,
    Downgrade = 2,
    Trial = 3,
    Promotion = 4,
    Administrative = 5,
    PaymentFailure = 6
}
