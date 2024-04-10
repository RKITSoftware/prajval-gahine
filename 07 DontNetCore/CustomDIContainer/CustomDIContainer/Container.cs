using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomDIContainer
{
    internal class Container
    {
        /// <summary>
        /// Dictionary for registring services
        /// </summary>
        private readonly Dictionary<Type, Func<object>> regs = new();

        /// <summary>
        /// Registering a Service using an base type
        /// </summary>
        /// <typeparam name="TService">Service Type</typeparam>
        /// <typeparam name="TImpl">Implementation type</typeparam>
        public void Register<TService, TImpl>() where TImpl : TService
        {
            regs.Add(typeof(TService), () => this.GetInstance(typeof(TImpl)));
        }

        /// <summary>
        /// Registering a Service using a factory method
        /// </summary>
        /// <typeparam name="TService">Service type</typeparam>
        /// <param name="factory">Factory method to generate instance of given type</param>
        public void Register<TService>(Func<TService> factory)
        {
            regs.Add(typeof(TService), () => factory());
        }

        /// <summary>
        /// Registering a service using a instance of that service
        /// </summary>
        /// <typeparam name="TService">Service type</typeparam>
        /// <param name="instance">Instance of TService</param>
        public void Register<TService>(TService instance)
        {
            regs.Add(typeof(TService), () => instance);
        }

        /// <summary>
        /// Registering a Singleton instance of TService
        /// </summary>
        /// <typeparam name="TService">Service Type</typeparam>
        /// <param name="factory">Factory method to generate instance of TService</param>
        public void RegisterSingleton<TService>(Func<TService> factory)
        {
            Lazy<TService> lazy = new Lazy<TService>(factory);
            Register(() => lazy.Value);
        }

        /// <summary>
        /// Getting the instacne of specified type from the container
        /// </summary>
        /// <param name="type">Service's type instance</param>
        /// <returns>Instance of TService registered</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public object GetInstance(Type type)
        {
            bool isAbstract = type.IsAbstract;
            bool exists = regs.TryGetValue(type, out Func<object> fac);
            if (exists) {
                return fac();
            }
            else if (!type.IsAbstract)
            {
                return this.CreateInstance(type);
            }
            throw new InvalidOperationException("no registration for: " + type);
        }

        /// <summary>
        /// Method to create Instance of specified type
        /// </summary>
        /// <param name="implementationType">Service type instance</param>
        /// <returns>Instance of Specified service</returns>
        private object CreateInstance(Type implementationType)
        {
            ConstructorInfo ctorInfo = implementationType.GetConstructors().First();
            IEnumerable<Type> paramTypes = ctorInfo.GetParameters().Select(p => p.ParameterType);
            var dependencies = paramTypes.Select(GetInstance).ToArray();
            return Activator.CreateInstance(implementationType, dependencies);
        }
    }
}
