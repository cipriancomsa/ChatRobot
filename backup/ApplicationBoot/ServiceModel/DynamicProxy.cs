namespace ApplicationBoot.ServiceModel
{
    using System;
    using System.Reflection;
    using System.Runtime.Remoting.Messaging;
    using System.Runtime.Remoting.Proxies;

    public class DynamicProxy<TService> : RealProxy
    {
        private readonly TService service;

        public DynamicProxy(TService service)
            : base(typeof (TService))
        {
            this.service = service;
        }

        public override IMessage Invoke(IMessage msg)
        {
            var methodCall = msg as IMethodCallMessage;
            var methodInfo = methodCall.MethodBase as MethodInfo;
            try
            {
                object result = methodInfo.Invoke(this.service, methodCall.InArgs);

                return new ReturnMessage(result, null, 0,
                    methodCall.LogicalCallContext, methodCall);
            }
            catch (Exception e)
            {
                return new ReturnMessage(e, methodCall);
            }
        }

        public TResult GetInstance<TResult>()
        {
            return (TResult) this.GetTransparentProxy();
        }
    }
}