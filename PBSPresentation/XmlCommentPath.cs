using System.Reflection;

namespace PBSPresentation
{
    public class XmlCommentPath
    {
        static string XmlCommentsFilePath
        {
            get
            {
                var basePath = PBSPresentation.XmlCommentPath.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }
    }
}
