using System.Reflection;

namespace CommonFramework.Miscellaneous
{
    public static class AssemblyUtil
    {
        public static Assembly GetRootAssembly()
        {
            // Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly() ?? Assembly.GetExecutingAssembly();
            Assembly rootAssembly = Assembly.GetEntryAssembly();
            if (rootAssembly == null)
            {
                // TODO: Add logic to check if calling assembly == executing assembly and crawl back up the stack until it isn't
                rootAssembly = Assembly.GetCallingAssembly();
            }
            if (rootAssembly == null)
            {
                rootAssembly = Assembly.GetExecutingAssembly();
            }
            return rootAssembly;
        }
    }
}
