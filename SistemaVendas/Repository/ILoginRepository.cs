﻿using SistemaVendas.Models;
using System.Threading.Tasks;

namespace SistemaVendas.Repository
{
    public interface ILoginRepository
    {
        Task<VendedorModel> ValidarLogin(VendedorModel login);
    }
}
