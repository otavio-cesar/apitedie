﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using apitedie.Models;

namespace apitedie.Controllers
{
    public class EmpresasController : ApiController
    {
        static readonly IEmpresaRepositorio repositorio = new EmpresaRepositorio();

        public IEnumerable<Empresas> GetAllEmpresas()
        {
            return repositorio.GetAll();
        }

        public Empresas GetEmpresa(int id)
        {
            Empresas item = repositorio.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }
      
        public IEnumerable<Empresas> GetListaEmpresa(string token, int idempresa)
        {
            return repositorio.GetAll().Where(
                p => string.Equals(p.IdEmpresa.ToString(), idempresa.ToString(), StringComparison.OrdinalIgnoreCase));
        }

        [Route("api/empresas/GetListaEmpresaByCEP")]
        public IEnumerable<Empresas> GetListaEmpresaByCEP(int CEP)
        {
            var repo = repositorio.GetAll();
            return repo.Where(p => p.CEPInicial <= CEP && p.CEPFinal >= CEP).ToList();
        }
    }
}
