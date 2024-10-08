﻿namespace SecondaApp.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        void Commit();
        void Rollback();
        int SaveChanges();

    }
}
