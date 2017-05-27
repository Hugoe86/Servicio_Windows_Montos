using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using SIAC.Parametros.Negocios;
using Reportes_Planeacion.Montos.Negocio;
using SIAC.Metodos_Generales;

namespace Servicio_Rpt_Plan_Montos
{
    public partial class Service1 : ServiceBase
    {
        public Timer Tiempo;


        /// <summary>
        /// 
        /// </summary>
        public Service1()
        {
            InitializeComponent();
            Tiempo = new Timer();
            Tiempo.Interval = 900000; // 900000 = 15 minutos     // 600000 = 10 minutos  //  1200000 = 20 minutos
            Tiempo.Elapsed += new ElapsedEventHandler(Tiempo_Contador);
        }

        /////*******************************************************************************************************
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        ///// <creo>Hugo Enrique Ramírez Aguilera</creo>
        ///// <fecha_creo>1</fecha_creo>
        ///// <modifico></modifico>
        ///// <fecha_modifico></fecha_modifico>
        ///// <causa_modificacion></causa_modificacion>
        ///*******************************************************************************************************
        protected override void OnStart(string[] args)
        {
            Tiempo.Enabled = true;
        }
        /////*******************************************************************************************************
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        ///// <creo>Hugo Enrique Ramírez Aguilera</creo>
        ///// <fecha_creo>1</fecha_creo>
        ///// <modifico></modifico>
        ///// <fecha_modifico></fecha_modifico>
        ///// <causa_modificacion></causa_modificacion>
        ///*******************************************************************************************************
        protected override void OnStop()
        {

        }


        /////*******************************************************************************************************
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        ///// <creo>Hugo Enrique Ramírez Aguilera</creo>
        ///// <fecha_creo>1</fecha_creo>
        ///// <modifico></modifico>
        ///// <fecha_modifico></fecha_modifico>
        ///// <causa_modificacion></causa_modificacion>
        ///*******************************************************************************************************
        public void Tiempo_Contador(object Sender, EventArgs e)
        {

            DateTime Dtime_Hora = DateTime.Now;

            if (Dtime_Hora.Hour >= 18 && Dtime_Hora.Hour <= 19)
            {
                Consultar_Informacion();
            }

        }// fin



