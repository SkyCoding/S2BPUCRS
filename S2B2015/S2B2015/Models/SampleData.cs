// ----------------------------------------------------------------------------------
// Microsoft Developer & Platform Evangelism
// 
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
// ----------------------------------------------------------------------------------
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
// ----------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace S2B2015.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<S2BStoreEntities>
    {
        protected override void Seed(S2BStoreEntities context)
        {

            var categoria = AddCategorias(context);

            context.SaveChanges();
        }

        private static List<Categoria> AddCategorias(S2BStoreEntities context)
        {
            var categorias = new List<Categoria>
            {
                new Categoria { strTitulo = "TESTE", strDescrição="TESTEE " },
                
            };

            categorias.ForEach(s => context.Categorias.Add(s));
            context.SaveChanges();
            return categorias;
        }
    }
}
  