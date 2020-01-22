using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cecati_117.Areas.Admin.ClasesCompartidas
{
    public class FuncionesUtilesController : Controller
    {
        #region metodos para manejar imagenes en el servidor
        public string AgregarImagen_Servidor(HttpPostedFileBase archivo, string ruta_para_guardar, HttpServerUtilityBase Server)
        {
            string pathReturn = "none";
            Random rnd = new Random();

            if (archivo.ContentLength > 0)
            {
                string path = archivo.FileName;
                int number = rnd.Next(10000);
                string filename = Path.GetFileNameWithoutExtension(path);
                string extension = Path.GetExtension(path);
                filename = filename + DateTime.Now.ToString("yymmssff") + number + extension;
                pathReturn = ruta_para_guardar + filename;
                filename = Path.Combine(Server.MapPath(ruta_para_guardar), filename);
                archivo.SaveAs(filename);
            }
            return pathReturn;
        }

        public void QuitarImagen_Servidor(string ruta_para_quitar,HttpServerUtilityBase Server)
        {
            string fullPath = Path.Combine(Server.MapPath(ruta_para_quitar));
            if(System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        public void QuitarTodas_LasImagenesDeRuta(string ruta_para_quitar, HttpServerUtilityBase Server)
        {
            if(Directory.Exists(Server.MapPath(ruta_para_quitar)))
            {
                var folderPath = Server.MapPath(ruta_para_quitar);
                System.IO.DirectoryInfo folderInfo = new DirectoryInfo(folderPath);
                foreach (FileInfo file in folderInfo.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in folderInfo.GetDirectories())
                {
                    dir.Delete(true);
                }
            }
       
        }

        public bool Comprobar_SiEsImagen(HttpPostedFileBase file)
        {
            if(file.Equals(null))
            {
                return false;
            }

            if (file.ContentType.Contains("image"))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion

        #region metodos para manejar cadenas de texto 
        public string SepararArray_DeString(string[] arreglo, string separador)
        {
            string result = String.Join(separador, arreglo);
            return result;
        }
        public string[] UnirString_EnArray(string cadena,char separador)
        {
            string[] result = cadena.Split(separador);
            return result;
        }
        #endregion
    }
}