        //*******************************************************************************
        //NOMBRE DE LA FUNCIÓN:Consultar_Informacion
        //DESCRIPCIÓN: Metodo que permite llenar el Grid con la informacion de la consulta
        //PARAMETROS: 
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 07/Abril/2016
        //MODIFICO:
        //FECHA_MODIFICO:
        //CAUSA_MODIFICACIÓN:
        //*******************************************************************************
        public void Consultar_Informacion()
        {
            Cls_Cat_Cor_Parametros_Negocio Rs_Parametros = new Cls_Cat_Cor_Parametros_Negocio();
            Cls_Rpt_Plan_Montos_Negocio Rs_Consulta = new Cls_Rpt_Plan_Montos_Negocio();
            DataTable Dt_Consulta_Facturacion_Estimado_No = new DataTable();
            DataTable Dt_Consulta_Facturacion_Estimado_Si = new DataTable();
            DataTable Dt_Consulta_Pagos = new DataTable();
            DataTable Dt_Tarifas = new DataTable();
            DataTable Dt_Reporte = new DataTable();
            DataTable Dt_Reporte_Pagos = new DataTable();
            DataTable Dt_Auxiliar = new DataTable();
            DataTable Dt_Resumen = new DataTable();
            DataTable Dt_Parametros = new DataTable();
            DataRow Dr_Nuevo_Elemento;
            Int32 Int_Mes = 0;
            String Str_Nombre_Mes = "";
            Dictionary<Int32, String> Dic_Meses;
            int Cont_Encabezao = 0;
            Double Db_Total = 0;
            Double Db_Total_Concepto = 0;
            String Str_Concepto_Agua_Id = "";
            String Str_Concepto_Agua_Comercial_Id = "";
            String Str_Concepto_Drenaje_Id = "";
            String Str_Concepto_Saneamiento_Id = "";
            Decimal Dc_Total_Facturado_Estimado_No = 0;
            Decimal Dc_Total_Facturado_Estimado_Si = 0;
            Decimal Dc_Total_Pagado = 0;
            DataTable Dt_Existencia = new DataTable();

            try
            {
                //  se consultan los parametros
                Dt_Parametros = Rs_Parametros.Consulta_Parametros();

                //  se obtienen los id de los parametros
                foreach (DataRow Registro in Dt_Parametros.Rows)
                {
                    Str_Concepto_Agua_Id = Registro["CONCEPTO_AGUA"].ToString();
                    Str_Concepto_Agua_Comercial_Id = Registro["Concepto_Agua_Comercial"].ToString();
                    Str_Concepto_Drenaje_Id = Registro["CONCEPTO_DRENAJE"].ToString();
                    Str_Concepto_Saneamiento_Id = Registro["CONCEPTO_SANAMIENTO"].ToString();
                }


                //  se consultan los valeres a reportar
                Dic_Meses = Cls_Metodos_Generales.Crear_Diccionario_Meses();
                Rs_Consulta.P_Anio = DateTime.Now.Year;
                Rs_Consulta.P_Mes = DateTime.Now.Month;
                Dt_Tarifas = Rs_Consulta.Consultar_Tarifas_Giro();
                Dt_Reporte = Crear_Tabla_Reporte();
                Dt_Reporte_Pagos = Crear_Tabla_Reporte();

                Dt_Consulta_Facturacion_Estimado_No = Rs_Consulta.Consultar_Facturacion_Planeacion();
                Dt_Consulta_Facturacion_Estimado_Si = Rs_Consulta.Consultar_Facturacion_Planeacion_Estimado_Si();
                Dt_Consulta_Pagos = Rs_Consulta.Consultar_Pagos_A_Facturacion_Planeacion();


                //********************************************************************************************************************
                //********************************************************************************************************************
                //********************************************************************************************************************
                //  Se ingresan los encabezados para la facturacion 
                foreach (DataRow Registro in Dt_Tarifas.Rows)
                {
                    Dr_Nuevo_Elemento = Dt_Reporte.NewRow();
                    Dr_Nuevo_Elemento["tarifa_Id"] = Registro["giro_id"].ToString();
                    Dr_Nuevo_Elemento["Accion"] = "0";
                    Dr_Nuevo_Elemento["Concepto"] = "Monto facturado por el servicio de agua de las tomas " + Registro["Nombre_Giro"].ToString() + " (" + Registro["Clave"].ToString() + ")";

                    Dt_Reporte.Rows.Add(Dr_Nuevo_Elemento);
                    //********************************************************************************
                    //********************************************************************************
                    //********************************************************************************
                    Dr_Nuevo_Elemento = Dt_Reporte.NewRow();
                    Dr_Nuevo_Elemento["tarifa_Id"] = Registro["giro_id"].ToString();
                    Dr_Nuevo_Elemento["Accion"] = "1";
                    Dr_Nuevo_Elemento["Concepto"] = "Monto facturado por el servicio de drenaje de las descargas " + Registro["Nombre_Giro"].ToString() + " (" + Registro["Clave"].ToString() + ")";

                    Dt_Reporte.Rows.Add(Dr_Nuevo_Elemento);
                    //********************************************************************************
                    //********************************************************************************
                    //********************************************************************************
                    Dr_Nuevo_Elemento = Dt_Reporte.NewRow();
                    Dr_Nuevo_Elemento["tarifa_Id"] = Registro["giro_id"].ToString();
                    Dr_Nuevo_Elemento["Accion"] = "2";
                    Dr_Nuevo_Elemento["Concepto"] = "Monto facturado por el servicio de tratemiento de las descargas " + Registro["Nombre_Giro"].ToString() + " (" + Registro["Clave"].ToString() + ")";

                    Dt_Reporte.Rows.Add(Dr_Nuevo_Elemento);
                }


                //********************************************************************************************************************
                //********************************************************************************************************************
                //********************************************************************************************************************
                //  Facturacion********************************************************************************************************************
                foreach (DataRow Registro in Dt_Reporte.Rows)
                {

                    Registro.BeginEdit();

                    Db_Total = 0;

                    for (int Cont_For = 1; Cont_For <= 12; Cont_For++)
                    {

                        //Dt_Auxiliar = Dt_Consulta_Facturacion.Copy();
                        //Dt_Auxiliar.DefaultView.RowFilter = "giro_id = '" + Registro["tarifa_id"].ToString() + "' and bimestre = '" + Cont_For.ToString() + "'";
                        //Dt_Auxiliar = Dt_Auxiliar.DefaultView.ToTable();

                        Str_Nombre_Mes = "";
                        if (Dic_Meses.ContainsKey(Cont_For) == true)
                        {
                            Str_Nombre_Mes = Dic_Meses[Cont_For];

                        }//    fin de la validacion del diccionario

                        Db_Total_Concepto = 0;

                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        if (Registro["Accion"].ToString() == "0")// Agua ó Agua_comercial
                        {

                            foreach (DataColumn Columna in Dt_Consulta_Facturacion_Estimado_No.Columns)
                            {
                                Type Tipo_Dato = Columna.DataType;
                            }


                            //  1   ********************************************************************************
                            Dc_Total_Facturado_Estimado_No = (from ord in Dt_Consulta_Facturacion_Estimado_No.AsEnumerable()
                                                  where (ord.Field<String>("Concepto_id") == Str_Concepto_Agua_Id
                                                  || ord.Field<String>("Concepto_id") == Str_Concepto_Agua_Comercial_Id)
                                                  && ord.Field<String>("giro_id") == Registro["tarifa_id"].ToString()
                                                  && ord.Field<Int32>("bimestre") == Cont_For
                                                  select ord.Field<Decimal>("Total_Facturado"))
                                                    .Sum();

                               //  1   ********************************************************************************
                            Dc_Total_Facturado_Estimado_Si = (from ord in Dt_Consulta_Facturacion_Estimado_Si.AsEnumerable()
                                                              where (ord.Field<String>("giro_id") == Registro["tarifa_id"].ToString()
                                                              && ord.Field<Int32>("bimestre") == Cont_For)
                                                              select ord.Field<Decimal>("Agua"))
                                                    .Sum();
                        }
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        else if (Registro["Accion"].ToString() == "1")// DRENAJE
                        {
                            //  2   ********************************************************************************
                            Dc_Total_Facturado_Estimado_No = (from ord in Dt_Consulta_Facturacion_Estimado_No.AsEnumerable()
                                                  where ord.Field<String>("Concepto_id") == Str_Concepto_Drenaje_Id
                                                   && ord.Field<String>("giro_id") == Registro["tarifa_id"].ToString()
                                                  && ord.Field<Int32>("bimestre") == Cont_For
                                                  select ord.Field<Decimal>("Total_Facturado"))
                                                    .Sum();


                            //  2   ********************************************************************************
                            Dc_Total_Facturado_Estimado_Si = (from ord in Dt_Consulta_Facturacion_Estimado_Si.AsEnumerable()
                                                              where (ord.Field<String>("giro_id") == Registro["tarifa_id"].ToString()
                                                              && ord.Field<Int32>("bimestre") == Cont_For)
                                                              select ord.Field<Decimal>("drenaje"))
                                                    .Sum();
                        }
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        else if (Registro["Accion"].ToString() == "2")// SANEAMIENTO
                        {
                            //  3   ********************************************************************************
                            Dc_Total_Facturado_Estimado_No = (from ord in Dt_Consulta_Facturacion_Estimado_No.AsEnumerable()
                                                  where ord.Field<String>("Concepto_id") == Str_Concepto_Saneamiento_Id
                                                   && ord.Field<String>("giro_id") == Registro["tarifa_id"].ToString()
                                                  && ord.Field<Int32>("bimestre") == Cont_For
                                                  select ord.Field<Decimal>("Total_Facturado"))
                                                  .Sum();

                            //  3   ********************************************************************************
                            Dc_Total_Facturado_Estimado_Si = (from ord in Dt_Consulta_Facturacion_Estimado_Si.AsEnumerable()
                                                              where (ord.Field<String>("giro_id") == Registro["tarifa_id"].ToString()
                                                              && ord.Field<Int32>("bimestre") == Cont_For)
                                                              select ord.Field<Decimal>("saneamiento"))
                                                    .Sum();
                        }
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
                        //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

                        //  se agrega el concepto al mes correspondiente
                        Registro[Str_Nombre_Mes] = Dc_Total_Facturado_Estimado_No + Dc_Total_Facturado_Estimado_Si;
                        Db_Total = Db_Total + Convert.ToDouble(Dc_Total_Facturado_Estimado_No) + Convert.ToDouble(Dc_Total_Facturado_Estimado_Si);
                    }

                    Registro["Total"] = Db_Total;
                    Db_Total = 0;

                    Registro.EndEdit();
                    Registro.AcceptChanges();

                }


                //********************************************************************************************************************
                //********************************************************************************************************************
                //********************************************************************************************************************
                //  Se ingresan los encabezados para los pagos 
                foreach (DataRow Registro in Dt_Tarifas.Rows)
                {
                    Dr_Nuevo_Elemento = Dt_Reporte_Pagos.NewRow();
                    Dr_Nuevo_Elemento["tarifa_Id"] = Registro["giro_id"].ToString();
                    Dr_Nuevo_Elemento["Accion"] = "0";
                    Dr_Nuevo_Elemento["Concepto"] = "Monto ingresados por el servicio de agua de las tomas " + Registro["Nombre_Giro"].ToString() + " (" + Registro["Clave"].ToString() + ")";

                    Dt_Reporte_Pagos.Rows.Add(Dr_Nuevo_Elemento);
                    //********************************************************************************
                    //********************************************************************************
                    //********************************************************************************
                    Dr_Nuevo_Elemento = Dt_Reporte_Pagos.NewRow();
                    Dr_Nuevo_Elemento["tarifa_Id"] = Registro["giro_id"].ToString();
                    Dr_Nuevo_Elemento["Accion"] = "1";
                    Dr_Nuevo_Elemento["Concepto"] = "Monto ingresados por el servicio de drenaje de las descargas " + Registro["Nombre_Giro"].ToString() + " (" + Registro["Clave"].ToString() + ")";

                    Dt_Reporte_Pagos.Rows.Add(Dr_Nuevo_Elemento);
                    //********************************************************************************
                    //********************************************************************************
                    //********************************************************************************
                    Dr_Nuevo_Elemento = Dt_Reporte_Pagos.NewRow();
                    Dr_Nuevo_Elemento["tarifa_Id"] = Registro["giro_id"].ToString();
                    Dr_Nuevo_Elemento["Accion"] = "2";
                    Dr_Nuevo_Elemento["Concepto"] = "Monto ingresados por el servicio de tratemiento de las descargas " + Registro["Nombre_Giro"].ToString() + " (" + Registro["Clave"].ToString() + ")";

                    Dt_Reporte_Pagos.Rows.Add(Dr_Nuevo_Elemento);
                }


                //********************************************************************************************************************
                //********************************************************************************************************************
                //********************************************************************************************************************
                //  pagos********************************************************************************************************************
                foreach (DataRow Registro in Dt_Reporte_Pagos.Rows)
                {

                    Registro.BeginEdit();

                    Db_Total = 0;

                    for (int Cont_For = 1; Cont_For <= 12; Cont_For++)
                    {

                        Dt_Auxiliar = Dt_Consulta_Pagos.Copy();
                        Dt_Auxiliar.DefaultView.RowFilter = "giro_id = '" + Registro["tarifa_id"].ToString() + "' and mes = '" + Cont_For.ToString() + "'";
                        Dt_Auxiliar = Dt_Auxiliar.DefaultView.ToTable();

                        Str_Nombre_Mes = "";
                        if (Dic_Meses.ContainsKey(Cont_For) == true)
                        {
                            Str_Nombre_Mes = Dic_Meses[Cont_For];

                        }//    fin de la validacion del diccionario

                        Dc_Total_Pagado = 0;



                        if (Registro["Accion"].ToString() == "0")// Agua ó Agua_comercial
                        {

                            foreach (DataColumn Columna in Dt_Consulta_Pagos.Columns)
                            {
                                Type Tipo_Dato = Columna.DataType;
                            }


                            //  1   ********************************************************************************
                            Dc_Total_Pagado = (from ord in Dt_Consulta_Pagos.AsEnumerable()
                                               where (ord.Field<String>("Concepto_id") == Str_Concepto_Agua_Id
                                               || ord.Field<String>("Concepto_id") == Str_Concepto_Agua_Comercial_Id)
                                               && ord.Field<String>("giro_id") == Registro["tarifa_id"].ToString()
                                               && ord.Field<Int32>("Mes") == Cont_For
                                               select ord.Field<Decimal>("Pagado"))
                                                    .Sum();
                        }
                        else if (Registro["Accion"].ToString() == "1")// DRENAJE
                        {
                            //  2   ********************************************************************************
                            Dc_Total_Facturado_Estimado_No = (from ord in Dt_Consulta_Pagos.AsEnumerable()
                                                  where ord.Field<String>("Concepto_id") == Str_Concepto_Drenaje_Id
                                                   && ord.Field<String>("giro_id") == Registro["tarifa_id"].ToString()
                                                  && ord.Field<Int32>("Mes") == Cont_For
                                                  select ord.Field<Decimal>("Pagado"))
                                                    .Sum();
                        }
                        else if (Registro["Accion"].ToString() == "2")// SANEAMIENTO
                        {
                            //  3   ********************************************************************************
                            Dc_Total_Facturado_Estimado_No = (from ord in Dt_Consulta_Pagos.AsEnumerable()
                                                  where ord.Field<String>("Concepto_id") == Str_Concepto_Saneamiento_Id
                                                   && ord.Field<String>("giro_id") == Registro["tarifa_id"].ToString()
                                                  && ord.Field<Int32>("Mes") == Cont_For
                                                  select ord.Field<Decimal>("Pagado"))
                                                  .Sum();
                        }


                        //  se agrega el concepto al mes correspondiente
                        Registro[Str_Nombre_Mes] = Dc_Total_Pagado;
                        Db_Total = Db_Total + Convert.ToDouble(Dc_Total_Pagado);
                    }

                    Registro["Total"] = Db_Total;
                    Db_Total = 0;

                    Registro.EndEdit();
                    Registro.AcceptChanges();

                }

                Rs_Consulta.P_Str_Usuario = "Servicio";

                //  se realizara la insercion de la informacion
                foreach (DataRow Registro in Dt_Reporte.Rows)
                {
                    Dt_Existencia.Clear();

                    Str_Nombre_Mes = "";
                    Str_Nombre_Mes = Dic_Meses[DateTime.Now.Month];
                    Rs_Consulta.P_Str_Nombre_Mes = Str_Nombre_Mes;
                    Rs_Consulta.P_Int_Servicio = Convert.ToInt32(Registro["Accion"].ToString());
                    Rs_Consulta.P_Giro_Id = Registro["tarifa_id"].ToString();
                    Rs_Consulta.P_Anio = DateTime.Now.Year;
                    Rs_Consulta.P_Mes_Insercion = DateTime.Now.Month;
                    Rs_Consulta.P_Dr_Registro = Registro;
                    Dt_Existencia = Rs_Consulta.Consultar_Si_Existe_Registro_Facturacion();

                    //  validacion de la consulta
                    if (Dt_Existencia != null && Dt_Existencia.Rows.Count > 0)
                    {
                        //  actualizacion
                        Rs_Consulta.P_Id = Dt_Existencia.Rows[0]["ID"].ToString();
                        Rs_Consulta.Actualizar_Registro_Facturacion();

                    }// fin del if
                    else
                    {
                        //  insercion
                        Rs_Consulta.Insertar_Registro_Facturacion();

                    }// fin el else

                }// fin foreach


                //  se realizara la insercion de la informacion
                foreach (DataRow Registro in Dt_Reporte_Pagos.Rows)
                {
                    Dt_Existencia.Clear();

                    Str_Nombre_Mes = "";
                    Str_Nombre_Mes = Dic_Meses[DateTime.Now.Month];
                    Rs_Consulta.P_Str_Nombre_Mes = Str_Nombre_Mes;
                    Rs_Consulta.P_Int_Servicio = Convert.ToInt32(Registro["Accion"].ToString());
                    Rs_Consulta.P_Giro_Id = Registro["tarifa_id"].ToString();
                    Rs_Consulta.P_Anio = Convert.ToInt32(DateTime.Now.Year);
                    Rs_Consulta.P_Mes_Insercion = DateTime.Now.Month;
                    Rs_Consulta.P_Dr_Registro = Registro;
                    Dt_Existencia = Rs_Consulta.Consultar_Si_Existe_Registro_Pago();

                    //  validacion de la consulta
                    if (Dt_Existencia != null && Dt_Existencia.Rows.Count > 0)
                    {
                        //  actualizacion
                        Rs_Consulta.P_Id = Dt_Existencia.Rows[0]["ID"].ToString();
                        Rs_Consulta.Actualizar_Registro_Pago();

                    }// fin del if
                    else
                    {
                        //  insercion
                        Rs_Consulta.Insertar_Registro_Pago();

                    }// fin el else

                }// fin foreach


            }
            catch (Exception Ex)
            {
                //Mostrar_Informacion(1, "Error: (Consultar_Notificaciones)" + Ex.ToString());
            }
        }



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
        private DataTable Crear_Tabla_Reporte()
        {
            DataTable Dt_Reporte = new DataTable();

            try
            {
                Dt_Reporte.Columns.Add("tarifa_Id");
                Dt_Reporte.Columns.Add("Accion");//0 (Agua), 1 (drenaje), 2 (saneamiento)
                Dt_Reporte.Columns.Add("Concepto");
                Dt_Reporte.Columns.Add("Enero", typeof(System.Double));
                Dt_Reporte.Columns.Add("Febrero", typeof(System.Double));
                Dt_Reporte.Columns.Add("Marzo", typeof(System.Double));
                Dt_Reporte.Columns.Add("Abril", typeof(System.Double));
                Dt_Reporte.Columns.Add("Mayo", typeof(System.Double));
                Dt_Reporte.Columns.Add("Junio", typeof(System.Double));
                Dt_Reporte.Columns.Add("Julio", typeof(System.Double));
                Dt_Reporte.Columns.Add("Agosto", typeof(System.Double));
                Dt_Reporte.Columns.Add("Septiembre", typeof(System.Double));
                Dt_Reporte.Columns.Add("Octubre", typeof(System.Double));
                Dt_Reporte.Columns.Add("Noviembre", typeof(System.Double));
                Dt_Reporte.Columns.Add("Diciembre", typeof(System.Double));
                Dt_Reporte.Columns.Add("Total", typeof(System.Double));
            }
            catch (Exception Ex)
            {
                //Mostrar_Informacion(1, "Error: (Crear_Tabla_Reporte)" + Ex.ToString());
            }

            return Dt_Reporte;
        }
    
    }
}
