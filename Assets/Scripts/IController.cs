using System;

namespace DefaultCompany
{
    public interface IController : IDisposable
    {
        void Initialize();
    }
}