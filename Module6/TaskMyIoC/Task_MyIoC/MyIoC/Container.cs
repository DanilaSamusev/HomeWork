using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyIoC
{
	public class Container
	{
		private readonly IDictionary<Type, Type> _registeredTipes;

		public Container()
		{
			_registeredTipes = new Dictionary<Type, Type>();
		}

		public void AddAssembly(Assembly assembly)
		{
			IEnumerable<Type> types = assembly.ExportedTypes;

			foreach (var type in types)
			{
				var constructorImportAttribute = type.GetCustomAttribute<ImportConstructorAttribute>();

				if (constructorImportAttribute != null || HasPropertyToImport(type))
				{
					_registeredTipes.Add(type, type);
				}

				var exportAttributes = type.GetCustomAttributes<ExportAttribute>();

				foreach (var exportAttribute in exportAttributes)
				{
					_registeredTipes.Add(exportAttribute.Contract ?? type, type);
				}
			}
		}

		public void AddType(Type type)
		{
			if (type == null)
			{
				throw new NullReferenceException($"{nameof(type)} is null!");
			}

			_registeredTipes.Add(type, type);
		}

		public void AddType(Type type, Type baseType)
		{
			if (type == null)
			{
				throw new NullReferenceException($"{nameof(type)} is null!");
			}

			if (baseType == null)
			{
				throw new NullReferenceException($"{nameof(baseType)} is null!");
			}

			_registeredTipes.Add(type, baseType);
		}

		public object CreateInstance(Type type)
		{
			Type actualType = _registeredTipes[type];

			if (actualType == null)
            {
				throw new Exception($"{nameof(type)} is not registered!");
            }

			var newInstance = Activator.CreateInstance(actualType, ResolveConstructorParameters(actualType));

			if (actualType.GetCustomAttribute<ImportConstructorAttribute>() != null)
			{
				return newInstance;
			}

			ResolveProperties(actualType, newInstance);

			return newInstance;
		}

		public T CreateInstance<T>()
		{
			var type = typeof(T);
			var instance = (T)CreateInstance(type);
			return instance;
		}

		public void Sample()
		{
			var container = new Container();
			container.AddAssembly(Assembly.GetExecutingAssembly());

			var customerBLL = (CustomerBLL)container.CreateInstance(typeof(CustomerBLL));
			var customerBLL2 = container.CreateInstance<CustomerBLL>();

			container.AddType(typeof(CustomerBLL));
			container.AddType(typeof(Logger));
			container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));
		}

		private object[] ResolveConstructorParameters(Type actualType)
		{
			var constructorInfo = actualType.GetConstructors().First();

			return constructorInfo.GetParameters().Select(parameter => CreateInstance(parameter.ParameterType)).ToArray();
		}

		private void ResolveProperties(Type type, object instance)
		{
			var propertiesInfo = GetPropertiesToImport(type);

			foreach (var property in propertiesInfo)
			{
				var resolvedProperty = CreateInstance(property.PropertyType);
				property.SetValue(instance, resolvedProperty);
			}
		}

		private IEnumerable<PropertyInfo> GetPropertiesToImport(Type type)
		{
			return type.GetProperties().Where(p => p.GetCustomAttribute<ImportAttribute>() != null);
		}

		private bool HasPropertyToImport(Type type)
		{
			var propertiesInfo = GetPropertiesToImport(type);

			return propertiesInfo.Any();
		}
	}
}
