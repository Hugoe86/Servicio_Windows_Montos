using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SIAC.Constantes;
using SharpContent.ApplicationBlocks.Data;
using Reportes_Planeacion.Montos.Negocio;
using System.Data.SqlClient;

namespace Reportes_Planeacion.Montos.Datos
{
    public class Cls_Rpt_Plan_Montos_Datos
    {
        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Tarifas_Giro
        //DESCRIPCION: Metodo que Consulta las cuentas congeladas con estatus de cobranza
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 11/Abril/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Tarifas_Giro(Cls_Rpt_Plan_Montos_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();
            String Str_My_Sql = "";
            try
            {
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                Str_My_Sql = "select *";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  from **********************************************************************************************************************************
                Str_My_Sql += " from Cat_Cor_Giros";

                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta


        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Facturacion_Planeacion
        //DESCRIPCION: Metodo que consulta los importes que se han facturado a lo largo de un año
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 11/Abril/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Facturacion_Planeacion(Cls_Rpt_Plan_Montos_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();
            String Str_My_Sql = "";
            try
            {
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                Str_My_Sql = "select ";
                Str_My_Sql += " g.GIRO_ID";
                Str_My_Sql += ", g.Nombre_Giro";
                Str_My_Sql += ", cc.Concepto_Id";
                Str_My_Sql += ", cc.Nombre";
                Str_My_Sql += ", sum(fd.Total) As Total_Facturado";
                Str_My_Sql += ", MONTH(f.Fecha_Emision) as Bimestre";
                Str_My_Sql += ", YEAR(f.Fecha_Emision) as Anio";


                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  from **********************************************************************************************************************************
                Str_My_Sql += " from Ope_Cor_Facturacion_Recibos f";
                Str_My_Sql += " join Ope_Cor_Facturacion_Recibos_Detalles fd on fd.No_Factura_Recibo = f.No_Factura_Recibo";
                Str_My_Sql += " join Cat_Cor_Conceptos_Cobros cc on cc.Concepto_ID = fd.Concepto_ID";
                Str_My_Sql += " JOIN Cat_Cor_Predios p on p.Predio_ID = f.Predio_ID";
                Str_My_Sql += " JOIN Cat_Cor_Giros_Actividades ga ON ga.Actividad_Giro_ID = p.Giro_Actividad_ID";
                Str_My_Sql += " JOIN Cat_Cor_Giros g ON g.GIRO_ID = ga.Giro_ID";
                Str_My_Sql += " JOIN Cat_Cor_Tarifas t ON t.Tarifa_ID = p.Tarifa_ID";
                Str_My_Sql += " JOIN CAT_COR_TIPOS_CUOTAS cu ON cu.CUOTA_ID = t.Cuota_ID";


                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  where **********************************************************************************************************************************
                Str_My_Sql += " where";
                Str_My_Sql += " YEAR(f.Fecha_Emision) = " + Datos.P_Anio;
                Str_My_Sql += " and MONTH(f.Fecha_Emision) = " + Datos.P_Mes;

                Str_My_Sql += " AND (cu.CLAVE = 'CF' OR cu.CLAVE = 'SM' )";
                Str_My_Sql += " and(cc.Concepto_ID = (select p.CONCEPTO_AGUA from Cat_Cor_Parametros p) " +
                                        " OR  cc.Concepto_ID = (select p.Concepto_Agua_Comercial from Cat_Cor_Parametros p)" +
                                        " OR cc.Concepto_ID = (select p.CONCEPTO_DRENAJE from Cat_Cor_Parametros p) " +
                                        " OR cc.Concepto_ID = (select p.CONCEPTO_SANAMIENTO from Cat_Cor_Parametros p))";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  GROUP BY **********************************************************************************************************************************
                Str_My_Sql += " GROUP BY";
                Str_My_Sql += " g.GIRO_ID";
                Str_My_Sql += ", g.Nombre_Giro";
                Str_My_Sql += ", cc.Concepto_ID ";
                Str_My_Sql += ", cc.Nombre";
                Str_My_Sql += ", f.Fecha_Emision";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ORDER BY **********************************************************************************************************************************
                Str_My_Sql += " ORDER BY";
                Str_My_Sql += " g.GIRO_ID";
                Str_My_Sql += ", f.Fecha_Emision";


                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta



        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Pagos_A_Facturacion_Planeacion
        //DESCRIPCION: Metodo que los pagos realizados a la facturacion
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 11/Abril/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Pagos_A_Facturacion_Planeacion(Cls_Rpt_Plan_Montos_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();
            String Str_My_Sql = "";
            try
            {
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                Str_My_Sql = "select ";
                Str_My_Sql += " g.GIRO_ID";
                Str_My_Sql += ", g.Nombre_Giro";
                Str_My_Sql += ", fd.Concepto_Id";
                Str_My_Sql += ", cc.Nombre";
                Str_My_Sql += ", sum(mc.importe) as Pagado";
                Str_My_Sql += ", month(f.Fecha_Emision) as Mes";
                Str_My_Sql += ", year(f.Fecha_Emision) as Año";


                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  from **********************************************************************************************************************************
                Str_My_Sql += " from ";
                Str_My_Sql += " Ope_Cor_Facturacion_Recibos f";
                Str_My_Sql += " JOIN Ope_Cor_Facturacion_Recibos_Detalles fd ON  f.No_Factura_Recibo = fd.No_Factura_Recibo ";
                Str_My_Sql += " JOIN Ope_Cor_Caj_Movimientos_Cobros mc ON mc.No_Movimiento_Facturacion = fd.No_Movimiento ";
                Str_My_Sql += " join Ope_Cor_Caj_Recibos_Cobros rc on rc.NO_RECIBO = mc.NO_RECIBO ";
                Str_My_Sql += " JOIN Cat_Cor_Predios p on p.Predio_ID = f.Predio_ID";
                Str_My_Sql += " JOIN Cat_Cor_Giros_Actividades ga ON ga.Actividad_Giro_ID = p.Giro_Actividad_ID";
                Str_My_Sql += " JOIN Cat_Cor_Giros g ON g.GIRO_ID = ga.Giro_ID";
                Str_My_Sql += " join Cat_Cor_Conceptos_Cobros cc on cc.Concepto_ID = fd.CONCEPTO_ID";


                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  where **********************************************************************************************************************************
                Str_My_Sql += " where";
                Str_My_Sql += " fd.Estatus in ('PAGADO') ";
                Str_My_Sql += " and year(rc.FECHA_CREO) = " + Datos.P_Anio;
                Str_My_Sql += " and month(rc.FECHA_CREO) =" + Datos.P_Mes;

                Str_My_Sql += " and(cc.Concepto_ID = (select p.CONCEPTO_AGUA from Cat_Cor_Parametros p) " +
                                      " OR  cc.Concepto_ID = (select p.Concepto_Agua_Comercial from Cat_Cor_Parametros p)" +
                                      " OR cc.Concepto_ID = (select p.CONCEPTO_DRENAJE from Cat_Cor_Parametros p) " +
                                      " OR cc.Concepto_ID = (select p.CONCEPTO_SANAMIENTO from Cat_Cor_Parametros p))";


                Str_My_Sql += "and year(f.Fecha_Emision) = year(rc.FECHA_CREO) " +
                                   " and MONTH(f.Fecha_Emision) = MONTH(rc.FECHA_CREO)";
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  GROUP BY **********************************************************************************************************************************
                Str_My_Sql += " GROUP BY";
                Str_My_Sql += " g.GIRO_ID";
                Str_My_Sql += ", g.Nombre_Giro";
                Str_My_Sql += ", fd.Concepto_ID ";
                Str_My_Sql += ", cc.Nombre";
                Str_My_Sql += ", rc.FECHA_CREO";
                Str_My_Sql += ", f.Fecha_Emision";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ORDER BY **********************************************************************************************************************************
                Str_My_Sql += " ORDER BY";
                Str_My_Sql += " g.GIRO_ID";
                Str_My_Sql += ", rc.FECHA_CREO";
                Str_My_Sql += ", fd.Concepto_ID";


                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta



        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Pagos_A_Facturacion_Planeacion
        //DESCRIPCION: Metodo que los pagos realizados a la facturacion
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 11/Abril/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Si_Existe_Registro_Facturacion(Cls_Rpt_Plan_Montos_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();
            String Str_My_Sql = "";
            try
            {
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                Str_My_Sql = "select ";
                Str_My_Sql += " * ";


                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  from **********************************************************************************************************************************
                Str_My_Sql += " from Ope_Cor_Plan_Montos_Facturacion";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  where **********************************************************************************************************************************
                Str_My_Sql += " where";
                Str_My_Sql += " Año = " + Datos.P_Anio;
                Str_My_Sql += " and giro_Id = '" + Datos.P_Giro_Id + "'";
                Str_My_Sql += " and servicio = " + Datos.P_Int_Servicio + "";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  **********************************************************************************************************************************

                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta


        //*******************************************************************************
        //NOMBRE_FUNCION:  Consultar_Pagos_A_Facturacion_Planeacion
        //DESCRIPCION: Metodo que los pagos realizados a la facturacion
        //PARAMETROS : 1.- Cls_Rpt_Cor_Cc_Reportes_Varios_Neogcio Datos, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 11/Abril/2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static DataTable Consultar_Si_Existe_Registro_Pago(Cls_Rpt_Plan_Montos_Negocio Datos)
        {
            DataTable Dt_Consulta = new DataTable();
            String Str_My_Sql = "";
            try
            {
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                Str_My_Sql = "select ";
                Str_My_Sql += " * ";


                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  from **********************************************************************************************************************************
                Str_My_Sql += " from Ope_Cor_Plan_Montos_Pagos";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  where **********************************************************************************************************************************
                Str_My_Sql += " where";
                Str_My_Sql += " Año = " + Datos.P_Anio;
                Str_My_Sql += " and giro_Id = '" + Datos.P_Giro_Id + "'";
                Str_My_Sql += " and servicio = " + Datos.P_Int_Servicio + "";

                //  ****************************************************************************************************************************************
                //  ****************************************************************************************************************************************
                //  **********************************************************************************************************************************

                Dt_Consulta = SqlHelper.ExecuteDataset(Cls_Constantes.Str_Conexion, CommandType.Text, Str_My_Sql).Tables[0];

            }
            catch (Exception Ex)
            {
                throw new Exception("Error: " + Ex.Message);
            }

            return Dt_Consulta;

        }// fin de consulta


        //*******************************************************************************
        //NOMBRE_FUNCION:  Modifica_Notificacion
        //DESCRIPCION: Metodo que ingresa la informacion de los montos de la facturacion
        //PARAMETROS : 1.- Cls_Rpt_Plan_Montos_Negocio Clase_Negocios, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 25-Octubre-2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static void Insertar_Registro_Facturacion(Cls_Rpt_Plan_Montos_Negocio Datos)
        {
            //Declaración de las variables
            SqlTransaction Obj_Transaccion = null;
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Constantes.Str_Conexion);
            SqlCommand Obj_Comando = new SqlCommand();
            String Mi_SQL = "";

            try
            {
                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;


                #region montos_facturacion


                Mi_SQL = "INSERT INTO  Ope_Cor_Plan_Montos_Facturacion (";
                Mi_SQL += "  Giro_Id";                          //  1
                Mi_SQL += ", Concepto";                         //  2
                Mi_SQL += ", Año";                              //  3
                Mi_SQL += ", " + Datos.P_Str_Nombre_Mes;        //  4
                Mi_SQL += ", Fecha_Creo";                       //  5
                Mi_SQL += ", Usuario_Creo";                     //  6
                Mi_SQL += ", Servicio";                         //  7
                Mi_SQL += ")";
                //***************************************************************************
                Mi_SQL += " values ";
                //***************************************************************************
                Mi_SQL += "(";
                Mi_SQL += "  '" + Datos.P_Giro_Id + "'";                                            //  1
                Mi_SQL += ", '" + Datos.P_Dr_Registro["Concepto"].ToString() + "'";                 //  2
                Mi_SQL += ",  " + Datos.P_Anio + "";                                                //  3
                Mi_SQL += ",  " + Datos.P_Dr_Registro[Datos.P_Str_Nombre_Mes].ToString() + "";      //  4
                Mi_SQL += ",  getdate()";                                                           //  5
                Mi_SQL += ", '" + Datos.P_Str_Usuario + "'";                                        //  6
                Mi_SQL += ", '" + Datos.P_Int_Servicio + "'";                                       //  7
                Mi_SQL += ")";                                      

                Obj_Comando.CommandText = Mi_SQL;
                Obj_Comando.ExecuteNonQuery();

                #endregion Fin montos_facturacion

                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //ejecucion de la transaccion    ***********************************************************************************
                Obj_Transaccion.Commit();
               

            }
            catch (SqlException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            finally
            {
                Obj_Conexion.Close();
            }


        }// fin del metodo



        //*******************************************************************************
        //NOMBRE_FUNCION:  Actualizar_Registro_Facturacion
        //DESCRIPCION: Metodo que ingresa la informacion de los montos de la facturacion
        //PARAMETROS : 1.- Cls_Rpt_Plan_Montos_Negocio Clase_Negocios, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 25-Octubre-2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static void Actualizar_Registro_Facturacion(Cls_Rpt_Plan_Montos_Negocio Datos)
        {
            //Declaración de las variables
            SqlTransaction Obj_Transaccion = null;
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Constantes.Str_Conexion);
            SqlCommand Obj_Comando = new SqlCommand();
            String Mi_SQL = "";

            try
            {
                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;


                #region montos_facturacion


                Mi_SQL = "update  Ope_Cor_Plan_Montos_Facturacion set ";
                Mi_SQL += "  " + Datos.P_Str_Nombre_Mes + " = " + Datos.P_Dr_Registro[Datos.P_Str_Nombre_Mes].ToString(); 
                Mi_SQL += ", fecha_modifico = getdate()";
                Mi_SQL += ", usuario_modifico = '" + Datos.P_Str_Usuario + "'";
                Mi_SQL += " where id = '" + Datos.P_Id + "'";
                Obj_Comando.CommandText = Mi_SQL;
                Obj_Comando.ExecuteNonQuery();

                #endregion Fin montos_facturacion

                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //ejecucion de la transaccion    ***********************************************************************************
                Obj_Transaccion.Commit();


            }
            catch (SqlException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            finally
            {
                Obj_Conexion.Close();
            }


        }// fin del metodo



        //*******************************************************************************
        //NOMBRE_FUNCION:  Modifica_Notificacion
        //DESCRIPCION: Metodo que ingresa la informacion de los montos de la facturacion
        //PARAMETROS : 1.- Cls_Rpt_Plan_Montos_Negocio Clase_Negocios, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 25-Octubre-2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static void Insertar_Registro_Pago(Cls_Rpt_Plan_Montos_Negocio Datos)
        {
            //Declaración de las variables
            SqlTransaction Obj_Transaccion = null;
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Constantes.Str_Conexion);
            SqlCommand Obj_Comando = new SqlCommand();
            String Mi_SQL = "";

            try
            {
                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;


                #region montos_facturacion


                Mi_SQL = "INSERT INTO  Ope_Cor_Plan_Montos_Pagos (";
                Mi_SQL += "  Giro_Id";                          //  1
                Mi_SQL += ", Concepto";                         //  2
                Mi_SQL += ", Año";                              //  3
                Mi_SQL += ", " + Datos.P_Str_Nombre_Mes;        //  4
                Mi_SQL += ", Fecha_Creo";                       //  5
                Mi_SQL += ", Usuario_Creo";                     //  6
                Mi_SQL += ", Servicio";                         //  7
                Mi_SQL += ")";
                //***************************************************************************
                Mi_SQL += " values ";
                //***************************************************************************
                Mi_SQL += "(";
                Mi_SQL += "  '" + Datos.P_Giro_Id + "'";                                            //  1
                Mi_SQL += ", '" + Datos.P_Dr_Registro["Concepto"].ToString() + "'";                 //  2
                Mi_SQL += ",  " + Datos.P_Anio + "";                                                //  3
                Mi_SQL += ",  " + Datos.P_Dr_Registro[Datos.P_Str_Nombre_Mes].ToString() + "";      //  4
                Mi_SQL += ",  getdate()";                                                           //  5
                Mi_SQL += ", '" + Datos.P_Str_Usuario + "'";                                        //  6
                Mi_SQL += ", '" + Datos.P_Int_Servicio + "'";                                       //  7
                Mi_SQL += ")";

                Obj_Comando.CommandText = Mi_SQL;
                Obj_Comando.ExecuteNonQuery();

                #endregion Fin montos_facturacion

                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //ejecucion de la transaccion    ***********************************************************************************
                Obj_Transaccion.Commit();


            }
            catch (SqlException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            finally
            {
                Obj_Conexion.Close();
            }


        }// fin del metodo



        //*******************************************************************************
        //NOMBRE_FUNCION:  Actualizar_Registro_Facturacion
        //DESCRIPCION: Metodo que ingresa la informacion de los montos de la facturacion
        //PARAMETROS : 1.- Cls_Rpt_Plan_Montos_Negocio Clase_Negocios, objeto de la clase de negocios
        //CREO       : Hugo Enrique Ramírez Aguilera
        //FECHA_CREO : 25-Octubre-2016
        //MODIFICO   :
        //FECHA_MODIFICO:
        //CAUSA_MODIFICO:
        //*******************************************************************************
        public static void Actualizar_Registro_Pago(Cls_Rpt_Plan_Montos_Negocio Datos)
        {
            //Declaración de las variables
            SqlTransaction Obj_Transaccion = null;
            SqlConnection Obj_Conexion = new SqlConnection(Cls_Constantes.Str_Conexion);
            SqlCommand Obj_Comando = new SqlCommand();
            String Mi_SQL = "";

            try
            {
                Obj_Conexion.Open();
                Obj_Transaccion = Obj_Conexion.BeginTransaction();
                Obj_Comando.Transaction = Obj_Transaccion;
                Obj_Comando.Connection = Obj_Conexion;


                #region montos_facturacion


                Mi_SQL = "update  Ope_Cor_Plan_Montos_Pagos set ";
                Mi_SQL += "  " + Datos.P_Str_Nombre_Mes + " = " + Datos.P_Dr_Registro[Datos.P_Str_Nombre_Mes].ToString();
                Mi_SQL += ", fecha_modifico = getdate()";
                Mi_SQL += ", usuario_modifico = '" + Datos.P_Str_Usuario + "'";
                Mi_SQL += " where id = '" + Datos.P_Id + "'";
                Obj_Comando.CommandText = Mi_SQL;
                Obj_Comando.ExecuteNonQuery();

                #endregion Fin montos_facturacion

                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //***********************************************************************************************************************
                //ejecucion de la transaccion    ***********************************************************************************
                Obj_Transaccion.Commit();


            }
            catch (SqlException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (DBConcurrencyException Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            catch (Exception Ex)
            {
                Obj_Transaccion.Rollback();
                throw new Exception("Error: " + Ex.Message);
            }
            finally
            {
                Obj_Conexion.Close();
            }


        }// fin del metodo


    }
}