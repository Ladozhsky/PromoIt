public interface IUserApplicationID<TUser> : IDisposable where TUser : class
    {
        Task SetUserApplicationIdAsync(TUser user, string applicationID, CancellationToken cancellationToken);

    }
