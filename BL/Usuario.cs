using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezExamenMoviesApiContext context = new DL.JrodriguezExamenMoviesApiContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"[UsuarioGetById] {IdUsuario}").AsEnumerable().FirstOrDefault();

                    ML.Usuario usuario = new ML.Usuario();

                    usuario.IdUsuario = query.IdUsuario;
                    usuario.UserName = query.UserName;
                    usuario.Password = query.Password;

                    result.Object = usuario;
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetByUserName(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.JrodriguezExamenMoviesApiContext context = new DL.JrodriguezExamenMoviesApiContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"[UsuarioGetByUserName] '{usuario.UserName}'").AsEnumerable().FirstOrDefault();

                    usuario = new ML.Usuario();
                    usuario.UserName = query.UserName;
                    usuario.Password = query.Password;

                    result.Object = usuario;
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}