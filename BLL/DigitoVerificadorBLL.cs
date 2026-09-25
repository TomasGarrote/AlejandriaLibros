using BE;
using DAL;
using Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace BLL
{
    public class DigitoVerificadorBLL
    {
        private readonly DigitoVerificadorDAL _controlDVDal = new DigitoVerificadorDAL();
        private readonly PerfilDAL _perfilDal = new PerfilDAL();
        private readonly UsuarioDAL _usuarioDal = new UsuarioDAL();
        private readonly BitacoraDAL _bitacoraDal = new BitacoraDAL();
        private readonly IdiomaDAL _idiomaDal = new IdiomaDAL();
        private readonly VentaDAL _ventaDal = new VentaDAL();
        private readonly LibroDAL _libroDal = new LibroDAL();


        public void RecalcularDVH_Usuario()
        {
            var listaUsuarios = _usuarioDal.ListarTodosLosUsuariosDVH();
            foreach (var usuario in listaUsuarios)
            {
                string dvhCalculado = usuario.DNI +
                        usuario.Nombre +
                        usuario.Apellido +
                        usuario.Username +
                        usuario.Password +
                        usuario.Email +
                        usuario.Bloqueado +
                        usuario.Activo +
                        usuario.Rol;

                
                _usuarioDal.ActualizarDVH(usuario.DNI, DigitoVerificador.CalcularDVH(dvhCalculado));
            }
        }

        public void RecalcularDVH_Venta()
        {
            var listaVentas = _ventaDal.ListarTodasLasVentas();
            foreach (var venta in listaVentas)
            {
                string dvhCalculado = venta.NroVenta +
                        venta.Fecha.ToString("yyyy-MM-dd HH:mm:ss") +
                        venta.DniUsuario +
                        venta.MedioDePago +
                        venta.Descuento.ToString() +
                        venta.Subtotal.ToString() +
                        venta.Total.ToString() +
                        venta.Detalle.ToString();
                
                _ventaDal.ActualizarVentaDVH(venta.NroVenta.ToString(), DigitoVerificador.CalcularDVH(dvhCalculado));
            }
        }

        public void RecalcularDVH_Detalle()
        {
            var listaDetalleVentas = _ventaDal.ListarTodosLosDetalles();
            foreach (var detVenta in listaDetalleVentas)
            {
                string dvhCalculado = detVenta.NroVenta.ToString() +
                        detVenta.CodigoLibro.ToString() +
                        detVenta.PrecioUnitario.ToString() +
                        detVenta.Cantidad.ToString() +
                        detVenta.Subtotal.ToString();

                _ventaDal.ActualizarDetalleDVH(detVenta.NroVenta.ToString(), DigitoVerificador.CalcularDVH(dvhCalculado));
            }
        }

        public void RecalcularDVH_Libro()
        {
            var listarLibros = _libroDal.ListarTodosLosLibros();
            foreach (var libro in listarLibros)
            {
                string dvhCalculado = libro.CodigoInterno.ToString()
                    + libro.ISBN
                    + libro.Titulo
                    + libro.Autor
                    + libro.Categoria
                    + libro.Ubicacion
                    + libro.Precio.ToString()
                    + libro.StockDisponible.ToString()
                    + libro.Activo.ToString();

                _libroDal.ActualizarDVH(libro.CodigoInterno.ToString(), DigitoVerificador.CalcularDVH(dvhCalculado));
            }
        }


        public void RecalcularDVH_Bitacora()
        {
            var listaBitacora = _bitacoraDal.listarTodosLosEventos();
            foreach (var evento in listaBitacora)
            {
                string dvhCalculado = evento.Login +
                        evento.Fecha.ToString("yyyy-MM-dd HH:mm:ss") +
                        evento.Modulo +
                        evento.Evento +
                        evento.Criticidad;
                _bitacoraDal.ActualizarDVH(evento.Id_Evento.ToString(), DigitoVerificador.CalcularDVH(dvhCalculado));
            }
        }
        public void RecalcularDVH_Idioma()
        {
            var listaIdiomas = _idiomaDal.listarTodosLosIdiomas();
            foreach (var idioma in listaIdiomas)
            {
                string dvhCalculado = idioma.UserName + idioma.CodigoIdioma;
                
                _idiomaDal.ActualizarDVH(idioma.UserName, DigitoVerificador.CalcularDVH(dvhCalculado));
            }
        }
        public void RecalcularDVH_Perfil()
        {
            var listaPerfiles = _perfilDal.ObtenerPerfiles();
            foreach (var perfil in listaPerfiles)
            {
                string dvhCalculado = perfil.Nombre;
                _perfilDal.ActualizarPefilDVH(perfil.Nombre, DigitoVerificador.CalcularDVH(dvhCalculado));
            }
        }
        public void RecalcularDVH_PermisoSimple()
        {
            var listaPermisos = _perfilDal.ObtenerPermisos();
            foreach (var permiso in listaPermisos)
            {
                string dvhCalculado = permiso.Nombre;
                _perfilDal.ActualizarPermisoSimpleDVH(permiso.Nombre, DigitoVerificador.CalcularDVH(dvhCalculado));
            }
        }
        public void RecalcularDVH_Familia()
        {
            var listaFamilias = _perfilDal.ObtenerFamilias();
            foreach (var familia in listaFamilias)
            {
                string dvhCalculado = familia.Nombre;
                _perfilDal.ActualizarDVH(familia.Nombre, DigitoVerificador.CalcularDVH(dvhCalculado));
            }
        }
       
        public void RecalcularDVV_Perfil()
        {
            var lista = _perfilDal.ObtenerPerfiles();
            var datos = new Dictionary<string, List<string>> {
            { "Nombre", lista.Select(x => x.Nombre).ToList() }
        };
            RecalcularDVVDeTablaGenerica("Perfil", datos);
        }

        public void RecalcularDVV_Usuario()
        {
            var lista = _usuarioDal.ListarTodosLosUsuariosDVH();
            var datos = new Dictionary<string, List<string>> {
                { "DNI", lista.Select(x => x.DNI).ToList() },
                { "Nombre", lista.Select(x => x.Nombre).ToList() },
                { "Apellido", lista.Select(x => x.Apellido).ToList() },
                { "UserName", lista.Select(x => x.Username).ToList() },
                { "Password", lista.Select(x => x.Password).ToList() },
                { "Email", lista.Select(x => x.Email).ToList() },
                { "Bloqueado", lista.Select(x => x.Bloqueado ? "1" : "0").ToList() },
                { "Activo", lista.Select(x => x.Activo ? "1" : "0").ToList() },
                { "Rol", lista.Select(x => x.Rol).ToList() },
                { "Intentos", lista.Select(x => x.Intentos.ToString()).ToList() }
            };
            RecalcularDVVDeTablaGenerica("Usuario", datos);
        }

        public void RecalcularDVV_Bitacora()
        {
            var lista = _bitacoraDal.listarTodosLosEventos();
            var datos = new Dictionary<string, List<string>> {
            { "Login", lista.Select(x => x.Login).ToList() },
            { "Fecha", lista.Select(x => x.Fecha.ToString("yyyy-MM-dd HH:mm:ss")).ToList() },
            { "Modulo", lista.Select(x => x.Modulo).ToList() },
            { "Evento", lista.Select(x => x.Evento).ToList() },
            { "Criticidad", lista.Select(x => x.Criticidad.ToString()).ToList() }
        };
            RecalcularDVVDeTablaGenerica("Bitacora", datos);
        }

        public void RecalcularDVV_Idioma()
        {
            var lista = _idiomaDal.listarTodosLosIdiomas();
            var datos = new Dictionary<string, List<string>> {
            { "UserName", lista.Select(x => x.UserName).ToList() },
            { "CodigoIdioma", lista.Select(x => x.CodigoIdioma).ToList() }
        };
            RecalcularDVVDeTablaGenerica("Idioma", datos);
        }

        public void RecalcularDVV_PermisoSimple()
        {
            var lista = _perfilDal.ObtenerPermisos();
            var datos = new Dictionary<string, List<string>> {
            { "Nombre", lista.Select(x => x.Nombre).ToList() }
        };
            RecalcularDVVDeTablaGenerica("PermisoSimple", datos);
        }

        public void RecalcularDVV_Familia()
        {
            var lista = _perfilDal.ObtenerFamilias();
            var datos = new Dictionary<string, List<string>> {
            { "Nombre", lista.Select(x => x.Nombre).ToList() }
        };
            RecalcularDVVDeTablaGenerica("Familia", datos);
        }

        public void RecalcularDVV_Libro()
        {
            var lista = _libroDal.ListarTodosLosLibros();
            var datos = new Dictionary<string, List<string>> {
                { "CodigoInterno", lista.Select(x => x.CodigoInterno.ToString()).ToList() },
                { "ISBN", lista.Select(x => x.ISBN).ToList() },
                { "Titulo", lista.Select(x => x.Titulo).ToList() },
                { "Autor", lista.Select(x => x.Autor).ToList() },
                { "Categoria", lista.Select(x => x.Categoria).ToList() },
                { "Ubicacion", lista.Select(x => x.Ubicacion).ToList() },
                { "Precio", lista.Select(x => x.Precio.ToString()).ToList() },
                { "StockDisponible", lista.Select(x => x.StockDisponible.ToString()).ToList() },
                { "Activo", lista.Select(x => x.Activo.ToString()).ToList() }
            };
            RecalcularDVVDeTablaGenerica("Libro", datos);
        }

        public void RecalcularDVV_Venta()
        {
            var lista = _ventaDal.ListarTodasLasVentas();
            var datos = new Dictionary<string, List<string>> {
                { "NroVenta", lista.Select(x => x.NroVenta.ToString()).ToList() },
                { "Fecha", lista.Select(x => x.Fecha.ToString("yyyy-MM-dd HH:mm:ss")).ToList() },
                { "DniUsuario", lista.Select(x => x.DniUsuario).ToList() },
                { "MedioPago", lista.Select(x => x.MedioDePago).ToList() },
                { "Descuento", lista.Select(x => x.Descuento.ToString()).ToList() },
                { "Subtotal", lista.Select(x => x.Subtotal.ToString()).ToList() },
                { "Total", lista.Select(x => x.Total.ToString()).ToList() },
                { "Detalle", lista.Select(x => x.Detalle.ToString()).ToList() }
            };
            RecalcularDVVDeTablaGenerica("Venta", datos);
        }
        public void RecalcularDVV_Detalle()
        {
            var lista = _ventaDal.ListarTodosLosDetalles();
            var datos = new Dictionary<string, List<string>> {
                { "NroVenta", lista.Select(x => x.NroVenta.ToString()).ToList() },
                { "CodigoLibro", lista.Select(x => x.CodigoLibro.ToString()).ToList() },
                { "Cantidad", lista.Select(x => x.Cantidad.ToString()).ToList() },
                { "PrecioUnitario", lista.Select(x => x.PrecioUnitario.ToString()).ToList() },
                { "Subtotal", lista.Select(x => x.Subtotal.ToString()).ToList() }
            };
            RecalcularDVVDeTablaGenerica("DetalleVenta", datos);
        }


        private void RecalcularDVVDeTablaGenerica(string nombreTabla, Dictionary<string, List<string>> columnasData)
        {
            List<string> hashesDeLasColumnas = new List<string>();

            foreach (KeyValuePair<string, List<string>> columna in columnasData)
            {
                string nombreColumna = columna.Key;
                List<string> valoresFila = columna.Value;

                string DVVColumna = DigitoVerificador.CalcularDVV(valoresFila);

                _controlDVDal.ActualizarDVV(nombreTabla, nombreColumna, DVVColumna);

                hashesDeLasColumnas.Add(DVVColumna);
            }
            string dvvTotalTabla = DigitoVerificador.CalcularDVV(hashesDeLasColumnas);

            _controlDVDal.ActualizarDVV(nombreTabla, "TOTAL_TABLA", dvvTotalTabla);

            List<string> todosLosTotalesTablas = ObtenerTodosLosTotalesTablasSistema(nombreTabla, dvvTotalTabla);

            string nuevoDvvGlobalBD = DigitoVerificador.CalcularDVV(todosLosTotalesTablas);

            _controlDVDal.ActualizarDVV("SISTEMA", "TOTAL_BASE_DATOS", nuevoDvvGlobalBD);
        }

        private List<string> ObtenerTodosLosTotalesTablasSistema(string tablaModificadaActual, string nuevoHashEnMemoria)
        {
            List<string> listaTotalesConsolidados = new List<string>();

            
            string[] arrayTablasNegocio = {
            "Perfil", "Usuario", "Bitacora", "Idioma", "PermisoSimple",
            "Familia"
        };

            foreach (string tabla in arrayTablasNegocio)
            {
                if (tabla == tablaModificadaActual)
                {
                    listaTotalesConsolidados.Add(nuevoHashEnMemoria);
                }
                else
                {
                    string totalHistoricoGuardado = _controlDVDal.ObtenerDVV(tabla, "TOTAL_TABLA") ?? "0";
                    listaTotalesConsolidados.Add(totalHistoricoGuardado);
                }
            }

            return listaTotalesConsolidados;
        }

       
        
        public List<string> EjecutarAuditoriaDetalladaCompleta()
        {
            List<string> reporteInconsistencias = new List<string>();

            var listaPerfil = _perfilDal.ObtenerPerfiles();
            List<string> colsErrPerfil = new List<string>();
            if (DigitoVerificador.CalcularDVV(listaPerfil.Select(x => x.Nombre).ToList()) != (_controlDVDal.ObtenerDVV("Perfil", "Nombre") ?? "0")) colsErrPerfil.Add("Nombre");

            AgregarErroresAlReporteOptimizada("Perfil", "Nombre", x => x.Nombre, x => x.DVH, CalcularDVH_Perfil, listaPerfil, colsErrPerfil, reporteInconsistencias);

            var listaUsuario = _usuarioDal.ListarTodosLosUsuariosDVH();
            List<string> colsErrUsuario = new List<string>();
            if (DigitoVerificador.CalcularDVV(listaUsuario.Select(x => x.DNI).ToList()) != (_controlDVDal.ObtenerDVV("Usuario", "DNI") ?? "0")) colsErrUsuario.Add("DNI");
            if (DigitoVerificador.CalcularDVV(listaUsuario.Select(x => x.Nombre).ToList()) != (_controlDVDal.ObtenerDVV("Usuario", "Nombre") ?? "0")) colsErrUsuario.Add("Nombre");
            if (DigitoVerificador.CalcularDVV(listaUsuario.Select(x => x.Apellido).ToList()) != (_controlDVDal.ObtenerDVV("Usuario", "Apellido") ?? "0")) colsErrUsuario.Add("Apellido");
            if (DigitoVerificador.CalcularDVV(listaUsuario.Select(x => x.Username).ToList()) != (_controlDVDal.ObtenerDVV("Usuario", "UserName") ?? "0")) colsErrUsuario.Add("UserName");
            if (DigitoVerificador.CalcularDVV(listaUsuario.Select(x => x.Password).ToList()) != (_controlDVDal.ObtenerDVV("Usuario", "Password") ?? "0")) colsErrUsuario.Add("Password");
            if (DigitoVerificador.CalcularDVV(listaUsuario.Select(x => x.Email).ToList()) != (_controlDVDal.ObtenerDVV("Usuario", "Email") ?? "0")) colsErrUsuario.Add("Email");
            if (DigitoVerificador.CalcularDVV(listaUsuario.Select(x => x.Bloqueado ? "1" : "0").ToList()) != (_controlDVDal.ObtenerDVV("Usuario", "Bloqueado") ?? "0")) colsErrUsuario.Add("Bloqueado");
            if (DigitoVerificador.CalcularDVV(listaUsuario.Select(x => x.Activo ? "1" : "0").ToList()) != (_controlDVDal.ObtenerDVV("Usuario", "Activo") ?? "0")) colsErrUsuario.Add("Activo");
            if (DigitoVerificador.CalcularDVV(listaUsuario.Select(x => x.Rol).ToList()) != (_controlDVDal.ObtenerDVV("Usuario", "Rol") ?? "0")) colsErrUsuario.Add("Rol");
            if (DigitoVerificador.CalcularDVV(listaUsuario.Select(x => x.Intentos.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("Usuario", "Intentos") ?? "0")) colsErrUsuario.Add("Intentos");

            AgregarErroresAlReporteOptimizada("Usuario", "DNI", x => x.DNI, x => x.DVH, CalcularDVH_Usuario, listaUsuario, colsErrUsuario, reporteInconsistencias);

            var listaBitacora = _bitacoraDal.listarTodosLosEventos();
            List<string> colsErrBitacora = new List<string>();
            if (DigitoVerificador.CalcularDVV(listaBitacora.Select(x => x.Login).ToList()) != (_controlDVDal.ObtenerDVV("Bitacora", "Login") ?? "0")) colsErrBitacora.Add("Login");
            if (DigitoVerificador.CalcularDVV(listaBitacora.Select(x => x.Fecha.ToString("yyyy-MM-dd HH:mm:ss")).ToList()) != (_controlDVDal.ObtenerDVV("Bitacora", "Fecha") ?? "0")) colsErrBitacora.Add("Fecha");
            if (DigitoVerificador.CalcularDVV(listaBitacora.Select(x => x.Modulo).ToList()) != (_controlDVDal.ObtenerDVV("Bitacora", "Modulo") ?? "0")) colsErrBitacora.Add("Modulo");
            if (DigitoVerificador.CalcularDVV(listaBitacora.Select(x => x.Evento).ToList()) != (_controlDVDal.ObtenerDVV("Bitacora", "Evento") ?? "0")) colsErrBitacora.Add("Evento");
            if (DigitoVerificador.CalcularDVV(listaBitacora.Select(x => x.Criticidad.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("Bitacora", "Criticidad") ?? "0")) colsErrBitacora.Add("Criticidad");


            AgregarErroresAlReporteOptimizada("Bitacora", "Id_Evento", x => x.Id_Evento.ToString(), x => x.DVH, CalcularDVH_Bitacora, listaBitacora, colsErrBitacora, reporteInconsistencias);

            var listaIdioma = _idiomaDal.listarTodosLosIdiomas();
            List<string> colsErrIdioma = new List<string>();
            if (DigitoVerificador.CalcularDVV(listaIdioma.Select(x => x.UserName).ToList()) != (_controlDVDal.ObtenerDVV("Idioma", "UserName") ?? "0")) colsErrIdioma.Add("UserName");
            if (DigitoVerificador.CalcularDVV(listaIdioma.Select(x => x.CodigoIdioma).ToList()) != (_controlDVDal.ObtenerDVV("Idioma", "CodigoIdioma") ?? "0")) colsErrIdioma.Add("CodigoIdioma");

            AgregarErroresAlReporteOptimizada("Idioma", "UserName", x => x.UserName, x => x.DVH, CalcularDVH_Idioma, listaIdioma, colsErrIdioma, reporteInconsistencias);

            var listaPermisoSimple = _perfilDal.ObtenerPermisos();
            List<string> colsErrPermisoSimple = new List<string>();
            if (DigitoVerificador.CalcularDVV(listaPermisoSimple.Select(x => x.Nombre).ToList()) != (_controlDVDal.ObtenerDVV("PermisoSimple", "Nombre") ?? "0")) colsErrPermisoSimple.Add("Nombre");

            AgregarErroresAlReporteOptimizada("PermisoSimple", "Nombre", x => x.Nombre, x => x.DVH, CalcularDVH_PermisoSimple, listaPermisoSimple, colsErrPermisoSimple, reporteInconsistencias);

            var listaFamilia = _perfilDal.ObtenerFamilias();
            List<string> colsErrFamilia = new List<string>();
            if (DigitoVerificador.CalcularDVV(listaFamilia.Select(x => x.Nombre).ToList()) != (_controlDVDal.ObtenerDVV("Familia", "Nombre") ?? "0")) colsErrFamilia.Add("Nombre");

            AgregarErroresAlReporteOptimizada("Familia", "Nombre", x => x.Nombre, x => x.DVH, CalcularDVH_Familia, listaFamilia, colsErrFamilia, reporteInconsistencias);

            var listaVenta = _ventaDal.ListarTodasLasVentas();
            List<string> colsErrVenta = new List<string>();
            if (DigitoVerificador.CalcularDVV(listaVenta.Select(x => x.NroVenta.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("Venta", "NroVenta") ?? "0")) colsErrVenta.Add("NroVenta");
            if (DigitoVerificador.CalcularDVV(listaVenta.Select(x => x.Fecha.ToString("yyyy-MM-dd HH:mm:ss")).ToList()) != (_controlDVDal.ObtenerDVV("Venta", "Fecha") ?? "0")) colsErrVenta.Add("Fecha");
            if (DigitoVerificador.CalcularDVV(listaVenta.Select(x => x.DniUsuario).ToList()) != (_controlDVDal.ObtenerDVV("Venta", "DniUsuario") ?? "0")) colsErrVenta.Add("DniUsuario");
            if (DigitoVerificador.CalcularDVV(listaVenta.Select(x => x.MedioDePago).ToList()) != (_controlDVDal.ObtenerDVV("Venta", "MedioPago") ?? "0")) colsErrVenta.Add("MedioPago");
            if (DigitoVerificador.CalcularDVV(listaVenta.Select(x => x.Descuento.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("Venta", "Descuento") ?? "0")) colsErrVenta.Add("Descuento");
            if (DigitoVerificador.CalcularDVV(listaVenta.Select(x => x.Subtotal.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("Venta", "Subtotal") ?? "0")) colsErrVenta.Add("Subtotal");
            if (DigitoVerificador.CalcularDVV(listaVenta.Select(x => x.Total.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("Venta", "Total") ?? "0")) colsErrVenta.Add("Total");
            if (DigitoVerificador.CalcularDVV(listaVenta.Select(x => x.Detalle.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("Venta", "Detalle") ?? "0")) colsErrVenta.Add("Detalle");
            
            AgregarErroresAlReporteOptimizada("Venta", "NroVenta", x => x.NroVenta.ToString(), x => x.DVH, CalcularDVH_Venta, listaVenta, colsErrVenta, reporteInconsistencias);

            var listaDetalle = _ventaDal.ListarTodosLosDetalles();
            List<string> colsErrDetalle = new List<string>();
            if (DigitoVerificador.CalcularDVV(listaDetalle.Select(x => x.NroVenta.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("DetalleVenta", "NroVenta") ?? "0")) colsErrDetalle.Add("NroVenta");
            if (DigitoVerificador.CalcularDVV(listaDetalle.Select(x => x.CodigoLibro.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("DetalleVenta", "CodigoLibro") ?? "0")) colsErrDetalle.Add("CodigoLibro");
            if (DigitoVerificador.CalcularDVV(listaDetalle.Select(x => x.Cantidad.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("DetalleVenta", "Cantidad") ?? "0")) colsErrDetalle.Add("Cantidad");
            if (DigitoVerificador.CalcularDVV(listaDetalle.Select(x => x.PrecioUnitario.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("DetalleVenta", "PrecioUnitario") ?? "0")) colsErrDetalle.Add("PrecioUnitario");
            if (DigitoVerificador.CalcularDVV(listaDetalle.Select(x => x.Subtotal.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("DetalleVenta", "Subtotal") ?? "0")) colsErrDetalle.Add("Subtotal");
            
            AgregarErroresAlReporteOptimizada("DetalleVenta", "NroVenta", x => x.NroVenta.ToString(), x => x.DVH, CalcularDVH_DetalleVenta, listaDetalle, colsErrDetalle, reporteInconsistencias);

            var listaLibro = _libroDal.ListarTodosLosLibros();
            List<string> colsErrLibro = new List<string>();
            if (DigitoVerificador.CalcularDVV(listaLibro.Select(x => x.CodigoInterno.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("Libro", "CodigoInterno") ?? "0")) colsErrLibro.Add("CodigoInterno");
            if (DigitoVerificador.CalcularDVV(listaLibro.Select(x => x.ISBN.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("Libro", "ISBN") ?? "0")) colsErrLibro.Add("ISBN");
            if (DigitoVerificador.CalcularDVV(listaLibro.Select(x => x.Titulo).ToList()) != (_controlDVDal.ObtenerDVV("Libro", "Titulo") ?? "0")) colsErrLibro.Add("Titulo");
            if (DigitoVerificador.CalcularDVV(listaLibro.Select(x => x.Autor).ToList()) != (_controlDVDal.ObtenerDVV("Libro", "Autor") ?? "0")) colsErrLibro.Add("Autor");
            if (DigitoVerificador.CalcularDVV(listaLibro.Select(x => x.Categoria).ToList()) != (_controlDVDal.ObtenerDVV("Libro", "Categoria") ?? "0")) colsErrLibro.Add("Categoria");
            if (DigitoVerificador.CalcularDVV(listaLibro.Select(x => x.Ubicacion).ToList()) != (_controlDVDal.ObtenerDVV("Libro", "Ubicacion") ?? "0")) colsErrLibro.Add("Ubicacion");
            if (DigitoVerificador.CalcularDVV(listaLibro.Select(x => x.Precio.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("Libro", "Precio") ?? "0")) colsErrLibro.Add("Precio");
            if (DigitoVerificador.CalcularDVV(listaLibro.Select(x => x.StockDisponible.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("Libro", "StockDisponible") ?? "0")) colsErrLibro.Add("StockDisponible");
            if (DigitoVerificador.CalcularDVV(listaLibro.Select(x => x.Activo.ToString()).ToList()) != (_controlDVDal.ObtenerDVV("Libro", "Activo") ?? "0")) colsErrLibro.Add("Activo");

            AgregarErroresAlReporteOptimizada("Libro", "CodigoInterno", x => x.CodigoInterno.ToString(), x => x.DVH, CalcularDVH_Libro, listaLibro, colsErrLibro, reporteInconsistencias);
            return reporteInconsistencias;
        }
        private void AgregarErroresAlReporteOptimizada<T>(
            string nombreTabla,
            string nombreColumnaPK,
            Func<T, string> selectorPK,
            Func<T, string> selectorDVHGuardado,
            Func<T, string> funcionCalcularDVH,
            List<T> listaRegistros,
            List<string> columnasErroneas,
            List<string> reporte)
        {
            if (columnasErroneas.Count == 0) return;

            List<string> registrosCorruptosPK = new List<string>();

            foreach (T registro in listaRegistros)
            {
                string dvhCalculado = funcionCalcularDVH(registro);
                string dvhGuardado = selectorDVHGuardado(registro);

                if (dvhCalculado != dvhGuardado)
                {
                    string valorPK = selectorPK(registro);
                    registrosCorruptosPK.Add(valorPK);

                    bool esNuevoConDvhCorrupto = false;

                    foreach (string col in columnasErroneas)
                    {
                        string dvvBD = _controlDVDal.ObtenerDVV(nombreTabla, col) ?? "0";

                        var listaSinEsteRegistro = listaRegistros
                            .Where(x => selectorPK(x) != valorPK)
                            .Select(x => {
                                var p = x.GetType().GetProperty(col);
                                return p?.GetValue(x, null)?.ToString() ?? "";
                            })
                            .ToList();

                        string dvvSimulado = DigitoVerificador.CalcularDVV(listaSinEsteRegistro);

                        if (dvvSimulado == dvvBD)
                        {
                            esNuevoConDvhCorrupto = true;
                            break;
                        }
                    }

                    if (esNuevoConDvhCorrupto)
                    {
                        reporte.Add($"[ERROR INSERCIÓN NO AUTORIZADA] Tabla: '{nombreTabla}' -> Se insertó un NUEVO registro (PK {nombreColumnaPK}: '{valorPK}') con DVH Inválido/Corrupto.");
                    }
                    else
                    {
                        string columnaCulpableReal = columnasErroneas.FirstOrDefault() ?? "Desconocida";
                        dynamic r = registro;

                        foreach (string col in columnasErroneas)
                        {
                            var p = r.GetType().GetProperty(col);
                            if (p != null)
                            {
                                columnaCulpableReal = col;
                            }
                        }

                        if (columnasErroneas.Contains("Criticidad") && nombreTabla == "Bitacora" && columnasErroneas.Count > 1)
                        {
                            var columnasFalsas = new List<string> { "Fecha", "Login" };
                            var candidatasReales = columnasErroneas.Where(c => !columnasFalsas.Contains(c)).ToList();
                            if (candidatasReales.Count == 1)
                            {
                                columnaCulpableReal = candidatasReales[0];
                            }
                        }

                        reporte.Add($"[ERROR MODIFICACIÓN] Tabla: '{nombreTabla}' -> Registro {nombreColumnaPK}: '{valorPK}' tiene datos alterados en la Columna: '{columnaCulpableReal}'.");
                    }
                }
            }
            if (registrosCorruptosPK.Count == 0)
            {
                string colsAfectadas = string.Join(", ", columnasErroneas);
                string registroInfiltradoPK = null;

                foreach (string col in columnasErroneas)
                {
                    string dvvBD = _controlDVDal.ObtenerDVV(nombreTabla, col) ?? "0";

                    foreach (T reg in listaRegistros)
                    {
                        string pkActual = selectorPK(reg);

                        var listaSinEstaFila = listaRegistros
                            .Where(x => selectorPK(x) != pkActual)
                            .Select(x => {
                                var p = x.GetType().GetProperty(col);
                                return p?.GetValue(x, null)?.ToString() ?? "";
                            })
                            .ToList();

                        string dvvSimulado = DigitoVerificador.CalcularDVV(listaSinEstaFila);

                        if (dvvSimulado == dvvBD)
                        {
                            registroInfiltradoPK = pkActual;
                            break;
                        }
                    }

                    if (registroInfiltradoPK != null) break;
                }

                if (registroInfiltradoPK != null)
                {
                    reporte.Add($"[ERROR INSERCIÓN NO AUTORIZADA] Tabla: '{nombreTabla}' -> Se detectó un NUEVO registro no autorizado (PK {nombreColumnaPK}: '{registroInfiltradoPK}') en columnas: {colsAfectadas}.");
                }
                else
                {
                    reporte.Add($"[ERROR ELIMINACIÓN] Tabla: '{nombreTabla}' -> Se ha detectado la ELIMINACIÓN de uno o más registros (Inconsistencia de DVV en columnas: {colsAfectadas}).");
                }
            }
        }

        private string CalcularDVH_Perfil(Familia p) => DigitoVerificador.CalcularDVH(p.Nombre);
        private string CalcularDVH_Usuario(Usuario u) => DigitoVerificador.CalcularDVH(u.DNI + u.Nombre + u.Apellido + u.Username + u.Password + u.Email + u.Bloqueado + u.Activo + u.Rol);
        private string CalcularDVH_Bitacora(Bitacora b) => DigitoVerificador.CalcularDVH(b.Login + b.Fecha.ToString("yyyy-MM-dd HH:mm:ss") + b.Modulo + b.Evento + b.Criticidad.ToString());
        private string CalcularDVH_Idioma(Idioma i) => DigitoVerificador.CalcularDVH(i.UserName + i.CodigoIdioma);
        private string CalcularDVH_PermisoSimple(PermisoSimple p) => DigitoVerificador.CalcularDVH(p.Nombre);
        private string CalcularDVH_Familia(Familia f) => DigitoVerificador.CalcularDVH(f.Nombre);
        private string CalcularDVH_Venta(Venta v) => DigitoVerificador.CalcularDVH(v.NroVenta.ToString() + v.Fecha.ToString() + v.DniUsuario + v.MedioDePago + v.Descuento.ToString() + v.Subtotal.ToString() + v.Total.ToString() + v.Detalle.ToString());
        private string CalcularDVH_DetalleVenta(DetalleVenta dv) => DigitoVerificador.CalcularDVH(dv.NroVenta.ToString() + dv.CodigoLibro.ToString() + dv.PrecioUnitario.ToString() + dv.Cantidad.ToString() + dv.Subtotal.ToString());
        private string CalcularDVH_Libro(Libro l) => DigitoVerificador.CalcularDVH(l.CodigoInterno.ToString() + l.ISBN + l.Titulo + l.Autor + l.Categoria + l.Ubicacion + l.Precio.ToString() + l.StockDisponible.ToString() + l.Activo.ToString());

        public void RecalcularDVV_General()
        {
            try
            {
                
                RecalcularDVH_Usuario();
                RecalcularDVH_Familia();
                RecalcularDVH_PermisoSimple();
                RecalcularDVH_Perfil();
                RecalcularDVH_Venta();
                RecalcularDVH_Detalle();
                RecalcularDVH_Libro();
                RecalcularDVH_Idioma();

                
                
                RecalcularDVV_Familia();
                RecalcularDVV_Idioma();
                RecalcularDVV_Perfil();
                RecalcularDVV_PermisoSimple();
                RecalcularDVV_Usuario();
                RecalcularDVV_Libro();
                RecalcularDVV_Venta();
                RecalcularDVV_Detalle();
                RegistrarEvento("Recalculo de DVV y DVH realizado correctamente", 1);
                RecalcularDVH_Bitacora();
                RecalcularDVV_Bitacora();
            }
            catch (Exception ex)
            {
                RegistrarEvento("Error al recalcular DVV y DVH", 3);
                throw new Exception(LanguageManager.Instance.GetTraduction("TextDV13") + ex.Message);
            }
            


        }

      
        private void RegistrarEvento(string descripcion, int criticidad)
        {
            try
            {
                string login = SessionManager.Instance?.UsuarioActual()?.Username ?? "Sistema";
                Bitacora evento = new Bitacora
                {
                    Login = login,
                    Modulo = "DigitoVerificador",
                    Fecha = DateTime.Now,
                    Evento = descripcion,
                    Criticidad = criticidad
                };
                string cadenaDVH = evento.Login + evento.Fecha.ToString("yyyy-MM-dd HH:mm:ss") + evento.Modulo + evento.Evento + evento.Criticidad.ToString();
                _bitacoraDal.RegistrarEvento(evento, DigitoVerificador.CalcularDVH(cadenaDVH));
            }
            catch { }
        }
    }
}
