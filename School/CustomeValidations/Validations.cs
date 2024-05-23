namespace School.CustomeValidations
{
    public class Validations
    {
        public string? NameValidation { get; set; } = "[A-Za-z\\s]{3,50}";
        public string? PassWordValidation { get; set; } = "[A-Z]{1}[a-z0-9@#$%_-]{7,15}";
        public string? MobileNumberValidation { get; set; } = "[6-9]\\d{9}";
    }
}
