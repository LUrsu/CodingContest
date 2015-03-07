using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CC.Domain.Entities;
using Ninject.Activation;

namespace CC.Web.App_Start
{
    public class ContextFactory : IProvider
    {

        public object Create(IContext context)
        {
            return new ContestEntities();
        }

        public Type Type
        {
            get { throw new NotImplementedException(); }
        }
    }
}
