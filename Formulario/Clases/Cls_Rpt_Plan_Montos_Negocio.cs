using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Reportes_Planeacion.Montos.Datos;

namespace Reportes_Planeacion.Montos.Negocio
{
    public class Cls_Rpt_Plan_Montos_Negocio
    {
        public Cls_Rpt_Plan_Montos_Negocio()
        {
        }

        #region Variables_Publicas
        public String P_Id { get; set; }
        public String P_Rpu { get; set; }
        public String P_No_Cuenta { get; set; }
        public String P_Giro_Id { get; set; }
        public Int32 P_Anio { get; set; }
        public Int32 P_Mes { get; set; }
        public DataRow P_Dr_Registro { get; set; }
        public Int32 P_Mes_Insercion { get; set; }
        public String P_Str_Nombre_Mes { get; set; }
        public String P_Str_Usuario { get; set; }
        public Int32 P_Int_Servicio { get; set; }

        #endregion

        #region Consultas
        public DataTable Consultar_Tarifas_Giro() { return Cls_Rpt_Plan_Montos_Datos.Consultar_Tarifas_Giro(this); }

        public DataTable Consultar_Facturacion_Planeacion() { return Cls_Rpt_Plan_Montos_Datos.Consultar_Facturacion_Planeacion(this); }
        public DataTable Consultar_Pagos_A_Facturacion_Planeacion() { return Cls_Rpt_Plan_Montos_Datos.Consultar_Pagos_A_Facturacion_Planeacion(this); }

        public DataTable Consultar_Si_Existe_Registro_Facturacion() { return Cls_Rpt_Plan_Montos_Datos.Consultar_Si_Existe_Registro_Facturacion(this); }

        public void Insertar_Registro_Facturacion() { Cls_Rpt_Plan_Montos_Datos.Insertar_Registro_Facturacion(this); }
        public void Actualizar_Registro_Facturacion() { Cls_Rpt_Plan_Montos_Datos.Actualizar_Registro_Facturacion(this); }

        public DataTable Consultar_Si_Existe_Registro_Pago() { return Cls_Rpt_Plan_Montos_Datos.Consultar_Si_Existe_Registro_Pago(this); }
        public void Insertar_Registro_Pago() { Cls_Rpt_Plan_Montos_Datos.Insertar_Registro_Pago(this); }
        public void Actualizar_Registro_Pago() { Cls_Rpt_Plan_Montos_Datos.Actualizar_Registro_Pago(this); }

        #endregion
    }
}