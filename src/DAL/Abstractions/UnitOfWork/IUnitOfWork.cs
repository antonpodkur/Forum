using System;
using DAL.Abstractions.Repositories;

namespace DAL.Abstractions.UnitOfWork
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        int Complete();
    }
}