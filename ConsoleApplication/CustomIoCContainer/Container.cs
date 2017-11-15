using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using CustomIoCContainer.Attributes;

namespace CustomIoCContainer
{
    public class Container : IContainer
    {
        private readonly Dictionary<Type, Type> mapper;

        public Container()
        {
            mapper = new Dictionary<Type, Type>();
        }

        public void AddAssembly(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("Assembly does not exist");
            }

            var types = assembly.DefinedTypes.Where(type => type.IsClass || type.IsInterface);

            foreach (var type in types)
            {
                AddMappersForExportTypes(type);

                if (type.GetCustomAttribute<ImportConstructorAttribute>() != null)
                {
                    mapper.Add(type, type);
                }
            }
        }

        public void AddType(Type type)
        {
            if(type == null)
            {
                throw new ArgumentNullException("Can not add not existed type");
            }

            AddType(type, type);
        }

        public void AddType(Type type, Type baseType)
        {
            if (type == null || baseType == null)
            {
                throw new ArgumentNullException("Can not add not existed type");
            }

            mapper.Add(baseType, type);
        }

        public object CreateInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("Can not create instance of null type");
            }

            if (!mapper.ContainsKey(type))
            {
                throw new InvalidOperationException("The type does not exist");
            }

            var baseType = mapper[type];
            var ctorInfo = baseType.GetConstructors().FirstOrDefault();

            var createdType = Activator.CreateInstance(baseType);

            if (ctorInfo != null)
            {
                var parameters = ctorInfo.GetParameters();
            }

            SetPropertiesValues(createdType);

            return createdType;
        }

        public T CreateInstance<T>()
        {
            return (T)CreateInstance(typeof(T));
        }

        private void AddMappersForExportTypes(Type type)
        {
            var exports = type.GetCustomAttributes<ExportAttribute>();
            if (exports == null) return;

            foreach (var export in exports)
            {
                if(export.Contract != null)
                {
                    mapper.Add(type, type);
                }
            }
        }

        private void SetPropertiesValues(object instance)
        {
            var properties = instance.GetType().GetProperties()
                .Where(p => p.GetCustomAttributes<ImportAttribute>() != null);
            foreach (var property in properties)
            {
                property.SetValue(instance, CreateInstance(property.PropertyType));
            }
        }
    }
}
