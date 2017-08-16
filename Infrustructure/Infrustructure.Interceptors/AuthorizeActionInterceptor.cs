﻿using System;
using System.Linq;
using Castle.Core.Internal;
using Castle.DynamicProxy;
using KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Attributes;

namespace KhanyisaIntel.Kbit.Framework.Infrustructure.AOP.Interceptors
{
    public class AuthorizeActionInterceptor: InterceptorBase, IInterceptor
    {
        //public IUserRepository UserRepository { get; set; }
        public void Intercept(IInvocation invocation)
        {
            AuthorizeActionAttribute attribute = 
                invocation.Method.GetAttribute<AuthorizeActionAttribute>();

            if(attribute == null)
                invocation.Proceed();

            if (
                !invocation.MethodInvocationTarget.DeclaringType.GetInterfaces()
                    .Any(x => x.Name.Contains("IApplicationService")))
            {
                throw new InvalidOperationException($"Invalid attribute usage. " +
                                                    $"{nameof(AuthorizeActionAttribute)} can only be used " +
                                                    $"on ApplicationService types.");
            }

            invocation.Proceed();
        }
    }
}
