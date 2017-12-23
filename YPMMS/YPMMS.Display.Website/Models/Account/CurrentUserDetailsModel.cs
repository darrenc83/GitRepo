namespace YPMMS.Display.Website.Models.Account
{
    /// <summary>
    /// Convenience POCO to store basic information about the current user in session
    /// </summary>
    public sealed class CurrentUserDetailsModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }

        public long Account { get; set; }
        public string Logo { get; set; }

    }
}