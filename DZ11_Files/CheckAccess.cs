using System.Security.AccessControl;
using System.Security.Principal;

namespace DZ11_Files
{
    internal class CheckAccess
    {
        // Доступность файла
        public static bool HasFileAccess(string filePath, FileSystemRights accessRight)
        {
            try
            {
                var fileInfo = new FileInfo(filePath);
                var fileSecurity = fileInfo.GetAccessControl();
                if (fileSecurity == null) 
                {
                    return false;
                }
                
                var rules = fileSecurity.GetAccessRules(true, true, typeof(NTAccount));
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);

                foreach (FileSystemAccessRule rule in rules)
                {
                    if (principal.IsInRole(rule.IdentityReference.Value) &&     // Относится ли текущий пользователь к тем, кому можно
                        (rule.FileSystemRights & accessRight) == accessRight && // Право совпадает с проверяемым
                        rule.AccessControlType == AccessControlType.Allow)      // Можно
                    {
                        return true;
                    }
                }

                return false; // ничего не нашлось, значит, нельзя
            }
            catch (UnauthorizedAccessException)
            {
                return false; // Нельзя совсем
            }
        }

        // Доступность директории
        public static bool HasDirectoryAccess(string directoryPath, FileSystemRights accessRight)
        {
            try
            {
                var fileInfo = new DirectoryInfo(directoryPath);
                var fileSecurity = fileInfo.GetAccessControl();
                if (fileSecurity == null)
                {
                    return false;
                }

                var rules = fileSecurity.GetAccessRules(true, true, typeof(NTAccount));
                var identity = WindowsIdentity.GetCurrent();
                var principal = new WindowsPrincipal(identity);

                foreach (FileSystemAccessRule rule in rules)
                {
                    if (principal.IsInRole(rule.IdentityReference.Value) &&     // Относится ли текущий пользователь к тем, кому можно
                        (rule.FileSystemRights & accessRight) == accessRight && // Право совпадает с проверяемым
                        rule.AccessControlType == AccessControlType.Allow)      // Можно
                    {
                        return true;
                    }
                }

                return false; // ничего не нашлось, значит, нельзя
            }
            catch (UnauthorizedAccessException)
            {
                return false; // Нельзя совсем
            }
        }
    }
}
