using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace DraftBook.Utilities
{
    public class DelegateFactory
    {
        public static D CreateDelegate<D>(MethodInfo methodInfo, Type[] parameterTypes) where D : class
        {
            if (methodInfo == null)
            {
                throw new ArgumentNullException("methodInfo");
            }
            if (parameterTypes == null)
            {
                throw new ArgumentNullException("parameterTypes");
            }
            var parameters = methodInfo.GetParameters();
            var dynamicMethod = new DynamicMethod(
                methodInfo.Name, 
                MethodAttributes.Static | MethodAttributes.Public, 
                CallingConventions.Standard, 
                methodInfo.ReturnType, 
                parameterTypes, 
                typeof(object), 
                true)
            {
                InitLocals = false
            };

            var dynamicEmit = new DynamicEmit(dynamicMethod);
            if (!methodInfo.IsStatic)
            {
                dynamicEmit.LoadArgument(0);
                dynamicEmit.CastTo(typeof(object), methodInfo.DeclaringType);
            }
            for (int index = 0; index < parameters.Length; index++)
            {
                dynamicEmit.LoadArgument(index + 1);
                dynamicEmit.CastTo(parameterTypes[index + 1], parameters[index].ParameterType);
            }
            dynamicEmit.Call(methodInfo);
            dynamicEmit.Return();

            return dynamicMethod.CreateDelegate(typeof(D)) as D;
        }
    }
}
