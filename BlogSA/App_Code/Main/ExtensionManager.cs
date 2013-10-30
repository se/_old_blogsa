using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Web.Configuration;
using System.Reflection;
using System.IO;

public class ExtensionManager
{
    public static ArrayList CodeAssemblies()
    {
        ArrayList codeAssemblies = new ArrayList();
        CompilationSection s = null;
        try
        {
            string assemblyName = "__code";
            try
            {
                s = (CompilationSection)WebConfigurationManager.GetSection("system.web/compilation");
            }
            catch (System.Security.SecurityException)
            {
                // No read permissions on web.config due to the trust level (must be High or Full)
            }

            if (s != null && s.CodeSubDirectories != null && s.CodeSubDirectories.Count > 0)
            {
                for (int i = 0; i < s.CodeSubDirectories.Count; i++)
                {
                    assemblyName = "App_SubCode_" + s.CodeSubDirectories[i].DirectoryName;
                    codeAssemblies.Add(Assembly.Load(assemblyName));
                }
            }
            else
            {
                Type t = Type.GetType("Mono.Runtime");
                if (t != null) assemblyName = "App_Code";
                codeAssemblies.Add(Assembly.Load(assemblyName));
            }
            //
            GetCompiledExtensions(codeAssemblies);
        }
        catch (System.IO.FileNotFoundException) {/*ignore - code directory has no files*/}

        return codeAssemblies;
    }
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ExtensionAttribute : System.Attribute
    {
        public ExtensionAttribute(string description, string version, string author)
        {
            _Description = description;
            _Version = version;
            _Author = author;
        }
        public ExtensionAttribute(string description, string version, string author, int priority)
        {
            _Description = description;
            _Version = version;
            _Author = author;
            _priority = priority;
        }
        private string _Description;
        public string Description
        {
            get { return _Description; }
        }
        private string _Version;
        public string Version
        {
            get { return _Version; }
        }
        private string _Author;
        public string Author
        {
            get { return _Author; }
        }
        private int _priority = 999;
        public int Priority
        {
            get { return _priority; }
        }
    }

    public class SortedExtension
    {
        public int Priority;
        public string Name;
        public string Type;

        public SortedExtension(int p, string n, string t)
        {
            Priority = p;
            Name = n;
            Type = t;
        }
    }

    public static void GetCompiledExtensions(ArrayList assemblies)
    {
        string s = Path.Combine(HttpContext.Current.Server.MapPath("~/"), "bin");
        string[] fileEntries = Directory.GetFiles(s);
        foreach (string fileName in fileEntries)
            if (fileName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
            {
                Assembly asm = Assembly.LoadFrom(fileName);
                object[] attr = asm.GetCustomAttributes(typeof(AssemblyConfigurationAttribute), false);
                if (attr.Length > 0)
                {
                    AssemblyConfigurationAttribute aca = (AssemblyConfigurationAttribute)attr[0];
                }
            }
    }

    public static void CompileExtension()
    {
        ArrayList codeAssemblies = CodeAssemblies();
        List<SortedExtension> sortedExtensions = new List<SortedExtension>();

        foreach (Assembly a in codeAssemblies)
        {
            Type[] types = a.GetTypes();
            foreach (Type type in types)
            {
                object[] attributes = type.GetCustomAttributes(typeof(ExtensionAttribute), false);
                foreach (object attribute in attributes)
                {
                    if (attribute.GetType().Name == "ExtensionAttribute")
                    {
                        ExtensionAttribute ext = (ExtensionAttribute)attribute;
                        sortedExtensions.Add(new SortedExtension(ext.Priority, type.Name, type.FullName));
                    }
                }
            }

            sortedExtensions.Sort(delegate(SortedExtension e1, SortedExtension e2)
            { return e1.Priority.CompareTo(e2.Priority); });
            foreach (SortedExtension x in sortedExtensions)
            {
                a.CreateInstance(x.Type);
            }
        }
    }
}