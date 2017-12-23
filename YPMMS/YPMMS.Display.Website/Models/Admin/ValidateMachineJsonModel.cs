namespace YPMMS.Display.Website.Models.Admin
{
    /// <summary>
    /// Model returned from a web API call to validate machine details entered by a user
    /// </summary>
    public sealed class ValidateMachineJsonModel
    {
        /// <summary>
        /// Error message to show on the Machine ID field
        /// </summary>
        public string IdErrorMessage { get; set; }

        /// <summary>
        /// Error message to show on the Machine Name field
        /// </summary>
        public string NameErrorMessage { get; set; }

        public bool IdError => !string.IsNullOrEmpty(IdErrorMessage);
        public bool NameError => !string.IsNullOrEmpty(NameErrorMessage);
    }
}
