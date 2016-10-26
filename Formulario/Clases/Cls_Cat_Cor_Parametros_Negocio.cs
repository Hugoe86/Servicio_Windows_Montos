using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIAC.Parametros.Datos;

/// <summary>
/// Summary description for Cls_Cat_Cor_Parametros
/// </summary>
/// 
namespace SIAC.Parametros.Negocios
{
    public class Cls_Cat_Cor_Parametros_Negocio
    {
        public Cls_Cat_Cor_Parametros_Negocio() { }

        #region Variables Públicas
        
            //Variables para el alta de la suspension de servicio
            public String P_Porcentaje_IVA{ get; set; }
            public String P_Porcentaje_Drenaje{ get; set; }
            public String P_Porcentaje_Saneamiento{ get; set; }
            public String P_Ruta_Archivos_Lecturas{ get; set; }
            public String P_Ruta_Archivo_Rutas{ get; set; }
            public String P_Dias_Notificacion{ get; set; }
            public String P_Porcentaje_Pago_Notificacion{ get; set; }
            public String P_Servicio_Corte_ID { get; set; }
            public String P_Tipo_Falla_Corte_ID { get; set; }
            public String P_Tipo_Falla_Bacheo_ID { get; set; }
            public String P_Tipo_Falla_Escombro_ID { get; set; }
            public String P_Verificacion_Tecnica_ID { get; set; }        
            public String P_Porcentaje_Minimo_P_Parcial { get; set; }
            public String P_Porcentaje_Recargos { get; set; }
            public String P_Porcentaje_Minimo_P_Inicial { get; set; }
            public String P_Porcentaje_Sugerido_P_Inicial_Convenio { get; set; }
            public String P_Tipo_Falla_Reconexion { get; set; }
            public String P_Tipo_Falla_Corte_Efectivo { get; set; }
            public String P_Monto_Notificacion_Sfirma{ get; set; }
            public String P_Tipo_Falla_Suspencion_Voluntaria{ get; set; }
            public String P_Ruta_Imagenes{ get; set; }
            public String P_Ruta_Calendario_Facturacion { get; set; }
            public String P_Url_Imagenes{ get; set; }
            public String P_Usar_Fecha_Vencimiento { get; set; }
            public String P_Cie { get; set; }
            public String P_Maximo_Meses_Adeudo{ get; set; }
            public String P_Calculo_Recargos_Pozo{ get; set; }
            public String P_Parametro_ID { get; set; }
            public String P_Porcentaje_Solo_Drenaje { get; set; }
            public String P_DIA_PAGO_FACTURACION { get; set; }
            public String P_CANTIDAD_PAGO_RELEVANTE { get; set; }
            public String P_CONCEPTO_RECARGOS{get;set;}
            public String P_CONCEPTO_REZAGO { get; set; }
            public String P_Cuenta_organismo { get; set; }
            public String P_PrecioM3_Por_Alumno { get; set; }

            public double Porcentaje_Interes_Insolutos { get; set; }
            public double Porcentaje_Interes_Insolutos_Vencidos { get; set; }
            public double Metros_Cuadrados { get; set; }
            public int Porcentaje_Descuento_Empleados { get; set; }
            public int Maximo_Mensualidades { get; set; }

            public string Concepto_Agua { get; set; }
            public string Concepto_Drenaje { get; set; }
            public string Concepto_Sanamiento { get; set; }
            public string Concepto_Redondeo { get; set; }
            public string Concepto_Redondeo_Anterior { get; set; }
            public string Concepto_Factibilidad { get; set; }
            public string Concepto_Iva { get; set; }
            public string Concepto_Cruz_Roja { get; set; }
            public string Concepto_Bomberos { get; set; }
            public string Concepto_Agua_Anticipo { get; set; }
            public string Concepto_Drenaje_Anticipo { get; set; }
            public string Concepto_Saneamiento_Anticipo { get; set; }
            public string Concepto_Recargo_Agua { get; set; }
            public string Concepto_Rezago_Agua { get; set; }
            public string Concepto_Recargo_Drenaje { get; set; }
            public string Concepto_Rezago_Drenaje { get; set; }
            public string Concepto_Recargo_Saneamiento { get; set; }
            public string Concepto_Rezago_Saneamiento { get; set; }
            public string Concepto_Agua_Comercial { get; set; }
            public string Concepto_Descuento_Empleados { get; set; }
            public string Concepto_Material { get; set; }
            public string Concepto_Vales { get; set; }
            public string Concepto_Factibilidad_Casa { get; set; }
            public string Concepto_Factibilidad_Imobiliaria { get; set; }
            public string Concepto_Metro_Excedente { get; set; }

            public int Dias_Vencimiento_Vales { get; set; }

            public string Factor_Desgloce_Anticipo_Dom { get; set; }
            public string Factor_Desgloce_Anticipo_Com { get; set; }


            public String P_Salario_Minimo { get; set; }
            public String P_Fte_Financiamiento { get; set; }
            public String P_Proyecto_Programa { get; set; }
            public String P_Dependencia { get; set; }
            public String P_Banco { get; set; }

            public DateTime P_Fecha_Inicio_Facturacion_Anual { get; set; }
            public int P_Limite_Anios_Cobro_Recargos { get; set; }
            public string P_Afectar_Presupuesto { get; set; }
            public string P_Reportes_Cajas_Elaboro { get; set; }
            public string P_Reportes_Cajas_Reviso { get; set; }
            public string P_Reportes_Cajas_VoBo { get; set; }
            public string P_URL_Imagen_Archivos { get; set; }
            public string P_Ruta_Imagen_Archivos { get; set; }
            public string P_Usuario_Modifico { get; set; }

            public string P_Pago_Cie { get; set; }
            public string P_Cuenta_Contable_Caja { get; set; }
            public string P_Concepto_Poliza_Contable { get; set; }
            public string P_Concepto_Poliza_Presupuestal { get; set; }

            public string P_Caja_Internet_Id { get; set; }
            public string P_Cajero_Internet_Id { get; set; }
            public string P_Sucursal_Internet_Id { get; set; }
            public string P_Ruta_Pagina_Facturacion { get; set; }

            //  Suspension de servicios

            public String P_Concepto_Suspension_Reconexion_Linea { get; set; }
            public String P_Concepto_Suspension_Reconexion_Cuadro { get; set; }
            public String P_Concepto_Suspension_Meses_De_Adeudo { get; set; }

        // paramtros de facturacion electronica
            public String No_Intentos_Acceso { get; set; }
            public String No_Bloqueos_Temporales { get; set; }
            public String Minutos_Bloqueo_Temporal { get; set; }

        #endregion

        #region Métodos
            public DataTable Consulta_Parametros() { return Cls_Cat_Cor_Parametros_Datos.Consulta_Parametros(); }
            public void Modifica_Parametro() { Cls_Cat_Cor_Parametros_Datos.Modifica_Parametro(this); }
            public void Actualizar_Parametros_Finanzas() { Cls_Cat_Cor_Parametros_Datos.Actualizar_Parametros_Finanzas(this); }
        #endregion
    }
}