using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Web;
using System.Configuration;
using SharpContent.ApplicationBlocks.Data;
using SIAC.Constantes;
using System.Data.SqlClient;

namespace SIAC.Metodos_Generales
{
    public class Cls_Metodos_Generales
    {
        
        /////*******************************************************************************************************
        ///// <summary>
        ///// genera un datatable nuevo con los campos para la 
        ///// </summary>
        ///// <returns>un datatable con los campos para mostrar accesos e ingresos por año y mes</returns>
        ///// <creo>Hugo Enrique Ramírez Aguilera</creo>
        ///// <fecha_creo>13-Enero-2016</fecha_creo>
        ///// <modifico></modifico>
        ///// <fecha_modifico></fecha_modifico>
        ///// <causa_modificacion></causa_modificacion>
        ///*******************************************************************************************************
        public static Dictionary<Int32, String> Crear_Diccionario_Meses()
        {
            var Diccionario = new Dictionary<Int32, String>();

            try
            {
                Diccionario.Add(1, "Enero");
                Diccionario.Add(2, "Febrero");
                Diccionario.Add(3, "Marzo");
                Diccionario.Add(4, "Abril");
                Diccionario.Add(5, "Mayo");
                Diccionario.Add(6, "Junio");
                Diccionario.Add(7, "Julio");
                Diccionario.Add(8, "Agosto");
                Diccionario.Add(9, "Septiembre");
                Diccionario.Add(10, "Octubre");
                Diccionario.Add(11, "Noviembre");
                Diccionario.Add(12, "Diciembre");
            }
            catch (Exception Ex)
            {
            }

            return Diccionario;
        }
    }
}