using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SIAC.Constantes;
using SharpContent.ApplicationBlocks.Data;
using SIAC.Parametros.Negocios;
using System.Text;
/// <summary>
/// Summary description for Cls_Ope_Cor_Cortes_Datos
/// </summary>
/// 
namespace SIAC.Parametros.Datos
{
    public class Cls_Cat_Cor_Parametros_Datos
    {
        public Cls_Cat_Cor_Parametros_Datos() { }

        //*******************************************************************************
        //NOMBRE_FUNCION: Consulta_Reconexiones
        //DESCRIPCION: Consulta las diversos pagados que son reconexion
        //PARAMETROS : 1.- Cls_Ope_Cor_Reconexiones_Negocio
        //CREO       : Javier Acosta Reynaga
        //FECHA_CREO : 30-Julio-2012
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consulta_Parametros()
        {
            String Mi_SQL = "SELECT * FROM Cat_Cor_Parametros";
                
            DataTable Data_Table = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Mi_SQL).Tables[0];
            return Data_Table;
        }

        //*******************************************************************************
        //NOMBRE_FUNCION: Insertar_Evento
        //DESCRIPCION: Inserta el evento de cuando se genera una orden de trabajo
        //PARAMETROS : 1.- Cls_Ope_Cor_Reconexiones_Negocio
        //CREO       : Javier Acosta Reynaga
        //FECHA_CREO : 31-Julio-2012
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static void Modifica_Parametro(Cls_Cat_Cor_Parametros_Negocio Datos)
        {
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Constantes.Str_Conexion);
            SqlCommand Obj_Comando = new SqlCommand();
            try
            {
                Obj_Conexion.Open();
                Obj_Comando.Connection = Obj_Conexion;

                String Mi_SQL = "UPDATE Cat_Cor_Parametros SET";

                Mi_SQL += " Usuario_Modifico = '" + Datos.P_Usuario_Modifico + "', " + 
                        "Fecha_Modifico = GETDATE() ";

                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Porcentaje_IVA))
                {
                    Mi_SQL += " ,Porcentaje_IVA = " + Datos.P_Porcentaje_IVA;
                }
                else
                {
                    Mi_SQL += " ,Porcentaje_IVA = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Porcentaje_Drenaje))
                {
                    Mi_SQL += " ,Porcentaje_Drenaje = " + Datos.P_Porcentaje_Drenaje;
                }
                else
                {
                    Mi_SQL += " ,Porcentaje_Drenaje = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Porcentaje_Saneamiento))
                {
                    Mi_SQL += " , Porcentaje_Saneamiento = " + Datos.P_Porcentaje_Saneamiento;
                }
                else
                {
                    Mi_SQL += " , Porcentaje_Saneamiento = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Ruta_Archivos_Lecturas))
                {
                    Mi_SQL += " , Ruta_Archivos_Lecturas = '" + Datos.P_Ruta_Archivos_Lecturas + "'" ;
                }
                else
                {
                    Mi_SQL += " , Ruta_Archivos_Lecturas = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Ruta_Archivo_Rutas))
                {
                    Mi_SQL += " , Ruta_Archivo_Rutas = '" + Datos.P_Ruta_Archivo_Rutas + "'";
                }
                else
                {
                    Mi_SQL += " , Ruta_Archivo_Rutas = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Ruta_Calendario_Facturacion))
                {
                    Mi_SQL += " , Ruta_Calendario_Facturacion = '" + Datos.P_Ruta_Calendario_Facturacion + "'";
                }
                else
                {
                    Mi_SQL += " , Ruta_Calendario_Facturacion = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Dias_Notificacion))
                {
                    Mi_SQL += " , Dias_Notificacion = " + Datos.P_Dias_Notificacion;
                }
                else
                {
                    Mi_SQL += " , Dias_Notificacion = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Porcentaje_Pago_Notificacion))
                {
                    Mi_SQL += " , Porcentaje_Pago_Notificacion = " + Datos.P_Porcentaje_Pago_Notificacion;
                }
                else
                {
                    Mi_SQL += " , Porcentaje_Pago_Notificacion = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Porcentaje_Recargos))
                {
                    Mi_SQL += " , Porcentaje_Recargos = " + Datos.P_Porcentaje_Recargos;
                }
                else
                {
                    Mi_SQL += " , Porcentaje_Recargos = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Tipo_Falla_Corte_ID))
                {
                    Mi_SQL += " , Tipo_Falla_Corte_ID = '" + Datos.P_Tipo_Falla_Corte_ID + "'";
                }
                else
                {
                    Mi_SQL += " , Tipo_Falla_Corte_ID = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Tipo_Falla_Bacheo_ID))
                {
                    Mi_SQL += " , Tipo_Falla_Bacheo_ID = '" + Datos.P_Tipo_Falla_Bacheo_ID + "'";
                }
                else
                {
                    Mi_SQL += " , Tipo_Falla_Bacheo_ID = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Tipo_Falla_Escombro_ID))
                {
                    Mi_SQL += " , Tipo_Falla_Escombro_ID = '" + Datos.P_Tipo_Falla_Escombro_ID+ "'";
                }
                else
                {
                    Mi_SQL += " , Tipo_Falla_Escombro_ID = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Porcentaje_Minimo_P_Parcial))
                {
                    Mi_SQL += " , Porcentaje_Minimo_Pago_Parcial = " + Datos.P_Porcentaje_Minimo_P_Parcial;
                }
                else
                {
                    Mi_SQL += " , Porcentaje_Minimo_Pago_Parcial = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Porcentaje_Minimo_P_Inicial))
                {
                    Mi_SQL += " , Porcentaje_Minimo_Convenio_Pago_Inicial = " + Datos.P_Porcentaje_Minimo_P_Inicial;
                }
                else
                {
                    Mi_SQL += " , Porcentaje_Minimo_Convenio_Pago_Inicial = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Porcentaje_Sugerido_P_Inicial_Convenio))
                {
                    Mi_SQL += " , Porcentaje_Sugerido_Pago_Inicial_Convenio = " + Datos.P_Porcentaje_Sugerido_P_Inicial_Convenio;
                }
                else
                {
                    Mi_SQL += " , Porcentaje_Sugerido_Pago_Inicial_Convenio = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Tipo_Falla_Reconexion))
                {
                    Mi_SQL += " , Tipo_Falla_Reconexion = '" + Datos.P_Tipo_Falla_Reconexion +  "'";
                }
                else
                {
                    Mi_SQL += " , Tipo_Falla_Reconexion = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Tipo_Falla_Corte_Efectivo))
                {
                    Mi_SQL += " , Tipo_Falla_Corte_Efectivo_ID = '" + Datos.P_Tipo_Falla_Corte_Efectivo +"'";
                }
                else
                {
                    Mi_SQL += " , Tipo_Falla_Corte_Efectivo_ID = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Monto_Notificacion_Sfirma))
                {
                    Mi_SQL += " , Monto_Notificacion_Sfirma = " + Datos.P_Monto_Notificacion_Sfirma;
                }
                else
                {
                    Mi_SQL += " , Monto_Notificacion_Sfirma = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Tipo_Falla_Suspencion_Voluntaria))
                {
                    Mi_SQL += " , Tipo_Falla_Suspension_Voluntaria = '" + Datos.P_Tipo_Falla_Suspencion_Voluntaria +"'";
                }
                else
                {
                    Mi_SQL += " , Tipo_Falla_Suspension_Voluntaria = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Ruta_Imagenes))
                {
                    Mi_SQL += " , ruta_imagenes = '" + Datos.P_Ruta_Imagenes + "'";
                }
                else
                {
                    Mi_SQL += " , ruta_imagenes = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Url_Imagenes))
                {
                    Mi_SQL += " , url_imagenes = '" + Datos.P_Url_Imagenes + "'";
                }
                else
                {
                    Mi_SQL += " , url_imagenes = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Usar_Fecha_Vencimiento))
                {
                    Mi_SQL += " , Usar_Fecha_Vencimiento = '" + Datos.P_Usar_Fecha_Vencimiento + "'";
                }
                else
                {
                    Mi_SQL += " , Usar_Fecha_Vencimiento = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Maximo_Meses_Adeudo))
                {
                    Mi_SQL += " , Maximo_Meses_Adeudo = " + Datos.P_Maximo_Meses_Adeudo;
                }
                else
                {
                    Mi_SQL += " , Maximo_Meses_Adeudo = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Salario_Minimo))
                {
                    Mi_SQL += " , SMGV = " + Datos.P_Salario_Minimo;
                    Mi_SQL += " , Salario_Minimo = " + Datos.P_Salario_Minimo;
                }
                else
                {
                    Mi_SQL += " , SMGV = NULL";
                    Mi_SQL += " , Salario_Minimo = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Verificacion_Tecnica_ID))
                {
                    Mi_SQL += " , Verificacion_Tecnica_ID = '" + Datos.P_Verificacion_Tecnica_ID + "'";
                }
                else
                {
                    Mi_SQL += " , Verificacion_Tecnica_ID = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                    //+ ",Pago_Cie = '" + Datos.P_Cie.Trim() + "'";
                if (!String.IsNullOrEmpty(Datos.P_CANTIDAD_PAGO_RELEVANTE))
                {
                    Mi_SQL = Mi_SQL + ", CANTIDAD_PAGO_RELEVANTE = " + Datos.P_CANTIDAD_PAGO_RELEVANTE;
                }
                else
                {
                    Mi_SQL = Mi_SQL + ", CANTIDAD_PAGO_RELEVANTE = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Cuenta_organismo))
                {
                    Mi_SQL = Mi_SQL + ", CUENTA_ORGANISMO = '" + Datos.P_Cuenta_organismo + "'";
                }
                else
                {
                    Mi_SQL = Mi_SQL + ", CUENTA_ORGANISMO = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Cuenta_Contable_Caja))
                {
                    Mi_SQL += ", Cuenta_Contable_Caja = '" + Datos.P_Cuenta_Contable_Caja + "'";
                }
                else
                {
                    Mi_SQL += ", Cuenta_Contable_Caja = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Concepto_Poliza_Contable))
                {
                    Mi_SQL += ", Concepto_Poliza_Contable = '" + Datos.P_Concepto_Poliza_Contable + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Poliza_Contable = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Concepto_Poliza_Presupuestal))
                {
                    Mi_SQL += ", Concepto_Poliza_Presupuestal = '" + Datos.P_Concepto_Poliza_Presupuestal + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Poliza_Presupuestal = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Caja_Internet_Id))
                {
                    Mi_SQL += ", CAJA_INTERNET_ID = '" + Datos.P_Caja_Internet_Id + "'";
                }
                else
                {
                    Mi_SQL += ", CAJA_INTERNET_ID = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Cajero_Internet_Id))
                {
                    Mi_SQL += ", CAJERO_INTERNET_ID = '" + Datos.P_Cajero_Internet_Id + "'";
                }
                else
                {
                    Mi_SQL += ", CAJERO_INTERNET_ID = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Sucursal_Internet_Id))
                {
                    Mi_SQL += ", SUCURSAL_INTERNET_ID = '" + Datos.P_Sucursal_Internet_Id + "'";
                }
                else
                {
                    Mi_SQL += ", SUCURSAL_INTERNET_ID = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Ruta_Pagina_Facturacion))
                {
                    Mi_SQL += ", Ruta_Pagina_Facturacion = '" + Datos.P_Ruta_Pagina_Facturacion + "'";
                }
                else
                {
                    Mi_SQL += ", Ruta_Pagina_Facturacion = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Banco))
                {
                    Mi_SQL = Mi_SQL + ", Banco_Id = '" + Datos.P_Banco + "'";
                }
                else
                {
                    Mi_SQL = Mi_SQL + ", Banco_Id = NULL ";
                }
                //*********************************************************************************
                //*********************************************************************************
                //  suspension de servicios **************************************************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Concepto_Suspension_Meses_De_Adeudo))
                {
                    Mi_SQL += ", Meses_Adeudo_Para_Congelar = '" + Datos.P_Concepto_Suspension_Meses_De_Adeudo + "'";
                }
                else
                {
                    Mi_SQL += ", Meses_Adeudo_Para_Congelar = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Concepto_Suspension_Reconexion_Linea))
                {
                    Mi_SQL += ", Concepto_Reconexion_Susp_Linea = '" + Datos.P_Concepto_Suspension_Reconexion_Linea + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Reconexion_Susp_Linea = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Concepto_Suspension_Reconexion_Cuadro))
                {
                    Mi_SQL += ", Concepto_Reconexion_Susp_Cuadro = '" + Datos.P_Concepto_Suspension_Reconexion_Cuadro + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Reconexion_Susp_Cuadro = NULL ";
                }

                //************************************************************************************************************************************
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Agua))
                {
                    Mi_SQL += ", CONCEPTO_AGUA = '" + Datos.Concepto_Agua + "' ";
                }
                else
                {
                    Mi_SQL += ", CONCEPTO_AGUA = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Drenaje))
                {
                    Mi_SQL += ", CONCEPTO_DRENAJE = '" + Datos.Concepto_Drenaje + "'";
                }
                else
                {
                    Mi_SQL += ", CONCEPTO_DRENAJE = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Sanamiento))
                {
                    Mi_SQL += ", CONCEPTO_SANAMIENTO = '" + Datos.Concepto_Sanamiento + "'";
                }
                else
                {
                    Mi_SQL += ", CONCEPTO_SANAMIENTO = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Redondeo))
                {
                    Mi_SQL += ", CONCEPTO_REDONDEO = '" + Datos.Concepto_Redondeo + "'";
                }
                else
                {
                    Mi_SQL += ", CONCEPTO_REDONDEO = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Redondeo_Anterior))
                {
                    Mi_SQL += ", CONCEPTO_REDONDEO_ANTERIOR = '" + Datos.Concepto_Redondeo_Anterior + "'";
                }
                else
                {
                    Mi_SQL += ", CONCEPTO_REDONDEO_ANTERIOR = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Factibilidad_Casa))
                {
                    Mi_SQL += ", Concepto_Factibilidad_Casa = '" + Datos.Concepto_Factibilidad_Casa + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Factibilidad_Casa = NULL";
                }

                if (!String.IsNullOrEmpty(Datos.Concepto_Factibilidad_Imobiliaria))
                {
                    Mi_SQL += ", Concepto_Factibilidad_Inmobiliaria = '" + Datos.Concepto_Factibilidad_Imobiliaria + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Factibilidad_Inmobiliaria = NULL";
                }

                if (!String.IsNullOrEmpty(Datos.Concepto_Metro_Excedente))
                {
                    Mi_SQL += ", Metro_Excedente = '" + Datos.Concepto_Metro_Excedente + "'";
                }
                else
                {
                    Mi_SQL += ", Metro_Excedente = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Iva))
                {
                    Mi_SQL += ", CONCEPTO_IVA = '" + Datos.Concepto_Iva + "'";
                }
                else
                {
                    Mi_SQL += ", CONCEPTO_IVA = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Cruz_Roja))
                {
                    Mi_SQL += ", Concepto_Cruz_Roja = '" + Datos.Concepto_Cruz_Roja + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Cruz_Roja = NULL";
                }

                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Bomberos))
                {
                    Mi_SQL += ", Concepto_Bomberos = '" + Datos.Concepto_Bomberos + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Bomberos = NULL";
                }

                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Agua_Anticipo))
                {
                    Mi_SQL += ", Concepto_Agua_Anticipo_Id = '" + Datos.Concepto_Agua_Anticipo + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Agua_Anticipo_Id = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Drenaje_Anticipo))
                {
                    Mi_SQL += ", Concepto_Drenaje_Anticipo_Id = '" + Datos.Concepto_Drenaje_Anticipo + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Drenaje_Anticipo_Id = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Saneamiento_Anticipo))
                {
                    Mi_SQL += ", Concepto_Saneamiento_Anticipo_Id = '" + Datos.Concepto_Saneamiento_Anticipo + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Saneamiento_Anticipo_Id = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Agua_Comercial))
                {
                    Mi_SQL += ", Concepto_Agua_Comercial = '" + Datos.Concepto_Agua_Comercial + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Agua_Comercial = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Recargo_Agua))
                {
                    Mi_SQL += ", Concepto_Recargo_Agua_Id = '" + Datos.Concepto_Recargo_Agua + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Recargo_Agua_Id = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Rezago_Agua))
                {
                    Mi_SQL += ", Concepto_Rezago_Agua_Id = '" + Datos.Concepto_Rezago_Agua + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Rezago_Agua_Id = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Recargo_Drenaje))
                {
                    Mi_SQL += ", Concepto_Recargo_Drenaje_Id = '" + Datos.Concepto_Recargo_Drenaje + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Recargo_Drenaje_Id = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Rezago_Drenaje))
                {
                    Mi_SQL += ", Concepto_Rezago_Drenaje_Id = '" + Datos.Concepto_Rezago_Drenaje + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Rezago_Drenaje_Id = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Recargo_Saneamiento))
                {
                    Mi_SQL += ", Concepto_Recargo_Saneamiento_Id = '" + Datos.Concepto_Recargo_Saneamiento + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Recargo_Saneamiento_Id = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Rezago_Saneamiento))
                {
                    Mi_SQL += ", Concepto_Rezago_Saneamiento_Id = '" + Datos.Concepto_Rezago_Saneamiento + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Rezago_Saneamiento_Id = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Factor_Desgloce_Anticipo_Dom))
                {
                    Mi_SQL += ", Factor_Desgloce_Anticipo_Dom = '" + Datos.Factor_Desgloce_Anticipo_Dom + "'";
                }
                else
                {
                    Mi_SQL += ", Factor_Desgloce_Anticipo_Dom = NULL";
                }

                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Factor_Desgloce_Anticipo_Com))
                {
                    Mi_SQL += ", Factor_Desgloce_Anticipo_Com = '" + Datos.Factor_Desgloce_Anticipo_Com + "'";
                }
                else
                {
                    Mi_SQL += ", Factor_Desgloce_Anticipo_Com = NULL";
                }

                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Reportes_Cajas_Elaboro))
                {
                    Mi_SQL += ", reportes_cajas_elaboro = '" + Datos.P_Reportes_Cajas_Elaboro + "'";
                }
                else
                {
                    Mi_SQL += ", reportes_cajas_elaboro = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Reportes_Cajas_Reviso))
                {
                    Mi_SQL += ", reportes_cajas_reviso = '" + Datos.P_Reportes_Cajas_Reviso + "'";
                }
                else
                {
                    Mi_SQL += ", reportes_cajas_reviso = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Reportes_Cajas_VoBo))
                {
                    Mi_SQL += ", reportes_cajas_vobo = '" + Datos.P_Reportes_Cajas_VoBo + "'";
                }
                else
                {
                    Mi_SQL += ", reportes_cajas_vobo = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_URL_Imagen_Archivos))
                {
                    Mi_SQL += ", url_imagen_archivos = '" + Datos.P_URL_Imagen_Archivos + "'";
                }
                else
                {
                    Mi_SQL += ", url_imagen_archivos = NULL";
                }


                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_Ruta_Imagen_Archivos))
                {
                    Mi_SQL += ", ruta_imagen_archivos = '" + Datos.P_Ruta_Imagen_Archivos + "'";
                }
                else
                {
                    Mi_SQL += ", ruta_imagen_archivos = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Porcentaje_Interes_Insolutos.ToString()))
                {
                    Mi_SQL += ", Porcentaje_Interes = " + Datos.Porcentaje_Interes_Insolutos + "";
                }
                else
                {
                    Mi_SQL += ", Porcentaje_Interes = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Porcentaje_Interes_Insolutos_Vencidos.ToString()))
                {
                    Mi_SQL += ", Porcentaje_Interes_Vencidos = " + Datos.Porcentaje_Interes_Insolutos_Vencidos + "";
                }
                else
                {
                    Mi_SQL += ", Porcentaje_Interes_Vencidos = NULL";
                }

                if (!String.IsNullOrEmpty(Datos.Metros_Cuadrados.ToString()))
                {
                    Mi_SQL += ", Metros_Cuadrados_Minimos= " + Datos.Metros_Cuadrados + " ";

                }
                else
                {
                    Mi_SQL += ",Metros_Cuadrados_Minimos=0";
                }


                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Maximo_Mensualidades.ToString()))
                {
                    Mi_SQL += ", Maximo_Mensualidades = " + Datos.Maximo_Mensualidades + "";
                }
                else
                {
                    Mi_SQL += ", Maximo_Mensualidades = NULL";
                }

                //*********************************************************************************
                //*********************************************************************************

                if (!String.IsNullOrEmpty(Datos.Dias_Vencimiento_Vales.ToString()))
                {
                    Mi_SQL += ",Dias_Vencimiento_Vales = " + Datos.Dias_Vencimiento_Vales + " ";
                }
                else {
                    Mi_SQL += ",Dias_Vencimiento_Vales=null ";
                }


                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Porcentaje_Descuento_Empleados.ToString()))
                {
                    Mi_SQL += ", Porcentaje_Descuento_Empleados = " + Datos.Porcentaje_Descuento_Empleados + "";
                }
                else
                {
                    Mi_SQL += ", Porcentaje_Descuento_Empleados = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.Concepto_Descuento_Empleados))
                {
                    Mi_SQL += ", Concepto_Descuento_Empleados = '" + Datos.Concepto_Descuento_Empleados + "'";
                }
                else
                {
                    Mi_SQL += ", Concepto_Descuento_Empleados = NULL";
                }
                //*********************************************************************************
                //*********************************************************************************
                if (!String.IsNullOrEmpty(Datos.P_PrecioM3_Por_Alumno))
                {
                    Mi_SQL += ", PrecioM3_Por_Alumno = " + Datos.P_PrecioM3_Por_Alumno;
                }
                else
                {
                    Mi_SQL += ", PrecioM3_Por_Alumno = 0";
                }
                //*********************************************************************************
                //*********************************************************************************

                if (!String.IsNullOrEmpty(Datos.No_Intentos_Acceso))
                {
                    Mi_SQL += ", NO_INTENTOS_ACCESO = " + Datos.No_Intentos_Acceso + "";
                }
                else
                {
                    Mi_SQL += ", NO_INTENTOS_ACCESO = 0";
                }

                if (!String.IsNullOrEmpty(Datos.No_Bloqueos_Temporales))
                {
                    Mi_SQL += ", NO_BLOQUEOS_TEMPORALES = " + Datos.No_Bloqueos_Temporales + "";
                }
                else {
                    Mi_SQL += ", NO_BLOQUEOS_TEMPORALES = 0";
                }

                if (!String.IsNullOrEmpty(Datos.Minutos_Bloqueo_Temporal))
                {

                    Mi_SQL += ", MINUTOS_BLOQUEO_TEMPORAL = " + Datos.Minutos_Bloqueo_Temporal + "";
                }
                else
                {
                    Mi_SQL += ", MINUTOS_BLOQUEO_TEMPORAL = 0";
                }



                if (!String.IsNullOrEmpty(Datos.Concepto_Material))
                    Mi_SQL +=", Concepto_Material_ID = '" + Datos.Concepto_Material + "'";
                else
                    Mi_SQL +=", Concepto_Material_ID = NULL";


                if (!String.IsNullOrEmpty(Datos.Concepto_Vales))
                    Mi_SQL += ", CONCEPTO_VALES_ID = '" + Datos.Concepto_Vales + "'";
                else
                    Mi_SQL += ", CONCEPTO_VALES_ID=null ";


                Mi_SQL +=" WHERE Parametro_ID = '" + Datos.P_Parametro_ID + "'";

                Obj_Comando.CommandText = Mi_SQL;
                Obj_Comando.ExecuteNonQuery();
                Obj_Conexion.Close();
            }
            catch (SqlException Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }
        }

        public static void Actualizar_Parametros_Finanzas(Cls_Cat_Cor_Parametros_Negocio Datos)
        {
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Constantes.Str_Conexion);
            SqlCommand Obj_Comando = new SqlCommand();
            StringBuilder Mi_SQL = new StringBuilder();
            try
            {
                Obj_Conexion.Open();
                Obj_Comando.Connection = Obj_Conexion;

                Mi_SQL.Append("UPDATE Cat_Cor_Parametros SET ");

                if (!String.IsNullOrEmpty(Datos.P_Cuenta_organismo))
                    Mi_SQL.Append("CUENTA_ORGANISMO  = '" + Datos.P_Cuenta_organismo + "'");
                else
                    Mi_SQL.Append("CUENTA_ORGANISMO = NULL");

                if (!String.IsNullOrEmpty(Datos.P_CANTIDAD_PAGO_RELEVANTE))
                    Mi_SQL.Append(", CANTIDAD_PAGO_RELEVANTE = " + Datos.P_CANTIDAD_PAGO_RELEVANTE);
                else
                    Mi_SQL.Append(", CANTIDAD_PAGO_RELEVANTE = NULL");

                //if (!String.IsNullOrEmpty(Datos.P_Pago_Cie))
                //    Mi_SQL.Append(", Pago_Cie = '" + Datos.P_Pago_Cie + "'");
                //else
                //    Mi_SQL.Append(", Pago_Cie = NULL");

                if (!String.IsNullOrEmpty(Datos.P_Cuenta_Contable_Caja))
                    Mi_SQL.Append(", Cuenta_Contable_Caja = '" + Datos.P_Cuenta_Contable_Caja + "'");
                else
                    Mi_SQL.Append(", Cuenta_Contable_Caja = NULL");

                //if (!String.IsNullOrEmpty(Datos.P_Afectar_Presupuesto))
                //    Mi_SQL.Append(", Afectar_Presupuesto ='" + Datos.P_Afectar_Presupuesto + "'");
                //else
                //    Mi_SQL.Append(", Afectar_Presupuesto = NULL");

                if (!String.IsNullOrEmpty(Datos.P_Fte_Financiamiento))
                    Mi_SQL.Append(", Fuente_Financiamiento_Id = '" + Datos.P_Fte_Financiamiento + "'");
                else
                   Mi_SQL.Append(", Fuente_Financiamiento_Id = NULL");

                if (!String.IsNullOrEmpty(Datos.P_Proyecto_Programa))
                    Mi_SQL.Append(", Proyecto_Programa_Id = '" + Datos.P_Proyecto_Programa + "'");
                else
                    Mi_SQL.Append(", Proyecto_Programa_Id = NULL");

                if (!String.IsNullOrEmpty(Datos.P_Dependencia))
                    Mi_SQL.Append(", Dependencia_Id = '" + Datos.P_Dependencia + "'");
                else
                    Mi_SQL.Append(", Dependencia_Id = NULL");

                if (!String.IsNullOrEmpty(Datos.P_Banco))
                   Mi_SQL.Append(", Banco_Id = '" + Datos.P_Banco + "'");
                else
                    Mi_SQL.Append(", Banco_Id = NULL ");

                if (!String.IsNullOrEmpty(Datos.P_Concepto_Poliza_Contable))
                    Mi_SQL.Append(", Concepto_Poliza_Contable = '" + Datos.P_Concepto_Poliza_Contable + "'");
                else
                    Mi_SQL.Append(", Concepto_Poliza_Contable = NULL ");
                
                if (!String.IsNullOrEmpty(Datos.P_Concepto_Poliza_Presupuestal))
                    Mi_SQL.Append(", Concepto_Poliza_Presupuestal = '" + Datos.P_Concepto_Poliza_Presupuestal + "'");
                else
                    Mi_SQL.Append(",Concepto_Poliza_Presupuestal= NULL");

                if (!String.IsNullOrEmpty(Datos.P_Reportes_Cajas_Elaboro))
                    Mi_SQL.Append(", reportes_cajas_elaboro = '" + Datos.P_Reportes_Cajas_Elaboro + "'");
                else
                    Mi_SQL.Append(", reportes_cajas_elaboro = NULL");

                if (!String.IsNullOrEmpty(Datos.P_Reportes_Cajas_Reviso))
                    Mi_SQL.Append(", reportes_cajas_reviso = '" + Datos.P_Reportes_Cajas_Reviso + "'");
                else
                    Mi_SQL.Append(", reportes_cajas_reviso = NULL");

                if (!String.IsNullOrEmpty(Datos.P_Reportes_Cajas_VoBo))
                    Mi_SQL.Append(", reportes_cajas_vobo = '" + Datos.P_Reportes_Cajas_VoBo + "'");
                else
                    Mi_SQL.Append(", reportes_cajas_vobo = NULL");

                if (!String.IsNullOrEmpty(Datos.P_Usuario_Modifico))
                    Mi_SQL.Append(", Usuario_Modifico = '" + Datos.P_Usuario_Modifico + "'");
                else
                    Mi_SQL.Append(", Usuario_Modifico = NULL");



          



                Mi_SQL.Append(", Fecha_Modifico = GETDATE()");
                Mi_SQL.Append(" WHERE Parametro_ID = '" + Datos.P_Parametro_ID + "'");

                Obj_Comando.CommandText = Mi_SQL.ToString();
                Obj_Comando.ExecuteNonQuery();
                Obj_Conexion.Close();
            }
            catch (SqlException Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }
        }
    }
}